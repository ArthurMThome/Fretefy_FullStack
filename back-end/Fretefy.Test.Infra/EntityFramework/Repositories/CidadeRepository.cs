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

        public IQueryable<Cidade> List()
        {
            return _dbSet.AsQueryable();
        }

        public IEnumerable<Cidade> ListByUf(string uf)
        {
            return _dbSet.Where(w => EF.Functions.Like(w.UF, $"%{uf}%"));
        }

        public IEnumerable<Cidade> Query(string terms)
        {
            return _dbSet.Where(w => EF.Functions.Like(w.Nome, $"%{terms}%") || EF.Functions.Like(w.UF, $"%{terms}%"));
        }
        public IEnumerable<Cidade> VerificarCidadeExistente(string nome, string uf)
        {
            return _dbSet.Where(w => EF.Functions.Like(w.Nome, $"%{nome}%") && EF.Functions.Like(w.UF, $"%{uf}%"));
        }
    }
}
