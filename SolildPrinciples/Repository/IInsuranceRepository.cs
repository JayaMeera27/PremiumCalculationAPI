using SolildPrinciples.Model;

namespace SolildPrinciples.Repository
{
    public interface IInsuranceRepository
    {
        Task SavePolicyDetails(InsuranceDetails insuranceDetails);
        Task<List<InsuranceDetails>> GetPolicyDetails(string policyType);

    }
}
