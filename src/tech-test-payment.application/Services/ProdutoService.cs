using AutoMapper;
using tech_test_payment.application.Dtos;
using tech_test_payment.application.Interfaces;
using tech_test_payment.domain.Interfaces;

namespace tech_test_payment.application.Services;

public class ProdutoService : IProdutoService
{
    private readonly IMapper _mapper;
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper)
    {
        _produtoRepository = produtoRepository;
        _mapper = mapper;
    }

    public async Task<List<ProdutoDto>> ListarTodosOsProdutos()
    {
        var produtos = await _produtoRepository.GetAllAsync();

        return _mapper.Map<List<ProdutoDto>>(produtos);
    }
}
