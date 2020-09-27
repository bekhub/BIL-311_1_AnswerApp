using System.Collections.Generic;
using AnswersApp.Models;

namespace AnswersApp.ViewModels.Questions
{
    public class AddPage<T>
    {
        public int? AnswerId { get; set; }
        public IEnumerable<T> Answers { get; set; }
        
        public string Text { get; set; }
    }
}