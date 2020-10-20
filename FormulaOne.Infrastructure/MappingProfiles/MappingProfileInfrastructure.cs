using AutoMapper;
using FormulaOne.Core.Models;
using FormulaOne.Core.DTOs;

namespace FormulaOne.Infrastructure.MappingProfiles
{
    /// <summary>
    /// AutoMapper mapping pofile.
    /// </summary>
    public class MappingProfileInfrastructure : Profile
    {
        public MappingProfileInfrastructure()
        {
            CreateMap<FormulaOneTeam, FormulaOneTeamDTO>().ReverseMap();
        }
    }
}
