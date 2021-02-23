using System;
using ECommerce.Utilities.DomainHelpers;

namespace ECommerce.Utilities.PresentationHelpers.Services
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork _uow;
        public BaseService(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }
    }
}
