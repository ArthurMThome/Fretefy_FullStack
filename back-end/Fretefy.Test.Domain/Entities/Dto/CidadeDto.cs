using System;

namespace Fretefy.Test.Domain.Entities.Dto
{
    public class CidadeDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string UF { get; set; }
        public Guid? RegiaoId { get; set; }
        public string? RegiaoNome { get; set; }
    }
}
