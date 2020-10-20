using System;
using System.ComponentModel.DataAnnotations;

namespace FormulaOne.Web.ViewModels.FormulaOneTeam
{
    public class FormulaOneTeamViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "A csapatnév megadása kötelező!")]
        [StringLength(150, ErrorMessage = "A {0} maximum {1} karakter hosszú lehet!")]
        [Display(Name = "Csapatnév")]
        public string Name { get; set; }

        [Required (ErrorMessage = "Az alapítás évét kötelező megadni!")]
        [Display(Name = "Alapítás éve")]
        [Range(1900, Int16.MaxValue, ErrorMessage = "Megadható értékek: min. {1} és max. {2}.")]
        public int FoundationYear { get; set; }
        
        [Required(ErrorMessage = "A győzelmek számának megadása kötelező!")]
        [Display(Name = "Győzelmek száma")]
        [Range(0,Int16.MaxValue,ErrorMessage = "Megadható értékek: min. {1} és max. {2}.")]
        public int NumberOfChampionshipWon { get; set; }
        
        [Required(ErrorMessage = "A nevezési díj állapotának megadása kötelező!")]
        [Display(Name = "Nevezési díj befizetve")]
        public bool IsEntryFeePaid { get; set; }
    }
}
