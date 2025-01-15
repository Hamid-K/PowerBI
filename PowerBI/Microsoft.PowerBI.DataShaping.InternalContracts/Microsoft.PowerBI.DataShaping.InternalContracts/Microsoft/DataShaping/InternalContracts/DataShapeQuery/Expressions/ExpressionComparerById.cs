using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions
{
	// Token: 0x020000C9 RID: 201
	internal sealed class ExpressionComparerById : IEqualityComparer<Expression>
	{
		// Token: 0x0600052D RID: 1325 RVA: 0x0000ADAA File Offset: 0x00008FAA
		private ExpressionComparerById()
		{
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000ADB4 File Offset: 0x00008FB4
		public bool Equals(Expression x, Expression y)
		{
			return (x == null && y == null) || (x != null && y != null && x.ExpressionId.Equals(y.ExpressionId));
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0000ADF0 File Offset: 0x00008FF0
		public int GetHashCode(Expression obj)
		{
			if (obj == null || obj.ExpressionId == null)
			{
				return -1;
			}
			return obj.ExpressionId.Value.Value;
		}

		// Token: 0x04000238 RID: 568
		internal static readonly ExpressionComparerById Instance = new ExpressionComparerById();
	}
}
