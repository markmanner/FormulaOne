using AutoMapper;
using FormulaOne.Core.DTOs;
using FormulaOne.Web.ViewModels.FormulaOneTeam;

namespace FormulaOne.Web.MappingProfiles
{
    /// <summary>
    /// AutoMapper mapping pofile.
    /// </summary>
    public class MappingProfileWeb : Profile
    {
        public MappingProfileWeb()
        {
            CreateMap<FormulaOneTeamDTO, FormulaOneTeamViewModel>().ReverseMap();
        }
    }
}
