using AutoMapper;
using Moq;
using tech_test_payment.application;
using tech_test_payment.application.Dtos;
using tech_test_payment.application.Errors;
using tech_test_payment.application.Interfaces;
using tech_test_payment.application.Services;
using tech_test_payment.domain.Entities;
using tech_test_payment.domain.Enums;
using tech_test_payment.domain.Erros;
using tech_test_payment.domain.Interfaces;
using tech_test_payment.domain.Shared;

namespace tech_test_payment.tests;

public class CriarVendaServiceTests
{
    private readonly Mock<IVendaRepository> _vendaRepositoryMock;
    private readonly Mock<IProdutoRepository> _produtoRepositoryMock;
    private readonly Mock<IVendedorRepository> _vendedorRepositoryMock;
    private readonly ICriarVendaService _sut;
    private readonly IMapper _mapper;

    public CriarVendaServiceTests()
    {        
       _vendaRepositoryMock = new();
        _produtoRepositoryMock = new();
        _vendedorRepositoryMock = new();

        _mapper = AutoMapperConfiguration.Create().CreateMapper();

        _sut = new CriarVendaService(
            _mapper,
            _vendaRepositoryMock.Object,
            _produtoRepositoryMock.Object,
            _vendedorRepositoryMock.Object
            );
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Nao_Encontrar_O_Vendedor_Ao_Registrar_A_Venda()
    {
        //Arrange
        var vendaDto = new RegistrarVendaDto
        {
            VendedorId = Guid.NewGuid(),
            Items = new()
        };

        _vendedorRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                            .ReturnsAsync(() => null);
        
        //Act
        var result = await _sut.RegistrarVenda(vendaDto);

        //Assert
        Assert.Equal(ApplicationErrors.VendaError.VendedorNaoEncontrado, result.Error);        
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Nao_Encontrar_O_Produto_Ao_Registrar_A_Venda()
    {
        //Arrange
        var vendaDto = new RegistrarVendaDto
        {
            VendedorId = Guid.NewGuid(),
            Items = new List<ItemsRegistrarVendaDto> { new ItemsRegistrarVendaDto { ProdutoId = Guid.NewGuid() } }
        };

        _vendedorRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                               .ReturnsAsync(new Vendedor());

        _produtoRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                              .ReturnsAsync(() => null);

        //Act
        var result = await _sut.RegistrarVenda(vendaDto);

        //Assert
        Assert.Equal(ApplicationErrors.VendaError.ProdutoNaoEncontrado, result.Error);
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_A_Venda_Nao_Apresentar_Items()
    {
        //Arrange
        var vendaDto = new RegistrarVendaDto
        {
            VendedorId = Guid.NewGuid(),
            Items = new()
        };

        _vendedorRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                               .ReturnsAsync(new Vendedor());        

        //Act
        var result = await _sut.RegistrarVenda(vendaDto);

        //Assert
        Assert.Equal(DomainErrors.VendaError.VendaDevePossuirItems, result.Error);
    }

    [Fact]
    public async Task Deve_Criar_A_Venda_Com_Status_AguardandoPagamento_E_Retornar_VendaDto()
    {
        //Arrange
        var vendaDto = new RegistrarVendaDto
        {
            VendedorId = Guid.NewGuid(),
            Items = new List<ItemsRegistrarVendaDto> { new ItemsRegistrarVendaDto { ProdutoId = Guid.NewGuid(), Quantidade = 1, Preco = 12 } },            
        };        

        _vendedorRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                               .ReturnsAsync(new Vendedor());

        _produtoRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                              .ReturnsAsync(new Produto());

        _vendaRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Venda>()))
                            .ReturnsAsync(new Venda(new Vendedor()));        

        //Act
        var result = await _sut.RegistrarVenda(vendaDto);

        //Assert
        Assert.NotNull(result.Value);
        Assert.IsType<VendaDto>(result.Value);
        Assert.Equal(VendaStatus.AguardandoPagamento.GetEnumDescription(), result.Value.Status);
    }
}
