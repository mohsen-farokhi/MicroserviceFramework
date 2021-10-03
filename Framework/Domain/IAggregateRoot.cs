using System.Collections.Generic;

namespace Framework.Domain
{
    public interface IAggregateRoot : IEntity
    {
        void ClearDomainEvents();

        IReadOnlyList<IDomainEvent> DomainEvents { get; }
    }
}
