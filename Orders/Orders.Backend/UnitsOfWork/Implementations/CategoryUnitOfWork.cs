using Orders.Backend.Repositories.Interfaces;
using Orders.Backend.UnitsOfWork.Interfaces;
using Orders.Shared.DTOs;
using Orders.Shared.Entities;
using Orders.Shared.Responses;

namespace Orders.Backend.UnitsOfWork.Implementations
{
    public class CategoryUnitOfWork : GenericUnitOfWork<Category>, ICategoryUnitOfWork
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryUnitOfWork(IGenericRepository<Category> repository, ICategoryRepository categoryRepository) : base(repository)
        {
            _categoryRepository = categoryRepository;
        }

        public override async Task<ActionResponse<IEnumerable<Category>>> GetAsync(PaginationDTO pagination) => await _categoryRepository.GetAsync(pagination);
        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _categoryRepository.GetTotalPagesAsync(pagination);
    }
}
