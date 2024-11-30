using SolildPrinciples.Model;

namespace SolildPrinciples.Services
{
    public class PremiumCalculatorService : IpremiumCalculatorService

    {
        public PremiumResult PremiumCalculation(InsuranceDetails insuranceDetails)
        {
            decimal basePremium = insuranceDetails.CoverageAmount * 0.01m;
            decimal riskFactor = insuranceDetails.RiskLevel == "High" ? 0.5m : 0;
            decimal finalPremium = basePremium+ riskFactor;
            return new PremiumResult
            {
                PolicyType = insuranceDetails.PolicyType,
                PremiumAmount = finalPremium,
                Message = $"Premium {finalPremium} is calculated successfully"
            };
        }
    }
}
