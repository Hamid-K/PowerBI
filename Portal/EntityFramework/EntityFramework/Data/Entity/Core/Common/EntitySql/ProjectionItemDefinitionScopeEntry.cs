using System;
using System.Data.Entity.Core.Common.CommandTrees;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x0200066C RID: 1644
	internal sealed class ProjectionItemDefinitionScopeEntry : ScopeEntry
	{
		// Token: 0x06004ECA RID: 20170 RVA: 0x0011EE3B File Offset: 0x0011D03B
		internal ProjectionItemDefinitionScopeEntry(DbExpression expression)
			: base(ScopeEntryKind.ProjectionItemDefinition)
		{
			this._expression = expression;
		}

		// Token: 0x06004ECB RID: 20171 RVA: 0x0011EE4B File Offset: 0x0011D04B
		internal override DbExpression GetExpression(string refName, ErrorContext errCtx)
		{
			return this._expression;
		}

		// Token: 0x04001C7B RID: 7291
		private readonly DbExpression _expression;
	}
}
