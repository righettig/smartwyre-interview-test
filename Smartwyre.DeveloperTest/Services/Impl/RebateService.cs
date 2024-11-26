using Smartwyre.DeveloperTest.Data.Interfaces;
using Smartwyre.DeveloperTest.Services.Interfaces;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Impl;

public class RebateService(IRebateDataStore rebateDataStore,
                           IProductDataStore productDataStore,
                           IIncentiveCalculatorFactory calculatorFactory) : IRebateService
{
    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        Rebate rebate = rebateDataStore.GetRebate(request.RebateIdentifier);
        Product product = productDataStore.GetProduct(request.ProductIdentifier);

        if (rebate == null || product == null)
        {
            return new CalculateRebateResult { Success = false };
        }

        var calculator = calculatorFactory.GetCalculator(rebate.Incentive);

        if (!calculator.IsEligible(rebate, product, request))
        {
            return new CalculateRebateResult { Success = false };
        }

        var rebateAmount = calculator.CalculateRebate(rebate, product, request);
        rebateDataStore.StoreCalculationResult(rebate, rebateAmount);

        return new CalculateRebateResult { Success = true };
    }
}
