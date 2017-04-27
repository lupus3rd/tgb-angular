using AngularDemo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularDemo.DTO
{
   public class BlogDetails
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
        public string Category { get; set; }
       // public List<Comment> Comments { get; set; }
        


    }
}
