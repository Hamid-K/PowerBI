using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x0200065B RID: 1627
	internal sealed class InvalidGroupInputRefScopeEntry : ScopeEntry
	{
		// Token: 0x06004DFA RID: 19962 RVA: 0x00118732 File Offset: 0x00116932
		internal InvalidGroupInputRefScopeEntry()
			: base(ScopeEntryKind.InvalidGroupInputRef)
		{
		}

		// Token: 0x06004DFB RID: 19963 RVA: 0x0011873C File Offset: 0x0011693C
		internal override DbExpression GetExpression(string refName, ErrorContext errCtx)
		{
			string text = Strings.InvalidGroupIdentifierReference(refName);
			throw EntitySqlException.Create(errCtx, text, null);
		}
	}
}
