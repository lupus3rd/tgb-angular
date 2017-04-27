using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
using Microsoft.EntityFrameworkCore;  

namespace AngularDemo.Domain
{
 public class BlogContext:DbContext     
     {    
        public BlogContext(DbContextOptions<BlogContext> options):base(options)    
        {    
    
        }  
        public DbSet<Blog> Blog { get; set; }  
        public DbSet<BlogCategory> BlogCategory { get; set; } 
        public DbSet<Comment> Comment { get; set; }  

    }    
}
