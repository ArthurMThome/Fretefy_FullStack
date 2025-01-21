using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Fretefy.Test.WebApi.Controllers
{
    [Route("api/cidade")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private readonly ICidadeService _cidadeService;

        public CidadeController(ICidadeService cidadeService)
        {
            _cidadeService = cidadeService;
        }

        [HttpGet]
        public IActionResult List([FromQuery] string uf, [FromQuery] string terms)
        {
            IEnumerable<Cidade> cidades;

            if (!string.IsNullOrEmpty(terms))
                cidades = _cidadeService.Query(terms);
            else if (!string.IsNullOrEmpty(uf))
                cidades = _cidadeService.ListByUf(uf);
            else
                cidades = _cidadeService.List();

            return Ok(cidades);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var cidades = _cidadeService.Get(id);
            return Ok(cidades);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Cidade cidade)
        {
            var result = _cidadeService.AdicionarCidade(cidade);

            if(result.Status == System.Net.HttpStatusCode.OK)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
