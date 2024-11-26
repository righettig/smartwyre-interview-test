using Moq;
using Smartwyre.DeveloperTest.Data.Interfaces;
using Smartwyre.DeveloperTest.Services.Impl;
using Smartwyre.DeveloperTest.Types;

public class RebateServiceTestsBase
{
    protected readonly Mock<IRebateDataStore> RebateDataStoreMock;
    protected readonly Mock<IProductDataStore> ProductDataStoreMock;

    protected readonly RebateService RebateService;

    public RebateServiceTestsBase()
    {
        RebateDataStoreMock = new Mock<IRebateDataStore>();
        ProductDataStoreMock = new Mock<IProductDataStore>();

        RebateService = new RebateService(RebateDataStoreMock.Object, ProductDataStoreMock.Object);
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
}
