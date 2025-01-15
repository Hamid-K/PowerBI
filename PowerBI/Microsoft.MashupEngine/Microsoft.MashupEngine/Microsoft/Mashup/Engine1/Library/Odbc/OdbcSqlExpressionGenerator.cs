using System;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x02000657 RID: 1623
	internal abstract class OdbcSqlExpressionGenerator
	{
		// Token: 0x06003363 RID: 13155
		public abstract bool TryGenerateConstant(OdbcTypeInfo typeInfo, Value value, out SqlExpression sqlExpression);

		// Token: 0x06003364 RID: 13156
		public abstract bool TryGetLimitClause(RowRange rowRange, out OdbcLimitClause limitClause, out RowRange localRowRange);

		// Token: 0x06003365 RID: 13157
		public abstract bool TryGetAdditionalFunctions(out ListValue functions);

		// Token: 0x06003366 RID: 13158
		public abstract bool TryGenerateInvocation(FunctionValue visitor, TypeValue rowType, Value groupKeys, Value invocation, out Value result);
	}
}
