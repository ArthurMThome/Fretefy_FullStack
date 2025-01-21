using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Fretefy.Test.Domain.Entities
{
    public class Regiao
    {
        public Regiao()
        {
        }

        public Regiao(Guid id, string nome, IEnumerable<Cidade> cidades)
        {
            Id = id;
            Status = 1;
            Nome = nome;
            Cidades = cidades;
        }

        public Guid Id { get; set; }
        public ushort Status { get; set; }
        public string Nome { get; set; }
        public IEnumerable<Cidade> Cidades { get; set; }
    }
}
