using FormulaOne.Core.Interfaces.IRepositories;
using FormulaOne.Core.Models;
using FormulaOne.Infrastructure.Data;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace FormulaOne.Infrastructure.Repositories
{
    public class FormulaOneTeamRepository : Repository<FormulaOneTeam>, IFormulaOneTeamRepository
    {
        private ApplicationDbContext _context;

        public FormulaOneTeamRepository(ApplicationDbContext context, ILogger<Repository<FormulaOneTeam>> logger) : base(context, logger)
        {
            _context = context;
        }

        /// <summary>
        /// Get list of Formula 1 teams which have won the most championships.
        /// </summary>
        /// <returns>IEnumerbale of FormulaOneTeam models</returns>
        public IEnumerable<FormulaOneTeam> GetGroupsByMostNumberOfChampionshipWon()
        {
            var maxNumberOfChampionShip = _context.FormulaOneTeams.Max(w => w.NumberOfChampionshipWon);

            return _context.FormulaOneTeams.Where(w => !w.IsDeleted && w.NumberOfChampionshipWon == maxNumberOfChampionShip).AsEnumerable();
        }
    }
}
