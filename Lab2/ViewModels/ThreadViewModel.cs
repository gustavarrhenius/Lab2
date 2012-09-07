﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lab2.Models.Entities;

namespace Lab2.ViewModels
{
    public class ThreadViewModel
    {
        public ForumThread Thread { get; set; }
        public List<ForumThread> Threads { get; set; }
        public List<Post> Posts { get; set; }
        public Post post { get; set; }
        public ThreadViewModel()
        {
            post = new Post();
        }
    }
}