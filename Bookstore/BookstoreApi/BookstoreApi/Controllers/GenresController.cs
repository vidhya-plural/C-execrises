using BookstoreApi.Data;
using BookstoreApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly BookstoreContext _context;

    public GenresController(BookstoreContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GenreDTO>>> GetGenres()
    {
        var genres = await _context.Genres
            .Select(g => new GenreDTO
            {
                genre_id = g.genre_id,
                genre_name = g.genre_name
            })
            .ToListAsync();

        return Ok(genres);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GenreDTO>> GetGenre(int id)
    {
        var genre = await _context.Genres
            .Where(g => g.genre_id == id)
            .Select(g => new GenreDTO
            {
                genre_id = g.genre_id,
                genre_name = g.genre_name
            })
            .FirstOrDefaultAsync();

        if (genre == null)
        {
            return NotFound();
        }

        return Ok(genre);
    }

    [HttpPost]
    public async Task<ActionResult<GenreDTO>> PostGenre(GenreDTO genreDTO)
    {
        var genre = new Genre
        {
            genre_name = genreDTO.genre_name
        };

        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();

        genreDTO.genre_id = genre.genre_id;

        return CreatedAtAction(nameof(GetGenre), new { id = genreDTO.genre_id }, genreDTO);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GenreDTO>> PutGenre(int id, GenreDTO genreDTO)
    {
        if (id != genreDTO.genre_id)
        {
            return BadRequest("Genre ID mismatch");
        }

        try
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound("Genre not found");
            }

            genre.genre_name = genreDTO.genre_name;

            _context.Entry(genre).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Return the updated genre as part of the response
            var updatedGenreDTO = new GenreDTO
            {
                genre_id = genre.genre_id,
                genre_name = genre.genre_name
            };

            return Ok(updatedGenreDTO);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!GenreExists(id))
            {
                return NotFound("Genre not found");
            }
            else
            {
                throw;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the genre.");
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre == null)
        {
            return NotFound("Genre not found");
        }

        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool GenreExists(int id)
    {
        return _context.Genres.Any(e => e.genre_id == id);
    }
}
