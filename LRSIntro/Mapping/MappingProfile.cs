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
                .ForMember(x => x.UserTitle, y => y.MapFrom(z => z.UserTitle.Description))
                .ForMember(x => x.EmailAddress, y => y.MapFrom(z => z.EmailAddress))
                .ForMember(x => x.IsActive, y => y.MapFrom(z => z.IsActive));

            CreateMap<User, UserEditDTO>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.Surname, y => y.MapFrom(z => z.Surname))
                .ForMember(x => x.BirthDate, y => y.MapFrom(z => z.BirthDate))
                .ForMember(x => x.UserType, y => y.MapFrom(z => GetUserType(z)))
                .ForMember(x => x.UserTitle, y => y.MapFrom(z => GetUserTitle(z)))
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
                .ForMember(x => x.UserTypeId, y => y.MapFrom(z => z.UserType))
                .ForMember(x => x.UserTitleId, y => y.MapFrom(z => z.UserTitle))
                .ForMember(x => x.EmailAddress, y => y.MapFrom(z => z.EmailAddress))
                .ForMember(x => x.IsActive, y => y.MapFrom(z => z.IsActive))
                .ForAllOtherMembers(x => x.Ignore());


        }
        private UserTitleDTO GetUserTitle(User user)
        {
            if (user.UserTitle != null)
            {
                return new UserTitleDTO
                {
                    Id = user.UserTitle.Id,
                    Description = user.UserTitle.Description
                };
            }
            else
            {
                return null;
            }

        }

        private UserTypeDTO GetUserType(User user)
        {
            if (user.UserType != null)
            {
                return new UserTypeDTO
                {
                    Id = user.UserType.Id,
                    Description = user.UserType.Description
                };
            }
            else
            {
                return null;
            }

        }
    }
}
