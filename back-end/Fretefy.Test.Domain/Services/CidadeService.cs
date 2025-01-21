using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Entities.Auxiliar;
using Fretefy.Test.Domain.Interfaces;
using Fretefy.Test.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fretefy.Test.Domain.Services
{
    public class CidadeService : ICidadeService
    {
        private readonly ICidadeRepository _cidadeRepository;

        public CidadeService(ICidadeRepository cidadeRepository)
        {
            _cidadeRepository = cidadeRepository;
        }

        public DefaultReturn<Cidade> AdicionarCidade(Cidade cidade)
        {
            if(string.IsNullOrEmpty(cidade.Nome))
                return new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.BadRequest, Message = "Nome da cidade está em branco.", Obj = cidade };

            if (string.IsNullOrEmpty(cidade.UF))
                return new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.BadRequest, Message = "Estado da cidade está em branco.", Obj = cidade };

            var resultCheck = _cidadeRepository.VerificarCidadeExistente(cidade.Nome, cidade.UF);
            if (resultCheck.Any())
                return new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.BadRequest, Message = "Já existe uma cidade com esse nome cadastrada nesse estado.", Obj = cidade };

            cidade.Id = Guid.NewGuid();

            return _cidadeRepository.AdicionarCidade(cidade);
        }

        public DefaultReturn<Cidade> Update(Cidade cidade)
        {
            try
            {
                if (cidade.Id == null || cidade.Id == Guid.Empty)
                    return new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.BadRequest, Message = "Id está null.", Obj = cidade };

                return _cidadeRepository.Update(cidade);
            }
            catch (Exception ex)
            {
                return new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message, Obj = cidade };
            }
        }

        public Cidade Get(Guid id)
        {
            return _cidadeRepository.List().FirstOrDefault(f => f.Id == id);
        }

        public IEnumerable<Cidade> List()
        {
            return _cidadeRepository.List();
        }

        public IEnumerable<Cidade> ListByUf(string uf)
        {
            return _cidadeRepository.ListByUf(uf);
        }

        public IEnumerable<Cidade> Query(string terms)
        {
            return _cidadeRepository.Query(terms);
        }
    }
}
