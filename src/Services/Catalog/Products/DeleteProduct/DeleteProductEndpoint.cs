


namespace CatalogAPI.Products.DeleteProduct;
//public record DeleteProductRequest(Guid Id);
public record DeleteProductResponse(bool IsSuccess);
public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{Id}", async (Guid Id, ISender sender) =>
        {
            var command = new DeleteProductCommand(Id);
            var result = await sender.Send(command);
            var response = result.Adapt<DeleteProductResponse>();

            return Results.Ok(response);
        })
            .WithName("DeleteProductResponse")
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("DeleteProductResponse")
            .WithDescription("DeleteProductResponse");
    }
}
