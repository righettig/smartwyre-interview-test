using Smartwyre.DeveloperTest.Data.Interfaces;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Smartwyre.DeveloperTest.Runner;

public class MyRebateDataStore : IRebateDataStore
{
    private readonly List<Rebate> rebates;

    public MyRebateDataStore()
    {
        rebates =
        [
            new Rebate
            {
                Identifier = "rebate1",
                Incentive = IncentiveType.FixedRateRebate,
                Amount = 0, // Amount is not applicable for FixedRateRebate
                Percentage = 10.0m
            },
            new Rebate
            {
                Identifier = "rebate2",
                Incentive = IncentiveType.AmountPerUom,
                Amount = 5.0m,
                Percentage = 0 // Percentage is not applicable for AmountPerUom
            },
            new Rebate
            {
                Identifier = "rebate3",
                Incentive = IncentiveType.FixedCashAmount,
                Amount = 50.0m,
                Percentage = 0 // Percentage is not applicable for FixedCashAmount
            },
            new Rebate
            {
                Identifier = "rebate4",
                Incentive = IncentiveType.ShinyNewIncentiveType,
                Amount = 25.0m,
                Percentage = 5.0m
            }
        ];
    }

    public Rebate GetRebate(string rebateIdentifier)
    {
        var rebate = rebates.FirstOrDefault(x => x.Identifier == rebateIdentifier);

        Console.WriteLine(
            $"Retriving rebate: Identifier {rebate.Identifier}, Amount: {rebate.Amount}, Percentage: {rebate.Percentage}, Incentive: {rebate.Incentive}");

        return rebate;
    }

    public void StoreCalculationResult(Rebate account, decimal rebateAmount)
    {
        Console.WriteLine($"Rebate amount: {rebateAmount}");
    }
}
