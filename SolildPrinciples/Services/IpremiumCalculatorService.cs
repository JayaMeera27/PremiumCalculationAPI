using SolildPrinciples.Model;

namespace SolildPrinciples.Services
{
    public interface IpremiumCalculatorService
    {
       PremiumResult PremiumCalculation(InsuranceDetails insuranceDetails); 
    }
}
