using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Entities.Auxiliar;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fretefy.Test.Infra.EntityFramework.Repositories
{
    public class RegiaoRepository : IRegiaoRepository
    {
        private DbSet<Regiao> _dbSet;
        private readonly DbContext _context;

        public RegiaoRepository(DbContext dbContext)
        {
            _dbSet = dbContext.Set<Regiao>();
            _context = dbContext;
        }

        public DefaultReturn<Regiao> AdicionarRegiao(Regiao regiao)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _dbSet.Add(regiao);
                    int registrosAfetados = _context.SaveChanges();
                    if (registrosAfetados > 0)
                    {
                        transaction.Commit();
                        return new DefaultReturn<Regiao> { Status = System.Net.HttpStatusCode.OK, Message = "Região adicionada com sucesso!", Obj = regiao };
                    }
                    else
                        return new DefaultReturn<Regiao> { Status = System.Net.HttpStatusCode.InternalServerError, Message = "Erro ao adicionar região.", Obj = regiao };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new DefaultReturn<Regiao> { Status = System.Net.HttpStatusCode.InternalServerError, Message = $"Erro ao adicionar região | Exception - {ex.Message}.", Obj = regiao };
                }
            }
        }

        public DefaultReturn<Regiao> Update(Regiao regiao)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var result = _dbSet.Update(regiao);
                    int registrosAfetados = _context.SaveChanges();
                    if (registrosAfetados > 0)
                    {
                        transaction.Commit();
                        return new DefaultReturn<Regiao> { Status = System.Net.HttpStatusCode.OK, Message = "Região alterada com sucesso!", Obj = regiao };
                    }
                    else
                        return new DefaultReturn<Regiao> { Status = System.Net.HttpStatusCode.InternalServerError, Message = "Erro ao atualizar região.", Obj = regiao };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new DefaultReturn<Regiao> { Status = System.Net.HttpStatusCode.InternalServerError, Message = $"Erro ao atualizar região | Exception - {ex.Message}.", Obj = regiao };
                }
            }
        }

        public DefaultReturn<IEnumerable<Regiao>> Listar()
        {
            try
            {
                return
                    new DefaultReturn<IEnumerable<Regiao>>
                    {
                        Status = System.Net.HttpStatusCode.OK,
                        Obj = _dbSet.AsQueryable().Include(r => r.Cidades).ToList().Select(r => new Regiao
                        {
                            Id = r.Id,
                            Nome = r.Nome,
                            Status = r.Status,
                            Cidades = r.Cidades.Select(c => new Cidade { Id = c.Id, Nome = c.Nome, UF = c.UF, RegiaoId = c.RegiaoId }).ToList()
                        }).ToList()
                    };
            }
            catch (Exception ex)
            {
                return new DefaultReturn<IEnumerable<Regiao>> { Status = System.Net.HttpStatusCode.InternalServerError, Message = $"Erro ao obter Regiões. | Exception - {ex.Message}." };
            }
        }

        public DefaultReturn<Regiao> ObterPorId(Guid id)
        {
            try
            {
                var result = Listar();

                if (result.Status != System.Net.HttpStatusCode.OK)
                    return new DefaultReturn<Regiao> { Status = System.Net.HttpStatusCode.InternalServerError, Message = "Erro ao obter regiões." };

                var regiao = result.Obj.Where(x => x.Id == id).FirstOrDefault();
                if (regiao == null)
                    return new DefaultReturn<Regiao> { Status = System.Net.HttpStatusCode.NotFound, Message = "Nenhuma região com esse id foi encontrada." };

                return new DefaultReturn<Regiao> { Status = System.Net.HttpStatusCode.OK, Obj = regiao };
            }
            catch (Exception ex)
            {
                return new DefaultReturn<Regiao> { Status = System.Net.HttpStatusCode.InternalServerError, Message = $"Erro ao procurar regiões | Exception - {ex.Message}." };
            }
        }

        public DefaultReturn<IEnumerable<Regiao>> ListarPorNome(string nome, bool beLike = true)
        {
            try
            {
                var result = Listar();

                if (result.Status != System.Net.HttpStatusCode.OK)
                    return new DefaultReturn<IEnumerable<Regiao>> { Status = System.Net.HttpStatusCode.InternalServerError, Message = "Erro ao obter regiões." };

                IEnumerable<Regiao> regioes;

                if(beLike)
                    regioes = result.Obj.Where(x => x.Nome.ToLower().Contains(nome.ToLower()));
                else
                    regioes = result.Obj.Where(x => x.Nome.ToLower() == nome.ToLower());

                if (regioes == null)
                    return new DefaultReturn<IEnumerable<Regiao>> { Status = System.Net.HttpStatusCode.NotFound, Message = "Nenhuma região com esse nome foi encontrada." };

                return new DefaultReturn<IEnumerable<Regiao>> { Status = System.Net.HttpStatusCode.OK, Obj = regioes };
            }
            catch (Exception ex)
            {
                return new DefaultReturn<IEnumerable<Regiao>> { Status = System.Net.HttpStatusCode.InternalServerError, Message = $"Erro ao procurar regiões | Exception - {ex.Message}." };
            }
        }

        public DefaultReturn<IEnumerable<Regiao>> ListarPorCidade(string cidade)
        {
            try
            {
                var result = Listar();

                if (result.Status != System.Net.HttpStatusCode.OK)
                    return new DefaultReturn<IEnumerable<Regiao>> { Status = System.Net.HttpStatusCode.InternalServerError, Message = "Erro ao obter regiões." };

                var regioes = result.Obj.Where(x => x.Cidades.Any(z => z.Nome.ToLower().Contains(cidade.ToLower())));
                if (regioes == null)
                    return new DefaultReturn<IEnumerable<Regiao>> { Status = System.Net.HttpStatusCode.NotFound, Message = "Nenhuma região com essa cidade foi encontrada." };

                return new DefaultReturn<IEnumerable<Regiao>> { Status = System.Net.HttpStatusCode.OK, Obj = regioes };
            }
            catch (Exception ex)
            {
                return new DefaultReturn<IEnumerable<Regiao>> { Status = System.Net.HttpStatusCode.InternalServerError, Message = $"Erro ao procurar regiões | Exception - {ex.Message}." };
            }
        }
    }
}