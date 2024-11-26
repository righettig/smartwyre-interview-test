using Smartwyre.DeveloperTest.Services.Interfaces;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Impl
{
    public class FixedCashAmountCalculator : IIncentiveCalculator
    {
        public IncentiveType IncentiveType => IncentiveType.FixedCashAmount;

        public bool IsEligible(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate != null
                && product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount)
                && rebate.Amount > 0;
        }

        public decimal CalculateRebate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate.Amount;
        }
    }
}
