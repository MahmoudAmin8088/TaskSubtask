using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskSubtask.core.Models;
using TaskSubtask.core.Models.ModelsDto;
using TaskSubtask.core.UnitOfWork;
using Task = TaskSubtask.core.Models.Task;

namespace TaskSubtask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TasksController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;   
        }

        [HttpGet]
        [ProducesResponseType(200,Type =typeof(List<TaskDto>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetAll(string? Title ,bool? isAse=true ,int? pageindex =0 ,int pagesize = 10)
        {
            var tasks = _unitOfWork.Tasks.GetAll(Title, isAse, pageindex, pagesize);

            if (tasks is null) return NotFound();

            var tasksDto = new List<TaskDto>();

            foreach (var task in tasks)
            {
                tasksDto.Add(_mapper.Map<TaskDto>(task));
            }

            return Ok(tasksDto);
        }
        
        [HttpGet("{taskId}",Name ="GetById")]
        [ProducesResponseType(200,Type=typeof(TaskDto))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetById(string taskId)
        {
            var task = _unitOfWork.Tasks.GetById(taskId);

            if (task is null) return NotFound();

            var taskDto = _mapper.Map<TaskDto>(task);

            return Ok(taskDto);

        }

        [HttpGet("[action]/{userId}")]
        [ProducesResponseType(200, Type = typeof(TaskDto))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetUsersTasks(string userId)
        {
            var tasks = _unitOfWork.Tasks.GetUsersTasks(userId);

            if(tasks is null) return NotFound();

            var tasksDto= new List<TaskDto>();

            foreach (var task in tasks)
            {
                tasksDto.Add(_mapper.Map<TaskDto>(task));
            }
            return Ok(tasksDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("[action]")]
        [ProducesResponseType(200,Type =typeof(TaskDto))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult Create([FromBody] TaskDto taskDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var task = _mapper.Map<Task>(taskDto);
            task.Date = DateTime.Now;
            
            if(_unitOfWork.Tasks.Create(task) is null)
            {
                ModelState.AddModelError("", "Something Went Wrong When Saving The Record");
                return StatusCode(500, ModelState);
            }
            _unitOfWork.Complete();
            taskDto.Date = task.Date;
            taskDto.TaskId = task.TaskId;
            return Ok(taskDto);
        }


        [HttpPut("[action]")]
        [ProducesResponseType(200, Type = typeof(TaskDto))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]

        public IActionResult Update([FromBody] TaskDto taskDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var obj = _unitOfWork.Tasks.GetById(taskDto.TaskId);

            var task = _mapper.Map<TaskDto,Task>(taskDto,obj);
            
            if(_unitOfWork.Tasks.Update(task) is null)
            {
                ModelState.AddModelError("", "Something Went Wrong When Updating The Record");
                return StatusCode(500, ModelState);
            }
            _unitOfWork.Complete();
            return Ok(taskDto);
        }

        [HttpDelete("{taskId}",Name ="Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult Delete(string taskId)
        {
            var task = _unitOfWork.Tasks.GetById(taskId);

            if (task is null) return NotFound(ModelState);

            if(_unitOfWork.Tasks.Delete(task) is null)
            {
                ModelState.AddModelError("", "Something Went Wrong When deleting The Record");
                return StatusCode(500, ModelState);
            }
            _unitOfWork.Complete();

            return NoContent();
        }



    }
}
