using Backend.Data;
using Backend.DTOs.Total;
using Backend.Enums;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Backend.Services
{
    public class TotalService(AppDbContext context) : ITotalService
    {
        public async Task<List<TotalPessoaResponse>> GetAllPessoaTotal()
        //Seleciona todas as pessoas, e adiciona uma a uma em uma lista, com suas receitas e despesas somadas individualmente.
            => await context.Pessoas.Select(
                pessoa => new TotalPessoaResponse(
                    pessoa.Id,
                    pessoa.Nome,
                    pessoa.Transacoes.Where(transacoes => transacoes.Tipo == TipoTransacao.Receita).Sum(receita => receita.Valor),
                    pessoa.Transacoes.Where(transacoes => transacoes.Tipo == TipoTransacao.Despesa).Sum(despesa => despesa.Valor)
                )
            ).ToListAsync();

        public async Task<TotalPessoaResponse?> GetPessoaTotalById(int id)
        // Encontra a pessoa com o ID especificado e retorna suas receitas e despesas somadas individualmente.
            => await context.Pessoas.Where(pessoa => pessoa.Id == id).Select(
                pessoa => new TotalPessoaResponse(
                    pessoa.Id,
                    pessoa.Nome,
                    pessoa.Transacoes.Where(transacoes => transacoes.Tipo == TipoTransacao.Receita).Sum(receita => receita.Valor),
                    pessoa.Transacoes.Where(transacoes => transacoes.Tipo == TipoTransacao.Despesa).Sum(despesa => despesa.Valor)
                )
            ).FirstOrDefaultAsync();
        public async Task<TotalGeralResponse?> GetTotalGeral()
        //Soma as receitas e despesas de todos e retorna.
        {
            var totalReceitas = await context.Transacoes.Where(transacao => transacao.Tipo == TipoTransacao.Receita).SumAsync(receita => (decimal?)receita.Valor) ?? 0;
            var totalDespesas = await context.Transacoes.Where(transacao => transacao.Tipo == TipoTransacao.Despesa).SumAsync(despesa => (decimal?)despesa.Valor) ?? 0;
            return new TotalGeralResponse(totalReceitas, totalDespesas);
        }
    }
}
