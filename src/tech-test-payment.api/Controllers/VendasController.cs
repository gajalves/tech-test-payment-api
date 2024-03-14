using Microsoft.AspNetCore.Mvc;
using tech_test_payment.application.Dtos;
using tech_test_payment.application.Interfaces;

namespace tech_test_payment.api.Controllers
{
    [Route("api/vendas")]
    [ApiController]
    public class VendasController : ControllerBase
    {
        private readonly IVendaService _vendaService;

        public VendasController(IVendaService vendaService)
        {
            _vendaService = vendaService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegistrarVendaDto dto)
        {
            var result = await _vendaService.RegistrarVenda(dto);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("{id}")]        
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _vendaService.ObterVendaPorId(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchStatus(Guid id, AtualizarStatusVendaDto dto)
        {
            var result = await _vendaService.AtualizarStatusVenda(id, dto);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
    }
}
