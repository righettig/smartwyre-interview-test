using Moq;
using Smartwyre.DeveloperTest.Data.Interfaces;
using Smartwyre.DeveloperTest.Services.Impl;
using Smartwyre.DeveloperTest.Services.Interfaces;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Units
{
    public class RebateServiceTests
    {
        private readonly Mock<IRebateDataStore> RebateDataStoreMock;
        private readonly Mock<IProductDataStore> ProductDataStoreMock;
        private readonly Mock<IIncentiveCalculatorFactory> CalculatorFactoryMock;
        private readonly Mock<IIncentiveCalculator> CalculatorMock;

        private readonly RebateService RebateService;

        public RebateServiceTests()
        {
            RebateDataStoreMock = new Mock<IRebateDataStore>();
            ProductDataStoreMock = new Mock<IProductDataStore>();
            CalculatorFactoryMock = new Mock<IIncentiveCalculatorFactory>();
            CalculatorMock = new Mock<IIncentiveCalculator>();

            RebateService = new RebateService(
                RebateDataStoreMock.Object,
                ProductDataStoreMock.Object,
                CalculatorFactoryMock.Object
            );
        }

        [Fact]
        public void Calculate_ShouldReturnFailure_WhenRebateIsNull()
        {
            // Arrange
            RebateDataStoreMock.Setup(x => x.GetRebate(It.IsAny<string>())).Returns((Rebate)null);
            ProductDataStoreMock.Setup(x => x.GetProduct(It.IsAny<string>())).Returns(new Product());

            var request = new CalculateRebateRequest();

            // Act
            var result = RebateService.Calculate(request);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void Calculate_ShouldReturnFailure_WhenProductIsNull()
        {
            // Arrange
            RebateDataStoreMock.Setup(x => x.GetRebate(It.IsAny<string>())).Returns(new Rebate());
            ProductDataStoreMock.Setup(x => x.GetProduct(It.IsAny<string>())).Returns((Product)null);

            var request = new CalculateRebateRequest();

            // Act
            var result = RebateService.Calculate(request);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void Calculate_ShouldReturnFailure_WhenCalculatorIsNotEligible()
        {
            // Arrange
            var rebate = new Rebate { Incentive = IncentiveType.FixedRateRebate };
            var product = new Product();
            var request = new CalculateRebateRequest();

            RebateDataStoreMock.Setup(x => x.GetRebate(It.IsAny<string>())).Returns(rebate);
            ProductDataStoreMock.Setup(x => x.GetProduct(It.IsAny<string>())).Returns(product);
            CalculatorFactoryMock.Setup(x => x.GetCalculator(rebate.Incentive)).Returns(CalculatorMock.Object);
            CalculatorMock.Setup(x => x.IsEligible(rebate, product, request)).Returns(false);

            // Act
            var result = RebateService.Calculate(request);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void Calculate_ShouldStoreResultAndReturnSuccess_WhenEligible()
        {
            // Arrange
            var rebate = new Rebate { Incentive = IncentiveType.FixedRateRebate };
            var product = new Product();
            var request = new CalculateRebateRequest();
            var expectedRebateAmount = 100m;

            RebateDataStoreMock.Setup(x => x.GetRebate(It.IsAny<string>())).Returns(rebate);
            ProductDataStoreMock.Setup(x => x.GetProduct(It.IsAny<string>())).Returns(product);
            CalculatorFactoryMock.Setup(x => x.GetCalculator(rebate.Incentive)).Returns(CalculatorMock.Object);
            CalculatorMock.Setup(x => x.IsEligible(rebate, product, request)).Returns(true);
            CalculatorMock.Setup(x => x.CalculateRebate(rebate, product, request)).Returns(expectedRebateAmount);

            // Act
            var result = RebateService.Calculate(request);

            // Assert
            Assert.True(result.Success);
            RebateDataStoreMock.Verify(x => x.StoreCalculationResult(rebate, expectedRebateAmount), Times.Once);
        }

        [Fact]
        public void Calculate_ShouldCallCorrectCalculator_BasedOnIncentiveType()
        {
            // Arrange
            var rebate = new Rebate { Incentive = IncentiveType.AmountPerUom };
            var product = new Product();
            var request = new CalculateRebateRequest();

            RebateDataStoreMock.Setup(x => x.GetRebate(It.IsAny<string>())).Returns(rebate);
            ProductDataStoreMock.Setup(x => x.GetProduct(It.IsAny<string>())).Returns(product);
            CalculatorFactoryMock.Setup(x => x.GetCalculator(rebate.Incentive)).Returns(CalculatorMock.Object);
            CalculatorMock.Setup(x => x.IsEligible(rebate, product, request)).Returns(true);

            // Act
            var result = RebateService.Calculate(request);

            // Assert
            Assert.True(result.Success);
            CalculatorFactoryMock.Verify(x => x.GetCalculator(rebate.Incentive), Times.Once);
        }
    }
}
