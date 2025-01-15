using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x02001217 RID: 4631
	internal abstract class SqlQueryExpressionVisitor<T>
	{
		// Token: 0x06007A27 RID: 31271
		protected abstract T VisitQuerySpecification(QuerySpecification querySpecification);

		// Token: 0x06007A28 RID: 31272
		protected abstract T VisitBinaryQueryOperation(BinaryQueryOperation queryOperation);

		// Token: 0x06007A29 RID: 31273 RVA: 0x001A6C38 File Offset: 0x001A4E38
		public virtual T VisitSqlQueryExpression(SqlQueryExpression queryExpression)
		{
			QuerySpecification querySpecification = queryExpression as QuerySpecification;
			if (querySpecification != null)
			{
				return this.VisitQuerySpecification(querySpecification);
			}
			BinaryQueryOperation binaryQueryOperation = queryExpression as BinaryQueryOperation;
			if (binaryQueryOperation != null)
			{
				return this.VisitBinaryQueryOperation(binaryQueryOperation);
			}
			return default(T);
		}
	}
}
