using Microsoft.AspNetCore.Mvc;
using tech_test_payment.application.Interfaces;

namespace tech_test_payment.api.Controllers;

[Route("api/produtos")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutosController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    public async Task<IActionResult> Get() 
    {
        var produtos = await _produtoService.ListarTodosOsProdutos();

        return Ok(produtos);
    }
}
