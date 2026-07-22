using CaixaEletronicoSQLite.Database;
using Microsoft.Data.Sqlite;
namespace CaixaEletronicoSQLite.Servicos;
using System.Globalization;

public class ContaServicos
{
    private const string ConnectionString = "Data Source=caixa.db";

    public int CriarConta(String nomeTitular)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        string query = """
                       INSERT INTO conta (NomeTitular, Saldo)
                       VALUES (@NomeTitular, 0);
                       SELECT last_insert_rowid();
                       """;

        using var comando = new SqliteCommand(query,connection);
        comando.Parameters.AddWithValue("@NomeTitular", nomeTitular);
        
        return (int)(long)comando.ExecuteScalar();
    }
    
    public void Depositar(int numeroConta, double valor)
    {
        if (valor <= 0)
        {
            Console.WriteLine("O depósito deve ser maior que 0");
            return;
        }

        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        if (!ContaExiste(numeroConta, connection))
        {
            Console.WriteLine("Conta não encontrada");
            return;
        }
        
        string query = """
                       UPDATE conta
                       SET Saldo = Saldo + @valor
                       WHERE NumeroConta = @numeroconta;

                       INSERT INTO transacao (TipoTransacao, Valor, DataTransacao, ContaOrigem)
                       VALUES ('Depósito', @valor, @data, @numeroconta);
                       """;

        using var comando = new SqliteCommand(query, connection);
        comando.Parameters.AddWithValue("@valor", valor);
        comando.Parameters.AddWithValue("@numeroconta", numeroConta);
        comando.Parameters.AddWithValue("@data", DateTime.Now.ToString("s"));

        comando.ExecuteNonQuery();
        Console.WriteLine("Depósito realizado com sucesso");
    }

    public void Sacar(int numeroConta, double valor)
    {
        if (valor <= 0)
        {
            Console.WriteLine("O valor deve ser maior que 0");
            return;
        }
        
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        
        if (!ContaExiste(numeroConta, connection))
        {
            Console.WriteLine("Conta não encontrada.");
            return;
        }
        
        string querySaldo = "SELECT Saldo FROM conta WHERE NumeroConta = @numeroConta;";
        using var comandoSaldo = new SqliteCommand(querySaldo, connection);
        comandoSaldo.Parameters.AddWithValue("@numeroConta", numeroConta);
        
        var resultadoSaldo = comandoSaldo.ExecuteScalar();
        if (resultadoSaldo == null)
        {
            Console.WriteLine("Conta não encontrada");
            return;
        }
        
        double saldoAtual = (double)resultadoSaldo;

        if (saldoAtual < valor)
        {
            Console.WriteLine("Saldo insuficiente");
            return;
        }
        
        string query = """
                       UPDATE conta
                       SET Saldo = Saldo - @valor
                       WHERE NumeroConta = @numeroConta;

                       INSERT INTO transacao (TipoTransacao, Valor, DataTransacao, ContaOrigem)
                       VALUES ('Saque', @valor, @data, @numeroConta);
                       """;
        
        using var comando = new SqliteCommand(query, connection);
        comando.Parameters.AddWithValue("@valor", valor);
        comando.Parameters.AddWithValue("@numeroConta", numeroConta);
        comando.Parameters.AddWithValue("@data", DateTime.Now.ToString("s"));
        
        Console.WriteLine($"Saque realizado com sucesso");
        
        comando.ExecuteNonQuery();
    }
    
    public void Transferir(int contaOrigem, int contaDestino, double valor)
    {
        if (valor <= 0)
        {
            Console.WriteLine("O valor deve ser maior que 0");
            return;
        }

        if (contaOrigem == contaDestino)
        {
            Console.WriteLine("Você não pode transferir para a mesma conta");
            return;
        }
        
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        
        if (!ContaExiste(contaOrigem, connection))
        {
            Console.WriteLine("Conta de origem não encontrada");
            return;
        }
        
        if (!ContaExiste(contaDestino, connection))
        {
            Console.WriteLine("Conta de destino não encontrada");
            return;
        }
        
        string queryDestino = "SELECT COUNT(*) FROM conta WHERE NumeroConta = @contaDestino;";
        using var comandoDestino = new SqliteCommand(queryDestino, connection);
        comandoDestino.Parameters.AddWithValue("@contaDestino", contaDestino);
        
        long numDestinos = (long)comandoDestino.ExecuteScalar();

        if (numDestinos == 0)
        {
            Console.WriteLine("Conta de destino não encontrada");
            return;
        }
        
        string querySaldo = "SELECT Saldo FROM conta WHERE NumeroConta = @contaOrigem;";
        using var comandoSaldo = new SqliteCommand(querySaldo, connection);
        comandoSaldo.Parameters.AddWithValue("@contaOrigem", contaOrigem);
        
        var resultadoSaldo = comandoSaldo.ExecuteScalar();
        
        if (resultadoSaldo == null)
        {
            Console.WriteLine("Conta de origem não encontrada");
            return;
        }
        
        double saldoOrigem = (double)resultadoSaldo;

        if (saldoOrigem < valor)
        {
            Console.WriteLine("Saldo insuficiente");
            return;
        }
        
        string query = """
                       UPDATE conta 
                       SET Saldo = Saldo - @valor 
                       WHERE NumeroConta = @contaOrigem;

                       UPDATE conta 
                       SET Saldo = Saldo + @valor 
                       WHERE NumeroConta = @contaDestino;

                       INSERT INTO transacao (TipoTransacao, Valor, DataTransacao, ContaOrigem, ContaDestino)
                       VALUES ('Transferência', @valor, @data, @contaOrigem, @contaDestino);
                       """;
        using var comando = new SqliteCommand(query, connection);
        comando.Parameters.AddWithValue("@valor", valor);
        comando.Parameters.AddWithValue("@data", DateTime.Now.ToString("s"));
        comando.Parameters.AddWithValue("@contaOrigem", contaOrigem);
        comando.Parameters.AddWithValue("@contaDestino", contaDestino);
        
        Console.WriteLine($"Tranferência realizado com sucesso");
        
        comando.ExecuteNonQuery();    }

    public void ConsultarSaldo(int numeroConta)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        
        string query = "SELECT NomeTitular, Saldo FROM conta WHERE NumeroConta = @numeroConta;";
        using var comando = new SqliteCommand(query, connection);
        comando.Parameters.AddWithValue("@numeroConta", numeroConta);
        
        using var reader = comando.ExecuteReader();
        if (reader.Read())
        {
            Console.WriteLine($"Titular: {reader["NomeTitular"]} | Saldo: {reader["Saldo"]}");
        }
        else
        {
            Console.WriteLine("Conta não encontrada");
        }
    }
    
    public void ConsultarHistorico(int numeroConta)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        
        if (!ContaExiste(numeroConta, connection))
        {
            Console.WriteLine("Conta não encontrada");
            return;
        }
        
        string query = "SELECT TipoTransacao, Valor, DataTransacao, ContaDestino " +
                       "FROM transacao " +
                       "WHERE ContaOrigem = @numeroconta OR ContaDestino = @numeroconta";
        
        using var comando = new SqliteCommand(query, connection);
        comando.Parameters.AddWithValue("@numeroconta", numeroConta);
        
        using var reader = comando.ExecuteReader();
        bool vazio = true;
        
        var ptBr = new CultureInfo("pt-BR");
        while (reader.Read())
        {
            vazio = false;
            Console.WriteLine($"Data: {reader["DataTransacao"]} \nTipo: {reader["TipoTransacao"]} \nValor: {Convert.ToDouble(reader["Valor"]).ToString("C2", ptBr)} \nConta de destino: {reader["ContaDestino"]}\n");
        }

        if (vazio)
        {
            Console.WriteLine("Nenhum histórico disponivel nessa conta");
        }
    }
    private bool ContaExiste(int numeroConta, SqliteConnection connection)
    {
        string query = "SELECT COUNT(*) FROM conta WHERE NumeroConta = @numeroConta;";
        using var comando = new SqliteCommand(query, connection);
        comando.Parameters.AddWithValue("@numeroConta", numeroConta);

        long quantidade = (long)comando.ExecuteScalar();
        return quantidade == 1;
    }

    public void ListarContas()
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        
        string query = "SELECT * FROM conta";
        using var comando = new SqliteCommand(query, connection);
        using var reader = comando.ExecuteReader();
        bool vazio = true;
        while (reader.Read())
        {
            vazio = false;
            Console.WriteLine($"Numero da conta: {reader["NumeroConta"]} \nNome do titular: {reader["NomeTitular"]}\n");
        }

        if (vazio)
        {
            Console.WriteLine("Nenhuma conta cadastrada");
        }
    }
}