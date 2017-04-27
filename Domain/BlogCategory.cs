using System.ComponentModel.DataAnnotations;  
using System.ComponentModel.DataAnnotations.Schema;  
  
namespace AngularDemo.Domain  
{  
    public class BlogCategory : Entity
    {  

        [Required]
        public string Name { get; set; }

    }  
}   