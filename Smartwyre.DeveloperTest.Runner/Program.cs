using Smartwyre.DeveloperTest.Data.Impl;
using Smartwyre.DeveloperTest.Services.Impl;
using Smartwyre.DeveloperTest.Services.Interfaces;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        var rebateDataStore = new RebateDataStore();
        var productDataStore = new ProductDataStore();

        // TODO: load calculator concrete types using reflection
        IEnumerable<IIncentiveCalculator> supportedCalculators =
        [
            new AmountPerUomCalculator(),
            new FixedCashAmountCalculator(),
            new FixedRateRebateCalculator(),

            // Adding support to a new calculator type
            new ShinyNewCalculator()
        ];

        var calculatorFactory = new IncentiveCalculatorFactory(supportedCalculators);

        var rebateService = new RebateService(rebateDataStore, productDataStore, calculatorFactory);

        var result = rebateService.Calculate(new CalculateRebateRequest());
    }
}
