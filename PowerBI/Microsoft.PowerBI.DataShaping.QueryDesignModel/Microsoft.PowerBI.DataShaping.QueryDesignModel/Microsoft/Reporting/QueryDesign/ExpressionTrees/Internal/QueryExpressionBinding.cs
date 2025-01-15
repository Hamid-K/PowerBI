using System;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000179 RID: 377
	internal sealed class QueryExpressionBinding : QueryExpressionBindingBase, IEquatable<QueryExpressionBinding>
	{
		// Token: 0x0600149C RID: 5276 RVA: 0x0003B2E2 File Offset: 0x000394E2
		internal QueryExpressionBinding(QueryExpression input, QueryVariableReferenceExpression varRef)
			: base(input, varRef)
		{
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x0003B2EC File Offset: 0x000394EC
		public sealed override bool Equals(object obj)
		{
			return this.Equals(obj as QueryExpressionBinding);
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x0003B2FA File Offset: 0x000394FA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x0003B302 File Offset: 0x00039502
		public bool Equals(QueryExpressionBinding other)
		{
			return base.Equals(other);
		}
	}
}
