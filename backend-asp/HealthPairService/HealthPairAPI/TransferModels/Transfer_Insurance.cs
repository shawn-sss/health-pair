using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthPairAPI.TransferModels
{
    public class Transfer_Insurance
    {
        public int InsuranceId { get; set; }
        public string InsuranceName { get; set; }

        public int[] ProviderIds { get; set; }
    }
}
