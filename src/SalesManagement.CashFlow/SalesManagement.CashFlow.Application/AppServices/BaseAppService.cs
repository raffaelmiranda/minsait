using SalesManagement.CashFlow.Application.Interfaces;
using SalesManagement.CashFlow.Domain.Interfaces.Repositories;

namespace SalesManagement.CashFlow.Application.AppServices
{
    public class BaseAppService<T> : IBaseAppService<T> where T : class
    {
        private readonly IBaseRepository<T> _repositoryBase;

        public BaseAppService(IBaseRepository<T> repo)
        {
            _repositoryBase = repo;
        }
        public T Atualizar(T entity)
        {
            _repositoryBase.Atualizar(entity);

            return entity;
   
        }

        public List<T> Obter()
        {
           return _repositoryBase.Obter();
        }

        public T ObterPorId(int id)
        {
            return _repositoryBase.ObterPorId(id);
        }

        public void RemoverPorId(int id)
        {
           _repositoryBase.RemoverPorId(id);
        }

        public void Remover(T entity)
        {
            _repositoryBase.Remover(entity);
        }

        public T Salvar(T entity)
        {
           return _repositoryBase.Salvar(entity);
        }
    }
}
