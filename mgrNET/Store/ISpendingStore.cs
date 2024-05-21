

using mgrNET.Domain;

namespace mgrNET.Store;

public interface ISpendingStore
{
    Task<IEnumerable<Spending>> GetAll();
    Task<Spending?> GetById(Guid id);
    Task Create(CreateSpendingParams createSpendingParams);
    Task Update(Guid id, UpdateSpendingParams updateSpendingParams);
    Task Delete(Guid id);
}

