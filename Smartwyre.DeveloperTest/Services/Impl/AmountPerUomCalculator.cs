using Smartwyre.DeveloperTest.Services.Interfaces;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Impl
{
    public class AmountPerUomCalculator : IIncentiveCalculator
    {
        public IncentiveType IncentiveType => IncentiveType.AmountPerUom;

        public bool IsEligible(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate != null
                && product != null
                && product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom)
                && rebate.Amount > 0
                && request.Volume > 0;
        }

        public decimal CalculateRebate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate.Amount * request.Volume;
        }
    }
}
