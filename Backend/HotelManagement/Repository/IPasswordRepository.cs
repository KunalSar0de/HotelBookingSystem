using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Models;

namespace HotelManagement.Repository
{
    public interface IPasswordRepository : IBaseRepository<CriticalData>
    {
		CriticalData GetByCustomerId(int customerId);
    }
}