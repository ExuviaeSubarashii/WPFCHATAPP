using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Dtos
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string? Password { get; set; }
        public string? HasPassword { get; set; }
        public byte[]? Image { get; set; }
        public string? Server { get; set; }
    }
}
