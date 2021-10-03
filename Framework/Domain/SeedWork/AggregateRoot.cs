using System.Collections.Generic;

namespace Framework.Domain.SeedWork
{
    public abstract class AggregateRoot : Entity, IAggregateRoot
	{
		protected AggregateRoot() : base()
		{
			_domainEvents = new List<IDomainEvent>();
		}

		// **********
		[System.Text.Json.Serialization.JsonIgnore]
		private readonly List<IDomainEvent> _domainEvents;

		[System.Text.Json.Serialization.JsonIgnore]
		public IReadOnlyList<IDomainEvent> DomainEvents
		{
			get
			{
				return _domainEvents;
			}
		}
		// **********

		protected void RaiseDomainEvent(IDomainEvent domainEvent)
		{
			if (domainEvent is null)
			{
				return;
			}

			_domainEvents?.Add(domainEvent);
		}

		protected void RemoveDomainEvent(IDomainEvent domainEvent)
		{
			if (domainEvent is null)
			{
				return;
			}

			_domainEvents?.Remove(domainEvent);
		}

		public void ClearDomainEvents()
		{
			_domainEvents?.Clear();
		}
	}
}
