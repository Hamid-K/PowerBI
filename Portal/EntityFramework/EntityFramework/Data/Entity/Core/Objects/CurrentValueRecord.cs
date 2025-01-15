using System;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000402 RID: 1026
	public abstract class CurrentValueRecord : DbUpdatableDataRecord
	{
		// Token: 0x06002FC3 RID: 12227 RVA: 0x00096ECB File Offset: 0x000950CB
		internal CurrentValueRecord(ObjectStateEntry cacheEntry, StateManagerTypeMetadata metadata, object userObject)
			: base(cacheEntry, metadata, userObject)
		{
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x00096ED6 File Offset: 0x000950D6
		internal CurrentValueRecord(ObjectStateEntry cacheEntry)
			: base(cacheEntry)
		{
		}
	}
}
