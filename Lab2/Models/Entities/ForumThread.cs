using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lab2.Models.Entities.Abstract;

namespace Lab2.Models.Entities
{
    public class ForumThread : IEntity
    {

        public ForumThread() { ID = Guid.NewGuid(); }
        
        public Guid ID { get; set; }
        
        public string Title { get; set; }

        public DateTime CreateDate { get; set; }

        public ForumThread(string title)
        {
            ID = Guid.NewGuid();
            Title = title;
            CreateDate = DateTime.UtcNow;
        }

    }
}