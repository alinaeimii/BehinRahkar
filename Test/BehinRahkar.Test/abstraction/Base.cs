using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Product.Application.AutoMapper;
using Product.Infrastructure.Data;

namespace BehinRahkar.Test.abstraction
{
    public class Base
    {
        protected readonly AppDbContext _dbContext;
        protected readonly IMapper _mapper;
        public Base()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("BehinRahkarTest");

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Mapping());
            });
            _mapper = mockMapper.CreateMapper();
            _dbContext = new AppDbContext(builder.Options);
        }
    }
}
