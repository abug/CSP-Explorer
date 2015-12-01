using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_CREST.Entities
{
    public class OfferTerm
    {
        public string Name { get; set; }
        public double Discount { get; set; }
        public IList<string> ExcludedMeterIds { get; set; }
        public DateTime EffectiveDate { get; set; }
    }

    public class Meter
    {
        public string MeterId { get; set; }
        public string MeterName { get; set; }
        public string MeterCategory { get; set; }
        public string MeterSubCategory { get; set; }
        public string Unit { get; set; }
        public IList<string> MeterTags { get; set; }
        public string MeterRegion { get; set; }
        public Dictionary<string, decimal> MeterRates { get; set; }
        public DateTime EffectiveDate { get; set; }
        public double IncludedQuantity { get; set; }
    }

    public class RateCard
    {
        public IList<OfferTerm> OfferTerms { get; set; }
        public IList<Meter> Meters { get; set; }
        public string Currency { get; set; }
        public string Locale { get; set; }
        public bool IsTaxIncluded { get; set; }
    }
}
