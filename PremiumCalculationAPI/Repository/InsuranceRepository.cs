using SolildPrinciples.Model;
using System.Data;
using System.Data.SqlClient;

namespace SolildPrinciples.Repository
{
    public class InsuranceRepository : IInsuranceRepository
    {
        private readonly string _connectionstring;
        public InsuranceRepository(IConfiguration configuration) 
        {
            _connectionstring = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<List<InsuranceDetails>> GetPolicyDetails(string policyType)
        { 
            var policies = new List<InsuranceDetails>();
            using(SqlConnection connection = new SqlConnection(_connectionstring))
            {
                connection.Open();
                using(SqlCommand command = new SqlCommand("sp_GetPolicyDetails",connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PolicyType", policyType);
                    using(SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (!reader.HasRows)
                        {
                            return policies;
                        }
                        while (await reader.ReadAsync())
                        {
                            policies.Add(new InsuranceDetails
                            {
                                PolicyType = reader["PolicyType"].ToString(),
                                NamedInsured = reader["InsuredName"].ToString(),
                                EffectiveDate = Convert.ToDateTime(reader["EffectiveDate"]),
                                CoverageAmount = Convert.ToInt32(reader["CoverageAmount"]),
                                RiskLevel = reader["RiskLevel"].ToString()

                            }) ;
                        }
                    }

                }
            }
            return policies;

        }

        public async Task SavePolicyDetails(InsuranceDetails details)
        {
            using(SqlConnection connection = new SqlConnection(_connectionstring))
            {
                connection.Open();
                using(SqlCommand sqlCommand = new SqlCommand("sp_SavePolicyDetails",connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@PolicyType", details.PolicyType);
                    sqlCommand.Parameters.AddWithValue("@InsuredName", details.NamedInsured);
                    sqlCommand.Parameters.AddWithValue("@EffectiveDate", details.EffectiveDate);
                    sqlCommand.Parameters.AddWithValue("@CoverageAmount", details.CoverageAmount);
                    sqlCommand.Parameters.AddWithValue("@RiskLevel", details.RiskLevel);

                    await sqlCommand.ExecuteNonQueryAsync();
                }
            }
            
        }

        public async Task<List<InsuranceDetails>> GetAllPolicy()
        {
            var policies = new List<InsuranceDetails>();
            using(SqlConnection connection = new SqlConnection(_connectionstring))
            {
                connection.Open();
                using(SqlCommand cmd = new SqlCommand("sp_GetAllPolicyDetails", connection))
                {
                    cmd.CommandType= CommandType.StoredProcedure;
                    using(SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if(!reader.HasRows)
                        {
                            return policies;
                        }
                        while(await  reader.ReadAsync())
                        {
                            policies.Add(new InsuranceDetails
                            {
                                PolicyType = reader["PolicyType"].ToString(),
                                NamedInsured = reader["InsuredName"].ToString(),
                                EffectiveDate = Convert.ToDateTime(reader["Effectivedate"].ToString()),
                                CoverageAmount = Convert.ToInt64(reader["CoverageAmount"]),
                                RiskLevel = reader["RiskLevel"].ToString()


                            });
                            
                            
                        }
                    }
                }
            }
            return policies;
        }


    }
}
