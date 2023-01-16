namespace SalesManagement.Report.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> : IDisposable where T : class
    {
        List<T> Obter();
        T ObterPorId(int id);
        T Salvar(T entity);
        T Atualizar(T entity);
        void RemoverPorId(int id);
        void Remover(T entity);
    }
}
