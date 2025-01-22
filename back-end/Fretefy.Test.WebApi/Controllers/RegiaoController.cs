using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Entities.Auxiliar;
using Fretefy.Test.Domain.Entities.Dto;
using Fretefy.Test.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;

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
                DefaultReturn<IEnumerable<RegiaoDto>> result;

                if (!string.IsNullOrEmpty(cidade))
                    result = _regiaoService.ListarPorCidade(cidade);
                else if (!string.IsNullOrEmpty(nome))
                    result = _regiaoService.ListarPorNome(nome);
                else
                    result = _regiaoService.Listar();

                return Ok(result);
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
                return Ok(result);
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
                return Ok(result);
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
                return Ok(result);
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
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(System.Net.HttpStatusCode.InternalServerError.GetHashCode(), new DefaultReturn<Regiao> { Status = System.Net.HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }

        [HttpGet("exportar")]
        public IActionResult ExportarParaExcel()
        {
            var result = _regiaoService.Listar();
            if(result.Status != System.Net.HttpStatusCode.OK)
                return StatusCode(result.Status.GetHashCode(), result);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Regiões");
                worksheet.Cells["A1"].LoadFromCollection(result.Obj);

                using (var memoryStream = new MemoryStream())
                {
                    package.SaveAs(memoryStream);

                    var responseStream = new MemoryStream();
                    memoryStream.CopyTo(responseStream);

                    responseStream.Position = 0;
                    return File(responseStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "regioes.xlsx");
                }
            }
        }
    }
}