using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000042 RID: 66
	internal sealed class Group
	{
		// Token: 0x060001D9 RID: 473 RVA: 0x00005FC1 File Offset: 0x000041C1
		internal Group(IList<GroupKey> groupKeys, ScopeIdDefinition scopeIdDefinition)
		{
			this._groupKeys = groupKeys;
			this._scopeIdDefinition = scopeIdDefinition;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00005FD7 File Offset: 0x000041D7
		internal IList<GroupKey> GroupKeys
		{
			get
			{
				return this._groupKeys;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00005FDF File Offset: 0x000041DF
		internal ScopeIdDefinition ScopeIdDefinition
		{
			get
			{
				return this._scopeIdDefinition;
			}
		}

		// Token: 0x04000121 RID: 289
		private readonly IList<GroupKey> _groupKeys;

		// Token: 0x04000122 RID: 290
		private readonly ScopeIdDefinition _scopeIdDefinition;
	}
}
