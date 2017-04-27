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
    [Route("api/Blog")]  
    public class BlogController : Controller  
    {  
        private readonly BlogContext _context;  
  
        public BlogController(BlogContext context)  
        {  
            _context = context;  
        }

        [HttpGet]
        public async Task<IActionResult> BlogList()
        {
            List<BlogDetails> ilIst = new List<BlogDetails>();
            var listData = await (from Bl in _context.Blog
                                  select new
                                  {
                                      Bl.Id,
                                      Bl.Title,
                                      Bl.CreatedOn,
                                      Bl.Content,
                                      Bl.Category

                                  }
                          ).ToListAsync();

            listData.ForEach(x =>
            {
                BlogDetails Obj = new BlogDetails();
                Obj.Id = x.Id;
                Obj.Title = x.Title;
                Obj.CreatedOn = x.CreatedOn;
                Obj.Content = x.Content;
                Obj.Category = x.Category.Name;
                ilIst.Add(Obj);
            });

            return Json(ilIst);
        }

        [HttpPost]
        public IActionResult AddBlog([FromBody]Blog BlObj)
        {
            BlObj.CreatedOn = DateTime.Now;
            _context.Blog.Add(BlObj);
            _context.SaveChanges();
            return Json("OK");
        }

        [HttpGet("{BlId}")]
        public async Task<IActionResult> BlogDetails(int BlId)
        {
            var BlDeatils = await (from Bl in _context.Blog
                                    where Bl.Id == BlId
                                    select new
                                    {
                                        Bl.Id,
                                        Bl.Title,
                                        Bl.CreatedOn,
                                        Bl.Content,
                                        Bl.CategoryId,
                                        Bl.Category.Name,
                                        Bl.Comments

                                    }
                          ).FirstAsync();

            return Json(BlDeatils);
        }

        [HttpPut]
        public IActionResult EditBlog([FromBody]Blog BlData)
        {
             BlData.CreatedOn = DateTime.Now;
            _context.Entry(BlData).State = EntityState.Modified;
            _context.SaveChanges();
            return Json("ok");
        }

        [HttpDelete]
        public IActionResult RemoveBlogDetails([FromBody]int BlId)
        {
            Blog Bl;
            Bl = _context.Blog.Where(x => x.Id == BlId).First();
            _context.Blog.Remove(Bl);
            _context.SaveChanges();
            return Json("OK");
        }

    }
}   