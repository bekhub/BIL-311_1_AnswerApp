using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnswersApp.Data;
using AnswersApp.Exceptions;
using AnswersApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AnswersApp.Services
{
    public class AnswersService
    {
        private AnswersContext Context { get; }
        public AnswersService(AnswersContext context)
        {
            Context = context;
        }

        public IEnumerable<Answer> List()
        {
            return Context.Answers;
        }

        public async Task<Answer> ById(int? id)
        {
            if (id == null) throw new InnerException("Answer not found");
            var answer = await Context.Answers.FirstOrDefaultAsync(a => a.Id == id);
            if(answer == null) throw new InnerException("Answer not found");
            return answer;
        }

        public async Task Add(Answer answer)
        {
            Context.Add(answer);
            await Context.SaveChangesAsync();
        }

        public async Task Edit(Answer answer)
        {
            Context.Answers.Update(answer);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            if(id == null) throw new InnerException("Answer not found");
            var answer = new Answer {Id = id.Value};
            Context.Entry(answer).State = EntityState.Deleted;
            await Context.SaveChangesAsync();
        }

        public (Answer, string) AnswerForQuestion(string question)
        {
            if (question == null) return (null, null);
            var words = question.Split();
            var answers = from i in Context.Answers.ToList().Where(x => words.Any(x.Text.Contains))
                orderby (from j in words 
                        select i.Text
                            .Split()
                            .Count(x => x == j)).Sum() 
                    descending
                select i;
            return (answers.First(), question);
        }
    }
}