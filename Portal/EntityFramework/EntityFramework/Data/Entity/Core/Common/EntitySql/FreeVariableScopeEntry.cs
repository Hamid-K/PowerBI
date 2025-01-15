using System;
using System.Data.Entity.Core.Common.CommandTrees;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x0200066D RID: 1645
	internal sealed class FreeVariableScopeEntry : ScopeEntry
	{
		// Token: 0x06004ECC RID: 20172 RVA: 0x0011EE53 File Offset: 0x0011D053
		internal FreeVariableScopeEntry(DbVariableReferenceExpression varRef)
			: base(ScopeEntryKind.FreeVar)
		{
			this._varRef = varRef;
		}

		// Token: 0x06004ECD RID: 20173 RVA: 0x0011EE63 File Offset: 0x0011D063
		internal override DbExpression GetExpression(string refName, ErrorContext errCtx)
		{
			return this._varRef;
		}

		// Token: 0x04001C7C RID: 7292
		private readonly DbVariableReferenceExpression _varRef;
	}
}
