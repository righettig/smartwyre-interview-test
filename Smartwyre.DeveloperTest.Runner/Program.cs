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
        // Create the necessary objects
        var rebateDataStore = new MyRebateDataStore();
        var productDataStore = new MyProductDataStore();

        IEnumerable<IIncentiveCalculator> supportedCalculators = LoadIncentiveCalculators();
        var calculatorFactory = new IncentiveCalculatorFactory(supportedCalculators);
        var rebateService = new RebateService(rebateDataStore, productDataStore, calculatorFactory);

        while (true)
        {
            Console.WriteLine("Enter Rebate Identifier (or 'Q' to quit):");
            string rebateIdentifier = Console.ReadLine();
            if (rebateIdentifier?.Trim().Equals("Q", StringComparison.OrdinalIgnoreCase) == true)
            {
                Console.WriteLine("Exiting...");
                break;
            }

            Console.WriteLine("Enter Product Identifier (or 'Q' to quit):");
            string productIdentifier = Console.ReadLine();
            if (productIdentifier?.Trim().Equals("Q", StringComparison.OrdinalIgnoreCase) == true)
            {
                Console.WriteLine("Exiting...");
                break;
            }

            Console.WriteLine("Enter Volume (or 'Q' to quit):");
            string volumeInput = Console.ReadLine();
            if (volumeInput?.Trim().Equals("Q", StringComparison.OrdinalIgnoreCase) == true)
            {
                Console.WriteLine("Exiting...");
                break;
            }

            if (!int.TryParse(volumeInput, out int volume))
            {
                Console.WriteLine("Invalid volume. Please enter a numeric value.");
                continue; // Restart the loop for a new attempt
            }

            var request = new CalculateRebateRequest
            {
                RebateIdentifier = rebateIdentifier,
                ProductIdentifier = productIdentifier,
                Volume = volume
            };

            var result = rebateService.Calculate(request);

            Console.WriteLine($"Calculation successful: {result.Success}");
        }
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
