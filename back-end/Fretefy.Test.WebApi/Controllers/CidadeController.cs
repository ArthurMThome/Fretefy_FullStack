using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Entities.Auxiliar;
using Fretefy.Test.Domain.Entities.Dto;
using Fretefy.Test.Domain.Interfaces.Services;
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
            try
            {
                DefaultReturn<IEnumerable<CidadeDto>> result;

                if (!string.IsNullOrEmpty(terms))
                    result = _cidadeService.Query(terms);
                else if (!string.IsNullOrEmpty(uf))
                    result = _cidadeService.ListByUf(uf);
                else
                    result = _cidadeService.Listar();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(System.Net.HttpStatusCode.InternalServerError.GetHashCode(), new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message, Obj = new Cidade { UF = uf, Nome = terms } });
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var result = _cidadeService.ObterPorId(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(System.Net.HttpStatusCode.InternalServerError.GetHashCode(), new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message, Obj = new Cidade { Id = id } });
            }
        }

        [HttpGet("regiao/{id}")]
        public IActionResult GetByRegiaoId(Guid id)
        {
            try
            {
                var result = _cidadeService.ObterPorRegiaoId(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(System.Net.HttpStatusCode.InternalServerError.GetHashCode(), new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message, Obj = new Cidade { Id = id } });
            }
        }

        [HttpGet("regiao")]
        public IActionResult GetRegiaoNull()
        {
            try
            {
                var result = _cidadeService.ObterPorRegiaoNull();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(System.Net.HttpStatusCode.InternalServerError.GetHashCode(), new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Cidade cidade)
        {
            try
            {
                var result = _cidadeService.AdicionarCidade(cidade);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(System.Net.HttpStatusCode.InternalServerError.GetHashCode(), new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message, Obj = cidade });
            }
        }

        public IActionResult Put([FromBody] Cidade cidade)
        {
            try
            {
                var result = _cidadeService.Update(cidade);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(System.Net.HttpStatusCode.InternalServerError.GetHashCode(), new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message, Obj = cidade });
            }
        }
    }
}