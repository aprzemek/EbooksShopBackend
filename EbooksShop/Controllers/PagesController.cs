using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EbooksShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EbooksShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        private readonly EbooksShopContext context;

        public PagesController(EbooksShopContext context)
        {
            this.context = context;
        }

        // GET /api/pages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Page>>> Get()
        {
            return await context.Pages.OrderBy(x => x.Id).ToListAsync();
        }

        // POST /api/pages
        [HttpPost]
        public async Task<ActionResult<Page>> Create(Page page)
        {
            context.Pages.Add(page);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(Create), page);
        }

        // PUT /api/pages/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Page>> Update(int id, Page page)
        {
            if (id != page.Id)
            {
                return BadRequest();
            }

            context.Entry(page).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE /api/pages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Page>> Delete(int id)
        {
            var page = await context.Pages.FindAsync(id);

            context.Pages.Remove(page);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
