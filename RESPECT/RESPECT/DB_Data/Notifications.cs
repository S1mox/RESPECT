using System;
using System.Collections.Generic;

namespace RESPECT.DB_Data
{
    public partial class Notifications
    {
        public int Id { get; set; }
        public int? IdSender { get; set; }
        public int? IdReceiver { get; set; }
        public int? IdRoom { get; set; }
        public int? Value { get; set; }
        public string Description { get; set; }
    }
}
