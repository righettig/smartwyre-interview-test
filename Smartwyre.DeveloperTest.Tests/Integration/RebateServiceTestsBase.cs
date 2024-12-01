using Moq;
using Smartwyre.DeveloperTest.Data.Interfaces;
using Smartwyre.DeveloperTest.Services.Impl;
using Smartwyre.DeveloperTest.Services.Interfaces;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Tests.Integration;

public abstract class RebateServiceTestsBase
{
    protected readonly Mock<IRebateDataStore> RebateDataStoreMock;
    protected readonly Mock<IProductDataStore> ProductDataStoreMock;
    protected readonly Mock<IIncentiveCalculatorFactory> IncentiveCalculatorFactoryMock;

    protected readonly RebateService RebateService;

    public RebateServiceTestsBase()
    {
        RebateDataStoreMock = new Mock<IRebateDataStore>();
        ProductDataStoreMock = new Mock<IProductDataStore>();
        IncentiveCalculatorFactoryMock = new Mock<IIncentiveCalculatorFactory>();

        IncentiveCalculatorFactoryMock
            .Setup(m => m.GetCalculator(It.IsAny<IncentiveType>()))
            .Returns(CreateCalculator());

        RebateService = new RebateService(RebateDataStoreMock.Object,
                                          ProductDataStoreMock.Object,
                                          IncentiveCalculatorFactoryMock.Object);
    }

    protected CalculateRebateRequest CreateRequest(string rebateId, string productId, decimal volume)
    {
        return new CalculateRebateRequest
        {
            RebateIdentifier = rebateId,
            ProductIdentifier = productId,
            Volume = volume
        };
    }

    protected abstract IIncentiveCalculator CreateCalculator();
}