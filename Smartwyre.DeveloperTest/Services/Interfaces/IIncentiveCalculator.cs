using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Interfaces
{
    public interface IIncentiveCalculator
    {
        IncentiveType IncentiveType { get; }
        bool IsEligible(Rebate rebate, Product product, CalculateRebateRequest request);
        decimal CalculateRebate(Rebate rebate, Product product, CalculateRebateRequest request);
    }
}
