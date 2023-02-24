using System;
using Smartstore.Web.Modelling;

namespace MyOrg.ExportTutorial.Models
{
    [Serializable, CustomModelPart]
    public class ProfileConfigurationModel
    {
        [LocalizedDisplay("Plugins.MyOrg.ExportTutorial.NumberOfExportedRows")]
        public int NumberOfExportedRows { get; set; } = 10;
    }
}