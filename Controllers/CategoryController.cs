using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using EShopAPI.Data;
using EShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EShopAPI.Controllers
{
    [ApiController]
    [Route("v1/categories")]
    public class CategoryController: ControllerBase
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        [ResponseCache(VaryByHeader="User-Agent", Location=ResponseCacheLocation.Any, Duration=30)]
        // [ResponseCache(Location=ResponseCacheLocation.None, Duration=0)] //Caso informação trazida pelo método não possa ficar cacheado, habilitar essa linha 
        public async Task<ActionResult<List<Category>>> GetCategories([FromServices] DataContext context)
        {
            var categories = await context.Categories.AsNoTracking().ToListAsync();
            return Ok(categories);
        }

        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Category>>> GetById(int id, [FromServices] DataContext context)
        {
            var categories = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
            return Ok(categories);
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles="employee")]
        public async Task<ActionResult<Category>> Post([FromBody] Category model, [FromServices] DataContext context)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                context.Categories.Add(model);
                await context.SaveChangesAsync();
                
                return Ok(model);
            }
            catch(Exception error)
            {
                return BadRequest(new { message = error.Message.ToString() });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles="employee")]
        public async Task<ActionResult<Category>> Put(int id, [FromBody]Category model, [FromServices] DataContext context)
        {
            if(!id.Equals(model.Id)) NotFound(new { message = "Category not found." });
            if(!ModelState.IsValid) return BadRequest(ModelState);
            
            try
            {
                context.Entry<Category>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (DbUpdateConcurrencyException error)
            {
                return BadRequest(new { message = error.Message });

            }catch (Exception error)
            {
                return BadRequest(new { message = error.Message });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles="employee")]
        public async Task<ActionResult<Category>> Delete(int id, [FromServices] DataContext context)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if(id.Equals(null)) return NotFound(new { message = "Category not found" });

            try
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return Ok(new { message = "Category successfully removed" });
            }
            catch(Exception error)
            {
                return BadRequest(new { message = error.Message });
            }
        }
    }
}