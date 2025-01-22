using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Entities.Auxiliar;
using Fretefy.Test.Domain.Entities.Dto;
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
                if (resultCheck.Status == System.Net.HttpStatusCode.UnprocessableEntity)
                    return new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.UnprocessableEntity, Message = "Já existe uma cidade com esse nome cadastrada nesse estado.", Obj = cidade };

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

        public DefaultReturn<IEnumerable<CidadeDto>> Listar()
        {
            try
            {
                var result = _cidadeRepository.Listar();
                if (result.Status == System.Net.HttpStatusCode.OK)
                {
                    var returnData = result.Obj.Select(Convert);
                    return new DefaultReturn<IEnumerable<CidadeDto>> { Status = result.Status, Message = result.Message, Obj = returnData };
                }

                return new DefaultReturn<IEnumerable<CidadeDto>> { Status = result.Status, Message = result.Message };
            }
            catch (Exception ex)
            {
                return new DefaultReturn<IEnumerable<CidadeDto>> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message };
            }
        }

        public DefaultReturn<IEnumerable<CidadeDto>> ObterPorRegiaoId(Guid id)
        {
            try
            {
                var result = _cidadeRepository.ObterPorRegiaoId(id);
                if (result.Status == System.Net.HttpStatusCode.OK)
                {
                    var returnData = result.Obj.Select(Convert);
                    return new DefaultReturn<IEnumerable<CidadeDto>> { Status = result.Status, Message = result.Message, Obj = returnData };
                }

                return new DefaultReturn<IEnumerable<CidadeDto>> { Status = result.Status, Message = result.Message };
            }
            catch (Exception ex)
            {
                return new DefaultReturn<IEnumerable<CidadeDto>> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message };
            }
        }

        public DefaultReturn<IEnumerable<CidadeDto>> ObterPorRegiaoNull()
        {
            try
            {
                var result = _cidadeRepository.ObterPorRegiaoNull();
                if (result.Status == System.Net.HttpStatusCode.OK)
                {
                    var returnData = result.Obj.Select(Convert);
                    return new DefaultReturn<IEnumerable<CidadeDto>> { Status = result.Status, Message = result.Message, Obj = returnData };
                }

                return new DefaultReturn<IEnumerable<CidadeDto>> { Status = result.Status, Message = result.Message };
            }
            catch (Exception ex)
            {
                return new DefaultReturn<IEnumerable<CidadeDto>> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message };
            }
        }

        public DefaultReturn<IEnumerable<CidadeDto>> ListByUf(string uf)
        {
            try
            {
                var result = _cidadeRepository.ListByUf(uf);
                if (result.Status == System.Net.HttpStatusCode.OK)
                {
                    var returnData = result.Obj.Select(Convert);
                    return new DefaultReturn<IEnumerable<CidadeDto>> { Status = result.Status, Message = result.Message, Obj = returnData };
                }

                return new DefaultReturn<IEnumerable<CidadeDto>> { Status = result.Status, Message = result.Message };
            }
            catch (Exception ex)
            {
                return new DefaultReturn<IEnumerable<CidadeDto>> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message, Obj = new List<CidadeDto> { new CidadeDto { UF = uf } } };
            }
        }

        public DefaultReturn<IEnumerable<CidadeDto>> Query(string terms)
        {
            try
            {
                var result = _cidadeRepository.Query(terms);
                if (result.Status == System.Net.HttpStatusCode.OK)
                {
                    var returnData = result.Obj.Select(Convert);
                    return new DefaultReturn<IEnumerable<CidadeDto>> { Status = result.Status, Message = result.Message, Obj = returnData };
                }

                return new DefaultReturn<IEnumerable<CidadeDto>> { Status = result.Status, Message = result.Message };
            }
            catch (Exception ex)
            {
                return new DefaultReturn<IEnumerable<CidadeDto>> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message, Obj = new List<CidadeDto> { new CidadeDto { Nome = terms } } };
            }
        }

        private CidadeDto Convert(Cidade obj)
        {
            return new CidadeDto
            {
                Id = obj.Id,
                Nome = obj.Nome,
                UF = obj.UF,
                RegiaoId = obj.RegiaoId,
                RegiaoNome = obj.Regiao?.Nome
            };
        }
    }
}
