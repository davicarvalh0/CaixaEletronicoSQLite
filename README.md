# Caixa Eletrônico SQLite

Sistema de caixa eletrônico em C# rodando direto no terminal.
Projeto desenvolvido como desafio de backend, com foco em persistência de dados com SQLite, boas práticas de acesso a banco (queries parametrizadas, sem SQL injection) e organização em camadas.

## Funcionalidades

- Criar conta (com número gerado automaticamente)
- Depositar
- Sacar (com validação de saldo)
- Transferir entre contas
- Consultar saldo
- Consultar histórico de transações
- Listar todas as contas

## Tecnologias

- C# (.NET)
- [SQLite](https://www.sqlite.org/) via [Microsoft.Data.Sqlite](https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/)

## Estrutura do projeto

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

## Como rodar localmente

### 1. Instale o .NET SDK

O projeto precisa do **.NET SDK** instalado na máquina — é ele quem traz os comandos `dotnet` usados para compilar e rodar o projeto.

**Verifique se já está instalado:**
```bash
dotnet --version
```
Se aparecer um número de versão (ex: `8.0.100`), pode pular para o passo 2. Se aparecer erro do tipo "comando não encontrado", instale primeiro.

**No Ubuntu/Debian:**
```bash
sudo apt update
sudo apt install dotnet-sdk-8.0
```

**No Windows:** baixe o instalador em [dotnet.microsoft.com/download](https://dotnet.microsoft.com/download) e execute-o.

**No macOS (com Homebrew):**
```bash
brew install dotnet-sdk
```

Depois de instalar, confirme rodando `dotnet --version` novamente.

### 2. Clone o repositório

```bash
git clone https://github.com/davicarvalh0/CaixaEletronicoSQLite.git
```

Isso baixa o projeto para uma pasta chamada `CaixaEletronicoSQLite` no seu computador.

### 3. Entre na pasta do projeto

```bash
cd CaixaEletronicoSQLite/CaixaEletronicoSQLite
```

### 4. Restaure as dependências do projeto

```bash
dotnet restore
```

### 5. Rode o projeto

```bash
dotnet run
```
O menu vai aparecer direto no terminal:

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
