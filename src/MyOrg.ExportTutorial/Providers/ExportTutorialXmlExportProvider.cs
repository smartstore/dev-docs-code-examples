using System;
using System.Threading;
using System.Threading.Tasks;
using MyOrg.ExportTutorial.Components;
using MyOrg.ExportTutorial.Models;
using Smartstore;
using Smartstore.Core.Catalog.Pricing;
using Smartstore.Core.Catalog.Products;
using Smartstore.Core.DataExchange;
using Smartstore.Core.DataExchange.Export;
using Smartstore.Core.Localization;
using Smartstore.Core.Platform.DataExchange.Export;
using Smartstore.Core.Widgets;
using Smartstore.Engine.Modularity;

namespace MyOrg.ExportTutorial.Providers
{
    [SystemName("MyOrg.ExportTutorial.ProductXml")]
    [FriendlyName("Export Tutorial XML product feed")]
    [ExportFeatures(Features =
        ExportFeatures.CreatesInitialPublicDeployment |
        ExportFeatures.OffersBrandFallback)]
    public class ExportTutorialXmlExportProvider : ExportProviderBase
    {
        public static string SystemName => "MyOrg.ExportTutorial.ProductXml";

        public override string FileExtension => "XML";

        public Localizer T { get; set; } = NullLocalizer.Instance;

        public override ExportConfigurationInfo ConfigurationInfo => new()
        {
            ConfigurationWidget = new ComponentWidget<ExportTutorialConfigurationViewComponent>(),
            ModelType = typeof(ProfileConfigurationModel)
        };

        protected override async Task ExportAsync(ExportExecuteContext context, CancellationToken cancelToken)
        {
            var config = (context.ConfigurationData as ProfileConfigurationModel) ?? new ProfileConfigurationModel();

            using var helper = new ExportXmlHelper(context.DataStream);
            var writer = helper.Writer;

            writer.WriteStartDocument();
            writer.WriteStartElement("products");

            while (context.Abort == DataExchangeAbortion.None && await context.DataSegmenter.ReadNextSegmentAsync())
            {
                var segment = await context.DataSegmenter.GetCurrentSegmentAsync();

                foreach (dynamic product in segment)
                {
                    if (context.Abort != DataExchangeAbortion.None)
                    {
                        break;
                    }

                    Product entity = product.Entity;

                    writer.WriteStartElement("product");

                    try
                    {
                        var calculatedPrice = (CalculatedPrice)product._Price;
                        var saving = calculatedPrice.Saving;

                        writer.WriteElementString("product-name", (string)product.Name);
                        writer.WriteElementString("sku", (string)product.Sku);
                        writer.WriteElementString("price", ((decimal)product.Price).FormatInvariant());

                        if (saving.HasSaving)
                        {
                            writer.WriteElementString("savings", saving.SavingPrice.Amount.FormatInvariant());
                        }

                        writer.WriteCData("desc", ((string)product.FullDescription).Truncate(5000));

                        context.RecordsSucceeded++;

                        if (context.RecordsSucceeded >= config.NumberOfExportedRows)
                        {
                            context.Abort = DataExchangeAbortion.Soft;
                        }
                    }
                    catch (OutOfMemoryException ex)
                    {
                        context.RecordOutOfMemoryException(ex, entity.Id, T);
                        context.Abort = DataExchangeAbortion.Hard;
                        throw;
                    }
                    catch (Exception ex)
                    {
                        context.RecordException(ex, entity.Id);
                    }

                    writer.WriteEndElement(); // product
                }
            }

            writer.WriteEndElement(); // products
            writer.WriteEndDocument();
        }
    }
}