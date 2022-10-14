using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TaskSubtask.core.IRepository;
using TaskSubtask.core.Models;
using TaskSubtask.core.UnitOfWork;
using TaskSubtask.Ef.Repository;

namespace TaskSubtask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public UsersController(IUnitOfWork unitOfWork ,IMapper mapper , UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        [ProducesResponseType(200,Type =typeof(List<Users>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = _unitOfWork.Users.GetAll();
            if (users is null)
                return NotFound();
            var usersList = new List<Users>();
            
            foreach (var user in users)
            {
                var role = await _userManager.GetRolesAsync(user);
                var map = _mapper.Map<Users>(user);
                map.Role = role;
                usersList.Add(map);
                
            }
            return Ok(usersList);
        }

        [HttpGet("{userId}",Name = "GetUser")]
        [ProducesResponseType(200, Type = typeof(Users))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]

        public IActionResult GetUser(string userId)
        {
            var obj = _unitOfWork.Users.GetById(userId);
            if (obj is null) return NotFound();
            var user = _mapper.Map<Users>(obj);
            return Ok(user);

        }

        [HttpDelete("{userId}",Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]

        public IActionResult DeleteUser(string userId)
        {
            var obj = _unitOfWork.Users.GetById(userId);
            if (obj is null) return NotFound(ModelState);

            if(_unitOfWork.Users.Delete(obj)is null)
            {
                ModelState.AddModelError("", "Something Went Wrong When deleting The User");
                return StatusCode(500, ModelState);
            }
            _unitOfWork.Complete();
            return NoContent();
            

            
        }

    }
}
