using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Entities.Auxiliar;
using Fretefy.Test.Domain.Entities.Dto;
using System;
using System.Collections.Generic;

namespace Fretefy.Test.Domain.Interfaces.Services
{
    public interface ICidadeService
    {
        DefaultReturn<Cidade> ObterPorId(Guid id);
        DefaultReturn<IEnumerable<CidadeDto>> Listar();
        DefaultReturn<IEnumerable<CidadeDto>> ObterPorRegiaoId(Guid id);
        DefaultReturn<IEnumerable<CidadeDto>> ObterPorRegiaoNull();
        DefaultReturn<IEnumerable<CidadeDto>> ListByUf(string uf);
        DefaultReturn<IEnumerable<CidadeDto>> Query(string terms);
        DefaultReturn<Cidade> AdicionarCidade(Cidade cidade);
        DefaultReturn<Cidade> Update(Cidade cidade);
    }
}
