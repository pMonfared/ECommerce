using System;

namespace ECommerce.Utilities.DomainHelpers.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly IUnitOfWork _uow;
        public BaseRepository(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }
    }
}
