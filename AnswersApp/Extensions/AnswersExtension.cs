using System.Linq;
using AnswersApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AnswersApp.Extensions
{
    public static class AnswersExtension
    {
        public static Answer ById(this DbSet<Answer> db, int id)
        {
            var answer = db.FirstOrDefault(x => x.Id == id);
            return answer;
        }
    }
}