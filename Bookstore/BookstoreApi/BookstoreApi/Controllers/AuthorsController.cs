using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookstoreApi.Data;
using BookstoreApi.Models;
using BookstoreApi.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookstoreContext _context;

        public AuthorsController(BookstoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthors()
        {
            try
            {
                var authors = await _context.Authors
                    .Select(a => new AuthorDTO
                    {
                        author_id = a.author_id,
                        name = a.name,
                        biography = a.biography
                    })
                    .ToListAsync();

                return Ok(authors);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching authors: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error fetching authors");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTO>> GetAuthor(int id)
        {
            try
            {
                var author = await _context.Authors
                    .Where(a => a.author_id == id)
                    .Select(a => new AuthorDTO
                    {
                        author_id = a.author_id,
                        name = a.name,
                        biography = a.biography
                    })
                    .FirstOrDefaultAsync();

                if (author == null)
                {
                    return NotFound();
                }

                return Ok(author);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching author {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error fetching author");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDTO>> PostAuthor(AuthorDTO authorDTO)
        {
            try
            {
                var author = new Author
                {
                    name = authorDTO.name,
                    biography = authorDTO.biography
                };

                _context.Authors.Add(author);
                await _context.SaveChangesAsync();

                authorDTO.author_id = author.author_id;

                return CreatedAtAction(nameof(GetAuthor), new { id = authorDTO.author_id }, authorDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating author: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating author");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorDTO>> PutAuthor(int id, AuthorDTO authorDTO)
        {
            if (id != authorDTO.author_id)
            {
                return BadRequest("Author ID mismatch");
            }

            try
            {
                var author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    return NotFound("Author not found");
                }

                author.name = authorDTO.name;
                author.biography = authorDTO.biography;

                _context.Entry(author).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                // Return the updated author as part of the response
                var updatedAuthorDTO = new AuthorDTO
                {
                    author_id = author.author_id,
                    name = author.name,
                    biography = author.biography
                };

                return Ok(updatedAuthorDTO);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
                {
                    return NotFound("Author not found");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the author.");
            }
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    return NotFound();
                }

                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting author {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting author");
            }
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.author_id == id);
        }
    }
}
