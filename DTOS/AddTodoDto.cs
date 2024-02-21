using Background.Model;
using MongoDB.Bson;

namespace Background.DTOS
{
    public class AddTodoDto
    {
        public string Title { get; set; } = string.Empty;
        public bool Completed { get; set; }
        public DateTime DeadLine { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public static implicit operator Todo(AddTodoDto dto)
       => new()
       {
           Title = dto.Title,
           DeadLine = dto.DeadLine,
           Email = dto.Email,
           PhoneNumber = dto.PhoneNumber,
       };
    }
}