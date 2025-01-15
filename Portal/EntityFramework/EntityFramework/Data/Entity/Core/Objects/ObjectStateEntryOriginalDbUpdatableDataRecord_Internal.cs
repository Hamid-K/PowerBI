using System;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000421 RID: 1057
	internal class ObjectStateEntryOriginalDbUpdatableDataRecord_Internal : OriginalValueRecord
	{
		// Token: 0x060032EB RID: 13035 RVA: 0x000A265C File Offset: 0x000A085C
		internal ObjectStateEntryOriginalDbUpdatableDataRecord_Internal(EntityEntry cacheEntry, StateManagerTypeMetadata metadata, object userObject)
			: base(cacheEntry, metadata, userObject)
		{
			EntityState state = cacheEntry.State;
			if (state == EntityState.Unchanged || state != EntityState.Deleted)
			{
			}
		}

		// Token: 0x060032EC RID: 13036 RVA: 0x000A2686 File Offset: 0x000A0886
		protected override object GetRecordValue(int ordinal)
		{
			return (this._cacheEntry as EntityEntry).GetOriginalEntityValue(this._metadata, ordinal, this._userObject, ObjectStateValueRecord.OriginalUpdatableInternal);
		}

		// Token: 0x060032ED RID: 13037 RVA: 0x000A26A6 File Offset: 0x000A08A6
		protected override void SetRecordValue(int ordinal, object value)
		{
			(this._cacheEntry as EntityEntry).SetOriginalEntityValue(this._metadata, ordinal, this._userObject, value);
		}
	}
}
