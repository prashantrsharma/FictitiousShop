namespace ProductService.Events.Contracts {
    using System;

    /// <summary>
    ///  Contract for ProductAdded Event 
    /// </summary>
    public interface IProductAddedEvent : IIntegrationEvent {
        int ProductId { get; }
        string Name { get; }
        string Description { get; }
        decimal UnitPrice { get; }
        long UnitsInStock { get; }
        bool IsActive { get; }
    }
}