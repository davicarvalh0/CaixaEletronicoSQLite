namespace CaixaEletronicoSQLite.Database;
using Microsoft.Data.Sqlite;

public class Banco
{
    private const string ConnectionString = "Data Source=caixa.db";

    public void CriarTable()
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        string query = """
                        CREATE TABLE IF NOT EXISTS conta (
                            NumeroConta INTEGER PRIMARY KEY AUTOINCREMENT,
                            NomeTitular TEXT NOT NULL,
                            Saldo REAL NOT NULL
                            );

                        CREATE TABLE IF NOT EXISTS transacao (
                            IdTransacao INTEGER PRIMARY KEY AUTOINCREMENT,
                            TipoTransacao TEXT NOT NULL,
                            Valor REAL NOT NULL,
                            DataTransacao TEXT NOT NULL,
                            ContaOrigem INTEGER NOT NULL,
                            ContaDestino  INTEGER
                        );
                        """;
        
            using var comando = new SqliteCommand(query, connection);
            comando.ExecuteNonQuery();
    }
}