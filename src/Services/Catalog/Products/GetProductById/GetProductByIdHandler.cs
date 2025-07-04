namespace CatalogAPI.Products.GetProductById;

public record GetProductByIdQuery(Guid id):IQuery<GetProductByIdQueryResult>;
public record GetProductByIdQueryResult(Product Product);

internal class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdQueryResult>
{
    public async Task<GetProductByIdQueryResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByIdQueryResult.Handle called with {@query}", query);

        var product = await session.LoadAsync<Product>(query.id,cancellationToken);

        if(product is null)
        {
            throw new ProductNotFoundException();
        }

        return new GetProductByIdQueryResult(product);
    }
}
