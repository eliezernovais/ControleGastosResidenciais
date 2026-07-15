using Backend.DTOs.Pessoas;
using Backend.Models;
namespace Backend.Services.Interfaces
{
    public interface IPessoaService
    {
        // Adiciona uma Nova Pessoa
        Task<PessoaResponse> AddPessoaAsync(PessoaRequest pessoa);
        // Retorna uma lista contendo todas as pessoas
        Task<List<PessoaResponse>> GetAllPessoaAsync();
        // Retorna uma unica pessoa pelo ID
        Task<PessoaResponse?> GetPessoaByIdAsync(int id);
        // Edita uma Pessoa 
        Task<bool> EditPessoaByIdAsync(int id,PessoaRequest pessoa);
        // Deleta uma Pessoa
        Task<bool> DeletePessoaByIdAsync(int id);
    }
}
