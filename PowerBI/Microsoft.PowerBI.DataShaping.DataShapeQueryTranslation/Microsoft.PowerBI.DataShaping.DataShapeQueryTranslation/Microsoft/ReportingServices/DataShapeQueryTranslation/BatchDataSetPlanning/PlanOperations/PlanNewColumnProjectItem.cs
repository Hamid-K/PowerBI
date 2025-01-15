using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000221 RID: 545
	internal sealed class PlanNewColumnProjectItem : PlanProjectItem
	{
		// Token: 0x060012CC RID: 4812 RVA: 0x00049716 File Offset: 0x00047916
		internal PlanNewColumnProjectItem(ExpressionId expressionId, string suggestedName, ExpressionContext expressionContext, ColumnReuseKind columnReuse)
			: this(suggestedName, expressionContext, columnReuse)
		{
			this.ExpressionId = new ExpressionId?(expressionId);
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x0004972E File Offset: 0x0004792E
		internal PlanNewColumnProjectItem(ExpressionNode expressionNode, string suggestedName, ExpressionContext expressionContext, ColumnReuseKind columnReuse)
			: this(suggestedName, expressionContext, columnReuse)
		{
			this.ExpressionNode = expressionNode;
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00049741 File Offset: 0x00047941
		private PlanNewColumnProjectItem(string suggestedName, ExpressionContext expressionContext, ColumnReuseKind columnReuse)
		{
			this.SuggestedName = suggestedName;
			this.ExpressionContext = expressionContext;
			this.ColumnReuse = columnReuse;
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x0004975E File Offset: 0x0004795E
		public ExpressionId? ExpressionId { get; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x00049766 File Offset: 0x00047966
		public ExpressionNode ExpressionNode { get; }

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x0004976E File Offset: 0x0004796E
		public string SuggestedName { get; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x00049776 File Offset: 0x00047976
		public ExpressionContext ExpressionContext { get; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x0004977E File Offset: 0x0004797E
		public ColumnReuseKind ColumnReuse { get; }

		// Token: 0x060012D4 RID: 4820 RVA: 0x00049786 File Offset: 0x00047986
		public override void Accept(IPlanProjectItemVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x00049790 File Offset: 0x00047990
		protected override int GetHashCodeInternal()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<ExpressionNode>(this.ExpressionNode, null), Hashing.GetHashCode<ExpressionId?>(this.ExpressionId, null), this.SuggestedName.GetHashCode(), this.ColumnReuse.GetHashCode());
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x000497DC File Offset: 0x000479DC
		public override bool Equals(PlanProjectItem other)
		{
			PlanNewColumnProjectItem planNewColumnProjectItem = other as PlanNewColumnProjectItem;
			if (planNewColumnProjectItem != null && this.ExpressionNode == planNewColumnProjectItem.ExpressionNode)
			{
				ExpressionId? expressionId = this.ExpressionId;
				int? num = ((expressionId != null) ? new int?(expressionId.GetValueOrDefault().Value) : null);
				expressionId = planNewColumnProjectItem.ExpressionId;
				int? num2 = ((expressionId != null) ? new int?(expressionId.GetValueOrDefault().Value) : null);
				if (((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null))) && this.SuggestedName == planNewColumnProjectItem.SuggestedName && this.ExpressionContext.Equals(planNewColumnProjectItem.ExpressionContext))
				{
					return this.ColumnReuse == planNewColumnProjectItem.ColumnReuse;
				}
			}
			return false;
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x000498C4 File Offset: 0x00047AC4
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("NewColumnProjectItem");
			if (this.ExpressionNode != null)
			{
				builder.WriteAttribute<ExpressionNode>("ExpressionNode", this.ExpressionNode, false, true);
			}
			if (this.ExpressionId != null)
			{
				builder.WriteAttribute<int>("ExpressionId", this.ExpressionId.Value.Value, false, false);
			}
			builder.WriteAttribute<string>("ColumnName", this.SuggestedName, false, false);
			builder.WriteAttribute<string>("ColumnReuse", this.ColumnReuse.ToString(), false, false);
			builder.EndObject();
		}
	}
}
