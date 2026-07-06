var builder = DistributedApplication.CreateBuilder(args);

var productService = builder.AddProject<Projects.ProductService_Api>("product-service");
var orderService = builder.AddProject<Projects.OrderService_Api>("order-service");

builder.Build().Run();
