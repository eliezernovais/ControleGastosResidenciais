using Backend.DTOs.Total;
namespace Backend.Services.Interfaces
{
    public interface ITotalService
    {
        // Retorna uma lista de todas as pessoas, suas receitas, despesas e saldos.
        Task<List<TotalPessoaResponse>> GetAllPessoaTotal();
        //Retorna os a receita,despesa e saldo de uma pessoa pelo ID dela.
        Task<TotalPessoaResponse?> GetPessoaTotalById(int id);
        //Retorna as Receitas,Despesas e saldo geral.
        Task<TotalGeralResponse?> GetTotalGeral();
    }
}
