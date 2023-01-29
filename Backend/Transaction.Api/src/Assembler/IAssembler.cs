using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transaction.Api.src.Assembler
{
    public interface IAssembler<T , TDTO>
    {
        T AssembleFromDto(TDTO dto);
		TDTO GetDTO(T obj);
    }
}