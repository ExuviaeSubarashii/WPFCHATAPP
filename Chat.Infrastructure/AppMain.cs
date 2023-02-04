using Chat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure
{
    public class AppMain
    {
        public static User User { get; set; }
        public static Message Message { get; set; }
        public static Server Servers { get; set; }

    }
}
