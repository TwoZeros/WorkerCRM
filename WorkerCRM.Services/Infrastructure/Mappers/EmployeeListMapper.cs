﻿using AutoMapper;
using WorkerCRM.Models;
using WorkerCRM.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerCRM.Services.Contract.Dto;
using WorkerCRM.Services.Infrastructure.Mappers.Interfaces;

namespace WorkerCRM.Services.Infrastructure.Mappers
{

    public interface IEmployeeListMapper : IModelMapper<EmployeeListDto, Employee>
    {
    }

    public class EmployeeListMapper : AbstractModelMapper<EmployeeListDto, Employee>, IEmployeeListMapper
    {
        protected override IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmployeeListDto, Employee>();

                cfg.CreateMap<Employee, EmployeeListDto>()
                .ForMember(x => x.Login, s => s.MapFrom(x => x.User.Login))
                .ForMember(x => x.BirthDay, s => s.MapFrom(x => x.BirthDay.ToString("d")))
                .ForMember(x => x.Created, s => s.MapFrom(x => x.CreatedDate.ToString("d")));
            });

            return config.CreateMapper();
        }
    }
}
