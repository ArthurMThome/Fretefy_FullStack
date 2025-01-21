using Newtonsoft.Json;
using System;

namespace Fretefy.Test.Domain.Entities
{
    public class Cidade : IEntity
    {
        public Cidade()
        {
        
        }

        public Cidade(string nome, string uf, Guid? regiaoId)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            UF = uf;
            RegiaoId = regiaoId;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string UF { get; set; }
        public Guid? RegiaoId { get; set; }
        [JsonIgnore]
        public Regiao Regiao { get; set; }
    }
}