using System;
using Microsoft.Mashup.Engine1.Language.Query;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000848 RID: 2120
	internal sealed class ColumnAccessRewriter : QueryExpressionVisitor
	{
		// Token: 0x06003D32 RID: 15666 RVA: 0x000C7263 File Offset: 0x000C5463
		private ColumnAccessRewriter(ODataColumns columns, Capabilities capability)
		{
			this.columns = columns;
			this.capability = capability;
		}

		// Token: 0x06003D33 RID: 15667 RVA: 0x000C7279 File Offset: 0x000C5479
		public static QueryExpression Rewrite(QueryExpression expression, ODataColumns columns, Capabilities capability)
		{
			return new ColumnAccessRewriter(columns, capability).Visit(expression);
		}

		// Token: 0x06003D34 RID: 15668 RVA: 0x000C7288 File Offset: 0x000C5488
		protected override QueryExpression VisitColumnAccess(ColumnAccessQueryExpression columnAccess)
		{
			string text = this.columns.Names[columnAccess.Column];
			if (!this.capability.CanFilter(text))
			{
				throw new NotSupportedException();
			}
			return this.columns.Expressions[columnAccess.Column].Expression;
		}

		// Token: 0x04002005 RID: 8197
		private readonly ODataColumns columns;

		// Token: 0x04002006 RID: 8198
		private readonly Capabilities capability;
	}
}
