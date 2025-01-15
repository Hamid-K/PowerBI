using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;

namespace Microsoft.DataShaping.Processing.Reconciliation
{
	// Token: 0x02000021 RID: 33
	internal sealed class ScopeLookupTable
	{
		// Token: 0x0600010F RID: 271 RVA: 0x00004E71 File Offset: 0x00003071
		internal ScopeLookupTable()
		{
			this._scopes = new Dictionary<string, Scope>(StringComparer.Ordinal);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004E89 File Offset: 0x00003089
		internal void Add(Scope scope)
		{
			this._scopes.Add(scope.Id, scope);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004E9D File Offset: 0x0000309D
		internal Scope Get(string id)
		{
			return this._scopes[id];
		}

		// Token: 0x040000A1 RID: 161
		private readonly Dictionary<string, Scope> _scopes;
	}
}
