using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApiPessoa.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration.EnvironmentVariables;

namespace ApiPessoa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private static List<Pessoa> pessoas = new List<Pessoa>();

        private readonly ILogger<PessoaController> _logger;

        public PessoaController(ILogger<PessoaController> logger)
        {
            _logger = logger;
        }

        [HttpPost("adicionar")]
        public IActionResult AdicionarPessoa([FromBody] Pessoa pessoa)
        {
            var p = new Pessoa("Pedro", "25");
            pessoas.Add(p);
            try
            {

                using (StreamWriter sw = new StreamWriter("data_atual_log.txt", true))
                {
                    foreach (var pessoaUnica in pessoas)
                    {
                        sw.WriteLine($"Nome: {pessoaUnica.nome}, Idade: {pessoaUnica.idade}\n");
                    }
                }

                return CreatedAtAction("GetPessoa", pessoa);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro ao adicionar a pessoa: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                if (System.IO.File.Exists("data_atual_log.txt"))
                {
                    string conteudo = System.IO.File.ReadAllText("data_atual_log.txt");
                    return Ok(conteudo);
                }
                else
                {
                    return NotFound("O arquivo não foi encontrado.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro ao ler o arquivo: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
}
