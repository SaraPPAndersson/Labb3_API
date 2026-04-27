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
        [HttpGet("GetInterestById/{userId}")]
        public async Task<ActionResult<IEnumerable<GetInterestResponse>>> GetInterestById(int userId)
        {
            var user = await _ctx.Users
                .AsNoTracking()
                .Where(u => u.Id == userId)
                 .Select(u => new
                 {
                     u.Id,
                     u.FullName,
                     Interests = u.UserInterests
                .Select(ui => new GetInterestResponse(
                    ui.InterestId,
                    ui.Interest.Title,
                    ui.Interest.Description
                ))
                //.ToList()
                 })
                .FirstOrDefaultAsync();

            if (user is null)
            {
                return NotFound($"User med id {userId} kunde inte hittas");
            }
            return Ok(user);
        }

        //Get link by user's id
        [HttpGet("GetLinkById/{userId}")]
        public async Task<ActionResult<IEnumerable<GetLinkResponse>>> GetLinkById(int userId)
        {
            var user = await _ctx.Users
                .AsNoTracking()
                .Where(u => u.Id == userId)
                 .Select(u => new
                 {
                     u.Id,
                     u.FullName,
                     Link = u.UserInterests.SelectMany(ui => ui.Links).Select(ui => new GetLinkResponse
                     (
                         ui.Id,
                         ui.Url

                     ))
                     .ToList()
                 })
                .FirstOrDefaultAsync();

            if (user is null)
            {
                return NotFound($"User med id {userId} kunde inte hittas");
            }
            return Ok(user);
        }

        [HttpPost("AddInterestInUser/{id}")]
        public async Task<IActionResult> AddUsersInterest(int id, AddInterestToUserRequest request)
        {
            var userToUpdate = await _ctx.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (userToUpdate is null)
            {
                return NotFound("User hittades inte");
            }

            var interest = await _ctx.Interests.FirstOrDefaultAsync(i => i.Id == request.interestId);
            if (interest is null)
            {
                return NotFound("Interest hittades inte");
            }

            var interestToAdd = new UserInterest
            {
                InterestId = request.interestId,
                UserId = id
            };

            if (await _ctx.UserInterests.AnyAsync(ui => ui.UserId == id && ui.InterestId == request.interestId))
            {
                return BadRequest("User har redan detta intresse");
            }

            await _ctx.UserInterests.AddAsync(interestToAdd);
            await _ctx.SaveChangesAsync();

            return Ok(interestToAdd);
        }
        //Add new link
        [HttpPost("AddNewLink")]
        public async Task<IActionResult> AddLink(AddLinkRequest request)
        {
            //prevent empty link cell
            if (string.IsNullOrWhiteSpace(request.url))
            {
                return BadRequest("Url får inte vara tom");
            }
            //Check if existing user
            var user = await _ctx.Users.FirstOrDefaultAsync(u => u.Id == request.userId);
            if (user is null)
            {
                return NotFound("User hittades inte");
            }
            //Check existing interest
            var interest = await _ctx.Interests.FirstOrDefaultAsync(i => i.Id == request.interestId);
            if (interest is null)
            {
                return NotFound("Interest hittades inte");
            }

            //Check if user already has this interest
            var existingUserInterest = await _ctx.UserInterests.FirstOrDefaultAsync(ui => ui.UserId == request.userId && ui.InterestId == request.interestId);
            if (existingUserInterest is null)
            {
                existingUserInterest = new UserInterest
                {

                    UserId = request.userId,
                    InterestId = request.interestId
                };
                await _ctx.UserInterests.AddAsync(existingUserInterest);
                //await _ctx.SaveChangesAsync(); 

            }
            else if (await _ctx.Links.AnyAsync(l => l.UserInterestId == existingUserInterest.Id && l.Url == request.url))
            {
                return BadRequest("Länken finns redan, välj annan länk.");
            }
            //If no link, create new one
            var addNewLink = new Link
            {
                //UserInterestId = existingUserInterest.Id,
                Url = request.url
            };

            //await _ctx.Links.AddAsync(addNewLink);
            existingUserInterest.Links.Add(addNewLink);
            await _ctx.SaveChangesAsync();

            return Ok(addNewLink);

        }
    }
}
