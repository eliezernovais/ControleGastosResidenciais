using Backend.Models;
namespace Backend.Services.Interfaces
{
    public interface IPessoaService
    {
        // Adiciona uma Nova Pessoa
        Task<bool> AddPessoaAsync(Pessoa pessoa);
        // Retorna uma lista contendo todas as pessoas
        Task<List<Pessoa>> GetAllPessoaAsync();
        // Retorna uma unica pessoa pelo ID
        Task<Pessoa?> GetPessoaByIdAsync(int id);
        // Edita uma Pessoa 
        Task<bool> EditPessoaByIdAsync(int id,Pessoa pessoa);
        // Deleta uma Pessoa
        Task<bool> DeletePessoaByIdAsync(int id);
    }
}
