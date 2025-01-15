using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x02000020 RID: 32
	internal abstract class ExpressionTable
	{
		// Token: 0x0600018A RID: 394 RVA: 0x0000679C File Offset: 0x0000499C
		protected ExpressionTable(ExpressionIdGenerator idGenerator)
		{
			this.m_idGenerator = idGenerator;
		}

		// Token: 0x0600018B RID: 395
		public abstract ExpressionNode GetNodeOrDefault(ExpressionId id);

		// Token: 0x0600018C RID: 396 RVA: 0x000067AB File Offset: 0x000049AB
		public ExpressionNode GetNode(ExpressionId id)
		{
			return this.GetNodeOrDefault(id);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000067B4 File Offset: 0x000049B4
		public T GetNode<T>(ExpressionId id) where T : ExpressionNode
		{
			ExpressionNode node = this.GetNode(id);
			Contract.RetailAssert(node is T, "Expected expression entry from wrong type (Expected: {0}, Actual: {1}) for {2}", typeof(T), node.GetType(), id);
			return (T)((object)node);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000067F8 File Offset: 0x000049F8
		public T GetNodeAs<T>(Expression expression) where T : ExpressionNode
		{
			ExpressionId id = ExpressionTable.GetId(expression);
			return this.GetNodeAs<T>(id);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00006813 File Offset: 0x00004A13
		public T GetNodeAs<T>(ExpressionId id) where T : ExpressionNode
		{
			return this.GetNode(id) as T;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00006828 File Offset: 0x00004A28
		private static ExpressionId GetId(Expression expression)
		{
			return expression.ExpressionId.Value;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00006843 File Offset: 0x00004A43
		public ExpressionNode GetNode(Expression expression)
		{
			return this.GetNode(ExpressionTable.GetId(expression));
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00006851 File Offset: 0x00004A51
		public ExpressionNode GetNode(SubExpressionNode subExpression)
		{
			return this.GetNode(subExpression.ExpressionId);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000685F File Offset: 0x00004A5F
		public WritableExpressionTable CreateEmptyWritableTable()
		{
			return new WritableExpressionTable(this.m_idGenerator);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000686C File Offset: 0x00004A6C
		public WritableExpressionTable CopyTable()
		{
			return new WritableExpressionTable(this.m_idGenerator, this.GetEntries());
		}

		// Token: 0x06000195 RID: 405
		internal abstract IEnumerable<ExpressionNode> GetEntries();

		// Token: 0x06000196 RID: 406
		public abstract ReadOnlyExpressionTable AsReadOnly();

		// Token: 0x04000058 RID: 88
		protected readonly ExpressionIdGenerator m_idGenerator;
	}
}
