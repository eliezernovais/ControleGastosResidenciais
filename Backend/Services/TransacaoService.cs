using Backend.Data;
using Backend.DTOs.Transacao;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Backend.Services
{
    public class TransacaoService(AppDbContext context) : ITransacaoService
    {
        public async Task<TransacaoResponse> CriarTransacao(TransacaoRequest transacaoRequest)
        {
            var pessoa = await context.Pessoas.FindAsync(transacaoRequest.PessoaId);
            //Verifica se a pessoa inserida é valida
            if (pessoa is null)
            {
                throw new ArgumentException("A pessoa inserida é invalida!", nameof(transacaoRequest.PessoaId));

                //Verifica se a pessoa é menor de idade, e caso seja, verifica se a transacao é uma receita
            }
            else if (pessoa.MenorDeIdade() == true && transacaoRequest.Tipo == Enums.TipoTransacao.Receita)
            {
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
            transacao => new TransacaoResponse
            {
                Id = transacao.Id,
                Descricao = transacao.Descricao,
                Valor = transacao.Valor,
                Tipo = transacao.Tipo,
                PessoaId = transacao.PessoaId
            }).ToListAsync();

        public async Task<TransacaoResponse?> GetTransacaoById(int id)
        => await context.Transacoes.Where(transacao => transacao.Id == id).Select(transacao => new TransacaoResponse
        {
            Id = transacao.Id,
            Descricao = transacao.Descricao,
            Valor = transacao.Valor,
            Tipo = transacao.Tipo,
            PessoaId = transacao.PessoaId
        }).FirstOrDefaultAsync();

        public async Task<List<TransacaoResponse>?> GetTransacaoByPessoaId(int pessoaId)
        {
            var pessoaExiste = await context.Pessoas.AnyAsync(pessoa => pessoa.Id == pessoaId);
            if (!pessoaExiste) return null;
            return await context.Transacoes.Where(transacao => transacao.PessoaId == pessoaId).Select(transacao => new TransacaoResponse
            {
                Id = transacao.Id,
                Descricao = transacao.Descricao,
                Valor = transacao.Valor,
                Tipo = transacao.Tipo,
                PessoaId = transacao.PessoaId
            }).ToListAsync();
        }
    }
}
