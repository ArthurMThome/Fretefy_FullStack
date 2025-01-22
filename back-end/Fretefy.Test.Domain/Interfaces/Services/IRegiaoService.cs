using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Entities.Auxiliar;
using Fretefy.Test.Domain.Entities.Dto;
using System;
using System.Collections.Generic;

namespace Fretefy.Test.Domain.Interfaces.Services
{
    public interface IRegiaoService
    {
        DefaultReturn<Regiao> ObterPorId(Guid id);
        DefaultReturn<IEnumerable<RegiaoDto>> Listar();
        DefaultReturn<IEnumerable<RegiaoDto>> ListarPorCidade(string cidade);
        DefaultReturn<IEnumerable<RegiaoDto>> ListarPorNome(string nome);
        DefaultReturn<RegiaoDto> ChangeStatus(Regiao regiao);
        DefaultReturn<Regiao> AdicionarRegiao(Regiao regiao);
        DefaultReturn<RegiaoDto> Update(Regiao regiao);
    }
}