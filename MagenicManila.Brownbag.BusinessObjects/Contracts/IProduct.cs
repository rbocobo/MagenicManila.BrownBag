using MagenicManila.Brownbag.BusinessObjects.Core.Contracts;

namespace MagenicManila.Brownbag.BusinessObjects.Contracts
{
    public interface IProduct
        : IBusinessBaseCore
    {
        int? Id { get; }
        string Name { get; set; }
        string Description { get; set; }
        string Brand { get; set; }
        decimal Price { get; set; }
        string SKU { get; set; }
    }
}
