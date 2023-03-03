﻿using System;
using System.Collections.Generic;

namespace Chat.Domain.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public string SenderName { get; set; } = null!;
        public DateTime SenderTime { get; set; }
        public string Message1 { get; set; } = null!;
        public string Server { get; set; } = null!;
        public string Channel { get; set; } = null!;

        public string ImageDir { get; set; } = null!;
    }
}
