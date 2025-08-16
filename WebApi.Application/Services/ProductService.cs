using WebApi.Application.DTOs;
using WebApi.Application.Mappings;
using WebApi.Domain.Common;
using WebApi.Domain.Entities;

namespace WebApi.Application.Services;

public class ProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

   public async Task<IEnumerable<ProductDto>> GetProductsAsync(CancellationToken cancellationToken)
   {
       var products = await _unitOfWork.Products.GetAllAsync(cancellationToken);
       return products.Select(o => o.ToDto());
   }

   public async Task<ProductDto?> GetProductAsync(Guid id, CancellationToken cancellationToken)
   {
       var product = await _unitOfWork.Products.GetByIdAsync(id, cancellationToken);
       return product?.ToDto();
   }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto dto, CancellationToken cancellationToken)
    {
        var product = new Product(Guid.NewGuid(), dto.Name, dto.Price);

        _unitOfWork.Products.Add(product);
        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return product.ToDto();
    }
}