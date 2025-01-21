using Fretefy.Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fretefy.Test.Domain.Interfaces.Repositories
{
    public interface IRegiaoRepository
    {
        IQueryable<Regiao> ObterPorId(Guid id);
        IQueryable<Regiao> ListarPorNome(string nome);
        IEnumerable<Regiao> Listar();
        IEnumerable<Regiao> ListarPorCidade(string cidade);
    }
}
