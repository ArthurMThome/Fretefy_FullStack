using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Entities.Filters;
using Fretefy.Test.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;

namespace Fretefy.Test.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegiaoController : ControllerBase
    {
        private readonly IRegiaoService _regiaoService;

        public RegiaoController(IRegiaoService regiaoService)
        {
            _regiaoService = regiaoService;
        }

        [HttpGet]
        public IActionResult List([FromBody] RegiaoFilter filter)
        {
            if (filter.Id.HasValue)
                return Ok(_regiaoService.ObterPorId(filter.Id.Value));

            IEnumerable<Regiao> regioes;

            if (!string.IsNullOrEmpty(filter.Cidade))
                regioes = _regiaoService.ListarPorCidade(filter.Cidade);
            else if (!string.IsNullOrEmpty(filter.Nome))
                regioes = _regiaoService.ListarPorNome(filter.Nome);
            else
                regioes = _regiaoService.Listar();

            return Ok(regioes);
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

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    _regiaoService.Delete(id);
        //    return NoContent();
        //}

        [HttpGet("exportar")]
        public IActionResult ExportarParaExcel([FromBody] RegiaoFilter filter)
        {
            IEnumerable<Regiao> regioes;

            if (!string.IsNullOrEmpty(filter.Cidade))
                regioes = _regiaoService.ListarPorCidade(filter.Cidade);
            else if (!string.IsNullOrEmpty(filter.Nome))
                regioes = _regiaoService.ListarPorNome(filter.Nome);
            else
                regioes = _regiaoService.Listar();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Regiões");
                worksheet.Cells["A1"].LoadFromCollection(regioes);

                using (var memoryStream = new MemoryStream())
                {
                    package.SaveAs(memoryStream);
                    memoryStream.Position = 0;

                    return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "regioes.xlsx");
                }
            }
        }
    }
}