using Fretefy.Test.Domain.Entities;
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

        public RegiaoRepository(DbContext dbContext)
        {
            _dbSet = dbContext.Set<Regiao>();
        }

        public IQueryable<Regiao> ObterPorId(Guid id)
        {
            return _dbSet.Where(w => EF.Functions.Like(w.Id.ToString(), $"%{id}%"));
        }
        public IEnumerable<Regiao> Listar()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<Regiao> ListarPorNome(string nome)
        {
            return _dbSet.Where(w => EF.Functions.Like(w.Nome, $"%{nome}%"));
        }

        public IEnumerable<Regiao> ListarPorCidade(string cidade)
        {
            return _dbSet.Where(w => EF.Functions.Like(w.Cidades.Select(x => x.Nome).ToString(), $"%{cidade}%"));
        }
    }
}