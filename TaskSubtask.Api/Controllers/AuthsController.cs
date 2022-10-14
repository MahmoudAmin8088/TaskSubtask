using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Data;
using TaskSubtask.core.IRepository;
using TaskSubtask.core.Models;

namespace TaskSubtask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;


        public AuthsController(IAuthRepository authRepository,IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }

        [HttpPost("register")]
        [ProducesResponseType(200, Type = typeof(AuthModel))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authRepository.RegisterAsync(model);

            if (!result.IsAuthentication)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("login")]
        [ProducesResponseType(200, Type = typeof(AuthModel))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authRepository.LoginAsync(model);

            if (!result.IsAuthentication)
                return BadRequest(result.Message);

            //return Ok(new { token = result.Token, expireOn = result.ExpiresOn });

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("[action]")]
        [ProducesResponseType(200, Type = typeof(AddRoleModel))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAdmin([FromBody] AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authRepository.AddAdminAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);


        }
        [Authorize(Roles = "Admin")]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAdmin([FromBody] DeleteRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authRepository.DeleteAdminAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok();


        }

        //[Authorize(Roles = "Admin")]
        //[HttpPost("adduser")]
        //[ProducesResponseType(200, Type = typeof(AuthModel))]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> AddUser([FromBody] RegisterModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var result = await _authRepository.RegisterAsync(model);
        //    if (!result.IsAuthentication)
        //    {
        //        return BadRequest(result.Message);
        //    }

        //    return Ok(result);

        //}

        //[HttpGet("[action]")]
        //[ProducesResponseType(200, Type = typeof(List<ApplicationUser>))]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]

        //public async Task<IActionResult> GetUsers()
        //{
        //    var result = await _authRepository.GetUsersAsync();
        //    var users = new List<Users>();
        //    if (result is null)
        //        return NotFound();
        //    foreach (var user in result)
        //    {
        //        users.Add(_mapper.Map<Users>(user));
        //    }
        //    return Ok(users);
        //}

    }
}
