using Moq;
using Smartwyre.DeveloperTest.Services.Impl;
using Smartwyre.DeveloperTest.Services.Interfaces;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    public class AmountPerUomTests : RebateServiceTestsBase
    {
        protected override IIncentiveCalculator CreateCalculator() => new AmountPerUomCalculator();

        [Fact]
        public void Calculate_Should_Return_Failure_If_Rebate_Is_Null()
        {
            // Arrange
            var request = CreateRequest("rebate1", "product1", 10);
            var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom };

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
            var rebate = new Rebate { Incentive = IncentiveType.AmountPerUom, Amount = 5 };

            RebateDataStoreMock.Setup(m => m.GetRebate("rebate1")).Returns(rebate);
            ProductDataStoreMock.Setup(m => m.GetProduct("product1")).Returns((Product)null);

            // Act
            var result = RebateService.Calculate(request);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void Calculate_Should_Return_Failure_If_Amount_Is_Zero()
        {
            // Arrange
            var request = CreateRequest("rebate1", "product1", 10);
            var rebate = new Rebate { Incentive = IncentiveType.AmountPerUom, Amount = 0 }; // Amount is zero
            var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom };

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
            var rebate = new Rebate { Incentive = IncentiveType.AmountPerUom, Amount = 5 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom };

            RebateDataStoreMock.Setup(m => m.GetRebate("rebate1")).Returns(rebate);
            ProductDataStoreMock.Setup(m => m.GetProduct("product1")).Returns(product);

            // Act
            var result = RebateService.Calculate(request);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void Calculate_Should_Return_Failure_If_AmountPerUom_Not_Supported()
        {
            // Arrange
            var request = CreateRequest("rebate1", "product1", 10);
            var rebate = new Rebate { Incentive = IncentiveType.AmountPerUom, Amount = 5 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate }; // No AmountPerUom support

            RebateDataStoreMock.Setup(m => m.GetRebate("rebate1")).Returns(rebate);
            ProductDataStoreMock.Setup(m => m.GetProduct("product1")).Returns(product);

            // Act
            var result = RebateService.Calculate(request);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void Calculate_Should_Return_Success_For_Valid_AmountPerUom()
        {
            // Arrange
            var request = CreateRequest("rebate1", "product1", 10);
            var rebate = new Rebate { Incentive = IncentiveType.AmountPerUom, Amount = 5 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom };

            RebateDataStoreMock.Setup(m => m.GetRebate("rebate1")).Returns(rebate);
            ProductDataStoreMock.Setup(m => m.GetProduct("product1")).Returns(product);

            // Act
            var result = RebateService.Calculate(request);

            // Assert
            Assert.True(result.Success);

            // TODO
            // RebateDataStoreMock.Verify(m => m.StoreCalculationResult(rebate, 50), Times.Once); // 10 * 5 = 50
        }
    }
}