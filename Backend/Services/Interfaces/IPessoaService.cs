using Backend.DTOs.Pessoas;
namespace Backend.Services.Interfaces
{
    public interface IPessoaService
    {
        // Adiciona uma Nova Pessoa
        Task<PessoaResponse> AddPessoa(PessoaRequest pessoa);
        // Retorna uma lista contendo todas as pessoas
        Task<List<PessoaResponse>> GetAllPessoa();
        // Retorna uma unica pessoa pelo ID
        Task<PessoaResponse?> GetPessoaById(int id);
        // Edita uma Pessoa 
        Task<bool> EditPessoaById(int id,PessoaRequest pessoa);
        // Deleta uma Pessoa
        Task<bool> DeletePessoaById(int id);
    }
}
