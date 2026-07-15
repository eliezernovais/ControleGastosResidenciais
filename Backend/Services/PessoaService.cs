using Backend.Models;
using Backend.Services.Interfaces;

namespace Backend.Services
{
    public class PessoaService : IPessoaService
    {
        public static List<Pessoa> pessoas = new List<Pessoa>
        {
            new Pessoa("Carlos",15) { Id = 1},
            new Pessoa("Roberto",25){ Id = 2}
        };
        public async Task<bool> AddPessoaAsync(Pessoa pessoa)
        {
            pessoas.Add(pessoa);
            return true;
        }

        public async Task<List<Pessoa>> GetAllPessoaAsync()
            => await Task.FromResult(pessoas);

        public async Task<Pessoa?> GetPessoaByIdAsync(int id)
        {
            var pessoa = pessoas.FirstOrDefault(pessoa => pessoa.Id == id);
            return await Task.FromResult(pessoa);
        }

        public async Task<bool> EditPessoaByIdAsync(int id, Pessoa pessoa)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeletePessoaByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
