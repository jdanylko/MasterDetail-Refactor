using MasterDetailExample.Context;
using MasterDetailExample.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterDetailExample.Repository
{
    public class ProductRepository: Repository<Product>, IProductRepository
    {
        //public ProductRepository(): this(new MyCompanyContext()) { }
        public ProductRepository(DbContext context) : base(context)
        {
        }
    }
}