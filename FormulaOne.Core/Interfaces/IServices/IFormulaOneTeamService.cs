using FormulaOne.Core.DTOs;
using System;
using System.Collections.Generic;

namespace FormulaOne.Core.Interfaces.IServices
{
    public interface IFormulaOneTeamService
    {
        IEnumerable<FormulaOneTeamDTO> GetAll();

        FormulaOneTeamDTO GetById(Guid id);
        
        void Add(FormulaOneTeamDTO team);

        void Update(FormulaOneTeamDTO team);

        void Delete(Guid id);
    }
}
