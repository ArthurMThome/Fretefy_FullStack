using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Entities.Auxiliar;
using System;
using System.Collections.Generic;

namespace Fretefy.Test.Domain.Interfaces.Services
{
    public interface ICidadeService
    {
        DefaultReturn<Cidade> ObterPorId(Guid id);
        DefaultReturn<IEnumerable<Cidade>> Listar();
        DefaultReturn<IEnumerable<Cidade>> ListByUf(string uf);
        DefaultReturn<IEnumerable<Cidade>> Query(string terms);
        DefaultReturn<Cidade> AdicionarCidade(Cidade cidade);
        DefaultReturn<Cidade> Update(Cidade cidade);
    }
}
