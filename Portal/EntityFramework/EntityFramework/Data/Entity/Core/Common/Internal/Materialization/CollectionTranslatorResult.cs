using System;
using System.Linq.Expressions;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x02000633 RID: 1587
	internal class CollectionTranslatorResult : TranslatorResult
	{
		// Token: 0x06004C65 RID: 19557 RVA: 0x0010DFC8 File Offset: 0x0010C1C8
		internal CollectionTranslatorResult(Expression returnedExpression, Type requestedType, Expression expressionToGetCoordinator)
			: base(returnedExpression, requestedType)
		{
			this.ExpressionToGetCoordinator = expressionToGetCoordinator;
		}

		// Token: 0x04001B0A RID: 6922
		internal readonly Expression ExpressionToGetCoordinator;
	}
}
