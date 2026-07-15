using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Services.Interfaces;
using Backend.DTOs.Pessoas;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController(IPessoaService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<PessoaResponse>>> GetAllPessoa() 
            => Ok(await service.GetAllPessoaAsync());
        [HttpGet("{id}")]
        public async Task<ActionResult<PessoaResponse>> GetPessoaById(int id)
        {
            var pessoa = await service.GetPessoaByIdAsync(id);
            return pessoa is null ? NotFound("Pessoa não Encontrada") : Ok(pessoa);
        }
        [HttpPost]
        public async Task<ActionResult<PessoaResponse>> AddPessoa(PessoaRequest pessoa)
        {
            var newPessoa = await service.AddPessoaAsync(pessoa);
            return CreatedAtAction(nameof(GetPessoaById), new { id = newPessoa.Id },newPessoa);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> AtualizarPessoa(int id, PessoaRequest pessoa) {
            var atualizado = await service.EditPessoaByIdAsync(id, pessoa);
            return atualizado ? NoContent() : NotFound("Nenhuma pessoa encontrada no ID informado!");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarPessoa(int id) {
            var deletado = await service.DeletePessoaByIdAsync(id);
            return deletado ? NoContent() : NotFound("Nenhuma pessoa encontrada no ID informado!");
        }
    }
}
