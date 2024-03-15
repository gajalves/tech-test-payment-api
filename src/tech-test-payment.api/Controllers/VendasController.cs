using Microsoft.AspNetCore.Mvc;
using tech_test_payment.application.Dtos;
using tech_test_payment.application.Interfaces;

namespace tech_test_payment.api.Controllers
{
    [Route("api/vendas")]
    [ApiController]
    public class VendasController : ControllerBase
    {
        private readonly IObterVendaService _obterVendaService;
        private readonly ICriarVendaService _criarVendaService;
        private readonly IAtualizarStatusVendaService _atualizarStatusVendaService;

        public VendasController(IObterVendaService obterVendaService, 
                                ICriarVendaService criarVendaService, 
                                IAtualizarStatusVendaService atualizarStatusVendaService)
        {
            _obterVendaService = obterVendaService;
            _criarVendaService = criarVendaService;
            _atualizarStatusVendaService = atualizarStatusVendaService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegistrarVendaDto dto)
        {
            var result = await _criarVendaService.RegistrarVenda(dto);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("{id}")]        
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _obterVendaService.ObterVendaPorId(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchStatus(Guid id, AtualizarStatusVendaDto dto)
        {
            var result = await _atualizarStatusVendaService.AtualizarStatusVenda(id, dto);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
    }
}
