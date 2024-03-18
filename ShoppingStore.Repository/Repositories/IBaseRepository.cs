using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Repository.Repositories
{
    public interface IBaseRepository<T>where T : class
    {
        public Task<T> GetById(int id);
        public IEnumerable<T> GetAll();

       public Task<T> Find(Expression<Func<T,bool>> match);
    }
}
