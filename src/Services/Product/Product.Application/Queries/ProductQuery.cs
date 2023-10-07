using AutoMapper;
using MediatR;
using Product.Application.DTOs;
using Product.Core.Entites;
using Product.Core.Interfaces;

namespace Product.Application.Queries;
public class ProductQuery
{
    public record ReadAllProductQuery() : IRequest<IEnumerable<ProductsDTO>>;

    public class ReadAllProductHandler : IRequestHandler<ReadAllProductQuery, IEnumerable<ProductsDTO>>
    {
        private readonly IRepository<Products> _productRepository;
        private readonly IMapper _mapper;
        public ReadAllProductHandler(IRepository<Products> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductsDTO>> Handle(ReadAllProductQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return _mapper.Map<IEnumerable<ProductsDTO>>(await _productRepository.GetAllAsync());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
