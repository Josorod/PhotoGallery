﻿using WebApi.Models;

namespace WebApi.Data.Entities
{
    public class Like : BaseEntity<int>
    {
        public int PhotoId { get; set; }
        public virtual Photo Photo { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
