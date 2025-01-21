using Fretefy.Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretefy.Test.Domain.Interfaces.Services
{
    public interface IRegiaoService
    {
        Regiao ObterPorId(Guid id);
        IEnumerable<Regiao> Listar();
        IEnumerable<Regiao> ListarPorCidade(string cidade);
        IEnumerable<Regiao> ListarPorNome(string nome);
    }
}