using System.Collections.Generic;
using System.Linq;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, UserDto>()
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photo.Url))
            .ForMember(dest => dest.MemberSince, opt => opt.MapFrom(src => src.DateJoined.CalculateDateSince()))
            .ForMember(dest => dest.QuestionsPosted, opt => opt.MapFrom(src => src.Questions.Count))
            .ForMember(dest => dest.QuestionsAnswered, opt => opt.MapFrom(src => src.Answers.Select(a => a.QuestionId).Distinct().Count()));

            CreateMap<Photo, PhotoDto>();
            CreateMap<UserVotes, UserVoteDto>();

            CreateMap<RegisterDto, AppUser>();

            CreateMap<Question, QuestionDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.AppUser.UserName))
            .ForMember(dest => dest.Answers, opt => opt.MapFrom(
                src => src.Answers.OrderByDescending(a => a.IsBestAnswer).ThenByDescending(a => a.Rank)))
            .ForMember(dest => dest.HasBestAnswer, opt => opt.MapFrom(src => src.Answers.Any(a => a.IsBestAnswer)))
            .ForMember(dest => dest.UserPhotoUrl, opt => opt.MapFrom(src => src.AppUser.Photo.Url))
            .ForMember(dest => dest.UserRep, opt => opt.MapFrom(
                src => src.AppUser.Questions.Count + src.AppUser.Answers.Select(a => a.QuestionId).Distinct().Count()));

            CreateMap<QuestionUpdateDto, Question>();
            CreateMap<QuestionCreateDto, Question>();

            CreateMap<Answer, AnswerDto>()
            .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.Question.Id))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.AppUser.UserName))
            .ForMember(dest => dest.UserPhotoUrl, opt => opt.MapFrom(src => src.AppUser.Photo.Url))
            .ForMember(dest => dest.UserRep, opt => opt.MapFrom(
                src => src.AppUser.Questions.Count + src.AppUser.Answers.Select(a => a.QuestionId).Distinct().Count()));

            CreateMap<AnswerUpdateDto, Answer>();
            CreateMap<AnswerCreateDto, Answer>();
        }
    }
}