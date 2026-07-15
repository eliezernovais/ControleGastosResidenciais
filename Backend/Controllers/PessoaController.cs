using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Services.Interfaces;
using System.Runtime.CompilerServices;
namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController(IPessoaService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Pessoa>>> GetAllPessoa() 
            => Ok(await service.GetAllPessoaAsync());
        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> GetPessoaById(int id)
        {
            var pessoa = await service.GetPessoaByIdAsync(id);
            return pessoa is null ? NotFound("Pessoa não Encontrada") : Ok(pessoa);
        }
        [HttpPut]
        public async Task<ActionResult<bool>> AddPessoa(Pessoa pessoa)
        {
            var concluido = await service.AddPessoaAsync(pessoa);
            return concluido == false ? BadRequest() : Created();
        }
    }
}
