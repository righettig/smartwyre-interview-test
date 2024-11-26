using Smartwyre.DeveloperTest.Data.Interfaces;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;
using System.Linq;

namespace Smartwyre.DeveloperTest.Runner;

public class MyProductDataStore : IProductDataStore
{
    private readonly List<Product> products;

    public MyProductDataStore()
    {
        products =
        [
            new Product
            {
                Id = 1,
                Identifier = "product1",
                Price = 100.00m,
                Uom = "kg",
                SupportedIncentives = SupportedIncentiveType.FixedRateRebate
            },
            new Product
            {
                Id = 2,
                Identifier = "product2",
                Price = 50.00m,
                Uom = "item",
                SupportedIncentives = SupportedIncentiveType.FixedCashAmount
            },
            new Product
            {
                Id = 3,
                Identifier = "product3",
                Price = 200.00m,
                Uom = "liter",
                SupportedIncentives = SupportedIncentiveType.ShinyNewIncentiveType
            },
            new Product
            {
                Id = 4,
                Identifier = "product4",
                Price = 75.00m,
                Uom = "box",
                SupportedIncentives = SupportedIncentiveType.AmountPerUom
            }
        ];
    }

    public Product GetProduct(string productIdentifier)
    {
        return products.FirstOrDefault(x => x.Identifier == productIdentifier);
    }
}
