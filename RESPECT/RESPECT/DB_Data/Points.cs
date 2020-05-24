using System;
using System.Collections.Generic;

namespace RESPECT.DB_Data
{
    public partial class Points
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public int Value { get; set; }
    }
}
