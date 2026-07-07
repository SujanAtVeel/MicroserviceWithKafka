using OrderService.Infrastructure;
using ProductService.Api.GraphQL;
using ProductService.Application;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
      .AddErrorFilter(error =>
    {
        if (builder.Environment.IsDevelopment())
        {
            // Attach the actual exception message/stack trace during dev
            return error.WithMessage(error.Exception?.ToString() ?? error.Message);
        }
        return error;
    });

var app = builder.Build();

app.MapGraphQL();
app.MapDefaultEndpoints();
app.Run();

