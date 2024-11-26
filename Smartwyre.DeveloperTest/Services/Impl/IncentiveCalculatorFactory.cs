using Smartwyre.DeveloperTest.Services.Interfaces;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Smartwyre.DeveloperTest.Services.Impl;

public class IncentiveCalculatorFactory(IEnumerable<IIncentiveCalculator> calculators) : IIncentiveCalculatorFactory
{
    public IIncentiveCalculator GetCalculator(IncentiveType incentiveType)
    {
        return calculators.FirstOrDefault(c => c.IncentiveType == incentiveType)
               ?? throw new InvalidOperationException("Unsupported incentive type");
    }
}
