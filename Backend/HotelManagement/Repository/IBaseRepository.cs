using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Repository
{
    public interface IBaseRepository<T> where T : class 
    { 
        T Add(T entity);
		T Get(int id);
		List<T> GetAll();
		void Update(T entity);
    }
}