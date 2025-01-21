using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Entities.Auxiliar;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Fretefy.Test.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Fretefy.Test.Domain.Services
{
    public class RegiaoService : IRegiaoService
    {
        private readonly IRegiaoRepository _regiaoRepository;

        public RegiaoService(IRegiaoRepository regiaoRepository)
        {
            _regiaoRepository = regiaoRepository;
        }

        public DefaultReturn<Regiao> ObterPorId(Guid id)
        {
            try
            {
                return _regiaoRepository.ObterPorId(id);
            }
            catch (Exception ex)
            {
                return new DefaultReturn<Regiao> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message, Obj = new Regiao { Id = id } };
            }
        }

        public DefaultReturn<IEnumerable<Regiao>> Listar()
        {
            try
            {
                return _regiaoRepository.Listar();
            }
            catch (Exception ex)
            {
                return new DefaultReturn<IEnumerable<Regiao>> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message };
            }
        }

        public DefaultReturn<IEnumerable<Regiao>> ListarPorCidade(string cidade)
        {
            try
            {
                return _regiaoRepository.ListarPorCidade(cidade);
            }
            catch (Exception ex)
            {
                return new DefaultReturn<IEnumerable<Regiao>> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message };
            }
        }

        public DefaultReturn<IEnumerable<Regiao>> ListarPorNome(string nome)
        {
            try
            {
                return _regiaoRepository.ListarPorNome(nome);
            }
            catch (Exception ex)
            {
                return new DefaultReturn<IEnumerable<Regiao>> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message, Obj = new List<Regiao> { new Regiao { Nome = nome } } };
            }
        }

        public DefaultReturn<Regiao> ChangeStatus(Regiao regiao)
        {
            try
            {
                if (regiao.Id == null || regiao.Id == Guid.Empty)
                    return new DefaultReturn<Regiao> { Status = System.Net.HttpStatusCode.BadRequest, Message = "Id está null." };

                if (regiao.Status == 1)
                    regiao.Status = 2;
                else
                    regiao.Status = 1;

                return _regiaoRepository.Update(regiao);
            }
            catch (Exception ex)
            {
                return new DefaultReturn<Regiao> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message, Obj = regiao };
            }
        }
    }
}
