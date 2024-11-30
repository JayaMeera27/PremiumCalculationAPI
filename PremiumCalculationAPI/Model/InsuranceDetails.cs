using System;

namespace SolildPrinciples.Model
{
    public class InsuranceDetails
    {
        public string PolicyType {  get; set; }
        public string NamedInsured {  get; set; }

        public DateTime EffectiveDate { get; set; }

        public decimal CoverageAmount { get; set; }

        public string RiskLevel { get; set; }



    }
}
