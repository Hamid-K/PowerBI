using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x0200003B RID: 59
	internal sealed class TableSubQueryExpressionNode : ExpressionNode
	{
		// Token: 0x06000284 RID: 644 RVA: 0x000076DD File Offset: 0x000058DD
		internal TableSubQueryExpressionNode(DataSetPlan dataSetPlan, IContextItem source, string name)
		{
			this.DataSetPlan = dataSetPlan;
			this.Source = source;
			this.Name = name;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000285 RID: 645 RVA: 0x000076FA File Offset: 0x000058FA
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.TableSubQueryExpression;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000286 RID: 646 RVA: 0x000076FE File Offset: 0x000058FE
		public DataSetPlan DataSetPlan { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00007706 File Offset: 0x00005906
		public IContextItem Source { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000770E File Offset: 0x0000590E
		public string Name { get; }

		// Token: 0x06000289 RID: 649 RVA: 0x00007718 File Offset: 0x00005918
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			TableSubQueryExpressionNode tableSubQueryExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<TableSubQueryExpressionNode>(this, other, out flag, out tableSubQueryExpressionNode))
			{
				return flag;
			}
			return this.DataSetPlan == tableSubQueryExpressionNode.DataSetPlan && this.Source == tableSubQueryExpressionNode.Source && this.Name == tableSubQueryExpressionNode.Name;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00007763 File Offset: 0x00005963
		protected override int GetHashCodeImpl()
		{
			return Hashing.CombineHash(this.DataSetPlan.GetHashCode(), this.Source.GetHashCode(), this.Name.GetHashCode());
		}
	}
}
