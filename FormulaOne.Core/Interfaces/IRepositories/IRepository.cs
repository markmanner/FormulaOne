using FormulaOne.Core.Interfaces.IModels;
using System;
using System.Collections.Generic;

namespace FormulaOne.Core.Interfaces.IRepositories
{
    /// <summary>
    /// Generic repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> :IDisposable where T : class, IBaseModel
    {
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        void Insert(T model);
        void Update(T model);
        void Delete(Guid id);
    }
}
