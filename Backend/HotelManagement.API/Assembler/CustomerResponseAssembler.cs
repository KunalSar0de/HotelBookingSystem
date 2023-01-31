using System;
using HotelManagement.Models;
using HotelManagement.Service;
using HotelManagementrc.Dto;

namespace HotelManagement.API.src.Assembler
{
	public class CustomerResponseAssembler : IAssembler<Customers, CustomerResponseDto>
	{
		private readonly IIdEncodeAndDecode _iidEncodeAndDecode;

		public CustomerResponseAssembler(IIdEncodeAndDecode iidEncodeAndDecode)
		{
			_iidEncodeAndDecode = iidEncodeAndDecode;
		}

		public Customers AssembleFromDto(CustomerResponseDto dto)
		{
			throw new NotImplementedException();
		}

		public CustomerResponseDto GetDTO(Customers obj)
		{
	
			return new CustomerResponseDto{
				CustomerId = _iidEncodeAndDecode.EncodeId(obj.Id),
				FirstName = obj.FirstName,
				LastName = obj.LastName,
				IsActive = obj.IsActive,
				CreatedOn = obj.CreatedOn,
				UserType = obj.UserType
			};
		}
	}
}