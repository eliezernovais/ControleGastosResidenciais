using Microsoft.AspNetCore.Mvc;
using Backend.Services.Interfaces;
using Backend.DTOs.Pessoas;
namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController(IPessoaService service) : ControllerBase
    {
        [HttpPost]
        // Adiciona uma nova Pessoa
        public async Task<ActionResult<PessoaResponse>> AddPessoa(PessoaRequest pessoaRequest)
        {
            var pessoa = await service.AddPessoa(pessoaRequest);
            return CreatedAtAction(nameof(GetPessoaById), new { id = pessoa.Id }, pessoa);
        }
        [HttpPut("{id}")]
        // Atualiza a Pessoa pelo ID dela
        public async Task<ActionResult> AtualizarPessoa(int id, PessoaRequest pessoa)
        {
            var atualizado = await service.EditPessoaById(id, pessoa);
            return atualizado ? NoContent() : NotFound("Nenhuma pessoa encontrada no ID informado!");
        }
        [HttpGet]
        // Lista Todas as Pessoas
        public async Task<ActionResult<List<PessoaResponse>>> GetAllPessoa() 
            => Ok(await service.GetAllPessoa());
        [HttpGet("{id}")]
        // Retorna a Pessoa pelo ID dela
        public async Task<ActionResult<PessoaResponse>> GetPessoaById(int id)
        {
            var pessoa = await service.GetPessoaById(id);
            return pessoa is null ? NotFound("Pessoa não Encontrada") : Ok(pessoa);
        }
        [HttpDelete("{id}")]
        //Deleta uma Pessoa pelo ID dela
        public async Task<ActionResult> DeletarPessoa(int id) {
            var deletado = await service.DeletePessoaById(id);
            return deletado ? NoContent() : NotFound("Nenhuma pessoa encontrada no ID informado!");
        }
    }
}
