using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x02000021 RID: 33
	internal sealed class WritableExpressionTable : ExpressionTable
	{
		// Token: 0x06000197 RID: 407 RVA: 0x0000687F File Offset: 0x00004A7F
		internal WritableExpressionTable()
			: base(new ExpressionIdGenerator())
		{
			this.m_entries = new List<ExpressionNode>(100);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00006899 File Offset: 0x00004A99
		internal WritableExpressionTable(ExpressionIdGenerator idGenerator)
			: base(idGenerator)
		{
			this.m_entries = new List<ExpressionNode>(this.m_idGenerator.IdCount);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000068B8 File Offset: 0x00004AB8
		internal WritableExpressionTable(ExpressionIdGenerator idGenerator, IEnumerable<ExpressionNode> entries)
			: this(idGenerator)
		{
			this.m_entries.AddRange(entries);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000068CD File Offset: 0x00004ACD
		public override ExpressionNode GetNodeOrDefault(ExpressionId id)
		{
			if (id.Value >= this.m_entries.Count)
			{
				return null;
			}
			return this.m_entries[id.Value];
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000068F7 File Offset: 0x00004AF7
		public void SetNode(ExpressionId id, ExpressionNode node)
		{
			this.m_entries.SetAtExtendedIndex(id.Value, node);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00006910 File Offset: 0x00004B10
		public void SetNode(Expression expression, ExpressionNode node)
		{
			if (expression.ExpressionId != null)
			{
				this.SetNode(expression.ExpressionId.Value, node);
				return;
			}
			expression.ExpressionId = new ExpressionId?(this.Add(node));
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00006955 File Offset: 0x00004B55
		public void SetNode(SubExpressionNode subExpression, ExpressionNode node)
		{
			this.SetNode(subExpression.ExpressionId, node);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00006964 File Offset: 0x00004B64
		public ExpressionId Add(ExpressionNode node)
		{
			ExpressionId expressionId = this.m_idGenerator.NewId();
			this.SetNode(expressionId, node);
			return expressionId;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00006986 File Offset: 0x00004B86
		public SubExpressionNode CreateSubExpression(ExpressionNode node)
		{
			return new SubExpressionNode(this.Add(node));
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00006994 File Offset: 0x00004B94
		public override ReadOnlyExpressionTable AsReadOnly()
		{
			return new ReadOnlyExpressionTable(this.m_idGenerator, this.m_entries.AsReadOnly());
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000069AC File Offset: 0x00004BAC
		internal override IEnumerable<ExpressionNode> GetEntries()
		{
			return this.m_entries;
		}

		// Token: 0x04000059 RID: 89
		private readonly List<ExpressionNode> m_entries;
	}
}
