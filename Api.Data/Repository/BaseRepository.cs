using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;




namespace Api.Data.Repository
{

    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly MyContext _context;
        private DbSet<T> _dataset;

        public BaseRepository(MyContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();

        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
            try
            {
                if (result != null)
                {
                    _dataset.Remove(result);
                    await _context.SaveChangesAsync();
                    return true;


                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<T> InsertAsync(T item)
        {
            try
            {
                if (item.Id == Guid.Empty)
                {
                    item.Id = Guid.NewGuid();

                }
                item.CreateAt = DateTime.UtcNow;
                _dataset.Add(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return item;
        }
        public async Task<bool> ExitsAsync(Guid id)
        {
            return await _dataset.AnyAsync(p => p.Id.Equals(id));
        }

        public async Task<T> SelectAsync(Guid id)
        {
            try
            {
                return await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            try
            {
                return await _dataset.ToListAsync();
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<T> UpdateAsync(T item)
        {
            item.UpdateAt = DateTime.UtcNow;
            var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));
            if (result == null)
                return null;
            item.CreateAt = result.CreateAt;
            try
            {
                _context.Entry(result).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return item;
        }
    }
}
