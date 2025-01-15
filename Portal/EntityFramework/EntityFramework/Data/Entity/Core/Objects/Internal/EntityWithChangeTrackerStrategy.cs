using System;
using System.Data.Entity.Core.Objects.DataClasses;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x0200043E RID: 1086
	internal sealed class EntityWithChangeTrackerStrategy : IChangeTrackingStrategy
	{
		// Token: 0x06003503 RID: 13571 RVA: 0x000AAB26 File Offset: 0x000A8D26
		public EntityWithChangeTrackerStrategy(IEntityWithChangeTracker entity)
		{
			this._entity = entity;
		}

		// Token: 0x06003504 RID: 13572 RVA: 0x000AAB35 File Offset: 0x000A8D35
		public void SetChangeTracker(IEntityChangeTracker changeTracker)
		{
			this._entity.SetChangeTracker(changeTracker);
		}

		// Token: 0x06003505 RID: 13573 RVA: 0x000AAB43 File Offset: 0x000A8D43
		public void TakeSnapshot(EntityEntry entry)
		{
			if (entry != null && entry.RequiresComplexChangeTracking)
			{
				entry.TakeSnapshot(true);
			}
		}

		// Token: 0x06003506 RID: 13574 RVA: 0x000AAB57 File Offset: 0x000A8D57
		public void SetCurrentValue(EntityEntry entry, StateManagerMemberMetadata member, int ordinal, object target, object value)
		{
			member.SetValue(target, value);
		}

		// Token: 0x06003507 RID: 13575 RVA: 0x000AAB63 File Offset: 0x000A8D63
		public void UpdateCurrentValueRecord(object value, EntityEntry entry)
		{
			bool flag = entry.WrappedEntity.IdentityType != this._entity.GetType();
			entry.UpdateRecordWithoutSetModified(value, entry.CurrentValues);
			if (flag)
			{
				entry.DetectChangesInProperties(true);
			}
		}

		// Token: 0x0400112E RID: 4398
		private readonly IEntityWithChangeTracker _entity;
	}
}
