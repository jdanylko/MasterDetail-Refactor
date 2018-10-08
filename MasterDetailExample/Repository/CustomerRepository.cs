using MasterDetailExample.Context;
using MasterDetailExample.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterDetailExample.Repository
{
    public class CustomerRepository: Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbContext context) : base(context)
        {
        }
    }
}
