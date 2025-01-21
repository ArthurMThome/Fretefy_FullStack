using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Entities.Auxiliar;
using System;
using System.Collections.Generic;

namespace Fretefy.Test.Domain.Interfaces.Repositories
{
    public interface IRegiaoRepository
    {
        DefaultReturn<Regiao> ObterPorId(Guid id);
        DefaultReturn<IEnumerable<Regiao>> ListarPorNome(string nome, bool beLike = true);
        DefaultReturn<IEnumerable<Regiao>> Listar();
        DefaultReturn<IEnumerable<Regiao>> ListarPorCidade(string cidade);
        DefaultReturn<Regiao> Update(Regiao regiao);
        DefaultReturn<Regiao> AdicionarRegiao(Regiao regiao);
    }
}
