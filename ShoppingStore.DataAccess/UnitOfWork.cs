using ShoppingStore.DataAccess.Repositories;
using ShoppingStore.Repository;
using ShoppingStore.Repository.Models;
using ShoppingStore.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBaseRepository<Category> Categories { get; private set; }
        public IBaseRepository<Product> Products { get; private set; }

        public IBaseRepository<Order> Orders { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Categories = new BaseRepository<Category>(_context);
            Products = new BaseRepository<Product>(_context);
            Orders = new BaseRepository<Order>(_context);
        }
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
