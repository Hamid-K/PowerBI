using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x02000027 RID: 39
	internal sealed class FilterInlinedCalculationExpressionNode : ExpressionNode
	{
		// Token: 0x0600021A RID: 538 RVA: 0x00006F04 File Offset: 0x00005104
		internal FilterInlinedCalculationExpressionNode(ExpressionNode expression, Calculation calculation, FilterCondition filterCondition, ExpressionNode filterExpression)
		{
			this.ExpressionNode = expression;
			this.Calculation = calculation;
			this.FilterCondition = filterCondition;
			this.FilterExpression = filterExpression;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00006F29 File Offset: 0x00005129
		public Calculation Calculation { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00006F31 File Offset: 0x00005131
		public ExpressionNode ExpressionNode { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00006F39 File Offset: 0x00005139
		public FilterCondition FilterCondition { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00006F41 File Offset: 0x00005141
		public ExpressionNode FilterExpression { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00006F49 File Offset: 0x00005149
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.FilterInlinedCalculation;
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00006F50 File Offset: 0x00005150
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			FilterInlinedCalculationExpressionNode filterInlinedCalculationExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<FilterInlinedCalculationExpressionNode>(this, other, out flag, out filterInlinedCalculationExpressionNode))
			{
				return flag;
			}
			return this.ExpressionNode.Equals(filterInlinedCalculationExpressionNode.ExpressionNode) && this.FilterCondition.Equals(filterInlinedCalculationExpressionNode.FilterCondition) && this.Calculation == filterInlinedCalculationExpressionNode.Calculation;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00006FA2 File Offset: 0x000051A2
		protected override int GetHashCodeImpl()
		{
			return Hashing.CombineHash(this.Calculation.GetHashCode(), this.ExpressionNode.GetHashCode(), this.FilterCondition.GetHashCode());
		}
	}
}
