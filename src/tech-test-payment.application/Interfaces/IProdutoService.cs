﻿using tech_test_payment.application.Dtos;

namespace tech_test_payment.application.Interfaces;

public interface IProdutoService
{
    Task<List<ProdutoDto>> ListarTodosOsProdutos();
}
