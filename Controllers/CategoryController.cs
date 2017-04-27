using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Http;  
using Microsoft.AspNetCore.Mvc;  
using AngularDemo.Domain;  
using AngularDemo.DTO;  
using Microsoft.EntityFrameworkCore;  
  
namespace AngularDemo.Controllers  
{  
    [Produces("application/json")]  
    [Route("api/categories")]  
    public class CategoryController : Controller  
    {  
        private readonly BlogContext _context;  
  
        public CategoryController(BlogContext context)  
        {  
            _context = context;  
        }  
        // GET: api/categories  
        [HttpGet]  
        public async Task<IActionResult> Get()  
        {  
            List<BlogCategory> BlogCategory_ = new List<BlogCategory>();  
            var BlogCategory = await (from data in _context.BlogCategory  
                                 select new  
                                 {  
                                     BlogCategoryId = data.Id,  
                                     BlogCategoryName = data.Name  
                                 }).ToListAsync();  
            BlogCategory.ForEach(x =>  
            {  
                BlogCategory pro = new BlogCategory();  
                pro.Id = x.BlogCategoryId;  
                pro.Name = x.BlogCategoryName;  
                BlogCategory_.Add(pro);  
            });  
  
  
            return Json(BlogCategory_);  
        }  
  
         
         
    }  
}   
