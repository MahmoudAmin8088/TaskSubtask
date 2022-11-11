using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskSubtask.core.Models;
using TaskSubtask.core.Models.ModelsDto;
using TaskSubtask.core.UnitOfWork;

namespace TaskSubtask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubtasksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubtasksController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<SubtaskDto>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetAllSubTasks(string? Title, bool? isAse = true, int? pageindex = 0, int? pagesize = 10)
        {
            var subtasks = _unitOfWork.Subtasks.GetAll(Title, isAse, pageindex, pagesize);

            if (subtasks is null) return NotFound();

            var subtasksDto = new List<SubtaskDto>();

            foreach (var subtask in subtasks)
            {
                subtasksDto.Add(_mapper.Map<SubtaskDto>(subtask));
            }
            return Ok(subtasksDto);
        }


        [HttpGet("{subtaskId}", Name = "GetSubtask")]
        [ProducesResponseType(200, Type = typeof(TaskDto))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetSubTask(string subtaskId)
        {
            var subtask = _unitOfWork.Subtasks.GetById(subtaskId);
            if (subtask is null) return NotFound();

            var subtaskDto = _mapper.Map<SubtaskDto>(subtask);

            return Ok(subtaskDto);
        }


        [HttpGet("[action]/{userId}")]
        [ProducesResponseType(200, Type = typeof(List<SubtaskDto>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetUsersSubTasks(string userId)
        {
            var subtasks = _unitOfWork.Subtasks.GetUsersSubTasks(userId);

            if (subtasks is null) return NotFound();

            var subtasksDto = new List<SubtaskDto>();
            foreach (var subtask in subtasks)
            {
                subtasksDto.Add(_mapper.Map<SubtaskDto>(subtask));
            }

            return Ok(subtasksDto);
        }


        [HttpGet("[action]/{taskId}")]
        [ProducesResponseType(200, Type = typeof(List<SubtaskDto>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetSubTasksOfTask(string taskId)
        {
            var subTasks = _unitOfWork.Subtasks.GetSubTasksOfTask(taskId);

            if (subTasks is null) return NotFound();

            var subtasksDto = new List<SubtaskDto>();

            foreach (var subtask in subTasks)
            {
                subtasksDto.Add(_mapper.Map<SubtaskDto>(subtask));
            }

            return Ok(subtasksDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("[action]")]
        [ProducesResponseType(200, Type = typeof(SubtaskDto))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult Create([FromBody] SubtaskDto subtaskDto)
        {
            if (!ModelState.IsValid)  return BadRequest(ModelState);

            var subtask = _mapper.Map<Subtask>(subtaskDto);

            subtask.Date = DateTime.Now;
            if (_unitOfWork.Subtasks.Create(subtask) is null)
            {
                ModelState.AddModelError("", "Something Went Wrong When Saving The Record");
                return StatusCode(500, ModelState);
            }
            _unitOfWork.Complete();
            subtaskDto.Date = subtask.Date;
            subtaskDto.SubTaskId = subtask.TaskId;
            return Ok(subtaskDto);

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("[action]")]
        [ProducesResponseType(200, Type = typeof(TaskDto))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult Update([FromBody] SubtaskUpdateDto subtaskDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var obj = _unitOfWork.Subtasks.GetById(subtaskDto.SubTaskId);

            var Subtask = _mapper.Map<SubtaskUpdateDto, Subtask>(subtaskDto, obj);
            if (_unitOfWork.Subtasks.Update(Subtask) is null)
            {
                ModelState.AddModelError("", "Something Went Wrong When Updating The Record");
                return StatusCode(500, ModelState);
            }
            _unitOfWork.Complete();
            return Ok(subtaskDto);

        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{subTaskId}", Name = ("DeleteSubTask"))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult Delete(string subtaskId)
        {
            var subtask = _unitOfWork.Subtasks.GetById(subtaskId);
            if (subtask is null)
                return NotFound();

            if (_unitOfWork.Subtasks.Delete(subtask) is null)
            {
                ModelState.AddModelError("", "Something Went Wrong When deleting The Record");
                return StatusCode(500, ModelState);
            }
            _unitOfWork.Complete();

            return NoContent();

        }

    }
}
