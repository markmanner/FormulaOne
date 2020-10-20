using System;

namespace FormulaOne.Core.DTOs
{
    public class FormulaOneTeamDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int FoundationYear { get; set; }
        public int NumberOfChampionshipWon { get; set; }
        public bool IsEntryFeePaid { get; set; }
    }
}
