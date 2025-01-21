using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Entities.Auxiliar;
using System;
using System.Collections.Generic;

namespace Fretefy.Test.Domain.Interfaces.Services
{
    public interface IRegiaoService
    {
        DefaultReturn<Regiao> ObterPorId(Guid id);
        DefaultReturn<IEnumerable<Regiao>> Listar();
        DefaultReturn<IEnumerable<Regiao>> ListarPorCidade(string cidade);
        DefaultReturn<IEnumerable<Regiao>> ListarPorNome(string nome);
        DefaultReturn<Regiao> ChangeStatus(Regiao regiao);
    }
}