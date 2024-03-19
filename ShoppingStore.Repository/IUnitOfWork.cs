using ShoppingStore.Repository.Models;
using ShoppingStore.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Repository
{
    public interface IUnitOfWork :IDisposable
    {
        IBaseRepository<Category> Categories { get; }
        IBaseRepository<Product> Products { get; }

        IBaseRepository<Order> Orders { get; }  

       public Task<int> Complete();

    }
}
