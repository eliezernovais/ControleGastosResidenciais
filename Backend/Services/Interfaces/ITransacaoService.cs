using Backend.DTOs.Transacao;
namespace Backend.Services.Interfaces
{
    public interface ITransacaoService
    {
        Task<TransacaoResponse> CriarTransacao(TransacaoRequest transacao);
        Task<List<TransacaoResponse>> GetAllTransacao();
        Task<TransacaoResponse?> GetTransacaoById(int id);
        Task<List<TransacaoResponse>> GetTransacaoByPessoaId(int pessoaId);
    }
}
