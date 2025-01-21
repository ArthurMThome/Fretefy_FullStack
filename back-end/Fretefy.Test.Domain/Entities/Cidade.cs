﻿using System;

namespace Fretefy.Test.Domain.Entities
{
    public class Cidade : IEntity
    {
        public Cidade()
        {
        
        }

        public Cidade(string nome, string uf)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            UF = uf;
            RegiaoId = null;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string UF { get; set; }
        public Guid? RegiaoId { get; set; }
    }
}
