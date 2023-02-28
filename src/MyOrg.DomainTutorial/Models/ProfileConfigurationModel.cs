using System;
using Smartstore.Web.Modelling;

namespace MyOrg.DomainTutorial.Models
{
    [Serializable, CustomModelPart]
    [LocalizedDisplay("Plugins.MyOrg.ExportTutorial.")]
    public class ProfileConfigurationModel
    {
        [LocalizedDisplay("*NumberOfExportedRows")]
        public int NumberOfExportedRows { get; set; } = 10;
    }
}