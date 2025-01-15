using System;
using Microsoft.DataShaping.Common;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x0200017A RID: 378
	internal abstract class QueryExpressionBindingBase
	{
		// Token: 0x060014A0 RID: 5280 RVA: 0x0003B30B File Offset: 0x0003950B
		protected QueryExpressionBindingBase(QueryExpression input, QueryVariableReferenceExpression varRef)
		{
			this.Expression = input;
			this.Variable = varRef;
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x0003B321 File Offset: 0x00039521
		public QueryExpression Expression { get; }

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x0003B329 File Offset: 0x00039529
		public QueryVariableReferenceExpression Variable { get; }

		// Token: 0x060014A3 RID: 5283 RVA: 0x0003B331 File Offset: 0x00039531
		public override bool Equals(object obj)
		{
			return this.Equals(obj as QueryExpressionBindingBase);
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x0003B33F File Offset: 0x0003953F
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Expression.GetHashCode(), this.Variable.GetHashCode());
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x0003B35C File Offset: 0x0003955C
		protected bool Equals(QueryExpressionBindingBase other)
		{
			return this == other || (other != null && this.Expression.Equals(other.Expression) && this.Variable.Equals(other.Variable));
		}
	}
}
