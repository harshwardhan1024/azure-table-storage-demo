using Azure;
using Azure.Data.Tables;

namespace BasicOperations;

public class Product : ITableEntity
{
    // Required by Get operations
    public Product() { }

    public Product(long id, string name, bool inStock, long categoryId, 
        string categoryName, decimal price, DateTime createdAt)
    {
        Id = id;
        Name = name;
        InStock = inStock;
        CategoryId = categoryId;
        CategoryName = categoryName;
        Price = price;
        CreatedAt = createdAt;

        PartitionKey = categoryId.ToString();
        RowKey = id.ToString();
    }

    public long Id { get; set; }

    public string Name { get; set; }

    public bool InStock { get; set; }

    public long CategoryId { get; set; }

    public string CategoryName { get; set; }

    public decimal Price { get; set; }

    public DateTime CreatedAt { get; set; }

    // System Properties
    public string PartitionKey { get; set; }
    
    public string RowKey { get; set; }
    
    public DateTimeOffset? Timestamp { get; set; }
    
    public ETag ETag { get; set; }
}
