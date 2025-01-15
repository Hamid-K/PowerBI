using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x0200002B RID: 43
	internal sealed class RemoveGroupingsExpressionNode : ExpressionNode
	{
		// Token: 0x0600022C RID: 556 RVA: 0x0000708C File Offset: 0x0000528C
		internal RemoveGroupingsExpressionNode(ExpressionNode expression, IEnumerable<ExpressionNode> groupKeysToRemove)
		{
			this.m_expression = expression;
			this.m_groupKeysToRemove = groupKeysToRemove.ToReadOnlyCollection<ExpressionNode>();
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600022D RID: 557 RVA: 0x000070A7 File Offset: 0x000052A7
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.RemoveGroupings;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600022E RID: 558 RVA: 0x000070AB File Offset: 0x000052AB
		public ExpressionNode Expression
		{
			get
			{
				return this.m_expression;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600022F RID: 559 RVA: 0x000070B3 File Offset: 0x000052B3
		public ReadOnlyCollection<ExpressionNode> GroupKeysToRemove
		{
			get
			{
				return this.m_groupKeysToRemove;
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000070BC File Offset: 0x000052BC
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			RemoveGroupingsExpressionNode removeGroupingsExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<RemoveGroupingsExpressionNode>(this, other, out flag, out removeGroupingsExpressionNode))
			{
				return flag;
			}
			return this.Expression.Equals(removeGroupingsExpressionNode.Expression) && this.GroupKeysToRemove.SetEquals(removeGroupingsExpressionNode.GroupKeysToRemove);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000070FE File Offset: 0x000052FE
		protected override int GetHashCodeImpl()
		{
			return Hashing.CombineHash(this.Expression.GetHashCode(), Hashing.CombineHashUnordered<ExpressionNode>(this.GroupKeysToRemove));
		}

		// Token: 0x0400009C RID: 156
		private readonly ExpressionNode m_expression;

		// Token: 0x0400009D RID: 157
		private readonly ReadOnlyCollection<ExpressionNode> m_groupKeysToRemove;
	}
}
