using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;

namespace System.Data.Entity.Core
{
	// Token: 0x020002D5 RID: 725
	internal interface IEntityStateEntry
	{
		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06002308 RID: 8968
		IEntityStateManager StateManager { get; }

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06002309 RID: 8969
		EntityKey EntityKey { get; }

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x0600230A RID: 8970
		EntitySetBase EntitySet { get; }

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x0600230B RID: 8971
		bool IsRelationship { get; }

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x0600230C RID: 8972
		bool IsKeyEntry { get; }

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x0600230D RID: 8973
		EntityState State { get; }

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x0600230E RID: 8974
		DbDataRecord OriginalValues { get; }

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x0600230F RID: 8975
		CurrentValueRecord CurrentValues { get; }

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06002310 RID: 8976
		BitArray ModifiedProperties { get; }

		// Token: 0x06002311 RID: 8977
		void AcceptChanges();

		// Token: 0x06002312 RID: 8978
		void Delete();

		// Token: 0x06002313 RID: 8979
		void SetModified();

		// Token: 0x06002314 RID: 8980
		void SetModifiedProperty(string propertyName);

		// Token: 0x06002315 RID: 8981
		IEnumerable<string> GetModifiedProperties();
	}
}
