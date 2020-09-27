using System.ComponentModel.DataAnnotations;

namespace AnswersApp.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        public int AnswerId { get; set; }
        
        public Answer Answer { get; set; }
        
        [Required]
        [MinLength(3)]
        public string Text { get; set; }
    }
}