using Smartwyre.DeveloperTest.Services.Impl.Calculators;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Units.Calculators
{
    public class FixedCashAmountCalculatorTests
    {
        FixedCashAmountCalculator calculator = new FixedCashAmountCalculator();

        private CalculateRebateRequest CreateRequest(string rebateId, string productId, decimal volume)
        {
            return new CalculateRebateRequest
            {
                RebateIdentifier = rebateId,
                ProductIdentifier = productId,
                Volume = volume
            };
        }

        #region IsEligible

        [Fact]
        public void IsEligible_Should_Return_Failure_If_Rebate_Is_Null()
        {
            // Arrange
            var request = CreateRequest("rebate1", "product1", 10);
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };

            // Act
            var result = calculator.IsEligible(null, product, request);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsEligible_Should_Return_Failure_If_Product_Is_Null()
        {
            // Arrange
            var request = CreateRequest("rebate1", "product1", 10);
            var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount, Amount = 100 };

            // Act
            var result = calculator.IsEligible(rebate, null, request);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsEligible_Should_Return_Failure_If_Rebate_Amount_Is_Zero()
        {
            // Arrange
            var request = CreateRequest("rebate1", "product1", 10);
            var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount, Amount = 0 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };

            // Act
            var result = calculator.IsEligible(rebate, product, request);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsEligible_Should_Return_Failure_If_FixedCashAmount_Not_Supported()
        {
            // Arrange
            var request = CreateRequest("rebate1", "product1", 10);
            var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount, Amount = 100 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate }; // No FixedCashAmount support

            // Act
            var result = calculator.IsEligible(rebate, product, request);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region CalculateRebate

        [Fact]
        public void CalculateRebate_Should_Return_Success_For_Valid_FixedCashAmount()
        {
            // Arrange
            var request = CreateRequest("rebate1", "product1", 10);
            var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount, Amount = 100 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };

            // Act
            var result = calculator.CalculateRebate(rebate, product, request);

            // Assert
            Assert.Equal(100, result);
        }

        #endregion
    }
}
