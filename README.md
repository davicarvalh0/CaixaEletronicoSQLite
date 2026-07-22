# 🏧 Caixa Eletrônico SQLite

Sistema de caixa eletrônico em C# rodando direto no terminal
Projeto desenvolvido como desafio de backend, com foco em persistência de dados com SQLite, boas práticas de acesso a banco (queries parametrizadas

## ✨ Funcionalidades

- Criar conta
- Depositar
- Sacar
- Transferir entre contas
- Consultar saldo
- Consultar histórico de transações
- Listar todas as contas

## 🛠️ Tecnologias

- C# (.NET)
- [SQLite](https://www.sqlite.org/) via [Microsoft.Data.Sqlite](https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/)

## 📂 Estrutura do projeto

```
CaixaEletronicoSQLite/
├── Program.cs
├── Database/
│   └── Banco.cs
├── Models/
│   ├── Conta.cs
│   └── Transacao.cs
└── Servicos/
    └── ContaServicos.cs
```

## ▶️ Como rodar localmente

1. Clone o repositório:

```bash
git clone https://github.com/davicarvalh0/CaixaEletronicoSQLite.git
cd CaixaEletronicoSQLite/CaixaEletronicoSQLite
```

2. Rode o projeto (precisa do [.NET SDK](https://dotnet.microsoft.com/download) instalado):

```bash
dotnet run
```

3. O menu vai aparecer direto no terminal:

```
==== Caixa Eletrônico ====
1 - Criar Conta
2 - Depositar
3 - Sacar
4 - Transferir
5 - Consultar Saldo
6 - Consultar Histórico
7 - Listar Contas
0 - Sair
```

Basta digitar o número da opção e seguir as instruções na tela.
