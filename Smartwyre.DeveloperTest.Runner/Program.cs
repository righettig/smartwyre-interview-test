using Smartwyre.DeveloperTest.Services.Impl;
using Smartwyre.DeveloperTest.Services.Interfaces;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        var rebateDataStore = new MyRebateDataStore();
        var productDataStore = new MyProductDataStore();

        IEnumerable<IIncentiveCalculator> supportedCalculators = LoadIncentiveCalculators();

        var calculatorFactory = new IncentiveCalculatorFactory(supportedCalculators);

        var rebateService = new RebateService(rebateDataStore, productDataStore, calculatorFactory);

        var result = rebateService.Calculate(new CalculateRebateRequest
        {
            RebateIdentifier = "rebate1",
            ProductIdentifier = "product1",
            Volume = 10
        });

        Console.WriteLine($"Calculation successful: {result.Success}");
    }

    static IEnumerable<IIncentiveCalculator> LoadIncentiveCalculators()
    {
        var assembly = typeof(IIncentiveCalculator).Assembly;

        // Find all types implementing IIncentiveCalculator
        var calculatorTypes = assembly.GetTypes()
            .Where(type => typeof(IIncentiveCalculator).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);

        // Instantiate each type
        foreach (var calculatorType in calculatorTypes)
        {
            if (Activator.CreateInstance(calculatorType) is IIncentiveCalculator calculator)
            {
                yield return calculator;
            }
        }
    }
}
