﻿namespace CatalogAPI.Products.GetProductById;

//public record GetProductByIdRequest();
public record GetProductByIdResponse(Product Product);
public class GetProductByIdEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id,ISender sender) =>
        {
            var query = new GetProductByIdQuery(id);
            var result = await sender.Send(query);
            var response =result.Adapt<GetProductByIdResponse>();
            return Results.Ok(response);
        })
            .WithName("GetProductById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("GetProductById")
            .WithDescription("GetProductById");
    }
}
