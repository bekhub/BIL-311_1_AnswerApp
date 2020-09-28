using System;
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

        public (string, string) AnswerForQuestion(string question)
        {
            if (question == null) return (null, null);
            var words = question.Split();
            var questions = from i in Context.Questions
                    .Include(q => q.Answer)
                    .ToList()
                    .Where(x => words.Any(o => x.Text.Contains(o,
                        StringComparison.CurrentCultureIgnoreCase)))
                orderby (from j in words 
                        select i.Text
                            .Split()
                            .Count(x => string.Equals(x, j, 
                                StringComparison.CurrentCultureIgnoreCase))).Sum() 
                    descending
                select i;
            if (questions.Any()) return (questions.First().Answer.Text, question);
            var answers = from i in Context.Answers.ToList()
                    .Where(x => words.Any(o => x.Text.Contains(o, 
                        StringComparison.CurrentCultureIgnoreCase)))
                orderby (from j in words 
                        select i.Text
                            .Split()
                            .Count(x => string.Equals(x, j, 
                                StringComparison.CurrentCultureIgnoreCase))).Sum() 
                    descending
                select i;
            return answers.Any() 
                ? (answers.First().Text, question) 
                : ("Sorry, but I don't have this question.", question);
        }
    }
}