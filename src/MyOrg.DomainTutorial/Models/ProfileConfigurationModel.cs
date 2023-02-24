using System;
using Smartstore.Web.Modelling;

namespace MyOrg.DomainTutorial.Models
{
    [Serializable, CustomModelPart]
    public class ProfileConfigurationModel
    {
        [LocalizedDisplay("Plugins.MyOrg.DomainTutorial.NumberOfExportedRows")]
        public int NumberOfExportedRows { get; set; } = 10;
    }
}