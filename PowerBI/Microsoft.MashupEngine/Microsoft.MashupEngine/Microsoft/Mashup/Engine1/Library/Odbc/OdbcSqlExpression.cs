using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x02000653 RID: 1619
	internal abstract class OdbcSqlExpression
	{
		// Token: 0x17001267 RID: 4711
		// (get) Token: 0x06003341 RID: 13121
		public abstract OdbcSqlExpressionKind Kind { get; }

		// Token: 0x17001268 RID: 4712
		// (get) Token: 0x06003342 RID: 13122
		public abstract TypeValue TypeValue { get; }

		// Token: 0x17001269 RID: 4713
		// (get) Token: 0x06003343 RID: 13123
		public abstract OdbcConditionExpression AsCondition { get; }

		// Token: 0x1700126A RID: 4714
		// (get) Token: 0x06003344 RID: 13124
		public abstract OdbcScalarExpression AsScalar { get; }

		// Token: 0x1700126B RID: 4715
		// (get) Token: 0x06003345 RID: 13125
		public abstract OdbcStatementExpression AsStatement { get; }
	}
}
