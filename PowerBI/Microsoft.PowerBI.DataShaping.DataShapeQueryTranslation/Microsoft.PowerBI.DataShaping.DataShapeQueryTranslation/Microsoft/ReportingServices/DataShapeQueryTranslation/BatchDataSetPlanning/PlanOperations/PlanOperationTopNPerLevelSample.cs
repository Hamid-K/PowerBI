using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000231 RID: 561
	internal sealed class PlanOperationTopNPerLevelSample : PlanOperation
	{
		// Token: 0x06001338 RID: 4920 RVA: 0x0004A2D7 File Offset: 0x000484D7
		internal PlanOperationTopNPerLevelSample(PlanOperation input, PlanExpression rowCount, string restartIndicatorColumnName, IReadOnlyList<PlanMemberSortItem> sortItems, IReadOnlyList<IReadOnlyList<Expression>> levels, LimitWindowExpansionInstance windowExpansionInstance)
		{
			this.Input = input;
			this.RowCount = rowCount;
			this.RestartIndicatorColumnName = restartIndicatorColumnName;
			this.SortItems = sortItems;
			this.Levels = levels;
			this.WindowExpansionInstance = windowExpansionInstance;
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x0004A30C File Offset: 0x0004850C
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x0004A315 File Offset: 0x00048515
		public PlanOperation Input { get; }

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600133B RID: 4923 RVA: 0x0004A31D File Offset: 0x0004851D
		public PlanExpression RowCount { get; }

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x0004A325 File Offset: 0x00048525
		public string RestartIndicatorColumnName { get; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x0004A32D File Offset: 0x0004852D
		public IReadOnlyList<PlanMemberSortItem> SortItems { get; }

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x0004A335 File Offset: 0x00048535
		public IReadOnlyList<IReadOnlyList<Expression>> Levels { get; }

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x0004A33D File Offset: 0x0004853D
		public LimitWindowExpansionInstance WindowExpansionInstance { get; }

		// Token: 0x06001340 RID: 4928 RVA: 0x0004A348 File Offset: 0x00048548
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationTopNPerLevelSample planOperationTopNPerLevelSample;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationTopNPerLevelSample>(this, other, out flag, out planOperationTopNPerLevelSample))
			{
				return flag;
			}
			return this.Input == planOperationTopNPerLevelSample.Input && this.RowCount == planOperationTopNPerLevelSample.RowCount && this.RestartIndicatorColumnName.Equals(planOperationTopNPerLevelSample.RestartIndicatorColumnName) && this.SortItems.SequenceEqual(planOperationTopNPerLevelSample.SortItems) && this.Levels.SequenceEqual(planOperationTopNPerLevelSample.Levels) && this.WindowExpansionInstance == planOperationTopNPerLevelSample.WindowExpansionInstance;
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x0004A3CC File Offset: 0x000485CC
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("TopNPerLevel");
			builder.WriteProperty<PlanExpression>("RowCount", this.RowCount, false);
			builder.WriteProperty<PlanOperation>("Input", this.Input, false);
			builder.WriteProperty<string>("RestartIndicatorColumnName", this.RestartIndicatorColumnName, false);
			builder.WriteProperty<IReadOnlyList<PlanMemberSortItem>>("SortItems", this.SortItems, false);
			builder.WriteProperty<IReadOnlyList<IReadOnlyList<Expression>>>("Levels", this.Levels, false);
			builder.WriteProperty<LimitWindowExpansionInstance>("WindowExpansionInstance", this.WindowExpansionInstance, false);
			builder.EndObject();
		}
	}
}
