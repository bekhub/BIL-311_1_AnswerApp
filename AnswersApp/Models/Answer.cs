using System.ComponentModel.DataAnnotations;

namespace AnswersApp.Models
{
    public class Answer
    {
        public int Id { get; set; }
        
        [Required]
        [MinLength(3)]
        public string Text { get; set; }
    }
}