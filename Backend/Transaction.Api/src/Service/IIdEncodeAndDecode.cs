using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transaction.Api.src.Service
{
    public interface IIdEncodeAndDecode
    {
        int DecodeId(string Id);
		string EncodeId(int Id);
    }
}