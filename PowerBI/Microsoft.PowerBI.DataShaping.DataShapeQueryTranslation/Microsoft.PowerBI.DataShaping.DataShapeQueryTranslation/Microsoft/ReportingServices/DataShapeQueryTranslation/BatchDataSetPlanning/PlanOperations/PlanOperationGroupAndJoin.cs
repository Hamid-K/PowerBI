using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000205 RID: 517
	internal sealed class PlanOperationGroupAndJoin : PlanOperation
	{
		// Token: 0x060011F9 RID: 4601 RVA: 0x000481D0 File Offset: 0x000463D0
		internal PlanOperationGroupAndJoin(IReadOnlyList<PlanGroupByMember> primaryGroupingBucket, IReadOnlyList<PlanGroupByMember> secondaryGroupingBucket, IReadOnlyList<Calculation> calculations, IReadOnlyList<PlanGroupByDataTransformColumn> groupingTransformColumns, IReadOnlyList<PlanDataTransformColumnMeasure> measureTransformColumns, IReadOnlyList<PlanOperation> contextTables, IReadOnlyList<ExistsFilterItem> existsFilters, IReadOnlyList<PlanGroupAndJoinAdditionalColumn> additionalColumns, IReadOnlyList<PlanGroupByGroupKey> additionalGroupingColumns, PlanGroupAndJoinPredicateBehavior predicateBehavior, bool allowEmptyGroups, bool suppressUnconstrainedJoinCheck, bool inUniqueMeasureNamesBlock, string telemetryName = null)
		{
			this.PrimaryGroupingBucket = primaryGroupingBucket;
			this.SecondaryGroupingBucket = secondaryGroupingBucket;
			this.Calculations = calculations;
			this.GroupingTransformColumns = groupingTransformColumns;
			this.MeasureTransformColumns = measureTransformColumns;
			this.ContextTables = contextTables;
			this.ExistsFilters = existsFilters;
			this.AdditionalColumns = additionalColumns;
			this.AdditionalGroupingColumns = additionalGroupingColumns;
			this.PredicateBehavior = predicateBehavior;
			this.AllowEmptyGroups = allowEmptyGroups;
			this.SuppressUnconstrainedJoinCheck = suppressUnconstrainedJoinCheck;
			this.InUniqueMeasureNamesBlock = inUniqueMeasureNamesBlock;
			this.TelemetryName = telemetryName;
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x00048250 File Offset: 0x00046450
		internal IReadOnlyList<PlanGroupByMember> PrimaryGroupingBucket { get; }

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060011FB RID: 4603 RVA: 0x00048258 File Offset: 0x00046458
		internal IReadOnlyList<PlanGroupByMember> SecondaryGroupingBucket { get; }

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x00048260 File Offset: 0x00046460
		internal IReadOnlyList<Calculation> Calculations { get; }

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x00048268 File Offset: 0x00046468
		internal IReadOnlyList<PlanGroupByDataTransformColumn> GroupingTransformColumns { get; }

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x00048270 File Offset: 0x00046470
		internal IReadOnlyList<PlanDataTransformColumnMeasure> MeasureTransformColumns { get; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x00048278 File Offset: 0x00046478
		internal IReadOnlyList<PlanOperation> ContextTables { get; }

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x00048280 File Offset: 0x00046480
		internal IReadOnlyList<ExistsFilterItem> ExistsFilters { get; }

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06001201 RID: 4609 RVA: 0x00048288 File Offset: 0x00046488
		internal IReadOnlyList<PlanGroupAndJoinAdditionalColumn> AdditionalColumns { get; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x00048290 File Offset: 0x00046490
		internal IReadOnlyList<PlanGroupByGroupKey> AdditionalGroupingColumns { get; }

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06001203 RID: 4611 RVA: 0x00048298 File Offset: 0x00046498
		internal PlanGroupAndJoinPredicateBehavior PredicateBehavior { get; }

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x000482A0 File Offset: 0x000464A0
		public bool AllowEmptyGroups { get; }

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06001205 RID: 4613 RVA: 0x000482A8 File Offset: 0x000464A8
		public bool SuppressUnconstrainedJoinCheck { get; }

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x000482B0 File Offset: 0x000464B0
		public bool InUniqueMeasureNamesBlock { get; }

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06001207 RID: 4615 RVA: 0x000482B8 File Offset: 0x000464B8
		public string TelemetryName { get; }

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x000482C0 File Offset: 0x000464C0
		public bool ShouldBeConsideredForTelemetry
		{
			get
			{
				return this.TelemetryName != null;
			}
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x000482CB File Offset: 0x000464CB
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x000482D4 File Offset: 0x000464D4
		public PlanOperationGroupAndJoin AddContextTables(IEnumerable<PlanOperation> contextTables)
		{
			return new PlanOperationGroupAndJoin(this.PrimaryGroupingBucket, this.SecondaryGroupingBucket, this.Calculations, this.GroupingTransformColumns, this.MeasureTransformColumns, this.ContextTables.Concat(contextTables).ToList<PlanOperation>(), this.ExistsFilters, this.AdditionalColumns, this.AdditionalGroupingColumns, this.PredicateBehavior, this.AllowEmptyGroups, this.SuppressUnconstrainedJoinCheck, this.InUniqueMeasureNamesBlock, null);
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00048340 File Offset: 0x00046540
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationGroupAndJoin planOperationGroupAndJoin;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationGroupAndJoin>(this, other, out flag, out planOperationGroupAndJoin))
			{
				return flag;
			}
			return this.PrimaryGroupingBucket.SequenceEqual(planOperationGroupAndJoin.PrimaryGroupingBucket) && this.SecondaryGroupingBucket.SequenceEqual(planOperationGroupAndJoin.SecondaryGroupingBucket) && this.Calculations.SequenceEqual(planOperationGroupAndJoin.Calculations) && this.GroupingTransformColumns.SequenceEqual(planOperationGroupAndJoin.GroupingTransformColumns) && this.MeasureTransformColumns.SequenceEqual(planOperationGroupAndJoin.MeasureTransformColumns) && this.ContextTables.SequenceEqual(planOperationGroupAndJoin.ContextTables) && this.ExistsFilters.SequenceEqual(planOperationGroupAndJoin.ExistsFilters) && this.AdditionalColumns.SequenceEqual(planOperationGroupAndJoin.AdditionalColumns) && this.AdditionalGroupingColumns.SequenceEqual(planOperationGroupAndJoin.AdditionalGroupingColumns) && this.PredicateBehavior == planOperationGroupAndJoin.PredicateBehavior && this.AllowEmptyGroups == planOperationGroupAndJoin.AllowEmptyGroups && this.SuppressUnconstrainedJoinCheck == planOperationGroupAndJoin.SuppressUnconstrainedJoinCheck && this.InUniqueMeasureNamesBlock == planOperationGroupAndJoin.InUniqueMeasureNamesBlock;
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x00048450 File Offset: 0x00046650
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("GroupAndJoin");
			builder.WriteAttribute<PlanGroupAndJoinPredicateBehavior>("PredicateBehavior", this.PredicateBehavior, false, false);
			builder.WriteAttribute<bool>("AllowEmptyGroups", this.AllowEmptyGroups, false, false);
			builder.WriteAttribute<bool>("SuppressUnconstrainedJoin", this.SuppressUnconstrainedJoinCheck, false, false);
			builder.WriteAttribute<bool>("InUniqueMeasureNamesBlock", this.InUniqueMeasureNamesBlock, false, false);
			builder.WriteProperty<IReadOnlyList<PlanGroupByMember>>("PrimaryGroupingBucket", this.PrimaryGroupingBucket, false);
			builder.WriteProperty<IReadOnlyList<PlanGroupByMember>>("SecondaryGroupingBucket", this.SecondaryGroupingBucket, false);
			builder.WriteProperty<IReadOnlyList<Calculation>>("Calculations", this.Calculations, false);
			builder.WriteProperty<IReadOnlyList<PlanGroupByDataTransformColumn>>("GroupingTransformColumns", this.GroupingTransformColumns, false);
			builder.WriteProperty<IReadOnlyList<PlanDataTransformColumnMeasure>>("MeasureTransformColumns", this.MeasureTransformColumns, false);
			builder.WriteProperty<IReadOnlyList<PlanGroupAndJoinAdditionalColumn>>("AdditionalColumns", this.AdditionalColumns, false);
			builder.WriteProperty<IReadOnlyList<PlanGroupByGroupKey>>("AdditionalGroupingColumns", this.AdditionalGroupingColumns, false);
			builder.WriteProperty<IReadOnlyList<ExistsFilterItem>>("ExistsFilters", this.ExistsFilters, false);
			builder.WriteProperty<IReadOnlyList<PlanOperation>>("ContextTables", this.ContextTables, false);
			builder.EndObject();
		}
	}
}
