var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres").WithDataVolume();
var productDb = postgres.AddDatabase("ProductServiceDb");
var orderDb = postgres.AddDatabase("OrderServiceDb");
var kafka = builder.AddKafka("kafka")
                    .WithKafkaUI()
                    .WithDataVolume()
                    .WithEnvironment("KAFKA_AUTO_CREATE_TOPICS_ENABLE", "true");

var productService = builder.AddProject<Projects.ProductService_Api>("product-service")
                            .WithReference(productDb)
                            .WithReference(kafka)
                            .WaitFor(productDb)
                            .WaitFor(kafka);

var orderService = builder.AddProject<Projects.OrderService_Api>("order-service")
                           .WithReference(orderDb)
                           .WithReference(kafka)
                           .WaitFor(orderDb)
                           .WaitFor(productService); ;

builder.Build().Run();
