using Moq;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    public class FixedCashAmountTests : RebateServiceTestsBase
    {
        [Fact]
        public void Calculate_Should_Return_Failure_If_Rebate_Is_Null()
        {
            // Arrange
            var request = CreateRequest("rebate1", "product1", 10);
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };

            RebateDataStoreMock.Setup(m => m.GetRebate("rebate1")).Returns((Rebate) null);
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
            var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount, Amount = 100 };

            RebateDataStoreMock.Setup(m => m.GetRebate("rebate1")).Returns(rebate);
            ProductDataStoreMock.Setup(m => m.GetProduct("product1")).Returns((Product) null);

            // Act
            var result = RebateService.Calculate(request);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void Calculate_Should_Return_Failure_If_Rebate_Amount_Is_Zero()
        {
            // Arrange
            var request = CreateRequest("rebate1", "product1", 10);
            var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount, Amount = 0 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };

            RebateDataStoreMock.Setup(m => m.GetRebate("rebate1")).Returns(rebate);
            ProductDataStoreMock.Setup(m => m.GetProduct("product1")).Returns(product);

            // Act
            var result = RebateService.Calculate(request);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void Calculate_Should_Return_Failure_If_FixedCashAmount_Not_Supported()
        {
            // Arrange
            var request = CreateRequest("rebate1", "product1", 10);
            var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount, Amount = 100 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate }; // No FixedCashAmount support

            RebateDataStoreMock.Setup(m => m.GetRebate("rebate1")).Returns(rebate);
            ProductDataStoreMock.Setup(m => m.GetProduct("product1")).Returns(product);

            // Act
            var result = RebateService.Calculate(request);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void Calculate_Should_Return_Success_For_Valid_FixedCashAmount()
        {
            // Arrange
            var request = CreateRequest("rebate1", "product1", 10);
            var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount, Amount = 100 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };

            RebateDataStoreMock.Setup(m => m.GetRebate("rebate1")).Returns(rebate);
            ProductDataStoreMock.Setup(m => m.GetProduct("product1")).Returns(product);

            // Act
            var result = RebateService.Calculate(request);

            // Assert
            Assert.True(result.Success);
            
            // TODO
            //RebateDataStoreMock.Verify(m => m.StoreCalculationResult(rebate, 100), Times.Once);
        }
    }
}