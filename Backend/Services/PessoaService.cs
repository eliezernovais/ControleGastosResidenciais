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
            p => new PessoaResponse { 
                Id = p.Id,
                Nome = p.Nome,
                Idade = p.Idade }).ToListAsync();

        public async Task<PessoaResponse?> GetPessoaById(int id)
        => await context.Pessoas.Where(p => p.Id == id).Select(
                p => new PessoaResponse{
                Id = p.Id,
                Nome = p.Nome,
                Idade = p.Idade}).FirstOrDefaultAsync();

        public async Task<bool> EditPessoaById(int id, PessoaRequest pessoa)
        {
            var pessoaAnterior = await context.Pessoas.FindAsync(id);

            if (pessoaAnterior is null) 
                return false;

            pessoaAnterior.Atualizar(pessoa.Nome, pessoa.Idade);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletePessoaById(int id)
        {
            var pessoa = await context.Pessoas.FindAsync(id);

            if (pessoa is null)
                return false;
            context.Pessoas.Remove(pessoa);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
