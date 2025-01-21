using System;
using System.Collections.Generic;

namespace Fretefy.Test.Domain.Entities
{
    public class Regiao
    {
        public Regiao()
        {

        }

        public Guid Id { get; set; }

        public string Nome { get; set; }

        public IEnumerable<Cidade> Cidades { get; set; }
    }
}
