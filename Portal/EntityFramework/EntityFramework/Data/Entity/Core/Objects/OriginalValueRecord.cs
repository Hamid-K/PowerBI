using System;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200042A RID: 1066
	public abstract class OriginalValueRecord : DbUpdatableDataRecord
	{
		// Token: 0x060033D7 RID: 13271 RVA: 0x000A77F8 File Offset: 0x000A59F8
		internal OriginalValueRecord(ObjectStateEntry cacheEntry, StateManagerTypeMetadata metadata, object userObject)
			: base(cacheEntry, metadata, userObject)
		{
		}
	}
}
