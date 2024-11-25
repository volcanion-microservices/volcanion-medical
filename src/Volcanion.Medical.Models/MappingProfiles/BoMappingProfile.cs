using AutoMapper;
using Volcanion.Medical.Models.Entities.Identity;
using Volcanion.Medical.Models.Response.BOs;

namespace Volcanion.Medical.Models.MappingProfiles;

public class BoMappingProfile : Profile
{
    public BoMappingProfile()
    {
        CreateMap<AccountResponseBO, Account>()
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<Role, RoleResponseBO>()
            .ReverseMap()
            .ForMember(dest => dest.RolePermissions, opt => opt.Ignore());

        CreateMap<Permission, PermissionResponseBO>()
            .ReverseMap()
            .ForMember(dest => dest.RolePermissions, opt => opt.Ignore());

        CreateMap<RolePermission, RolePermissionResponseBO>()
            .ReverseMap()
            .ForMember(dest => dest.Permission, opt => opt.Ignore());

        CreateMap<GrantPermission, GrantPermissionResponseBO>()
            .ReverseMap()
            .ForMember(dest => dest.Account, opt => opt.Ignore());
    }
}
