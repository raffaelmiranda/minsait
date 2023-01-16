using Microsoft.EntityFrameworkCore;
using SalesManagement.Report.Domain.Interfaces.Repositories;
using SalesManagement.Report.Infrastructure.Persistence.Contexts;

namespace SalesManagement.Report.Infrastructure.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly WorkerContext _context;

        public BaseRepository(WorkerContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public List<T> Obter()
        {
            return _context.Set<T>().AsNoTracking().ToList();
        }

        public T ObterPorId(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void RemoverPorId(int id)
        {
            var entity = ObterPorId(id);

            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public void Remover(T entity)
        {

            _context.Set<T>().Remove(entity);

            _context.SaveChanges();
        }

        public T Salvar(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public T Atualizar(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
            _context.SaveChanges();

            return entity;
        }
    }
}
