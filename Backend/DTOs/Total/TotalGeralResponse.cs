namespace Backend.DTOs.Total
{
    public class TotalGeralResponse
    {
        public decimal TotalGeralReceitas { get; }
        public decimal TotalGeralDespesas { get; }
        public decimal SaldoGeral { get; }
        public TotalGeralResponse(decimal totalGeralReceitas, decimal totalGeralDespesas)
        {
            TotalGeralReceitas = totalGeralReceitas;
            TotalGeralDespesas = totalGeralDespesas;
            SaldoGeral = totalGeralReceitas - totalGeralDespesas;

        }
    }
}
