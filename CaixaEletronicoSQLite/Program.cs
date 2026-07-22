using CaixaEletronicoSQLite.Database;
using CaixaEletronicoSQLite.Servicos;

var banco = new Banco();
banco.CriarTable();

int n;

while (true)
{
    Console.Clear();
    Console.WriteLine("==== Caixa Eletrônico ====");
    Console.WriteLine("1 - Criar Conta");
    Console.WriteLine("2 - Depositar");
    Console.WriteLine("3 - Sacar");
    Console.WriteLine("4 - Transferir");
    Console.WriteLine("5 - Consultar Saldo");
    Console.WriteLine("6 - Consultar Histórico");
    Console.WriteLine("7 - Listar Contas");
    Console.WriteLine("0 - Sair");
    Console.WriteLine("Escolha uma opção:");
    Console.WriteLine("==========================");

    if (!int.TryParse(Console.ReadLine(), out n))
    {
        Console.WriteLine("Opção inválida");
        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
        continue;
    }

    switch (n)
    {
        
        case 0:
        {
            Console.WriteLine("Saindo do sistema...");
            break;
        }

        case 1:
        {
            Console.WriteLine("Digite  o nome do titular da conta:");
            string nomeTitular = Console.ReadLine();
            var contaServicos = new ContaServicos();
            int numeroConta = contaServicos.CriarConta(nomeTitular);
            if (numeroConta == -1)
            {
                break;
            }
            Console.WriteLine("Conta criada com sucesso");
            Console.WriteLine($"Número da conta: {numeroConta}");
            break;
        }

        case 2:
        {
            var contaServicos = new ContaServicos();
            contaServicos.ListarContas();
            Console.WriteLine("Digite o número da conta que você deseja fazer um depósito:");
            if (!int.TryParse(Console.ReadLine(), out int numeroConta))
            {
                Console.WriteLine("Número de conta inválido");
                break;
            }
            Console.WriteLine("Digite o valor do depósito");
            if (!double.TryParse(Console.ReadLine(), out double valorDeposito))
            {
                Console.WriteLine("Valor inválido");
                break;
            }
            contaServicos.Depositar(numeroConta, valorDeposito);
            break;
        }
            

        case 3:
        {
            var contaServicos = new ContaServicos();
            contaServicos.ListarContas();
            Console.WriteLine("Digite o número da conta que você quer sacar um valor: ");
            if (!int.TryParse(Console.ReadLine(), out int numeroConta))
            {
                Console.WriteLine("Número de conta inválido");
                break;
            }            
            Console.WriteLine("Digite o valor de saque: ");
            if (!double.TryParse(Console.ReadLine(), out double valorSaque))
            {
                Console.WriteLine("Valor inválido");
                break;
            }            
            contaServicos.Sacar(numeroConta, valorSaque);
            break;
        }

        case 4:
        {
            var contaServicos = new ContaServicos();
            contaServicos.ListarContas();
            Console.WriteLine("Digite o número da conta do titular:");
            if (!int.TryParse(Console.ReadLine(), out int contaOrigem))
            {
                Console.WriteLine("Número de conta inválido");
                break;
            }            
            Console.WriteLine("Digite o número da conta que deseja fazer a transferência:");
            if (!int.TryParse(Console.ReadLine(), out int contaDestino))
            {
                Console.WriteLine("Número de conta inválido");
                break;
            }            
            Console.WriteLine("Digite o valor que deseja realizar na transferência: ");
            if (!double.TryParse(Console.ReadLine(), out double valorTransferencia))
            {
                Console.WriteLine("Valor inválido");
                break;
            }
            contaServicos.Transferir(contaOrigem, contaDestino, valorTransferencia);
            break;
        }
            
        case 5:
        {
            var contaServicos = new ContaServicos();
            contaServicos.ListarContas();
            Console.Write("Digite o numero da conta que você quer ver o saldo: ");
            if (!int.TryParse(Console.ReadLine(), out int numeroConta))
            {
                Console.WriteLine("Número de conta inválido");
                break;
            }            
            contaServicos.ConsultarSaldo(numeroConta);
            break;
        }
        
        case 6:
        {
            var contaServicos = new ContaServicos();
            contaServicos.ListarContas();
            Console.WriteLine("De qual conta você quer consultar o histórico?");
            if (!int.TryParse(Console.ReadLine(), out int numeroConta))
            {
                Console.WriteLine("Número de conta inválido");
                break;
            }            
            contaServicos.ConsultarHistorico(numeroConta);
            break;
        }

        case 7:
        {
            var contaServicos = new ContaServicos();
            contaServicos.ListarContas();
            break;
        }

        default:
            Console.WriteLine("Opção inválida");
            break;
    }
    
    if (n != 0)
    {
        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
    }

}