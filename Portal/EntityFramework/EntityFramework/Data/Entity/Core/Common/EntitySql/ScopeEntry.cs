using System;
using System.Data.Entity.Core.Common.CommandTrees;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000666 RID: 1638
	internal abstract class ScopeEntry
	{
		// Token: 0x06004E1E RID: 19998 RVA: 0x00118910 File Offset: 0x00116B10
		internal ScopeEntry(ScopeEntryKind scopeEntryKind)
		{
			this._scopeEntryKind = scopeEntryKind;
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x06004E1F RID: 19999 RVA: 0x0011891F File Offset: 0x00116B1F
		internal ScopeEntryKind EntryKind
		{
			get
			{
				return this._scopeEntryKind;
			}
		}

		// Token: 0x06004E20 RID: 20000
		internal abstract DbExpression GetExpression(string refName, ErrorContext errCtx);

		// Token: 0x04001C59 RID: 7257
		private readonly ScopeEntryKind _scopeEntryKind;
	}
}
