using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;
using System.Linq;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        // Obter tarefa por ID
        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var tarefa = _context.Tarefas.Find(id);

            if (tarefa == null)
            {
                return NotFound(); // Retorna 404 se a tarefa não for encontrada
            }

            return Ok(tarefa); // Retorna 200 OK com a tarefa encontrada
        }

        // Obter todas as tarefas
        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var tarefas = _context.Tarefas.ToList(); // Busca todas as tarefas
            return Ok(tarefas); // Retorna 200 OK com a lista de tarefas
        }

        // Obter tarefas por título
        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            var tarefas = _context.Tarefas
                .Where(x => x.Titulo.Contains(titulo)) // Filtra tarefas que contenham o título informado
                .ToList();

            return Ok(tarefas); // Retorna 200 OK com as tarefas encontradas
        }

        // Obter tarefas por data
        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
            return Ok(tarefa); // Retorna 200 OK com as tarefas encontradas
        }

        // Obter tarefas por status
        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            var tarefa = _context.Tarefas.Where(x => x.Status == status);
            return Ok(tarefa); // Retorna 200 OK com as tarefas encontradas
        }

        // Criar nova tarefa
        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
            {
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });
            }

            _context.Tarefas.Add(tarefa); // Adiciona a tarefa ao DbContext
            _context.SaveChanges(); // Salva as mudanças no banco de dados

            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa); // Retorna 201 Created com a tarefa criada
        }

        // Atualizar tarefa existente
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
            {
                return NotFound(); // Retorna 404 se a tarefa não for encontrada
            }

            if (tarefa.Data == DateTime.MinValue)
            {
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });
            }

            // Atualiza as propriedades da tarefa
            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;

            _context.SaveChanges(); // Salva as mudanças no banco de dados

            return Ok(tarefaBanco); // Retorna 200 OK com a tarefa atualizada
        }

        // Deletar tarefa
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
            {
                return NotFound(); // Retorna 404 se a tarefa não for encontrada
            }

            _context.Tarefas.Remove(tarefaBanco); // Remove a tarefa do DbContext
            _context.SaveChanges(); // Salva as mudanças no banco de dados

            return NoContent(); // Retorna 204 No Content (sucesso sem conteúdo)
        }
    }
}