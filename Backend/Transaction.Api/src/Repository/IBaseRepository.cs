using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transaction.Api.src.Repository
{
    public interface IBaseRepository<T> 
    {
        T Add(T entity);
		T Get(int id);
		List<T> GetAll();
		void Update(T entity);
    }
}