using Azure.Data.Tables;
using BasicOperations;

var connectionString = "<connection string>";

// Clients
var tableServiceClient = new TableServiceClient(connectionString);
await tableServiceClient.CreateTableIfNotExistsAsync("Products");
var tableClient = new TableClient(connectionString, "Products");


// Table Management
await tableServiceClient.CreateTableAsync("Reports");
await tableServiceClient.CreateTableIfNotExistsAsync("Reports");
await tableServiceClient.DeleteTableAsync("Reports");



// CRUD Operations

// --- Inserting ---
var product = new Product(1050, "Clean Code", true, 5050, "Books", 42.50m, DateTime.UtcNow);
// await tableClient.AddEntityAsync(product);

// --- Upserting ---
var product1 = new Product(1050, "Clean Code", false, 5050, "Books", 49.99m, DateTime.UtcNow);
await tableClient.UpsertEntityAsync(product1);

var product2 = new Product(1060, "Refactoring", true, 5050, "Books", 40.99m, DateTime.UtcNow);
await tableClient.UpsertEntityAsync(product2);

// --- Get an entity ---
var response = await tableClient.GetEntityAsync<Product>(5050.ToString(), 1050.ToString());
if (response.HasValue)
{
    var productReturned = response.Value;
    Console.WriteLine($"Name of the product is: {productReturned.Name}");
}

// --- Querying multiple entities ---
var page = tableClient.QueryAsync<Product>(p => p.InStock == true && p.CategoryName == "Books");
await foreach (var p in page)
{
    Console.WriteLine($"Name of the product is: {p.Name}");
}

// --- Updating an Entity ---
response = await tableClient.GetEntityAsync<Product>(5050.ToString(), 1050.ToString());
if (response.HasValue)
{
    var productToUpdate = response.Value;
    productToUpdate.Name = "Clean Code By Uncle Bob";
    await tableClient.UpdateEntityAsync(productToUpdate, productToUpdate.ETag);
}


// --- Deleting an Entity ---
await tableClient.DeleteEntityAsync(5050.ToString(), 1050.ToString());