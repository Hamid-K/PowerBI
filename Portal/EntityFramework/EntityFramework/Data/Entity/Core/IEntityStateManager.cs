using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core
{
	// Token: 0x020002D6 RID: 726
	internal interface IEntityStateManager
	{
		// Token: 0x06002316 RID: 8982
		IEnumerable<IEntityStateEntry> GetEntityStateEntries(EntityState state);

		// Token: 0x06002317 RID: 8983
		IEnumerable<IEntityStateEntry> FindRelationshipsByKey(EntityKey key);

		// Token: 0x06002318 RID: 8984
		IEntityStateEntry GetEntityStateEntry(EntityKey key);

		// Token: 0x06002319 RID: 8985
		bool TryGetEntityStateEntry(EntityKey key, out IEntityStateEntry stateEntry);

		// Token: 0x0600231A RID: 8986
		bool TryGetReferenceKey(EntityKey dependentKey, AssociationEndMember principalRole, out EntityKey principalKey);
	}
}
