using Microsoft.AspNetCore.Http;
using Backend.DTOs.Total;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TotalController(ITotalService service) : ControllerBase
    {
        [HttpGet("Pessoas")]
        //Retorna uma lista com todas as pessoas, sua receita, sua despesa e seu saldo.
        public async Task<ActionResult<List<TotalPessoaResponse>>> GetAllPessoasTotal()
            => Ok(await service.GetAllPessoaTotal());
        [HttpGet("Pessoa/{id}")]
        //Retorna a receita,despesa e saldo de uma pessoa, pelo ID dela.
        public async Task<ActionResult<TotalPessoaResponse>> GetPessoaTotalById(int id)
        {
            var totalpessoa = await service.GetPessoaTotalById(id);
            return totalpessoa is null ? NotFound("Pessoa não encontrada!") : Ok(totalpessoa);
        }
            
        [HttpGet("Geral")]
        //Retorna as Receitas, Despesas e o Saldo Geral de todos.
        public async Task<ActionResult<TotalGeralResponse>> GetTotalGeral()
            => Ok(await service.GetTotalGeral());
    }
}
