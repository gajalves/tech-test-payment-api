﻿namespace tech_test_payment.domain.Entities;

public class Produto : EntidadeBase
{
    public string Nome { get; private set; }
    public decimal Preco { get; private set; }

    public Produto()
    { }

    public Produto(string nome, decimal preco)
    {
        Nome = nome;
        Preco = preco;
    }

    
    public Produto(Guid id, string nome, decimal preco)
    {
        Id = id;
        Nome = nome;
        Preco = preco;
    }
}
