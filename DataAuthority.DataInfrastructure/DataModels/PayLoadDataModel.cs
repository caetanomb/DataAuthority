using System;
using System.Collections.Generic;
using System.Text;

namespace DataAuthority.DataInfrastructure.DataModels
{
    public class PayLoadDataModel
    {
        public int Id { get; set; }
        public int ProvidedPayLoadId { get; set; }
        public string Data { get; set; }
        public string Origin { get; set; }
    }
}
