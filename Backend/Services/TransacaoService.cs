using Backend.Data;
using Backend.DTOs.Transacao;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
namespace Backend.Services
{
    public class TransacaoService(AppDbContext context) : ITransacaoService
    {
        public async Task<TransacaoResponse> CriarTransacao(TransacaoRequest transacaoRequest){
            var pessoa = await context.Pessoas.FindAsync(transacaoRequest.PessoaId);
            //Verifica se a pessoa inserida é valida
            if (pessoa is null) {
                throw new ArgumentException("A pessoa inserida é invalida!", nameof(transacaoRequest.PessoaId));

            //Verifica se a pessoa é menor de idade, e caso seja, verifica se a transacao é uma receita
            } else if (pessoa.MenorDeIdade() == true && transacaoRequest.Tipo == Enums.TipoTransacao.Receita) {
                throw new ArgumentException("Menores de Idade somente podem cadastrar Despesas!");
            }
            var Transacao = new Transacao(
                    transacaoRequest.Descricao,
                    transacaoRequest.Valor,
                    transacaoRequest.Tipo,
                    transacaoRequest.PessoaId);
            context.Transacoes.Add(Transacao);
            await context.SaveChangesAsync();
            return new TransacaoResponse
            {
                Id = Transacao.Id,
                Descricao = Transacao.Descricao,
                Valor = Transacao.Valor,
                Tipo = Transacao.Tipo,
                PessoaId = Transacao.PessoaId
            };
        }
        public async Task<List<TransacaoResponse>> GetAllTransacao()
        => await context.Transacoes.Select(
            t => new TransacaoResponse { 
                Id = t.Id,
                Descricao = t.Descricao,
                Valor = t.Valor,
                Tipo = t.Tipo,
                PessoaId = t.PessoaId }).ToListAsync();

        public async Task<TransacaoResponse?> GetTransacaoById(int id)
        => await context.Transacoes.Where(t => t.Id == id).Select(t => new TransacaoResponse {
            Id = t.Id,
            Descricao = t.Descricao,
            Valor = t.Valor,
            Tipo = t.Tipo,
            PessoaId = t.PessoaId
        }).FirstOrDefaultAsync();

        public async Task<List<TransacaoResponse>> GetTransacaoByPessoaId(int pessoaId)
        => await context.Transacoes.Where(t => t.PessoaId == pessoaId).Select(t => new TransacaoResponse
           {
               Id = t.Id,
               Descricao = t.Descricao,
               Valor = t.Valor,
               Tipo = t.Tipo,
               PessoaId = t.PessoaId
           }).ToListAsync();
        }
}
