using AutoMapper;
using MediatR;
using Product.Application.DTOs;
using Product.Core.Entites;
using Product.Core.Interfaces;

namespace Product.Application.Commands;
public class ProductCommand
{
    public record AddProductCommand(ProductsDTO ProductItem) : IRequest<ProductsDTO>;

    public class AddProductHandler : IRequestHandler<AddProductCommand, ProductsDTO>
    {
        private readonly IRepository<Products> _productRepository;
        private readonly IMapper _mapper;
        public AddProductHandler(IRepository<Products> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ProductsDTO> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Products>(request.ProductItem);
            return _mapper.Map<ProductsDTO>(await _productRepository.AddAsync(product));
        }
    }
}
