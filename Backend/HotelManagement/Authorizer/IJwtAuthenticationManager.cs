using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Dto.Response;

namespace HotelManagement.Authorizer
{
    public interface IJwtAuthenticationManager
    {
        TokenResponse Authenticate(string userName,string password);
    }
}