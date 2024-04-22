using Orders.Backend.Repositories.Interfaces;
using Orders.Backend.UnitsOfWork.Interfaces;
using Orders.Shared.DTOs;
using Orders.Shared.Entities;
using Orders.Shared.Responses;

namespace Orders.Backend.UnitsOfWork.Implementations
{
    public class StatesUnitOfWork : GenericUnitOfWork<State>, IStatesUnitOfWork
    {
        private readonly IStatesRepository _statesRepository;

        public StatesUnitOfWork(IGenericRepository<State> repository, IStatesRepository statesRepository) : base(repository)
        {
            _statesRepository = statesRepository;
        }

        public async Task<ActionResponse<State>> GetAsync(int id) => await _statesRepository.GetAsync(id);

        public async Task<ActionResponse<IEnumerable<State>>> GetAsync() => await _statesRepository.GetAsync();
        public override Task<ActionResponse<IEnumerable<State>>> GetAsync(PaginationDTO pagination) => _statesRepository.GetAsync(pagination);
        public override Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => _statesRepository.GetTotalPagesAsync(pagination);
    }
}
