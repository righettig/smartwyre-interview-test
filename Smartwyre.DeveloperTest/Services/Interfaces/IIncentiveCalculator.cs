using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Interfaces
{
    public interface IIncentiveCalculator
    {
        CalculateRebateResult CalculateRebate(Rebate rebate);
    }
}
