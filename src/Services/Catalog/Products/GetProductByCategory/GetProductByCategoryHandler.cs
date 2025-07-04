namespace CatalogAPI.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category):IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);
public class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByCategoryResult.Handle called with {@query}", query);

        var products = await session.Query<Product>()
            .Where(x => x.Category.Contains(query.Category)).ToListAsync(cancellationToken);

        if (products.IsEmpty())
            throw new ProductNotFoundException();

        return new GetProductByCategoryResult(products);
    }
}
