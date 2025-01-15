using System;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000191 RID: 401
	internal sealed class QueryGroupExpressionBinding : QueryExpressionBindingBase, IEquatable<QueryGroupExpressionBinding>
	{
		// Token: 0x0600155C RID: 5468 RVA: 0x0003BF19 File Offset: 0x0003A119
		internal QueryGroupExpressionBinding(QueryExpression input, QueryVariableReferenceExpression varRef)
			: base(input, varRef)
		{
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x0003BF23 File Offset: 0x0003A123
		public sealed override bool Equals(object obj)
		{
			return this.Equals(obj as QueryGroupExpressionBinding);
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x0003BF31 File Offset: 0x0003A131
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x0003BF39 File Offset: 0x0003A139
		public bool Equals(QueryGroupExpressionBinding other)
		{
			return base.Equals(other);
		}
	}
}
