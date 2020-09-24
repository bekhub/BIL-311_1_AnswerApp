using System.Collections.Generic;
using System.Threading.Tasks;
using AnswersApp.Data;
using AnswersApp.Exceptions;
using AnswersApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AnswersApp.Services
{
    public class QuestionsService
    {
        private AnswersContext Context { get; set; }

        public QuestionsService(AnswersContext context)
        {
            Context = context;
        }

        public IEnumerable<Question> List()
        {
            return Context.Questions;
        }

        public async Task<Question> ById(int? id)
        {
            if (id == null) throw new InnerException("Question not found");
            var question = await Context.Questions.FirstOrDefaultAsync(a => a.Id == id);
            if(question == null) throw new InnerException("Question not found");
            return question;
        }

        public async Task Add(Question question)
        {
            Context.Add(question);
            await Context.SaveChangesAsync();
        }

        public async Task Edit(Question question)
        {
            Context.Questions.Update(question);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            if(id == null) throw new InnerException("Answer not found");
            var question = new Question {Id = id.Value};
            Context.Entry(question).State = EntityState.Deleted;
            await Context.SaveChangesAsync();
        }
    }
}