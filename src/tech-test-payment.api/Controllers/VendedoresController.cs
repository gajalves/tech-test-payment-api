using Microsoft.AspNetCore.Mvc;
using tech_test_payment.application.Interfaces;

namespace tech_test_payment.api.Controllers;

[Route("api/vendedores")]
[ApiController]
public class VendedoresController : ControllerBase
{
    private readonly IVendedorService _vendedorService;

    public VendedoresController(IVendedorService vendedorService)
    {
        _vendedorService = vendedorService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var vendedores = await _vendedorService.ListarTodosOsVendedores();

        return Ok(vendedores);
    }
}
