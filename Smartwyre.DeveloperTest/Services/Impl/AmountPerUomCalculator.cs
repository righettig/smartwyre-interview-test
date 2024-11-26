using Smartwyre.DeveloperTest.Services.Interfaces;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Services.Impl
{
    public class AmountPerUomCalculator : IIncentiveCalculator
    {
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
