using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transaction.Api.src.Dto.Response;

namespace Transaction.Api.src.Authorizer
{
    public interface IJwtAuthenticationManager
    {
        TokenResponse Authenticate(string userName,string password);
    }
}