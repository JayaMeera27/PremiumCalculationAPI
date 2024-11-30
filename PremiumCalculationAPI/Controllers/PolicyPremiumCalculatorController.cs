using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolildPrinciples.Model;
using SolildPrinciples.Repository;
using SolildPrinciples.Services;

namespace SolildPrinciples.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PolicyPremiumCalculatorController : ControllerBase
    {
        private readonly IpremiumCalculatorService _ipremiumCalculatorService;
        private readonly IInsuranceRepository _insuranceRepository;

        public PolicyPremiumCalculatorController(IpremiumCalculatorService ipremiumCalculatorService, IInsuranceRepository insuranceRepository)
        {
            _ipremiumCalculatorService = ipremiumCalculatorService;
            _insuranceRepository = insuranceRepository;
        }

        [HttpPost("calculate-premium")]
        public async Task<IActionResult> CalculatePremium([FromBody] InsuranceDetails details)
        {
            var result = _ipremiumCalculatorService.PremiumCalculation(details);
            await _insuranceRepository.SavePolicyDetails(details);
            return Ok(result);
        }

        [HttpGet("policies/{policyType}")]
        public async Task<IActionResult> GetPolicy(string policyType)
        {
            var policies = await _insuranceRepository.GetPolicyDetails(policyType);
            return Ok(policies);
        }

        [HttpGet("all-policies")]
        public async Task<IActionResult> GetAllPolicies()
        {
            var policies = await _insuranceRepository.GetAllPolicy();
            return Ok(policies);
        }
    }
}
