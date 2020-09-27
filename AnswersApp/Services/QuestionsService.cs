using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnswersApp.Data;
using AnswersApp.Exceptions;
using AnswersApp.Extensions;
using AnswersApp.Models;
using AnswersApp.ViewModels.Questions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AnswersApp.Services
{
    public class QuestionsService
    {
        private AnswersContext Context { get; }

        public QuestionsService(AnswersContext context)
        {
            Context = context;
        }

        public IEnumerable<Question> List()
        {
            return Context.Questions;
        }

        public IEnumerable<IndexPage> IndexPage()
        {
            var questions = Context.Questions.Join(
                Context.Answers,
                q => q.AnswerId,
                a => a.Id,
                (q, a) => new IndexPage
                {
                    Id = q.Id,
                    Text = q.Text,
                    Answer = a.Text
                });
            return questions;
        }

        public AddPage<SelectListItem> AddPage(int? answerId)
        {
            var answers = Context.Answers.Select(x => new SelectListItem
            {
                Text = x.Text,
                Value = x.Id.ToString()
            }).ToList();
            answers.Insert(0, new SelectListItem{Text = "Choose answer"});
            return new AddPage<SelectListItem>
            {
                AnswerId = answerId,
                Answers = answers
            };
        }

        public EditPage<SelectListItem> EditPage(int id)
        {
            var answers = Context.Answers.Select(x => new SelectListItem
            {
                Text = x.Text,
                Value = x.Id.ToString()
            }).ToList();
            answers.Insert(0, new SelectListItem{Text = "Choose answer"});
            var question = Context.Questions.ById(id);
            return new EditPage<SelectListItem>
            {
                Id = id,
                Answers = answers,
                AnswerId = question.AnswerId,
                Text = question.Text
            };
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