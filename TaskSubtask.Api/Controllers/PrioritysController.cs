using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskSubtask.core.Models;
using TaskSubtask.core.UnitOfWork;

namespace TaskSubtask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrioritysController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PrioritysController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(200,Type =typeof(List<Priority>)) ]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult GetPriority()
        {
            var priority=_unitOfWork.Prioritys.GetAll();
            if (priority is null) return NotFound();

            return Ok(priority);
            
        }
    }
}
