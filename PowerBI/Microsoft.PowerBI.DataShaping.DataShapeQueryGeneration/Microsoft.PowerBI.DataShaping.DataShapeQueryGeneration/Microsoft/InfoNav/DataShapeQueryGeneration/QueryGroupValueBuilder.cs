using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000B0 RID: 176
	internal class QueryGroupValueBuilder
	{
		// Token: 0x06000676 RID: 1654 RVA: 0x00018AC8 File Offset: 0x00016CC8
		internal QueryGroupValueBuilder(ProjectedDsqExpression expression, IConceptualColumn field, bool isProjected, bool isIdentityKey, bool isOrderByKey)
		{
			this._expression = expression;
			this._field = field;
			this._isProjected = isProjected;
			this._isIdentityKey = isIdentityKey;
			this._isOrderByKey = isOrderByKey;
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00018AF5 File Offset: 0x00016CF5
		internal QueryGroupValueBuilder(ProjectedDsqExpression intervalMinValue, ProjectedDsqExpression intervalMaxValue, IConceptualColumn underlyingField)
		{
			this._intervalMinValue = intervalMinValue;
			this._intervalMaxValue = intervalMaxValue;
			this._field = underlyingField;
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x00018B12 File Offset: 0x00016D12
		internal bool IsIdentityOnly
		{
			get
			{
				return this._isIdentityKey && !this._isProjected;
			}
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00018B27 File Offset: 0x00016D27
		internal void AddSelectIndexWithThisIdentity(int selectIndex)
		{
			Util.AddToLazyList<int>(ref this._selectIndicesWithThisIdentity, selectIndex);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00018B35 File Offset: 0x00016D35
		internal bool MatchesSingleExpression(ExpressionNode expression)
		{
			return this._expression.MatchesExpression(expression);
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00018B43 File Offset: 0x00016D43
		internal void SetIsIdentity()
		{
			this._isIdentityKey = true;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00018B4C File Offset: 0x00016D4C
		internal void SetIsOrderByKey()
		{
			this._isOrderByKey = true;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00018B55 File Offset: 0x00016D55
		internal bool TryPromoteToProjected(ProjectedDsqExpression expression)
		{
			if (this._isProjected)
			{
				return false;
			}
			this._isProjected = true;
			this._expression = expression;
			return true;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00018B70 File Offset: 0x00016D70
		internal void AddAdditionalSemanticQuerySelectIndex(int selectIndex)
		{
			this._expression.AddAdditionalSemanticQuerySelectIndex(selectIndex);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00018B7E File Offset: 0x00016D7E
		internal ExpressionNode GetDsqExpressionNode()
		{
			return this._expression.Value.DsqExpression;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00018B90 File Offset: 0x00016D90
		internal ProjectedDsqExpression GetProjectedDsqExpression()
		{
			return this._expression;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00018B98 File Offset: 0x00016D98
		internal DsqExpressionAggregates GetAggregates()
		{
			if (this._expression != null)
			{
				return this._expression.Aggregates;
			}
			if (this._intervalMinValue != null)
			{
				return this._intervalMinValue.Aggregates;
			}
			return null;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00018BC4 File Offset: 0x00016DC4
		internal QueryGroupValue ToGroupValue()
		{
			if (this._intervalMinValue != null && this._intervalMaxValue != null)
			{
				return new QueryGroupIntervalValue(this._intervalMinValue, this._intervalMaxValue, this._field);
			}
			QueryGroupValueBindingHints queryGroupValueBindingHints = new QueryGroupValueBindingHints(this._field, this._selectIndicesWithThisIdentity, this._isIdentityKey, this._isOrderByKey, this._isProjected);
			return new QueryGroupSingleValue(this._expression, queryGroupValueBindingHints);
		}

		// Token: 0x04000373 RID: 883
		private ProjectedDsqExpression _expression;

		// Token: 0x04000374 RID: 884
		private IConceptualColumn _field;

		// Token: 0x04000375 RID: 885
		private List<int> _selectIndicesWithThisIdentity;

		// Token: 0x04000376 RID: 886
		private ProjectedDsqExpression _intervalMinValue;

		// Token: 0x04000377 RID: 887
		private ProjectedDsqExpression _intervalMaxValue;

		// Token: 0x04000378 RID: 888
		private bool _isIdentityKey;

		// Token: 0x04000379 RID: 889
		private bool _isOrderByKey;

		// Token: 0x0400037A RID: 890
		private bool _isProjected;
	}
}
