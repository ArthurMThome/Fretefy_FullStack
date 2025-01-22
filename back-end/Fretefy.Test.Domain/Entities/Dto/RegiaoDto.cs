using System;
using System.Collections.Generic;

namespace Fretefy.Test.Domain.Entities.Dto
{
    public class RegiaoDto
    {
        public Guid Id { get; set; }
        public ushort Status { get; set; }
        public string Nome { get; set; }
        public IEnumerable<CidadeDto> Cidades { get; set; }
    }
}
