using System.ComponentModel.DataAnnotations;

namespace TodoListASP.Models
{
    public class TodoListItem
    {
        public int Id { get; set; }
        public DateTime AddDate { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Заголовок должен содержать не менее двух символов!")]
        [MaxLength(200, ErrorMessage = "Заголовок должен содержать не более 200 символов!")]
        public string Title { get; set; }
        public bool IsDone { get; set; }
    }
}
