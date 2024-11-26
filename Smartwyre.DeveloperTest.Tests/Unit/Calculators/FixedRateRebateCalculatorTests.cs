using Smartwyre.DeveloperTest.Services.Impl.Calculators;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Units.Calculators
{
    public class FixedRateRebateCalculatorTests
    {
        FixedRateRebateCalculator calculator = new FixedRateRebateCalculator();

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
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate };

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
            var rebate = new Rebate { Incentive = IncentiveType.FixedRateRebate, Amount = 100 };

            // Act
            var result = calculator.IsEligible(rebate, null, request);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsEligible_Should_Return_Failure_If_Price_Is_Zero()
        {
            // Arrange
            var request = CreateRequest("rebate1", "product1", 10);
            var rebate = new Rebate { Incentive = IncentiveType.FixedRateRebate, Percentage = 0.1m };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 0 }; // Price is zero

            // Act
            var result = calculator.IsEligible(rebate, product, request);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Calculate_Should_Return_Failure_If_Volume_Is_Zero()
        {
            // Arrange
            var request = CreateRequest("rebate1", "product1", 0); // Volume is zero
            var rebate = new Rebate { Incentive = IncentiveType.FixedRateRebate, Percentage = 0.1m };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 100 };

            // Act
            var result = calculator.IsEligible(rebate, product, request);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Calculate_Should_Return_Failure_If_Percentage_Is_Zero()
        {
            // Arrange
            var request = CreateRequest("rebate1", "product1", 10);
            var rebate = new Rebate { Incentive = IncentiveType.FixedRateRebate, Percentage = 0 }; // Percentage is zero
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 100 };

            // Act
            var result = calculator.IsEligible(rebate, product, request);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Calculate_Should_Return_Failure_If_FixedRateRebate_Not_Supported()
        {
            // Arrange
            var request = CreateRequest("rebate1", "product1", 10);
            var rebate = new Rebate { Incentive = IncentiveType.FixedRateRebate, Amount = 100 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom }; // No FixedRateRebate support

            // Act
            var result = calculator.IsEligible(rebate, product, request);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region CalculateRebate

        [Fact]
        public void CalculateRebate_Should_Return_Success_For_Valid_FixedRateRebate()
        {
            // Arrange
            var request = CreateRequest("rebate1", "product1", 10);
            var rebate = new Rebate { Incentive = IncentiveType.FixedRateRebate, Percentage = 0.1m };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 200 };

            // Act
            var result = calculator.CalculateRebate(rebate, product, request);

            // Assert
            Assert.Equal(200, result);
        }

        #endregion
    }
}
