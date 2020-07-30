using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    /// <summary>
    /// An controller to perform CRUD operations on Character
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;

        }

        /// <summary>
        /// Get all characters.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /character/getall
        ///
        /// Sample response:
        ///
        ///     {
        ///        "data": [
        ///            {
        ///                "id": 1,
        ///                "name": "Muchlis Adhi",
        ///                "hitPoints": 100,
        ///                "strength": 50,
        ///                "defense": 30,
        ///                "intelligence": 70,
        ///                "class": 1,
        ///                "weapon": null,
        ///                "skills": [],
        ///                "fights": 1,
        ///                "victories": 0,
        ///                "defeats": 0
        ///            },
        ///            {
        ///                "id": 2,
        ///                "name": "Jojo",
        ///                "hitPoints": 100,
        ///                "strength": 15,
        ///                "defense": 20,
        ///                "intelligence": 30,
        ///                "class": 1,
        ///                "weapon": null,
        ///                "skills": [],
        ///                "fights": 1,
        ///                "victories": 1,
        ///                "defeats": 0
        ///            }
        ///        ],
        ///        "success": true,
        ///        "message": null
        ///     }
        ///
        /// Sample response header:
        /// 
        ///     content-type: application/json; charset=utf-8
        ///     Authorization: Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJXaXJhdGFtYSIsIm5iZiI6MTU5NjEwMTQ3OSwiZXhwIjoxNTk2MTg3ODc5LCJpYXQiOjE1OTYxMDE0Nzl9.QiVo-mXgGFjYPSTS9jW_yxiZzDtMZLow-fSshYUgwgv7ZlyScEGzwnmdyACfNsW17jEP4ZXPXmweqIJsATpC4g 
        ///     location: http://localhost:5000/character/getall 
        ///
        /// </remarks>
        /// <response code="200">Returns all the character.</response>
        /// <response code="401">If Unauthorized User</response>
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        /// <summary>
        /// Gets a Character by ID
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     GET /character/1
        ///
        /// Sample response:
        ///
        ///     {
        ///        "data": {
        ///            "id": 1,
        ///            "name": "Muchlis Adhi",
        ///            "hitPoints": 100,
        ///            "strength": 50,
        ///            "defense": 30,
        ///            "intelligence": 70,
        ///            "class": 1,
        ///            "weapon": null,
        ///            "skills": [],
        ///            "fights": 1,
        ///            "victories": 0,
        ///            "defeats": 0
        ///        },
        ///        "success": true,
        ///        "message": null
        ///     }
        ///
        /// Sample response header:
        /// 
        ///     content-type: application/json; charset=utf-8
        ///     Authorization: Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJXaXJhdGFtYSIsIm5iZiI6MTU5NjEwMTQ3OSwiZXhwIjoxNTk2MTg3ODc5LCJpYXQiOjE1OTYxMDE0Nzl9.QiVo-mXgGFjYPSTS9jW_yxiZzDtMZLow-fSshYUgwgv7ZlyScEGzwnmdyACfNsW17jEP4ZXPXmweqIJsATpC4g 
        ///     location: http://localhost:5000/character/id 
        ///
        /// </remarks>
        /// <param name="id">The Id of a Character.</param>
        /// <returns></returns>
        /// <response code="200">Returns the character with the specified ID</response>
        /// <response code="401">If Unauthorized User</response> 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        /// <summary>
        /// Creates a character.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /character
        ///     {
        ///        "name": "Percival",
        ///        "hitPoints": 100,
        ///        "strength": 60,
        ///        "defense": 40,
        ///        "intelligence": 10,
        ///        "class": 1
        ///     }
        ///
        /// Sample response body:
        ///
        ///     {
        ///        "data": [
        ///            {
        ///                "id": 1,
        ///                "name": "Muchlis Adhi",
        ///                "hitPoints": 100,
        ///                "strength": 50,
        ///                "defense": 30,
        ///                "intelligence": 70,
        ///                "class": 1,
        ///                "weapon": null,
        ///                "skills": [],
        ///                "fights": 1,
        ///                "victories": 0,
        ///                "defeats": 0
        ///            },
        ///            {
        ///                "id": 2,
        ///                "name": "Percival",
        ///                "hitPoints": 100,
        ///                "strength": 60,
        ///                "defense": 40,
        ///                "intelligence": 10,
        ///                "class": 1,
        ///                "weapon": null,
        ///                "skills": [],
        ///                "fights": 0,
        ///                "victories": 0,
        ///                "defeats": 0
        ///            }
        ///        ],
        ///        "success": true,
        ///        "message": null
        ///     }
        /// 
        /// Sample response header:
        /// 
        ///     content-type: application/json; charset=utf-8
        ///     Authorization: Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJXaXJhdGFtYSIsIm5iZiI6MTU5NjEwMTQ3OSwiZXhwIjoxNTk2MTg3ODc5LCJpYXQiOjE1OTYxMDE0Nzl9.QiVo-mXgGFjYPSTS9jW_yxiZzDtMZLow-fSshYUgwgv7ZlyScEGzwnmdyACfNsW17jEP4ZXPXmweqIJsATpC4g 
        ///     location: http://localhost:5000/character 
        /// 
        /// </remarks>
        /// <param name="newCharacter"></param>
        /// <returns>List created character by User</returns>
        /// <response code="201">Returns list of character created by spesific User based on Token</response>
        /// <response code="401">If Unauthorized User</response>
        [HttpPost]
        public async Task<IActionResult> AddCharacter(AddCharacterDto newCharacter)
        {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }

        /// <summary>
        /// Modifies a character
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     PUT /character/
        ///
        ///     {
        ///        "id": 2,
        ///        "name": "Percival",
        ///        "hitPoints": 30,
        ///        "strength": 90,
        ///        "defense": 50,
        ///        "intelligence": 70,
        ///        "class": 1
        ///     }
        ///
        /// Sample response:
        ///
        ///     {
        ///        "data": {
        ///            "id": 2,
        ///            "name": "Percival",
        ///            "hitPoints": 30,
        ///            "strength": 90,
        ///            "defense": 50,
        ///            "intelligence": 70,
        ///            "class": 1
        ///        },
        ///        "success": true,
        ///        "message": null
        ///     }
        ///
        /// Sample response header:
        /// 
        ///     content-type: application/json; charset=utf-8
        ///     Authorization: Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJXaXJhdGFtYSIsIm5iZiI6MTU5NjEwMTQ3OSwiZXhwIjoxNTk2MTg3ODc5LCJpYXQiOjE1OTYxMDE0Nzl9.QiVo-mXgGFjYPSTS9jW_yxiZzDtMZLow-fSshYUgwgv7ZlyScEGzwnmdyACfNsW17jEP4ZXPXmweqIJsATpC4g 
        ///     location: http://localhost:5000/character/ 
        ///
        /// </remarks>
        /// <param name="updatedCharacter"></param>
        /// <returns>An modified character</returns>
        /// <response code="200">Returns an modified character</response>
        /// <response code="404">If Character not found because not belong to user based on token</response>
        /// <response code="401">If Unauthorized User</response>
        [HttpPut]
        public async Task<IActionResult> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            ServiceResponse<GetCharacterDto> response = await _characterService.UpdateCharacter(updatedCharacter);
            if (response.Data == null) 
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Modifies a character
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     DELETE /character/2
        ///
        /// Sample response:
        ///
        ///     {
        ///        "data": [
        ///            {
        ///                "id": 1,
        ///                "name": "Muchlis Adhi",
        ///                "hitPoints": 100,
        ///                "strength": 50,
        ///                "defense": 30,
        ///                "intelligence": 70,
        ///                "class": 1,
        ///                "weapon": null,
        ///                "skills": [],
        ///                "fights": 1,
        ///                "victories": 0,
        ///                "defeats": 0
        ///            }
        ///        ],
        ///        "success": true,
        ///        "message": null
        ///     }
        ///
        /// Sample response header:
        /// 
        ///     content-type: application/json; charset=utf-8
        ///     Authorization: Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJXaXJhdGFtYSIsIm5iZiI6MTU5NjEwMTQ3OSwiZXhwIjoxNTk2MTg3ODc5LCJpYXQiOjE1OTYxMDE0Nzl9.QiVo-mXgGFjYPSTS9jW_yxiZzDtMZLow-fSshYUgwgv7ZlyScEGzwnmdyACfNsW17jEP4ZXPXmweqIJsATpC4g 
        ///     location: http://localhost:5000/character/ 
        ///
        /// </remarks>
        /// <param name="id">The Id of the Character.</param>
        /// <returns>List created Character by User</returns>
        /// <response code="200">Returns list Character created by user with deleted character</response>
        /// <response code="404">If Character not found because not belong to user based on token</response>
        /// <response code="401">If Unauthorized User</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse<List<GetCharacterDto>> response = await _characterService.DeleteCharacter(id);
            if (response.Data == null) 
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}