using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Context
{
    public class OrganizadorContext : DbContext
    {
        public OrganizadorContext(DbContextOptions<OrganizadorContext> options) 
            : base(options)
        {
        }

        // DbSet que representa a tabela de tarefas no banco de dados
        public DbSet<Tarefa> Tarefas { get; set; }
    }
}