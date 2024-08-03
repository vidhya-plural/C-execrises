using LocalGym.Data;
using LocalGym.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalGym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainersController : ControllerBase
    {
        private readonly LocalGymRepository _repository;

        public TrainersController(LocalGymRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trainer>>> GetTrainers()
        {
            return Ok(await _repository.GetTrainersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Trainer>> GetTrainer(int id)
        {
            var trainer = await _repository.GetTrainerAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }
            return Ok(trainer);
        }

        [HttpPost]
        public async Task<ActionResult<Trainer>> PostTrainer(Trainer trainer)
        {
            var createdTrainer = await _repository.CreateTrainerAsync(trainer);
            return CreatedAtAction(nameof(GetTrainer), new { id = createdTrainer.TrainerId }, createdTrainer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainer(int id, Trainer trainer)
        {
            if (id != trainer.TrainerId)
            {
                return BadRequest();
            }

            var updatedTrainer = await _repository.UpdateTrainerAsync(trainer);
            return Ok(updatedTrainer);
        }
    }
}
