using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskSubtask.core.Models;
using TaskSubtask.core.Models.ModelsDto;
using Task = TaskSubtask.core.Models.Task;

namespace TaskSubtask.core.Mapping
{
    public class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<Task, TaskDto>();
            CreateMap<TaskDto, Task>().ForMember(x => x.Date, opt => opt.Ignore());

            CreateMap<Subtask, SubtaskDto>();
            CreateMap<SubtaskDto, Subtask>().ForMember(x => x.Date, opt => opt.Ignore());

            CreateMap<RegisterModel, ApplicationUser>().ReverseMap();
            CreateMap<ApplicationUser, Users>().ReverseMap();


        }
    }
}
