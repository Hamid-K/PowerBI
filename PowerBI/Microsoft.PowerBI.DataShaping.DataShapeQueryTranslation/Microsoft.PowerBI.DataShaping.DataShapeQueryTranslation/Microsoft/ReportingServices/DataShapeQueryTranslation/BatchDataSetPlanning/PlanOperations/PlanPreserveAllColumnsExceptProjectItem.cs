using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.InfoNav;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000220 RID: 544
	internal sealed class PlanPreserveAllColumnsExceptProjectItem : PlanProjectItem
	{
		// Token: 0x060012C3 RID: 4803 RVA: 0x0004962A File Offset: 0x0004782A
		internal PlanPreserveAllColumnsExceptProjectItem(IReadOnlyList<string> columnNames)
			: this(columnNames, null)
		{
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00049634 File Offset: 0x00047834
		internal PlanPreserveAllColumnsExceptProjectItem(IReadOnlyList<PlanProjectItem> planProjectItems)
			: this(null, planProjectItems)
		{
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x0004963E File Offset: 0x0004783E
		private PlanPreserveAllColumnsExceptProjectItem(IReadOnlyList<string> columnNames, IReadOnlyList<PlanProjectItem> planProjectItems)
		{
			this.ColumnNames = columnNames;
			this.PlanProjectItems = planProjectItems;
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x00049654 File Offset: 0x00047854
		public IReadOnlyList<string> ColumnNames { get; }

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x0004965C File Offset: 0x0004785C
		public IReadOnlyList<PlanProjectItem> PlanProjectItems { get; }

		// Token: 0x060012C8 RID: 4808 RVA: 0x00049664 File Offset: 0x00047864
		public override void Accept(IPlanProjectItemVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0004966D File Offset: 0x0004786D
		protected override int GetHashCodeInternal()
		{
			return Microsoft.DataShaping.Common.Hashing.CombineHash(Microsoft.DataShaping.Common.Hashing.CombineHashReadonly<string>(this.ColumnNames, null), Microsoft.DataShaping.Common.Hashing.CombineHashReadonly<PlanProjectItem>(this.PlanProjectItems, null));
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x0004968C File Offset: 0x0004788C
		public override bool Equals(PlanProjectItem other)
		{
			PlanPreserveAllColumnsExceptProjectItem planPreserveAllColumnsExceptProjectItem = other as PlanPreserveAllColumnsExceptProjectItem;
			bool? flag = Util.AreEqual<PlanPreserveAllColumnsExceptProjectItem>(this, planPreserveAllColumnsExceptProjectItem);
			if (flag == null)
			{
				return this.ColumnNames.SequenceEqualReadOnly(planPreserveAllColumnsExceptProjectItem.ColumnNames) && this.PlanProjectItems.SequenceEqualReadOnly(planPreserveAllColumnsExceptProjectItem.PlanProjectItems);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x000496DF File Offset: 0x000478DF
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("PlanPreserveAllColumnsExceptProjectItem");
			builder.WriteProperty<IReadOnlyList<string>>("Columns", this.ColumnNames, false);
			builder.WriteProperty<IReadOnlyList<PlanProjectItem>>("PlanProjectItems", this.PlanProjectItems, false);
			builder.EndObject();
		}
	}
}
