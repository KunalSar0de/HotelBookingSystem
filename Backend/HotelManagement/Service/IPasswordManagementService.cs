using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Service
{
    public interface IPasswordManagementService
    {
        string GeneratePassword(string passwordString,int custId);
		string DecodePassword(int custId,string encryptedResult);
    }
}