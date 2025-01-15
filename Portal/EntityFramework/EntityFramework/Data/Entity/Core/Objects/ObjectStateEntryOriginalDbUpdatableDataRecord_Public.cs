using System;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000422 RID: 1058
	internal sealed class ObjectStateEntryOriginalDbUpdatableDataRecord_Public : ObjectStateEntryOriginalDbUpdatableDataRecord_Internal
	{
		// Token: 0x060032EE RID: 13038 RVA: 0x000A26C6 File Offset: 0x000A08C6
		internal ObjectStateEntryOriginalDbUpdatableDataRecord_Public(EntityEntry cacheEntry, StateManagerTypeMetadata metadata, object userObject, int parentEntityPropertyIndex)
			: base(cacheEntry, metadata, userObject)
		{
			this._parentEntityPropertyIndex = parentEntityPropertyIndex;
		}

		// Token: 0x060032EF RID: 13039 RVA: 0x000A26D9 File Offset: 0x000A08D9
		protected override object GetRecordValue(int ordinal)
		{
			return (this._cacheEntry as EntityEntry).GetOriginalEntityValue(this._metadata, ordinal, this._userObject, ObjectStateValueRecord.OriginalUpdatablePublic, this.GetPropertyIndex(ordinal));
		}

		// Token: 0x060032F0 RID: 13040 RVA: 0x000A2700 File Offset: 0x000A0900
		protected override void SetRecordValue(int ordinal, object value)
		{
			StateManagerMemberMetadata stateManagerMemberMetadata = this._metadata.Member(ordinal);
			if (stateManagerMemberMetadata.IsComplex)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_SetOriginalComplexProperties(stateManagerMemberMetadata.CLayerName));
			}
			object obj = value ?? DBNull.Value;
			EntityEntry entityEntry = this._cacheEntry as EntityEntry;
			EntityState state = entityEntry.State;
			if (entityEntry.HasRecordValueChanged(this, ordinal, obj))
			{
				if (stateManagerMemberMetadata.IsPartOfKey)
				{
					throw new InvalidOperationException(Strings.ObjectStateEntry_SetOriginalPrimaryKey(stateManagerMemberMetadata.CLayerName));
				}
				Type clrType = stateManagerMemberMetadata.ClrType;
				if (DBNull.Value == obj && clrType.IsValueType() && !stateManagerMemberMetadata.CdmMetadata.Nullable)
				{
					throw new InvalidOperationException(Strings.ObjectStateEntry_NullOriginalValueForNonNullableProperty(stateManagerMemberMetadata.CLayerName, stateManagerMemberMetadata.ClrMetadata.Name, stateManagerMemberMetadata.ClrMetadata.DeclaringType.FullName));
				}
				base.SetRecordValue(ordinal, value);
				if (state == EntityState.Unchanged && entityEntry.State == EntityState.Modified)
				{
					entityEntry.ObjectStateManager.ChangeState(entityEntry, state, EntityState.Modified);
				}
				entityEntry.SetModifiedPropertyInternal(this.GetPropertyIndex(ordinal));
			}
		}

		// Token: 0x060032F1 RID: 13041 RVA: 0x000A27FC File Offset: 0x000A09FC
		private int GetPropertyIndex(int ordinal)
		{
			if (this._parentEntityPropertyIndex != -1)
			{
				return this._parentEntityPropertyIndex;
			}
			return ordinal;
		}

		// Token: 0x0400108D RID: 4237
		private readonly int _parentEntityPropertyIndex;
	}
}
