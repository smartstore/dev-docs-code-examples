using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyOrg.ExportTutorial.Components;
using MyOrg.ExportTutorial.Models;
using Smartstore;
using Smartstore.Core.Catalog.Pricing;
using Smartstore.Core.Catalog.Products;
using Smartstore.Core.DataExchange;
using Smartstore.Core.DataExchange.Csv;
using Smartstore.Core.DataExchange.Export;
using Smartstore.Core.Localization;
using Smartstore.Core.Widgets;
using Smartstore.Engine.Modularity;

namespace MyOrg.ExportTutorial.Providers
{
    [SystemName("MyOrg.ExportTutorial.ProductCsv")]
    [FriendlyName("Export Tutorial CSV product feed")]
    [ExportFeatures(Features =
        ExportFeatures.CreatesInitialPublicDeployment |
        ExportFeatures.OffersBrandFallback)]
    public class ExportTutorialCsvExportProvider : ExportProviderBase
    {
        public static string SystemName => "MyOrg.ExportTutorial.ProductCsv";

        public override string FileExtension => "CSV";

        public Localizer T { get; set; } = NullLocalizer.Instance;

        private CsvConfiguration _csvConfiguration;

        private CsvConfiguration CsvConfiguration
        {
            get
            {
                _csvConfiguration ??= new CsvConfiguration
                {
                    Delimiter = ';',
                    SupportsMultiline = false
                };

                return _csvConfiguration;
            }
        }

        public override ExportConfigurationInfo ConfigurationInfo => new()
        {
            ConfigurationWidget = new ComponentWidget<ExportTutorialConfigurationViewComponent>(),
            ModelType = typeof(ProfileConfigurationModel)
        };

        protected override async Task ExportAsync(ExportExecuteContext context, CancellationToken cancelToken)
        {
            var config = (context.ConfigurationData as ProfileConfigurationModel) ?? new ProfileConfigurationModel();

            var columns = new string[]
            {
                "ProductName",
                "SKU",
                "Price",
                "Savings",
                "Description"
            };

            using var writer = new CsvWriter(new StreamWriter(context.DataStream, Encoding.UTF8, 1024, true));

            writer.WriteFields(columns);
            writer.NextRow();

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

                    try
                    {
                        var calculatedPrice = (CalculatedPrice)product._Price;
                        var saving = calculatedPrice.Saving;

                        writer.WriteFields(new string[]
                        {
                            product.Name,
                            product.Sku,
                            ((decimal)product.Price).FormatInvariant(),
                            saving.HasSaving ? saving.SavingPrice.Amount.FormatInvariant() : string.Empty,
                            ((string)product.FullDescription).Truncate(5000)
                        });

                        writer.NextRow();
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
                }
            }
        }
    }
}