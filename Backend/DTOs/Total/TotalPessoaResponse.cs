namespace Backend.DTOs.Total
{
    public class TotalPessoaResponse
    {
        public int PessoaId { get; }
        public string Nome { get; }
        public decimal TotalReceitas { get; }
        public decimal TotalDespesas { get; }
        public decimal Saldo { get; }
        public TotalPessoaResponse(int pessoaId,string nome, decimal totalReceitas, decimal totalDespesas){
            PessoaId = pessoaId;
            Nome = nome;
            TotalReceitas = totalReceitas;
            TotalDespesas = totalDespesas;
            Saldo = totalReceitas - totalDespesas;

        }
    }
}
