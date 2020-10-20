namespace FormulaOne.Core.Interfaces.IModels
{
    /// <summary>
    /// Models which can be logically deleted.
    /// </summary>
    public interface ILogicalDeletableBaseModel : IBaseModel
    {
        public bool IsDeleted { get; set; }
    }
}
