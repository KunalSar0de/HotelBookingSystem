using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Transaction.Api.src.Service;

namespace Transaction.Api.src.Controller
{
	
	[Consumes("application/json")]
	[Produces("application/json")]
	[FormatFilter]
    public class BaseController : ControllerBase
    {
		private readonly IIdEncodeAndDecode _idEncodeAndDecode;

		public BaseController(IIdEncodeAndDecode idEncodeAndDecode)
		{
			_idEncodeAndDecode = idEncodeAndDecode;
		}

		public string EncodeId(int id)
		{
			var encodedId = _idEncodeAndDecode.EncodeId(id);
			return encodedId;
		}

		public int DecodeId(string id)
		{
			var decodedId = _idEncodeAndDecode.DecodeId(id);
			return decodedId;
		}
	}
}