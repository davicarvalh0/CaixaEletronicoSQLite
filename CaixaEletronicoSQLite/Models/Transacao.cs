namespace CaixaEletronicoSQLite.Models;

public class Transacao
{
    public int IdTransacao  { get; set; }
    public string TipoTransacao  { get; set; }
    public double Valor { get; set; }
    public DateTime DataTransacao  { get; set; }
    public int ContaOrigem  { get; set; }
    public int ContaDestino  { get; set; }
} 