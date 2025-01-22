using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Entities.Auxiliar;
using Fretefy.Test.Domain.Entities.Dto;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Fretefy.Test.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

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

        public DefaultReturn<RegiaoDto> Update(Regiao regiao)
        {
            try
            {
                if (regiao.Id == null || regiao.Id == Guid.Empty)
                    return new DefaultReturn<RegiaoDto> { Status = System.Net.HttpStatusCode.BadRequest, Message = "Id está null." };

                if(regiao.Cidades.GroupBy(p => p.Id).Any(g => g.Count() > 1))
                    return new DefaultReturn<RegiaoDto> { Status = System.Net.HttpStatusCode.BadRequest, Message = "Existe cidade duplicada." };

                var newCidades = regiao.Cidades.Select(x => x.Id).ToList();

                var result = _regiaoRepository.Update(regiao);
                if (result.Status == System.Net.HttpStatusCode.OK)
                {
                    var resultList = _cidadeService.ObterPorRegiaoId(regiao.Id);
                    if(resultList.Status == System.Net.HttpStatusCode.OK)
                    {
                        foreach (var cidade in resultList.Obj)
                        {
                            if (!newCidades.Contains(cidade.Id))
                            {
                                var newcidade = new Cidade() 
                                { 
                                    Id = cidade.Id,
                                    Nome = cidade.Nome,
                                    UF = cidade.UF,
                                    RegiaoId = null
                                };

                                var resultUpdate = _cidadeService.Update(newcidade);
                                if(resultUpdate.Status != System.Net.HttpStatusCode.OK)
                                    return new DefaultReturn<RegiaoDto> { Status = resultUpdate.Status, Message = resultUpdate.Message };
                            }
                        }
                    }

                    var returnData = Convert(result.Obj);
                    return new DefaultReturn<RegiaoDto> { Status = result.Status, Message = result.Message, Obj = returnData };
                }

                return new DefaultReturn<RegiaoDto> { Status = result.Status, Message = result.Message };
            }
            catch (Exception ex)
            {
                return new DefaultReturn<RegiaoDto> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message };
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

        public DefaultReturn<IEnumerable<RegiaoDto>> Listar()
        {
            try
            {
                var result = _regiaoRepository.Listar();
                if(result.Status == System.Net.HttpStatusCode.OK)
                {
                    var returnData = result.Obj.Select(Convert);
                    return new DefaultReturn<IEnumerable<RegiaoDto>> { Status = result.Status, Message = result.Message, Obj= returnData };
                }

                return new DefaultReturn<IEnumerable<RegiaoDto>> { Status = result.Status, Message = result.Message };
            }
            catch (Exception ex)
            {
                return new DefaultReturn<IEnumerable<RegiaoDto>> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message };
            }
        }

        public DefaultReturn<IEnumerable<RegiaoDto>> ListarPorCidade(string cidade)
        {
            try
            {
                var result = _regiaoRepository.ListarPorCidade(cidade);
                if (result.Status == System.Net.HttpStatusCode.OK)
                {
                    var returnData = result.Obj.Select(Convert);
                    return new DefaultReturn<IEnumerable<RegiaoDto>> { Status = result.Status, Message = result.Message, Obj = returnData };
                }

                return new DefaultReturn<IEnumerable<RegiaoDto>> { Status = result.Status, Message = result.Message };
            }
            catch (Exception ex)
            {
                return new DefaultReturn<IEnumerable<RegiaoDto>> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message };
            }
        }

        public DefaultReturn<IEnumerable<RegiaoDto>> ListarPorNome(string nome)
        {
            try
            {
                var result = _regiaoRepository.ListarPorNome(nome);
                if (result.Status == System.Net.HttpStatusCode.OK)
                {
                    var returnData = result.Obj.Select(Convert);
                    return new DefaultReturn<IEnumerable<RegiaoDto>> { Status = result.Status, Message = result.Message, Obj = returnData };
                }

                return new DefaultReturn<IEnumerable<RegiaoDto>> { Status = result.Status, Message = result.Message };
            }
            catch (Exception ex)
            {
                return new DefaultReturn<IEnumerable<RegiaoDto>> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message, Obj = new List<RegiaoDto> { new RegiaoDto { Nome = nome } } };
            }
        }

        public DefaultReturn<RegiaoDto> ChangeStatus(Regiao regiao)
        {
            try
            {
                if (regiao.Id == null || regiao.Id == Guid.Empty)
                    return new DefaultReturn<RegiaoDto> { Status = System.Net.HttpStatusCode.BadRequest, Message = "Id está null." };

                if (regiao.Status == 1)
                    regiao.Status = 2;
                else
                    regiao.Status = 1;

                var result = _regiaoRepository.Update(regiao);
                if (result.Status == System.Net.HttpStatusCode.OK)
                {
                    var returnData = Convert(result.Obj);
                    return new DefaultReturn<RegiaoDto> { Status = result.Status, Message = result.Message, Obj = returnData };
                }

                return new DefaultReturn<RegiaoDto> { Status = result.Status, Message = result.Message };
            }
            catch (Exception ex)
            {
                return new DefaultReturn<RegiaoDto> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message };
            }
        }

        private RegiaoDto Convert(Regiao obj)
        {
            return new RegiaoDto
            {
                Id = obj.Id,
                Status = obj.Status,
                Nome = obj.Nome,
                Cidades = obj.Cidades.Select(x => new CidadeDto
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    UF = x.UF,
                    RegiaoId = x.RegiaoId
                })
            };
        }
    }
}
