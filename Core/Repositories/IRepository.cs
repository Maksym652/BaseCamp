namespace WebApp.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        bool Create(T item);

        bool Update(T item);

        bool Delete(int id);
    }
}
