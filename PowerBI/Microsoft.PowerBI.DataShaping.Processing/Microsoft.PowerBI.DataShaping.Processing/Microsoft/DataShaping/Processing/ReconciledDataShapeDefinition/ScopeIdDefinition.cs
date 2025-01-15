using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000044 RID: 68
	internal sealed class ScopeIdDefinition
	{
		// Token: 0x060001DE RID: 478 RVA: 0x00005FFE File Offset: 0x000041FE
		internal ScopeIdDefinition(IList<ScopeKey> scopeKeys)
		{
			this._scopeKeys = scopeKeys;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000600D File Offset: 0x0000420D
		internal IList<ScopeKey> ScopeKeys
		{
			get
			{
				return this._scopeKeys;
			}
		}

		// Token: 0x04000124 RID: 292
		private readonly IList<ScopeKey> _scopeKeys;
	}
}
