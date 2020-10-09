using AutoMapper;
using LRSIntro.DTO;
using LRSIntro.Models;

namespace LRSIntro.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.Surname, y => y.MapFrom(z => z.Surname))
                .ForMember(x => x.BirthDate, y => y.MapFrom(z => z.BirthDate))
                .ForMember(x => x.UserType, y => y.MapFrom(z => z.UserType.Description))
                .ForMember(x => x.UserTypeId, y => y.MapFrom(z => z.UserType.Id))
                .ForMember(x => x.UserTitle, y => y.MapFrom(z => z.UserTitle.Description))
                .ForMember(x => x.UserTitleId, y => y.MapFrom(z => z.UserTitle.Id))
                .ForMember(x => x.EmailAddress, y => y.MapFrom(z => z.EmailAddress))
                .ForMember(x => x.IsActive, y => y.MapFrom(z => z.IsActive));

            CreateMap<UserTitle, UserTitleDTO>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Description, y => y.MapFrom(z => z.Description));

            CreateMap<UserType, UserTypeDTO>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Description, y => y.MapFrom(z => z.Description));

            CreateMap<UserAddOrUpdateDTO, User>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.Surname, y => y.MapFrom(z => z.Surname))
                .ForMember(x => x.BirthDate, y => y.MapFrom(z => z.BirthDate))
                .ForMember(x => x.UserTypeId, y => y.MapFrom(z => z.UserTypeId))
                .ForMember(x => x.UserTitleId, y => y.MapFrom(z => z.UserTitleId))
                .ForMember(x => x.EmailAddress, y => y.MapFrom(z => z.EmailAddress))
                .ForMember(x => x.IsActive, y => y.MapFrom(z => z.IsActive))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<User, UserAddOrUpdateDTO>();
        }

    }
}
