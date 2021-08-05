using AutoMapper;
using FullCalenderDemo.Areas.Identity.Data;
using FullCalenderDemo.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCalenderDemo.Automapper.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, CreateUserVM>() // means you want to map from ApplicationUser to CreateUserVM  
            .ForMember(d => d.Email, source => source.MapFrom(s => s.Email))
            .ReverseMap();
            CreateMap<ApplicationUser, UserVM>() 
            .ForMember(d => d.Id, source => source.MapFrom(s => s.Id))
            .ForMember(d => d.Email, source => source.MapFrom(s => s.Email))
            .ReverseMap();
        }
       
    }
}
