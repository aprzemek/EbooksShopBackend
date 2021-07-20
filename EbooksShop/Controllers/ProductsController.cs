using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EbooksShop.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EbooksShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly EbooksShopContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductsController(EbooksShopContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET /api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get(int p = 1)
        {
            int pageSize = 4;
            var products = context.Products
                                    .OrderBy(x => x.Id)
                                    .Include(x => x.Category)
                                    .Skip((p - 1) * pageSize)
                                    .Take(pageSize);

            return await products.ToListAsync();
        }

        // GET /api/products/category
        [HttpGet("{slug}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetByCategory(string slug, int p = 1)
        {
            Category category = await context.Categories.Where(x => x.Slug == slug).FirstOrDefaultAsync();
            if (category == null) return NotFound();

            int pageSize = 4;
            var products = context.Products
                                    .OrderBy(x => x.Id)
                                    .Where(x => x.CategoryId == category.Id)
                                    .Skip((p - 1) * pageSize)
                                    .Take(pageSize);

            return await products.ToListAsync();
        }

        // GET /api/products/count/category
        [HttpGet("count/{slug}")]
        public async Task<ActionResult<int>> GetProductCount(string slug)
        {
            if (slug == "all")
            {
                return await context.Products.CountAsync();
            }

            Category category = await context.Categories.Where(x => x.Slug == slug).FirstOrDefaultAsync();
            if (category == null) return NotFound();

            return await context.Products.Where(x => x.CategoryId == category.Id).CountAsync();
        }

        // POST /api/products
        [HttpPost]
        public async Task<ActionResult<Product>> Create([FromForm] Product product)
        {
            string imageName = "noimage.png";
            if (product.ImageUpload != null)
            {
                string uploadsDir = Path.Combine(webHostEnvironment.WebRootPath, "media/products");
                imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
                string filePath = Path.Combine(uploadsDir, imageName);
                FileStream fs = new FileStream(filePath, FileMode.Create);
                await product.ImageUpload.CopyToAsync(fs);
                fs.Close();
            }

            product.Image = imageName;

            context.Products.Add(product);

            await context.SaveChangesAsync();

            return Ok();
        }

        // PUT /api/products
        [HttpPut]
        public async Task<ActionResult<Product>> Update([FromForm] Product product)
        {
            if (product.ImageUpload != null)
            {
                string uploadsDir = Path.Combine(webHostEnvironment.WebRootPath, "media/products");

                var currentImage = (from p in context.Products
                                    where p.Id == product.Id
                                    select p.Image).Single();

                if (!string.Equals(currentImage, "noimage.png"))
                {
                    string oldImagePath = Path.Combine(uploadsDir, currentImage);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
                string filePath = Path.Combine(uploadsDir, imageName);
                FileStream fs = new FileStream(filePath, FileMode.Create);
                await product.ImageUpload.CopyToAsync(fs);
                fs.Close();
                product.Image = imageName;
            }

            context.Entry(product).State = EntityState.Modified;
            if (product.ImageUpload == null)
            {
                context.Entry(product).Property("Image").IsModified = false;
            }

            await context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE /api/products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            var product = await context.Products.FindAsync(id);

            if (!string.Equals(product.Image, "noimage.png"))
            {
                string uploadsDir = Path.Combine(webHostEnvironment.WebRootPath, "media/products");
                string oldImagePath = Path.Combine(uploadsDir, product.Image);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            context.Products.Remove(product);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }


}
