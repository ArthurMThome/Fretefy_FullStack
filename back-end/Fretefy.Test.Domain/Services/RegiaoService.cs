using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Entities.Auxiliar;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Fretefy.Test.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fretefy.Test.Domain.Services
{
    public class RegiaoService : IRegiaoService
    {
        private readonly IRegiaoRepository _regiaoRepository;
        private readonly ICidadeService _cidadeService;

        public RegiaoService(
            IRegiaoRepository regiaoRepository,
            ICidadeService cidadeService
            )
        {
            _regiaoRepository = regiaoRepository;
            _cidadeService = cidadeService;
        }


        public DefaultReturn<Regiao> AdicionarRegiao(Regiao regiao)
        {
            try
            {
                if (string.IsNullOrEmpty(regiao.Nome))
                    return new DefaultReturn<Regiao> { Status = System.Net.HttpStatusCode.BadRequest, Message = "Nome da região está em branco.", Obj = regiao };

                if (!regiao.Cidades.Any())
                    return new DefaultReturn<Regiao> { Status = System.Net.HttpStatusCode.BadRequest, Message = "Não é possivel cadastrar região sem uma cidade.", Obj = regiao };

                var resultCheck = _regiaoRepository.ListarPorNome(regiao.Nome, false);
                if (resultCheck.Status == System.Net.HttpStatusCode.OK && resultCheck.Obj.Any())
                    return new DefaultReturn<Regiao> { Status = System.Net.HttpStatusCode.BadRequest, Message = "Já existe uma região com esse nome cadastrada.", Obj = regiao };

                var newRegiao = new Regiao();
                newRegiao.Id = Guid.NewGuid();
                newRegiao.Nome = regiao.Nome;
                newRegiao.Status = regiao.Status;

                var resultAdd = _regiaoRepository.AdicionarRegiao(newRegiao);

                if (resultAdd.Status != System.Net.HttpStatusCode.OK)
                    return resultAdd;

                var cidades = new List<Cidade>();

                foreach (var city in regiao.Cidades)
                {
                    city.RegiaoId = newRegiao.Id;
                    var resultUpdate = _cidadeService.Update(city);
                    if(resultUpdate.Status != System.Net.HttpStatusCode.OK)
                        return new DefaultReturn<Regiao> { Status = System.Net.HttpStatusCode.InternalServerError, Message = "Erro ao vincular cidade.", Obj = regiao };

                    city.Regiao = null;
                    cidades.Add(city);
                }

                newRegiao.Cidades = cidades;
                return new DefaultReturn<Regiao> { Status = System.Net.HttpStatusCode.OK, Obj = newRegiao };
            }
            catch (Exception ex)
            {
                return new DefaultReturn<Regiao> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message, Obj = regiao };
            }
        }

        public DefaultReturn<Regiao> Update(Regiao regiao)
        {
            try
            {
                if (regiao.Id == null || regiao.Id == Guid.Empty)
                    return new DefaultReturn<Regiao> { Status = System.Net.HttpStatusCode.BadRequest, Message = "Id está null.", Obj = regiao };

                return _regiaoRepository.Update(regiao);
            }
            catch (Exception ex)
            {
                return new DefaultReturn<Regiao> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message, Obj = regiao };
            }
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
