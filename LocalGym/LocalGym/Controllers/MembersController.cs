using LocalGym.Data;
using LocalGym.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalGym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly LocalGymRepository _repository;

        public MembersController(LocalGymRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            return Ok(await _repository.GetMembersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
            var member = await _repository.GetMemberAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }

        [HttpPost]
        public async Task<ActionResult<Member>> PostMember(Member member)
        {
            var createdMember = await _repository.CreateMemberAsync(member);
            return CreatedAtAction(nameof(GetMember), new { id = createdMember.MemberId }, createdMember);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember(int id, Member member)
        {
            if (id != member.MemberId)
            {
                return BadRequest();
            }

            var updatedMember = await _repository.UpdateMemberAsync(member);
            return Ok(updatedMember);
        }
    }
}
