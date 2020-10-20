using FormulaOne.Core.Interfaces.IModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormulaOne.Core.Models
{
    public class FormulaOneTeam : ILogicalDeletableBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [Range(1900, Int16.MaxValue)]
        public int FoundationYear { get; set; }

        [Required]
        [Range(0, Int16.MaxValue)]
        public int NumberOfChampionshipWon { get; set; }
        
        public bool IsEntryFeePaid { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}
