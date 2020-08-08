using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShopAPI.Data;
using EShopAPI.Models;
using EShopAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EShopAPI.Controllers
{
    [Route("v1")]
    public class HomeController: Controller
    {
        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> GetHome([FromServices] DataContext context)
        {
            var employee = new User { Id=1, Username = "employee", Password = "employee123", Role = "employee" };
            var manager = new User { Id=2, Username = "manager", Password = "manager123", Role = "manager" };
            var category = new Category { Id=1, Title="Smartphones" };
            var product = new Product { Id=1, Title="iPhone X2", Price=999, Category=category, CategoryId=1, Description="World's most expansive phone ever" };

            context.Add(employee);
            context.Add(manager);
            context.Add(category);
            context.Add(product);

            await context.SaveChangesAsync();
            return Ok(new{ message = "Finished the first setup" });
        }
    }
}