using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Interfaces
{
    public interface IIncentiveCalculatorFactory
    {
        IIncentiveCalculator GetCalculator(IncentiveType incentiveType);
    }
}