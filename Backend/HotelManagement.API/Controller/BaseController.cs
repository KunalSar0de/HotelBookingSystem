using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Service;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.API.src.Controller
{
	
	[Consumes("application/json")]
	[Produces("application/json")]
	[FormatFilter]
    public class BaseController : Microsoft.AspNetCore.Mvc.Controller
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