using System;
using System.Data.Entity.Core.Objects.DataClasses;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000445 RID: 1093
	internal interface IChangeTrackingStrategy
	{
		// Token: 0x06003540 RID: 13632
		void SetChangeTracker(IEntityChangeTracker changeTracker);

		// Token: 0x06003541 RID: 13633
		void TakeSnapshot(EntityEntry entry);

		// Token: 0x06003542 RID: 13634
		void SetCurrentValue(EntityEntry entry, StateManagerMemberMetadata member, int ordinal, object target, object value);

		// Token: 0x06003543 RID: 13635
		void UpdateCurrentValueRecord(object value, EntityEntry entry);
	}
}
