using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EbooksShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EbooksShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly EbooksShopContext context;

        public OrdersController(EbooksShopContext context)
        {
            this.context = context;
        }

        // GET /api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            return await context.Orders.OrderBy(x => x.Id).ToListAsync();
        }

        // POST /api/orders
        [HttpPost]
        public async Task<ActionResult<Order>> Create([FromForm] Order order)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(Create), order);
        }
    }
}

