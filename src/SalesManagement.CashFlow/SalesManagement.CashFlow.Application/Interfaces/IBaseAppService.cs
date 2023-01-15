namespace SalesManagement.CashFlow.Application.Interfaces
{
    public interface IBaseAppService<T>
    {
        List<T> Obter();
        T ObterPorId(int id);
        T Salvar(T entity);
        T Atualizar(T entity);
        void RemoverPorId(int id);
        void Remover(T entity);
    }
}
