using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Entities.Auxiliar;
using Fretefy.Test.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Fretefy.Test.WebApi.Controllers
{
    [Route("api/regiao")]
    [ApiController]
    public class RegiaoController : ControllerBase
    {
        private readonly IRegiaoService _regiaoService;

        public RegiaoController(IRegiaoService regiaoService)
        {
            _regiaoService = regiaoService;
        }

        [HttpGet]
        public IActionResult List([FromQuery] string nome, [FromQuery] string cidade)
        {
            try
            {
                DefaultReturn<IEnumerable<Regiao>> result;

                if (!string.IsNullOrEmpty(cidade))
                    result = _regiaoService.ListarPorCidade(cidade);
                else if (!string.IsNullOrEmpty(nome))
                    result = _regiaoService.ListarPorNome(nome);
                else
                    result = _regiaoService.Listar();

                if (result.Status == System.Net.HttpStatusCode.OK)
                    return Ok(result);

                return StatusCode(result.Status.GetHashCode(), result);
            }
            catch (Exception ex)
            {
                return StatusCode(System.Net.HttpStatusCode.InternalServerError.GetHashCode(), new DefaultReturn<Regiao> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var result = _regiaoService.ObterPorId(id);

                if (result.Status == System.Net.HttpStatusCode.OK)
                    return Ok(result);

                return StatusCode(result.Status.GetHashCode(), result);
            }
            catch (Exception ex)
            {
                return StatusCode(System.Net.HttpStatusCode.InternalServerError.GetHashCode(), new DefaultReturn<Regiao> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message, Obj = new Regiao { Id = id } });
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Regiao regiao)
        {
            try
            {
                var result = _regiaoService.AdicionarRegiao(regiao);

                if (result.Status == System.Net.HttpStatusCode.OK)
                    return Ok(result);

                return StatusCode(result.Status.GetHashCode(), result);
            }
            catch (Exception ex)
            {
                return StatusCode(System.Net.HttpStatusCode.InternalServerError.GetHashCode(), new DefaultReturn<Regiao> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message, Obj = regiao });
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Regiao regiao)
        {
            try
            {
                var result = _regiaoService.Update(regiao);

                if (result.Status == System.Net.HttpStatusCode.OK)
                    return Ok(result);

                return StatusCode(result.Status.GetHashCode(), result);
            }
            catch (Exception ex)
            {
                return StatusCode(System.Net.HttpStatusCode.InternalServerError.GetHashCode(), new DefaultReturn<Regiao> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message, Obj = regiao });
            }
        }

        [HttpPut("changestatus")]
        public IActionResult ChangeStatus(Regiao regiao)
        {
            try
            {
                var result = _regiaoService.ChangeStatus(regiao);

                if (result.Status == System.Net.HttpStatusCode.OK)
                    return Ok(result);

                return StatusCode(result.Status.GetHashCode(), result);
            }
            catch (Exception ex)
            {
                return StatusCode(System.Net.HttpStatusCode.InternalServerError.GetHashCode(), new DefaultReturn<Regiao> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }

        //[HttpPost]
        //public ActionResult<Regiao> Post(Regiao regiao)
        //{
        //    _regiaoService.Create(regiao);
        //    return CreatedAtAction("Get", new { id = regiao.Id }, regiao);
        //}

        //[HttpPut("{id}")]
        //public IActionResult Put(int id, Regiao regiao)
        //{
        //    if (id != regiao.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _regiaoService.Update(regiao);

        //    return NoContent();
        //}


        //[HttpGet("exportar")]
        //public IActionResult ExportarParaExcel([FromBody] RegiaoFilter filter)
        //{
        //    IEnumerable<Regiao> regioes;

        //    if (!string.IsNullOrEmpty(filter.Cidade))
        //        regioes = _regiaoService.ListarPorCidade(filter.Cidade);
        //    else if (!string.IsNullOrEmpty(filter.Nome))
        //        regioes = _regiaoService.ListarPorNome(filter.Nome);
        //    else
        //        regioes = _regiaoService.Listar();

        //    using (var package = new ExcelPackage())
        //    {
        //        var worksheet = package.Workbook.Worksheets.Add("Regiões");
        //        worksheet.Cells["A1"].LoadFromCollection(regioes);

        //        using (var memoryStream = new MemoryStream())
        //        {
        //            package.SaveAs(memoryStream);
        //            memoryStream.Position = 0;

        //            return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "regioes.xlsx");
        //        }
        //    }
        //}
    }
}