using SalesManagement.CashFlow.Application.Interfaces;
using SalesManagement.CashFlow.Application.Models;
using SalesManagement.CashFlow.Domain.Entities;
using SalesManagement.CashFlow.Domain.Interfaces.Repositories;

namespace SalesManagement.CashFlow.Application.AppServices
{
    public class LancamentoBancarioAppService : BaseAppService<LancamentoBancario>, ILancamentoBancarioAppService
    {
        private readonly ILancamentoBancarioRepository _repository;

        public LancamentoBancarioAppService(ILancamentoBancarioRepository repositoryLancamentoBancario): base(repositoryLancamentoBancario)
        {
            _repository = repositoryLancamentoBancario;
        }

        public List<LancamentoBancarioObter> ObterTodosComTipoLancamento()
        {
            List<LancamentoBancario>  lancamentosModel = _repository.ObterTodosComTipoLancamento();

            List<LancamentoBancarioObter> response = new List<LancamentoBancarioObter>();

            foreach (LancamentoBancario item in lancamentosModel)
            {
                LancamentoBancarioObter lancamentoBancario = new LancamentoBancarioObter();
                lancamentoBancario.Id = item.Id.Value;
                lancamentoBancario.Descricao = item.Descricao;
                lancamentoBancario.Valor = item.Valor;
                lancamentoBancario.TipoLancamentoId = item.TipoLancamento.Id;
                lancamentoBancario.TipoLancamento = item.TipoLancamento.Nome;
                lancamentoBancario.Categoria = item.Categoria;

                response.Add(lancamentoBancario);
            }

            return response;
        }
    }
}
