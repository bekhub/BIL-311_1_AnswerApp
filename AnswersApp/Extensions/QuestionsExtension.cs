using System.Linq;
using AnswersApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AnswersApp.Extensions
{
    public static class QuestionsExtension
    {
        public static Question ById(this DbSet<Question> db, int id)
        {
            var question = db.FirstOrDefault(x => x.Id == id);
            return question;
        }
    }
}