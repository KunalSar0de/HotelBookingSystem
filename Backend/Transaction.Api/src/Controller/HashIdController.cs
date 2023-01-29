using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Transaction.Api.src.Service;

namespace Transaction.Api.src.Controller
{
	public class HashIdController : BaseController
	{
		public HashIdController(IIdEncodeAndDecode idEncodeAndDecode) : base(idEncodeAndDecode)
		{
		}


		[Route("gethashid/{id}")]
		[HttpGet]
		public IActionResult GetId(string id)
		{
			var decodedId = DecodeId(id);
			return Ok(decodedId); 
			
		}

		[Route("gethashid/{id:int}")]
		[HttpGet]
		public  IActionResult GetIntId(int id)
		{
			var encodedId = EncodeId(id);
			return Ok(encodedId);
		}
	}
}