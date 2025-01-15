using System;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000420 RID: 1056
	internal sealed class ObjectStateEntryDbUpdatableDataRecord : CurrentValueRecord
	{
		// Token: 0x060032E7 RID: 13031 RVA: 0x000A2590 File Offset: 0x000A0790
		internal ObjectStateEntryDbUpdatableDataRecord(EntityEntry cacheEntry, StateManagerTypeMetadata metadata, object userObject)
			: base(cacheEntry, metadata, userObject)
		{
			EntityState state = cacheEntry.State;
			if (state == EntityState.Unchanged || state != EntityState.Added)
			{
			}
		}

		// Token: 0x060032E8 RID: 13032 RVA: 0x000A25BC File Offset: 0x000A07BC
		internal ObjectStateEntryDbUpdatableDataRecord(RelationshipEntry cacheEntry)
			: base(cacheEntry)
		{
			EntityState state = cacheEntry.State;
			if (state == EntityState.Unchanged || state != EntityState.Added)
			{
			}
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x000A25E4 File Offset: 0x000A07E4
		protected override object GetRecordValue(int ordinal)
		{
			if (this._cacheEntry.IsRelationship)
			{
				return (this._cacheEntry as RelationshipEntry).GetCurrentRelationValue(ordinal);
			}
			return (this._cacheEntry as EntityEntry).GetCurrentEntityValue(this._metadata, ordinal, this._userObject, ObjectStateValueRecord.CurrentUpdatable);
		}

		// Token: 0x060032EA RID: 13034 RVA: 0x000A2623 File Offset: 0x000A0823
		protected override void SetRecordValue(int ordinal, object value)
		{
			if (this._cacheEntry.IsRelationship)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CantModifyRelationValues);
			}
			(this._cacheEntry as EntityEntry).SetCurrentEntityValue(this._metadata, ordinal, this._userObject, value);
		}
	}
}
