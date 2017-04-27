using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;  
using System.ComponentModel.DataAnnotations.Schema;  
  
namespace AngularDemo.Domain  
{  
    public class Blog : Entity
    {  
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
        public int CategoryId { get; set; }  

        public virtual BlogCategory Category { get; set; }
        public List<Comment> Comments { get; set; }
    }  
}   