using AutoMapper;
using Moq;
using tech_test_payment.application;
using tech_test_payment.application.Dtos;
using tech_test_payment.application.Errors;
using tech_test_payment.application.Interfaces;
using tech_test_payment.application.Interfaces.AtualizarStatusVendaStrategy;
using tech_test_payment.application.Services;
using tech_test_payment.application.Services.AtualizarStatusVendaStrategy;
using tech_test_payment.domain.Entities;
using tech_test_payment.domain.Enums;
using tech_test_payment.domain.Interfaces;
using tech_test_payment.domain.Shared;

namespace tech_test_payment.tests;

public class AtualizarStatusDaVendaServiceTests
{
    private readonly Mock<IVendaRepository> _vendaRepositoryMock;
    private readonly Mock<ISelecionadorDeAlteracaoDeStatusDaVenda> _selecionadorDeAlteracaoDeStatusDaVendaMock;
    private readonly Mock<IAtualizarStatusVenda> _atualizarStatusVendaMock;
    private readonly IMapper _mapper;
    private readonly IAtualizarStatusVendaService _sut;

    public AtualizarStatusDaVendaServiceTests()
    {
        _vendaRepositoryMock = new();
        _selecionadorDeAlteracaoDeStatusDaVendaMock = new();
        _atualizarStatusVendaMock = new();
        
        _mapper = AutoMapperConfiguration.Create().CreateMapper();

        _sut = new AtualizarStatusVendaService(
            _mapper,
            _vendaRepositoryMock.Object,
            _selecionadorDeAlteracaoDeStatusDaVendaMock.Object
            );
    }

    [Fact]
    public async Task Deve_Retornar_Erro_De_Venda_Nao_Encontrada_Quando_Nao_Encontrar_A_Venda()
    {
        //Arrange
        var atualizarStatusVendaDto = new AtualizarStatusVendaDto { Status = 1 };

        _vendaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(() => null);
        
        //Act
        var result = await _sut.AtualizarStatusVenda(It.IsAny<Guid>(), atualizarStatusVendaDto);

        //Assert
        Assert.Equal(ApplicationErrors.VendaError.VendaNaoEncontradaAtualizarStatus, result.Error);
    }

    [Fact]
    public async Task Deve_Retornar_Erro_De_Status_Nao_Existe()
    {
        //Arrange
        var atualizarStatusVendaDto = new AtualizarStatusVendaDto { Status = 99 };

        _vendaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new Venda());
        
        //Act
        var result = await _sut.AtualizarStatusVenda(It.IsAny<Guid>(), atualizarStatusVendaDto);

        //Assert
        Assert.Equal(ApplicationErrors.StatusVendaError.StatusInformadoNaoExiste, result.Error);
    }

    [Theory]
    [InlineData(VendaStatus.Cancelada)]
    [InlineData(VendaStatus.Entregue)]    
    public async Task Deve_Retornar_Erro_De_Nao_E_Possivel_Alterar_Status_Quando_Status_Nao_Permitir(VendaStatus status)
    {
        //Arrange
        var atualizarStatusVendaDto = new AtualizarStatusVendaDto { Status = (int)status };
        var venda = new Venda(new Vendedor());        

        _vendaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(venda);
        _selecionadorDeAlteracaoDeStatusDaVendaMock.Setup(s => s.Selecionar(venda.Status))
            .Returns(() => null);
            
        //Act
        var result = await _sut.AtualizarStatusVenda(It.IsAny<Guid>(), atualizarStatusVendaDto);

        //Assert
        Assert.Equal(
            ApplicationErrors.StatusVendaError.NaoEPossivelAlterarStatus(venda.Status.GetEnumDescription(), status.GetEnumDescription()), result.Error);
    }

    [Theory]
    [InlineData(VendaStatus.AguardandoPagamento)]
    [InlineData(VendaStatus.EnviadoParaTransportadora)]
    [InlineData(VendaStatus.PagamentoAprovado)]
    public async Task Deve_Retornar_Erro_De_Nao_E_Possivel_Alterar_Status_Quando_Ambos_Forem_Iguais(VendaStatus status)
    {
        //Arrange
        var atualizarStatusVendaDto = new AtualizarStatusVendaDto { Status = (int)status };
        var venda = new Venda(new Vendedor());
        venda.AlterarStatus(status);

        _vendaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(venda);

        _selecionadorDeAlteracaoDeStatusDaVendaMock.Setup(s => s.Selecionar(venda.Status))
            .Returns(SetupAtualizadorDeStatusBaseadoNoStatus(status));

        //Act
        var result = await _sut.AtualizarStatusVenda(It.IsAny<Guid>(), atualizarStatusVendaDto);

        //Assert
        Assert.Equal(
            ApplicationErrors.StatusVendaError.NaoEPossivelAlterarStatus(venda.Status.GetEnumDescription(), status.GetEnumDescription()), result.Error);
    }

    [Theory]
    [InlineData(VendaStatus.AguardandoPagamento, VendaStatus.EnviadoParaTransportadora)]
    [InlineData(VendaStatus.AguardandoPagamento, VendaStatus.Entregue)]
    [InlineData(VendaStatus.PagamentoAprovado, VendaStatus.AguardandoPagamento)]
    [InlineData(VendaStatus.PagamentoAprovado, VendaStatus.Entregue)]
    [InlineData(VendaStatus.EnviadoParaTransportadora, VendaStatus.PagamentoAprovado)]
    [InlineData(VendaStatus.EnviadoParaTransportadora, VendaStatus.AguardandoPagamento)]
    [InlineData(VendaStatus.EnviadoParaTransportadora, VendaStatus.Cancelada)]
    public async Task Deve_Retornar_Erro_De_Nao_E_Possivel_Alterar_Status_Para_Nao_Permitidos(VendaStatus statusAtual, VendaStatus statusDestino)
    {
        //Arrange
        var atualizarStatusVendaDto = new AtualizarStatusVendaDto { Status = (int)statusDestino };
        var venda = new Venda(new Vendedor());
        venda.AlterarStatus(statusAtual);

        _vendaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(venda);

        _selecionadorDeAlteracaoDeStatusDaVendaMock.Setup(s => s.Selecionar(venda.Status))
            .Returns(SetupAtualizadorDeStatusBaseadoNoStatus(statusAtual));

        //Act
        var result = await _sut.AtualizarStatusVenda(It.IsAny<Guid>(), atualizarStatusVendaDto);

        //Assert
        Assert.Equal(
            ApplicationErrors.StatusVendaError.NaoEPossivelAlterarStatus(venda.Status.GetEnumDescription(), statusDestino.GetEnumDescription()), result.Error);
    }

    [Theory]
    [InlineData(VendaStatus.AguardandoPagamento, VendaStatus.PagamentoAprovado)]
    [InlineData(VendaStatus.AguardandoPagamento, VendaStatus.Cancelada)]
    [InlineData(VendaStatus.PagamentoAprovado, VendaStatus.EnviadoParaTransportadora)]
    [InlineData(VendaStatus.PagamentoAprovado, VendaStatus.Cancelada)]
    [InlineData(VendaStatus.EnviadoParaTransportadora, VendaStatus.Entregue)]    
    public async Task Deve_Alterar_O_Status_E_Retornar_VendaDto_Com_Novo_Status(VendaStatus statusAtual, VendaStatus statusDestino)
    {
        //Arrange
        var atualizarStatusVendaDto = new AtualizarStatusVendaDto { Status = (int)statusDestino };
        var venda = new Venda(new Vendedor());
        venda.AlterarStatus(statusAtual);

        _vendaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(venda);

        _selecionadorDeAlteracaoDeStatusDaVendaMock.Setup(s => s.Selecionar(venda.Status))
            .Returns(SetupAtualizadorDeStatusBaseadoNoStatus(statusAtual));

        _vendaRepositoryMock.Setup(x => x.UpdateStatusAsync(venda))
            .ReturnsAsync(venda);

        //Act
        var result = await _sut.AtualizarStatusVenda(It.IsAny<Guid>(), atualizarStatusVendaDto);

        //Assert
        Assert.IsType<VendaDto>(result.Value);
        Assert.Equal(venda.Status.GetEnumDescription(), result.Value.Status);
    }


    private IAtualizarStatusVenda SetupAtualizadorDeStatusBaseadoNoStatus(VendaStatus status)
    {
        if (status ==VendaStatus.AguardandoPagamento)
            return new AtualizarStatusAguardandoPagamento();

        if (status == VendaStatus.EnviadoParaTransportadora)
            return new AtualizarStatusEnviadoParaTransportadora();

        if (status == VendaStatus.PagamentoAprovado)
            return new AtualizarStatusPagamentoAprovado();

        return null;
    }
}
