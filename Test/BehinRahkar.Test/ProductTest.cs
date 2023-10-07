using AutoMapper;
using BehinRahkar.Test.abstraction;
using Microsoft.EntityFrameworkCore;
using Product.Application.AutoMapper;
using Product.Application.DTOs;
using Product.Core.Entites;
using Product.Infrastructure.Data;
using Product.Infrastructure.Repositories;
using Xunit;
using static Product.Application.Commands.ProductCommand;
using static Product.Application.Queries.ProductQuery;

namespace BehinRahkar.Test
{
    public class ProductTest : Base
    {

        [Fact]
        public async void Add_product()
        {
            // Arrange
            var productRepository = new Repository<Products>(_dbContext);
            var productMock = new ProductsDTO { Name = "test", Price = 10 };

            // Act
            var handler = new AddProductHandler(productRepository, _mapper);
            var result = await handler.Handle(new AddProductCommand(productMock), CancellationToken.None);

            // Assert
            Assert.Equal(result.Name, productMock?.Name);
            Assert.Equal(result.Price, productMock?.Price);
        }


        [Fact]
        public async void get_all_product()
        {
            // Arrange
            var productRepository = new Repository<Products>(_dbContext);
            var productMock = new Products { Name = "test", Price = 10 };

            // Act
            await productRepository.AddAsync(productMock);
            var handler = new ReadAllProductHandler(productRepository, _mapper);
            var result = (await handler.Handle(new ReadAllProductQuery(), CancellationToken.None)).FirstOrDefault();

            // Assert
            Assert.Equal(result?.Name, productMock?.Name);
            Assert.Equal(result?.Price, productMock?.Price);
        }
    }


}
