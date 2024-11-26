using Smartwyre.DeveloperTest.Data.Interfaces;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Runner;

public class MyRebateDataStore : IRebateDataStore
{
    public Rebate GetRebate(string rebateIdentifier)
    {
        // Access database to retrieve account, code removed for brevity 
        var rebate = new Rebate
        {
            Identifier = rebateIdentifier,
            Amount = 1000,
            Percentage = 1,
            Incentive = IncentiveType.ShinyNewIncentiveType,
        };

        Console.WriteLine(
            $"Retriving rebate: Identifier {rebate.Identifier}, Amount: {rebate.Amount}, Percentage: {rebate.Percentage}, Incentive: {rebate.Incentive}");

        return rebate;
    }

    public void StoreCalculationResult(Rebate account, decimal rebateAmount)
    {
        Console.WriteLine($"Rebate amount: {rebateAmount}");

        // Update account in database, code removed for brevity
    }
}
