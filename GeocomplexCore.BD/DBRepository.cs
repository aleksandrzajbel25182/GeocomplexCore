using Geocomplex.Interface;
using GeocomplexCore.BD.Context;
using GeocomplexCore.DAL.Entityes.Base;
using Microsoft.EntityFrameworkCore;

namespace GeocomplexCore.BD
{
    internal class DBRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly GeocomplexContext _db;
        private readonly DbSet<T> _Set;

        public bool AutoSaveChanges { get; set; } = true;

        public DBRepository(GeocomplexContext db)
        {
            _db = db;
            _Set = db.Set<T>();
        }

        public virtual IQueryable<T> Items => _Set;
        #region Добавление/Add
        public T Add(T item)
        {
            if (item is null) throw new ArgumentException(nameof(item));
            _db.Entry(item).State = EntityState.Added;
            if(AutoSaveChanges)
                _db.SaveChanges();
            return item;
        }

        public async Task<T> AddAsync(T item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentException(nameof(item));
            _db.Entry(item).State = EntityState.Added;
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
            return item;
        }
        #endregion
        #region Получение, чтение / Get
               
        public T Get(int id)
        {
            return Items.SingleOrDefault(item => item.Id == id);
        }

        public async Task<T> GetAsync(int id, CancellationToken Cancel = default)
        {
            return await Items.SingleOrDefaultAsync(item => item.Id == id, Cancel).ConfigureAwait(false);
        }
        #endregion
        #region Обновление/Update

        public void Update(T item)
        {
            if (item is null) throw new ArgumentException(nameof(item));
            _db.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges)
                 _db.SaveChanges();
           
        }

        public async Task UpdateAsync(T item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentException(nameof(item));
            _db.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }
        #endregion

        public void Remove(int id)
        {
            _db.Remove(new T { Id = id });

            if (AutoSaveChanges)
                _db.SaveChanges();
        }

        public async Task RemoveAsync(int id, CancellationToken Cancel = default)
        {
            _db.Remove(new T { Id = id });
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }
    }
}
