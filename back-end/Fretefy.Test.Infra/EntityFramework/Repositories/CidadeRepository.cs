using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Entities.Auxiliar;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            var result = _dbSet.Add(cidade);
            int registrosAfetados = _context.SaveChanges();
            if (registrosAfetados > 0)
                return new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.OK, Message = "Registro adicionado com sucesso!", Obj = cidade };
            else
                return new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.InternalServerError, Message = "Erro ao adicionar registro.", Obj = cidade };
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
