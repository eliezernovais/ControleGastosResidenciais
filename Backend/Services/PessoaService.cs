using Backend.Data;
using Backend.DTOs.Pessoas;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class PessoaService(AppDbContext context) : IPessoaService
    {
        public async Task<PessoaResponse> AddPessoa(PessoaRequest pessoaRequest)
        {
            var pessoa = new Pessoa(
                pessoaRequest.Nome,
                pessoaRequest.Idade
            );
            context.Pessoas.Add(pessoa);
            await context.SaveChangesAsync();
            return new PessoaResponse
            {
                Id = pessoa.Id,
                Nome = pessoa.Nome,
                Idade = pessoa.Idade
            };
        }

        public async Task<List<PessoaResponse>> GetAllPessoa()
        => await context.Pessoas.Select(
            pessoa => new PessoaResponse { 
                Id = pessoa.Id,
                Nome = pessoa.Nome,
                Idade = pessoa.Idade }).ToListAsync();

        public async Task<PessoaResponse?> GetPessoaById(int id)
        => await context.Pessoas.Where(pessoa => pessoa.Id == id).Select(
                pessoa => new PessoaResponse{
                Id = pessoa.Id,
                Nome = pessoa.Nome,
                Idade = pessoa.Idade}).FirstOrDefaultAsync();

        public async Task<bool> EditPessoaById(int id, PessoaRequest pessoa)
        {
            var pessoaAnterior = await context.Pessoas.FindAsync(id);
            //Caso nao encontre a pessoa pelo ID, retorna edição nao concluida
            if (pessoaAnterior is null) return false;
            //Caso Encontre, atualiza a Pessoa
            pessoaAnterior.Atualizar(pessoa.Nome, pessoa.Idade);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletePessoaById(int id)
        {
            var pessoa = await context.Pessoas.FindAsync(id);
            // Caso nao encontre a pessoa pelo ID, Retorna Deleção nao concluida
            if (pessoa is null) return false;
            // Caso Encontre, Deleta a pessoa
            context.Pessoas.Remove(pessoa);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
