using Backend.DTOs.Transacao;
namespace Backend.Services.Interfaces
{
    public interface ITransacaoService
    {
        //Cria uma Nova Transação
        Task<TransacaoResponse> CriarTransacao(TransacaoRequest transacao);
        // Lista todas as Transações
        Task<List<TransacaoResponse>> GetAllTransacao();
        // Retorna a Transação pelo ID
        Task<TransacaoResponse?> GetTransacaoById(int id);
        // Lista todas as Transações de uma Pessoa pelo ID dela
        Task<List<TransacaoResponse>> GetTransacaoByPessoaId(int pessoaId);
    }
}
