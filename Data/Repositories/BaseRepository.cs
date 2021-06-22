using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Core.Repositories;

namespace WebApp.Data.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T>
    {
        public abstract bool Create(T item);
        public abstract bool Delete(int id);
        public abstract IEnumerable<T> GetAll();
        public abstract T GetById(int id);
        public abstract bool Update(T item);
    }
}
