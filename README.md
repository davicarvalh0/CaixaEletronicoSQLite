🏧 Caixa Eletrônico SQLite

Sistema de caixa eletrônico em C# rodando direto no terminal — sem interface gráfica, sem frescura, só operações bancárias de verdade acontecendo no console.
Projeto desenvolvido como desafio de backend, com foco em persistência de dados com SQLite, boas práticas de acesso a banco (queries parametrizadas, sem SQL injection) e organização em camadas.

✨ Funcionalidades
Criar conta (com número gerado automaticamente)
Depositar
Sacar (com validação de saldo)
Transferir entre contas
Consultar saldo
Consultar histórico de transações
🛠️ Tecnologias
C# (.NET)
SQLite via Microsoft.Data.Sqlite
📂 Estrutura do projeto
CaixaEletronicoSQLite/
├── Program.cs
├── Database/
│   └── Banco.cs          
├── Models/
│   ├── Conta.cs           
│   └── Transacao.cs       
└── Servicos/
    └── ContaServicos.cs
▶️ Como rodar localmente
Clone o repositório:
bash
git clone https://github.com/davicarvalh0/CaixaEletronicoSQLite.git
cd CaixaEletronicoSQLite/CaixaEletronicoSQLite
Rode o projeto (precisa do .NET SDK instalado):
bash
dotnet run
O menu vai aparecer direto no terminal:
==== Caixa Eletrônico ====
1 - Criar Conta
2 - Depositar
3 - Sacar
4 - Transferir
5 - Consultar Saldo
6 - Consultar Histórico
0 - Sair

Basta digitar o número da opção e seguir as instruções na tela.

💾 Sobre a persistência

Assim que o programa roda pela primeira vez, um arquivo caixa.db é criado automaticamente com as tabelas conta e transacao. Todas as operações são salvas nele — feche o programa, abra de novo, e suas contas e saldos continuam exatamente onde pararam.

📏 Regras de negócio
Toda conta tem um número único, gerado pelo próprio banco (AUTOINCREMENT)
Saldo nunca fica negativo — saques e transferências acima do saldo disponível são bloqueados
Toda transação (depósito, saque ou transferência) fica registrada com data, hora, tipo e valor
Não é possível transferir para a própria conta
