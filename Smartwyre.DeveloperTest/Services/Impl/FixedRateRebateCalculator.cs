using Smartwyre.DeveloperTest.Services.Interfaces;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Impl
{
    public class FixedRateRebateCalculator : IIncentiveCalculator
    {
        public IncentiveType IncentiveType => IncentiveType.FixedRateRebate;

        public bool IsEligible(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate != null
                && product != null
                && product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate)
                && rebate.Percentage > 0
                && product.Price > 0
                && request.Volume > 0;
        }

        public decimal CalculateRebate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return product.Price * rebate.Percentage * request.Volume;
        }
    }
}
