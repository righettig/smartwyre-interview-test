using Smartwyre.DeveloperTest.Data.Impl;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Services.Impl;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        var rebateDataStore = new RebateDataStore();
        var productDataStore = new ProductDataStore();

        var rebateService = new RebateService(rebateDataStore, productDataStore);

        var result = rebateService.Calculate(new CalculateRebateRequest());
    }
}
