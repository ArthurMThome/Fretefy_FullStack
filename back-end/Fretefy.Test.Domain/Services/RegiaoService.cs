using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Fretefy.Test.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Fretefy.Test.Domain.Interfaces.Services;
using System.Linq;

namespace Fretefy.Test.Domain.Services
{
    public class RegiaoService : IRegiaoService
    {
        private readonly IRegiaoRepository _regiaoRepository;

        public RegiaoService(IRegiaoRepository regiaoRepository)
        {
            _regiaoRepository = regiaoRepository;
        }

        public Regiao ObterPorId(Guid id)
        {
            return _regiaoRepository.ObterPorId(id).FirstOrDefault();
        }
        public IEnumerable<Regiao> Listar()
        {
            return _regiaoRepository.Listar();
        }
        public IEnumerable<Regiao> ListarPorCidade(string cidade)
        {
            return _regiaoRepository.ListarPorCidade(cidade);
        }
        public IEnumerable<Regiao> ListarPorNome(string nome)
        {
            return _regiaoRepository.ListarPorNome(nome);
        }
    }
}
