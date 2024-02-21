using Background.Model;
using MongoDB.Bson;

namespace Background.DTOS
{
    public class TodoDto
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public bool Completed { get; set; }
        public DateTime DeadLine { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public static implicit operator Todo(TodoDto dto)
           => new()
           {
               Id = ObjectId.Parse(dto.Id),
               Completed = dto.Completed,
               Title = dto.Title,
               DeadLine = dto.DeadLine,
               Email = dto.Email,
               PhoneNumber = dto.PhoneNumber,
           };
        public static implicit operator TodoDto(Todo dto)
            => new()
            {
                Id = dto.Id.ToString(),
                Title = dto.Title,
                Completed = dto.Completed,
                DeadLine = dto.DeadLine,
                Email = dto.Email
            };
    }
}