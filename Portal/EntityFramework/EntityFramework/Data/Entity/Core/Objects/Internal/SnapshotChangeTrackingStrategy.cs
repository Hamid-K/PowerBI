using System;
using System.Data.Entity.Core.Objects.DataClasses;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000458 RID: 1112
	internal sealed class SnapshotChangeTrackingStrategy : IChangeTrackingStrategy
	{
		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06003664 RID: 13924 RVA: 0x000B0573 File Offset: 0x000AE773
		public static SnapshotChangeTrackingStrategy Instance
		{
			get
			{
				return SnapshotChangeTrackingStrategy._instance;
			}
		}

		// Token: 0x06003665 RID: 13925 RVA: 0x000B057A File Offset: 0x000AE77A
		private SnapshotChangeTrackingStrategy()
		{
		}

		// Token: 0x06003666 RID: 13926 RVA: 0x000B0582 File Offset: 0x000AE782
		public void SetChangeTracker(IEntityChangeTracker changeTracker)
		{
		}

		// Token: 0x06003667 RID: 13927 RVA: 0x000B0584 File Offset: 0x000AE784
		public void TakeSnapshot(EntityEntry entry)
		{
			if (entry != null)
			{
				entry.TakeSnapshot(false);
			}
		}

		// Token: 0x06003668 RID: 13928 RVA: 0x000B0590 File Offset: 0x000AE790
		public void SetCurrentValue(EntityEntry entry, StateManagerMemberMetadata member, int ordinal, object target, object value)
		{
			if (target == entry.Entity)
			{
				((IEntityChangeTracker)entry).EntityMemberChanging(member.CLayerName);
				member.SetValue(target, value);
				((IEntityChangeTracker)entry).EntityMemberChanged(member.CLayerName);
				if (member.IsComplex)
				{
					entry.UpdateComplexObjectSnapshot(member, target, ordinal, value);
					return;
				}
			}
			else
			{
				member.SetValue(target, value);
				if (entry.State != EntityState.Added)
				{
					entry.DetectChangesInProperties(true);
				}
			}
		}

		// Token: 0x06003669 RID: 13929 RVA: 0x000B05F8 File Offset: 0x000AE7F8
		public void UpdateCurrentValueRecord(object value, EntityEntry entry)
		{
			entry.UpdateRecordWithoutSetModified(value, entry.CurrentValues);
			entry.DetectChangesInProperties(false);
		}

		// Token: 0x040011A4 RID: 4516
		private static readonly SnapshotChangeTrackingStrategy _instance = new SnapshotChangeTrackingStrategy();
	}
}
