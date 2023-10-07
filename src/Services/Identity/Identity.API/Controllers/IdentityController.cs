using Identity.API.DTOs;
using Identity.API.Helper;
using Identity.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Shared;
using Shared.Helper;
using System.Text.Json;


namespace Identity.API.Controllers
{
    [ApiController]
    public class IdentityController : ControllerBase
    {

        [HttpPost("token")]
        public async Task<IActionResult> GetToken([FromBody] UserDTO userDto)
        {
            string jsonContent = await UserJson.GetUserJson();
            var usersList = JsonSerializer.Deserialize<List<User>>(jsonContent);

            var user = usersList.Find(f => f.username == userDto.Username);

            if (user is null)
                return NotFound();

            if (!Encryption.Verify(userDto.Password, user.password))
                return BadRequest("username or password is wrong!");
            string token = Token.GenerateToken(user);

            return Ok(new TokenDTO { Value = token });
        }

        [HttpPost("revoke")]
        public async Task<IActionResult> addToBlackList(IDistributedCache cache, [FromBody] TokenDTO tokenDto)
        {
            var cachedData = await cache.GetStringAsync($"Token_{tokenDto.Value}");

            if (cachedData is null)
            {
                await cache.SetStringAsync($"Token_{tokenDto.Value}", tokenDto.Value, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2) });
            }

            return Ok("added to blackList!");
        }
    }
}
