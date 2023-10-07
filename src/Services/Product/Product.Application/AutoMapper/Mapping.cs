using AutoMapper;
using Product.Application.DTOs;
using Product.Core.Entites;

namespace Product.Application.AutoMapper;
public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<Products, ProductsDTO>().ReverseMap();
    }
}