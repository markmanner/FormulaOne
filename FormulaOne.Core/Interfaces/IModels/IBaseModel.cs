using System;

namespace FormulaOne.Core.Interfaces.IModels
{
    /// <summary>
    /// Every database models implement this interface.
    /// </summary>
    public interface IBaseModel
    {
        public Guid Id { get; set; }
    }
}
