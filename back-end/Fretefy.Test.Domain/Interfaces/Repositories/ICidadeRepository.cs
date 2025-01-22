using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Entities.Auxiliar;
using System;
using System.Collections.Generic;

namespace Fretefy.Test.Domain.Interfaces.Repositories
{
    public interface ICidadeRepository
    {
        DefaultReturn<IEnumerable<Cidade>> Listar();
        DefaultReturn<IEnumerable<Cidade>> ObterPorRegiaoId(Guid id);
        DefaultReturn<IEnumerable<Cidade>> ObterPorRegiaoNull();
        DefaultReturn<IEnumerable<Cidade>> ListByUf(string uf);
        DefaultReturn<IEnumerable<Cidade>> Query(string terms);
        DefaultReturn<IEnumerable<Cidade>> VerificarCidadeExistente(string nome, string uf);
        DefaultReturn<Cidade> AdicionarCidade(Cidade cidade);
        DefaultReturn<Cidade> Update(Cidade newCidade);
        DefaultReturn<Cidade> ObterPorId(Guid id);
    }
}
