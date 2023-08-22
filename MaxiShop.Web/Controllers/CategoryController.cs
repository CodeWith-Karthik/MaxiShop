using MaxiShop.Web.Data;
using MaxiShop.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaxiShop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult Get()
        {
            var categories = _dbContext.Category.ToList();
            return Ok(categories);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("Details")]
        public ActionResult Get(int id)
        {
            var category = _dbContext.Category.FirstOrDefault(x=>x.Id == id);

            if(category == null)
            {
                return NotFound($"Category not found for Id - {id}");
            }

            return Ok(category);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult Create([FromBody]Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _dbContext.Category.Add(category);
            _dbContext.SaveChanges();
            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public ActionResult Update([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _dbContext.Category.Update(category);
            _dbContext.SaveChanges();
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var category = _dbContext.Category.FirstOrDefault(x => x.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            _dbContext.Category.Remove(category);
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}
