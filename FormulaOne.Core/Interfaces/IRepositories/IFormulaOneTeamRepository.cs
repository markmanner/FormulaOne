using FormulaOne.Core.Models;
using System.Collections.Generic;

namespace FormulaOne.Core.Interfaces.IRepositories
{
    public interface IFormulaOneTeamRepository : IRepository<FormulaOneTeam>
    {
        /// <summary>
        /// Get list of Formula 1 teams which have won the most championships.
        /// </summary>
        /// <returns></returns>
        IEnumerable<FormulaOneTeam> GetGroupsByMostNumberOfChampionshipWon();
    }
}
