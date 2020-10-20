using FormulaOne.Core.Interfaces.IModels;
using FormulaOne.Core.Interfaces.IRepositories;
using FormulaOne.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FormulaOne.Infrastructure.Repositories
{
    /// <summary>
    /// Generic repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : class, ILogicalDeletableBaseModel
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _entities;
        private ILogger<Repository<T>> _logger;

        public Repository(ApplicationDbContext context, ILogger<Repository<T>> logger)
        {
            _context = context;
            _entities = _context.Set<T>();
            _logger = logger;
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.Where(w => !w.IsDeleted).AsEnumerable();
        }

        public T GetById(Guid id)
        {
            var entity = _entities.SingleOrDefault(w => !w.IsDeleted && w.Id == id);
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            return entity;
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            try
            {
                _entities.Add(entity);

                _context.SaveChanges();
                _logger.LogInformation($"New entity: {entity.Id}");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            try
            {
                _entities.Update(entity);

                _context.SaveChanges();
                _logger.LogInformation($"Modified entity: {entity.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public void Delete(Guid id)
        {
            var entity = _entities.SingleOrDefault(w => !w.IsDeleted && w.Id == id);
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entity.IsDeleted = true;

            try
            {
                _entities.Update(entity);

                _context.SaveChanges();

                _logger.LogInformation($"Deleted entity: {entity.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
