using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions
{
	// Token: 0x020000CA RID: 202
	internal sealed class ExpressionComparerByOriginalNode : IEqualityComparer<Expression>
	{
		// Token: 0x06000531 RID: 1329 RVA: 0x0000AE34 File Offset: 0x00009034
		private ExpressionComparerByOriginalNode()
		{
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0000AE3C File Offset: 0x0000903C
		public bool Equals(Expression x, Expression y)
		{
			return (x == null && y == null) || (x != null && y != null && x.OriginalNode.Equals(y.OriginalNode));
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0000AE5F File Offset: 0x0000905F
		public int GetHashCode(Expression obj)
		{
			if (obj == null)
			{
				return -1;
			}
			return obj.OriginalNode.GetHashCode();
		}

		// Token: 0x04000239 RID: 569
		internal static readonly ExpressionComparerByOriginalNode Instance = new ExpressionComparerByOriginalNode();
	}
}
