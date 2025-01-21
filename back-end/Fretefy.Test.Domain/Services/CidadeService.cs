using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Entities.Auxiliar;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Fretefy.Test.Domain.Interfaces.Services;
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
            try
            {
                if (string.IsNullOrEmpty(cidade.Nome))
                    return new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.BadRequest, Message = "Nome da cidade está em branco.", Obj = cidade };

                if (string.IsNullOrEmpty(cidade.UF))
                    return new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.BadRequest, Message = "Estado da cidade está em branco.", Obj = cidade };

                var resultCheck = _cidadeRepository.VerificarCidadeExistente(cidade.Nome, cidade.UF);
                if (resultCheck.Status == System.Net.HttpStatusCode.OK && resultCheck.Obj.Any())
                    return new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.BadRequest, Message = "Já existe uma cidade com esse nome cadastrada nesse estado.", Obj = cidade };

                cidade.Id = Guid.NewGuid();

                return _cidadeRepository.AdicionarCidade(cidade);
            }
            catch (Exception ex)
            {
                return new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message, Obj = cidade };
            }
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

        public DefaultReturn<Cidade> ObterPorId(Guid id)
        {
            try
            {
                return _cidadeRepository.ObterPorId(id);
            }
            catch (Exception ex)
            {
                return new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message, Obj = new Cidade { Id = id } };
            }
        }

        public DefaultReturn<IEnumerable<Cidade>> Listar()
        {
            try
            {
                return _cidadeRepository.Listar();
            }
            catch (Exception ex)
            {
                return new DefaultReturn<IEnumerable<Cidade>> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message };
            }
        }

        public DefaultReturn<IEnumerable<Cidade>> ListByUf(string uf)
        {
            try
            {
                return _cidadeRepository.ListByUf(uf);
            }
            catch (Exception ex)
            {
                return new DefaultReturn<IEnumerable<Cidade>> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message, Obj = new List<Cidade> { new Cidade { UF = uf } } };
            }
        }

        public DefaultReturn<IEnumerable<Cidade>> Query(string terms)
        {
            try
            {
                return _cidadeRepository.Query(terms);
            }
            catch (Exception ex)
            {
                return new DefaultReturn<IEnumerable<Cidade>> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message, Obj = new List<Cidade> { new Cidade { Nome = terms } } };
            }
        }
    }
}
