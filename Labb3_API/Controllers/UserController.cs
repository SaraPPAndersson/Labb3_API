using Labb3_API.Models;
using Labb3_API.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Labb3_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly InterestDbContext _ctx;

        public UserController(InterestDbContext ctx)
        {
            _ctx = ctx;
        }
        //Get all user with information
        [HttpGet("GetAllUser")]
        public async Task<ActionResult<IEnumerable<GetUserResponse>>> GetAll()
        {
            return Ok(await _ctx.Users
                .AsNoTracking()
                .Select(u => new GetUserResponse(
                    u.Id,
                    u.FullName,
                    u.Email,
                    u.Phone
                    ))
                .ToListAsync());
        }

        //Get interest by user's id
        [HttpGet("GetInterestById/{id}")]
        public async Task<ActionResult<User>> GetInterestById(int id)
        {
            var user = await _ctx.Users
                .AsNoTracking()
                .Where(u => u.Id == id)
                 .Select(u => new
                 {
                     u.Id,
                     u.FullName,
                     Interest = u.Links.Select(l => new
                     {
                         l.InterestId,
                         l.Interest.Name,
                         l.Interest.Description
                     })
                 })
                .FirstOrDefaultAsync();

            if (user is null)
            {
                return NotFound($"User med id {id} kunde inte hittas");
            }
            return Ok(user);
        }

        //Get link by user's id
        [HttpGet("GetLinkById/{id}", Name ="GetLinkByUserId")]
        public async Task<ActionResult<User>> GetLinkById(int id)
        {
            var user = await _ctx.Users
                .AsNoTracking()
                .Where(u => u.Id == id)
                 .Select(u => new
                 {
                     u.Id,
                     u.FullName,
                     Link = u.Links.Select(l => new
                     {
                         l.Id,
                         l.Url
                     })
                 })
                .FirstOrDefaultAsync();

            if (user is null)
            {
                return NotFound($"User med id {id} kunde inte hittas");
            }
            return Ok(user);
        }

        [HttpPost("AddInterestInUser/{id}")]
        public async Task<IActionResult>AddUsersInterest(int id, int interestId)
        {
            var userToUpdate = await _ctx.Users.FirstOrDefaultAsync(u => u.Id == id);
            var interest = await _ctx.Interests.FirstOrDefaultAsync(i => i.Id == interestId);
            if (userToUpdate is null)
            {
                return NotFound("User hittades inte");
            }
            if (interest is null)
            {
                return NotFound("Interest hittades inte");
            }

            var linksToAdd = new Link
            {
                InterestId = interestId,
                UserId = id
            };

            if(await _ctx.Links.AnyAsync(l => l.UserId == id && l.InterestId == interestId))
            {
                return BadRequest("User har redan detta intresse");
            }

            await _ctx.Links.AddAsync(linksToAdd);
            await _ctx.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInterestById), new { id = linksToAdd.Id }, linksToAdd); 
        }
        //Add new link
        [HttpPost("AddNewLink")]
        public async Task<IActionResult> AddLink(int userId, int interestId, string url)
        {
            //prevent empty link 
            if (string.IsNullOrWhiteSpace(url))
            {
                return BadRequest("Url får inte vara tom");
            }
            //Check if existing user
            var user = await _ctx.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user is null)
            {
                return NotFound("User hittades inte");
            }
            //Check existing interest
            var interest = await _ctx.Interests.FirstOrDefaultAsync(i => i.Id == interestId);
            if (user is null)
            {
                return NotFound("Interest hittades inte");
            }
            //Check if there's link for user and interest
            if (await _ctx.Links.AnyAsync(l => l.UserId == userId && l.InterestId == interestId && l.Url == url))
            {
                return BadRequest("Länken finns redan");
            }
            //Check if user already has this interest
            var existingLink = await _ctx.Links.FirstOrDefaultAsync(l => l.UserId == userId && l.InterestId == interestId);
            if(existingLink != null)
            {
                //If a link already exist for this interest, not allow another one
                if(!string.IsNullOrWhiteSpace(existingLink.Url))
                {
                    return BadRequest("Det finns redan en länk för detta intresse");
                }
                //If url is missing, update on existing row instead of creating new row with same user and interest matching
                existingLink.Url = url;
                await _ctx.SaveChangesAsync();
                return Ok(existingLink);
            }
            //If no link, create new one
            var addNewLink = new Link
            {
                UserId = userId,
                InterestId = interestId,
                Url = url
            };

            await _ctx.Links.AddAsync(addNewLink);
            await _ctx.SaveChangesAsync();

            return Ok(addNewLink);

        }
}
}
