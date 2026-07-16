using Backend.DTOs.Transacao;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacaoController(ITransacaoService service) : ControllerBase
    {
        [HttpPost]
        // Cria uma nova Transacao
        public async Task<ActionResult<TransacaoResponse>> CriarTransacao(TransacaoRequest transacaoRequest) {
            try
            {
                var transacao = await service.CriarTransacao(transacaoRequest);
                return CreatedAtAction(nameof(GetTransacaoById), new { id = transacao.Id }, transacao);
            }catch (ArgumentException erro) {
                return BadRequest(erro.Message);
            }
        }

        [HttpGet]
        // Lista Todas as Transacoes
        public async Task<ActionResult<List<TransacaoResponse>>> GetAllTransacao()
            => await service.GetAllTransacao();

        [HttpGet("{id}")]
        // Retorna a Transacao pelo ID
        public async Task<ActionResult<TransacaoResponse>> GetTransacaoById(int id)
        {
            var Transacao = await service.GetTransacaoById(id);
            return Transacao is null ? NotFound("Transacao nao Encontrada") : Ok(Transacao);
        }

        [HttpGet("Pessoa/{pessoaId}")]
        // Lista todas as Transacoes da Pessoa pelo ID da Pessoa
        public async Task<ActionResult<List<TransacaoResponse>?>> GetTransacaoByPessoaId(int pessoaId)
        {
            var transacoes = await service.GetTransacaoByPessoaId(pessoaId);
            if (transacoes == null)
            {
                return NotFound("Nenhuma pessoa encontrada no ID informado!");
            }else if(!transacoes.Any()){
                return NotFound("Nenhuma Transação encontrada no ID informado!");
            }
            return Ok(transacoes);
        }
    }
}
