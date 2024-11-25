using AutoMapper;
using Volcanion.Medical.Models.Entities.Identity;
using Volcanion.Medical.Models.Request.DTOs;

namespace Volcanion.Medical.Models.MappingProfiles;

public class DtoMappingProfile : Profile
{
    public DtoMappingProfile()
    {
        CreateMap<AccountRequestDTO, Account>();
        CreateMap<PermissionRequestDTO, Permission>();
        CreateMap<GrantPermissionRequestDTO, GrantPermission>();
        CreateMap<RolePermissionRequestDTO, RolePermission>();
        CreateMap<RoleRequestDTO, Role>();
    }
}
