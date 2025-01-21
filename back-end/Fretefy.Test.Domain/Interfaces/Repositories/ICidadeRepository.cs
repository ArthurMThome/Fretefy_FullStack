using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Entities.Auxiliar;
using System.Collections.Generic;
using System.Linq;

namespace Fretefy.Test.Domain.Interfaces.Repositories
{
    public interface ICidadeRepository
    {
        IQueryable<Cidade> List();
        IEnumerable<Cidade> ListByUf(string uf);
        IEnumerable<Cidade> Query(string terms);
        IEnumerable<Cidade> VerificarCidadeExistente(string nome, string uf);
        DefaultReturn<Cidade> AdicionarCidade(Cidade cidade);
        DefaultReturn<Cidade> Update(Cidade newCidade);
    }
}
