using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transaction.Api.src.Repository
{
    public interface IReadWriteRepository<T>
    {
        T add(T obj);
		T Get(int id);
		List<T> GetAll();
		void Update(T obj);
    }
}