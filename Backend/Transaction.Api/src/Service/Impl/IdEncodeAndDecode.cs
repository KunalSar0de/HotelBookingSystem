using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HashidsNet;
using Microsoft.Extensions.Configuration;

namespace Transaction.Api.src.Service.Impl
{
	public class IdEncodeAndDecode : IIdEncodeAndDecode
	{
		private readonly IConfiguration _configuration;

		public IdEncodeAndDecode(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public int DecodeId(string Id)
		{
			string saltString = _configuration["HashIds:Salt"];
			int minLength = Convert.ToInt32(_configuration["HashIds:Length"]);
			var hashids = new Hashids(minHashLength:minLength,salt:saltString);
			int decodedId = 0;
			var id = hashids.Decode(Id); 
			if(id.Length > 0)
				return id[0];
			return decodedId;
		}
 
		public string EncodeId(int Id)
		{
			string saltString = _configuration["HashIds:Salt"];
			int minLength = Convert.ToInt32(_configuration["HashIds:Length"]);
			var hashids = new Hashids(saltString , minLength);
			return hashids.Encode(Id);	
		}
	}
}