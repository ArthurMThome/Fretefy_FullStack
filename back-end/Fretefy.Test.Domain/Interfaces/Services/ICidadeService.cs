﻿using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Entities.Auxiliar;
using System;
using System.Collections.Generic;

namespace Fretefy.Test.Domain.Interfaces
{
    public interface ICidadeService
    {
        Cidade Get(Guid id);
        IEnumerable<Cidade> List();
        IEnumerable<Cidade> ListByUf(string uf);
        IEnumerable<Cidade> Query(string terms);
        DefaultReturn<Cidade> AdicionarCidade(Cidade cidade);
        DefaultReturn<Cidade> Update(Cidade cidade);
    }
}
