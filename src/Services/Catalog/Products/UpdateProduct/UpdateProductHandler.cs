namespace CatalogAPI.Products.UpdateProduct;
public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccess);
public class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand commnad, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductCommandHandler.Handle called for {@commnad}", commnad);
        var product = await session.LoadAsync<Product>(commnad.Id, cancellationToken);

        if(product is null)
        {
            throw new ProductNotFoundException();
        }

        product.Name = commnad.Name;
        product.Category = commnad.Category;
        product.Description = commnad.Description;
        product.ImageFile = commnad.ImageFile;
        product.Price = commnad.Price;

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}
