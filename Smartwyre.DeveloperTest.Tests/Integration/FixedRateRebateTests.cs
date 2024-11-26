using Smartwyre.DeveloperTest.Services.Impl;
using Smartwyre.DeveloperTest.Services.Impl.Calculators;
using Smartwyre.DeveloperTest.Services.Interfaces;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Integration
{
    public class FixedRateRebateTests : RebateServiceTestsBase
    {
        protected override IIncentiveCalculator CreateCalculator() => new FixedRateRebateCalculator();

        [Fact]
        public void Calculate_Should_Return_Failure_If_Rebate_Is_Null()
        {
            // Arrange
            var request = CreateRequest("rebate1", "product1", 10);
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate };

            RebateDataStoreMock.Setup(m => m.GetRebate("rebate1")).Returns((Rebate)null);
            ProductDataStoreMock.Setup(m => m.GetProduct("product1")).Returns(product);

            // Act
            var result = RebateService.Calculate(request);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void Calculate_Should_Return_Failure_If_Product_Is_Null()
        {
            // Arrange
            var request = CreateRequest("rebate1", "product1", 10);
            var rebate = new Rebate { Incentive = IncentiveType.FixedRateRebate, Amount = 100 };

            RebateDataStoreMock.Setup(m => m.GetRebate("rebate1")).Returns(rebate);
            ProductDataStoreMock.Setup(m => m.GetProduct("product1")).Returns((Product)null);

            // Act
            var result = RebateService.Calculate(request);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void Calculate_Should_Return_Failure_If_Price_Is_Zero()
        {
            // Arrange
            var request = CreateRequest("rebate1", "product1", 10);
            var rebate = new Rebate { Incentive = IncentiveType.FixedRateRebate, Percentage = 0.1m };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 0 }; // Price is zero

            RebateDataStoreMock.Setup(m => m.GetRebate("rebate1")).Returns(rebate);
            ProductDataStoreMock.Setup(m => m.GetProduct("product1")).Returns(product);

            // Act
            var result = RebateService.Calculate(request);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void Calculate_Should_Return_Failure_If_Volume_Is_Zero()
        {
            // Arrange
            var request = CreateRequest("rebate1", "product1", 0); // Volume is zero
            var rebate = new Rebate { Incentive = IncentiveType.FixedRateRebate, Percentage = 0.1m };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 100 };

            RebateDataStoreMock.Setup(m => m.GetRebate("rebate1")).Returns(rebate);
            ProductDataStoreMock.Setup(m => m.GetProduct("product1")).Returns(product);

            // Act
            var result = RebateService.Calculate(request);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void Calculate_Should_Return_Failure_If_Percentage_Is_Zero()
        {
            // Arrange
            var request = CreateRequest("rebate1", "product1", 10);
            var rebate = new Rebate { Incentive = IncentiveType.FixedRateRebate, Percentage = 0 }; // Percentage is zero
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 100 };

            RebateDataStoreMock.Setup(m => m.GetRebate("rebate1")).Returns(rebate);
            ProductDataStoreMock.Setup(m => m.GetProduct("product1")).Returns(product);

            // Act
            var result = RebateService.Calculate(request);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void Calculate_Should_Return_Failure_If_FixedRateRebate_Not_Supported()
        {
            // Arrange
            var request = CreateRequest("rebate1", "product1", 10);
            var rebate = new Rebate { Incentive = IncentiveType.FixedRateRebate, Amount = 100 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom }; // No FixedRateRebate support

            RebateDataStoreMock.Setup(m => m.GetRebate("rebate1")).Returns(rebate);
            ProductDataStoreMock.Setup(m => m.GetProduct("product1")).Returns(product);

            // Act
            var result = RebateService.Calculate(request);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void Calculate_Should_Return_Success_For_Valid_FixedRateRebate()
        {
            // Arrange
            var request = CreateRequest("rebate1", "product1", 10);
            var rebate = new Rebate { Incentive = IncentiveType.FixedRateRebate, Percentage = 0.1m };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 200 };

            RebateDataStoreMock.Setup(m => m.GetRebate("rebate1")).Returns(rebate);
            ProductDataStoreMock.Setup(m => m.GetProduct("product1")).Returns(product);

            // Act
            var result = RebateService.Calculate(request);

            // Assert
            Assert.True(result.Success);
        }
    }
}