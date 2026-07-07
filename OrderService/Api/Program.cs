using OrderService.Api.GraphQL;
using OrderService.Application;
using OrderService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Mutation>();

var app = builder.Build();

app.MapGraphQL();
app.MapDefaultEndpoints();
app.Run();
