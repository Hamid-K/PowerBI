using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x0200008B RID: 139
	internal class ExpressionEquatableComparer<T> : IEqualityComparer<T> where T : IExpressionEquatable<T>
	{
		// Token: 0x06000361 RID: 865 RVA: 0x00006EC5 File Offset: 0x000050C5
		internal ExpressionEquatableComparer(IEqualityComparer<Expression> exprComparer)
		{
			this.m_exprComparer = exprComparer;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00006ED4 File Offset: 0x000050D4
		public bool Equals(T left, T right)
		{
			return left.Equals(right, this.m_exprComparer);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00006EEA File Offset: 0x000050EA
		public int GetHashCode(T obj)
		{
			return obj.GetHashCode(this.m_exprComparer);
		}

		// Token: 0x04000176 RID: 374
		private readonly IEqualityComparer<Expression> m_exprComparer;
	}
}
