using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x0200009F RID: 159
	internal interface IExpressionEquatable<T>
	{
		// Token: 0x060003BB RID: 955
		bool Equals(T other, IEqualityComparer<Expression> expressionComparer);

		// Token: 0x060003BC RID: 956
		int GetHashCode(IEqualityComparer<Expression> expressionComparer);
	}
}
