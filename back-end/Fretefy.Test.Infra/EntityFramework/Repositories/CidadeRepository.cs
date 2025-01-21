using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Entities.Auxiliar;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fretefy.Test.Infra.EntityFramework.Repositories
{
    public class CidadeRepository : ICidadeRepository
    {
        private DbSet<Cidade> _dbSet;
        private readonly DbContext _context;

        public CidadeRepository(DbContext dbContext)
        {
            _dbSet = dbContext.Set<Cidade>();
            _context = dbContext;
        }

        public DefaultReturn<Cidade> AdicionarCidade(Cidade cidade)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _dbSet.Add(cidade);
                    int registrosAfetados = _context.SaveChanges();
                    if (registrosAfetados > 0)
                    {
                        transaction.Commit();
                        return new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.OK, Message = "Cidade adicionada com sucesso!", Obj = cidade };
                    }
                    else
                        return new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.InternalServerError, Message = "Erro ao adicionar cidade.", Obj = cidade };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.InternalServerError, Message = $"Erro ao adicionar cidade | Exception - {ex.Message}.", Obj = cidade };
                }
            }
        }

        public DefaultReturn<Cidade> Update(Cidade cidade)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var result = _dbSet.Update(cidade);
                    int registrosAfetados = _context.SaveChanges();
                    if (registrosAfetados > 0)
                    {
                        transaction.Commit();
                        return new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.OK, Message = "Cidade alterada com sucesso!", Obj = cidade };
                    }
                    else
                        return new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.InternalServerError, Message = "Erro ao atualizar cidade.", Obj = cidade };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.InternalServerError, Message = $"Erro ao atualizar cidade | Exception - {ex.Message}.", Obj = cidade };
                }
            }
        }

        public DefaultReturn<IEnumerable<Cidade>> Listar()
        {
            try
            {
                return new DefaultReturn<IEnumerable<Cidade>> { Status = System.Net.HttpStatusCode.OK, Obj = _dbSet.AsQueryable() };
            }
            catch (Exception ex)
            {
                return new DefaultReturn<IEnumerable<Cidade>> { Status = System.Net.HttpStatusCode.InternalServerError, Message = $"Erro ao atualizar cidade | Exception - {ex.Message}." };
            }
        }

        public DefaultReturn<Cidade> ObterPorId(Guid id)
        {
            try
            {
                var result = Listar();

                if(result.Status != System.Net.HttpStatusCode.OK)
                    return new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.InternalServerError, Message = "Erro ao obter cidades." };
                
                var cidade = result.Obj.Where(x => x.Id == id).FirstOrDefault();
                if(cidade == null)
                    return new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.NotFound, Message = "Nenhuma cidade com esse id foi encontrada." };                
                
                return new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.OK, Obj = cidade };
            }
            catch (Exception ex)
            {
                return new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.InternalServerError, Message = $"Erro ao procurar cidades | Exception - {ex.Message}." };
            }
        }

        public DefaultReturn<IEnumerable<Cidade>> ListByUf(string uf)
        {
            try
            {
                var result = Listar();

                if (result.Status != System.Net.HttpStatusCode.OK)
                    return new DefaultReturn<IEnumerable<Cidade>> { Status = System.Net.HttpStatusCode.InternalServerError, Message = "Erro ao obter cidades." };

                var cidades = result.Obj.Where(x => x.UF.ToLower() == uf.ToLower());
                if (!cidades.Any())
                    return new DefaultReturn<IEnumerable<Cidade>> { Status = System.Net.HttpStatusCode.NotFound, Message = "Nenhuma cidade com esse UF encontrada." };

                return new DefaultReturn<IEnumerable<Cidade>> { Status = System.Net.HttpStatusCode.OK, Obj = cidades };
            }
            catch (Exception ex)
            {
                return new DefaultReturn<IEnumerable<Cidade>> { Status = System.Net.HttpStatusCode.InternalServerError, Message = $"Erro ao procurar cidades | Exception - {ex.Message}." };
            }
        }

        public DefaultReturn<IEnumerable<Cidade>> Query(string terms)
        {
            try
            {
                var result = Listar();

                if (result.Status != System.Net.HttpStatusCode.OK)
                    return new DefaultReturn<IEnumerable<Cidade>> { Status = System.Net.HttpStatusCode.InternalServerError, Message = "Erro ao obter cidades." };

                var cidades = result.Obj.Where(x => x.Nome.ToLower() == terms.ToLower());
                if (!cidades.Any())
                    return new DefaultReturn<IEnumerable<Cidade>> { Status = System.Net.HttpStatusCode.NotFound, Message = "Nenhuma cidade com esse nome encontrada." };

                return new DefaultReturn<IEnumerable<Cidade>> { Status = System.Net.HttpStatusCode.OK, Obj = cidades };
            }
            catch (Exception ex)
            {
                return new DefaultReturn<IEnumerable<Cidade>> { Status = System.Net.HttpStatusCode.InternalServerError, Message = $"Erro ao atualizar cidade | Exception - {ex.Message}." };
            }
        }

        public DefaultReturn<IEnumerable<Cidade>> VerificarCidadeExistente(string nome, string uf)
        {
            try
            {
                var result = Listar();

                if (result.Status != System.Net.HttpStatusCode.OK)
                    return new DefaultReturn<IEnumerable<Cidade>> { Status = System.Net.HttpStatusCode.InternalServerError, Message = "Erro ao obter cidades." };

                var cidades = result.Obj.Where(x => x.Nome.ToLower() == nome.ToLower() && x.UF.ToLower() == uf.ToLower());
                if (cidades.Any())
                    return new DefaultReturn<IEnumerable<Cidade>> { Status = System.Net.HttpStatusCode.UnprocessableEntity, Message = "Já existe uma cidade com esse nome cadastrada nesse UF." };

                return new DefaultReturn<IEnumerable<Cidade>> { Status = System.Net.HttpStatusCode.OK, Obj = cidades };
            }
            catch (Exception ex)
            {
                return new DefaultReturn<IEnumerable<Cidade>> { Status = System.Net.HttpStatusCode.InternalServerError, Message = $"Erro ao atualizar cidade | Exception - {ex.Message}." };
            }
        }
    }
}
