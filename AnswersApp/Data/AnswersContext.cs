using AnswersApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AnswersApp.Data
{
    public class AnswersContext: DbContext
    {
        public AnswersContext(DbContextOptions<AnswersContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        
        public DbSet<Answer> Answers { get; set; }

        public DbSet<Question> Questions { get; set; }
    }
}