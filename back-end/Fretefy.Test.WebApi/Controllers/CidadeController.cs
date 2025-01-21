using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Entities.Auxiliar;
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
                DefaultReturn<IEnumerable<Cidade>> result;

                if (!string.IsNullOrEmpty(terms))
                    result = _cidadeService.Query(terms);
                else if (!string.IsNullOrEmpty(uf))
                    result = _cidadeService.ListByUf(uf);
                else
                    result = _cidadeService.Listar();

                if (result.Status == System.Net.HttpStatusCode.OK)
                    return Ok(result);

                return StatusCode(result.Status.GetHashCode(), result);
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
                if (result.Status == System.Net.HttpStatusCode.OK)
                    return Ok(result);

                return StatusCode(result.Status.GetHashCode(), result);
            }
            catch (Exception ex)
            {
                return StatusCode(System.Net.HttpStatusCode.InternalServerError.GetHashCode(), new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message, Obj = new Cidade { Id = id } });
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Cidade cidade)
        {
            try
            {
                var result = _cidadeService.AdicionarCidade(cidade);

                if (result.Status == System.Net.HttpStatusCode.OK)
                    return Ok(result);

                return StatusCode(result.Status.GetHashCode(), result);
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

                if (result.Status == System.Net.HttpStatusCode.OK)
                    return Ok(result);

                return StatusCode(result.Status.GetHashCode(), result);
            }
            catch (Exception ex)
            {
                return StatusCode(System.Net.HttpStatusCode.InternalServerError.GetHashCode(), new DefaultReturn<Cidade> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message, Obj = cidade });
            }
        }
    }
}