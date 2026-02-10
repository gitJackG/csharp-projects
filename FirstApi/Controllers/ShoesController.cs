using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FirstApi.Models;
using FirstApi.Data;
using Microsoft.EntityFrameworkCore;

namespace FirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoesController : ControllerBase
    {
        //static private List<Shoe> shoes = new List<Shoe>
        //{
        //    new Shoe
        //    {
        //        Id = 1,
        //        Name = "Nike SB",
        //        Brand = "Nike",
        //        Price = 100
        //    },
        //    new Shoe
        //    {
        //        Id = 2,
        //        Name = "Nike Air Force 1",
        //        Brand = "Nike",
        //        Price = 110
        //    }
        //};

        private readonly FirstAPIContext _context;

        public ShoesController(FirstAPIContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Shoe>>> GetShoes()
        {
            return Ok(await _context.Shoes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Shoe>> GetShoeById(int id)
        {
            var shoe = await _context.Shoes.FindAsync(id);
            if (shoe == null)
            {
                return NotFound();
            }

            return Ok(shoe);
        }

        [HttpPost]
        public async Task<ActionResult<Shoe>> AddShoe(Shoe newShoe)
        {
            if (newShoe == null)
            {
                return BadRequest();
            }

            _context.Shoes.Add(newShoe);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShoeById), new { id = newShoe.Id }, newShoe);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShoe(int id, Shoe updatedShoe)
        {
            var shoe = await _context.Shoes.FindAsync(id);
            if (shoe == null)
            {
                return NotFound();
            }
            if (updatedShoe == null)
            {
                return BadRequest();
            }

            shoe.Id = updatedShoe.Id;
            shoe.Name = updatedShoe.Name;
            shoe.Brand = updatedShoe.Brand;
            shoe.Price = updatedShoe.Price;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoe(int id)
        {
            var shoe = await _context.Shoes.FindAsync(id);

            if (shoe == null)
            {
                return NotFound();
            }

            _context.Shoes.Remove(shoe);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
