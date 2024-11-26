using Smartwyre.DeveloperTest.Services.Interfaces;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Services.Impl
{
    public class FixedCashAmountCalculator : IIncentiveCalculator
    {
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
