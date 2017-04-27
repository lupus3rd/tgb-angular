using System.ComponentModel.DataAnnotations;  
using System.ComponentModel.DataAnnotations.Schema;  
  
namespace AngularDemo.Domain  
{  
    public class Comment : Entity
    {
       public string Content { get; set; }
    
    }  
}   