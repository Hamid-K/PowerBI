using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000208 RID: 520
	internal sealed class PlanGroupByMember : PlanGroupByItem
	{
		// Token: 0x0600121E RID: 4638 RVA: 0x00048772 File Offset: 0x00046972
		internal PlanGroupByMember(DataMember member, bool includeSortByMeasureKeysAtMeasureScope, bool excludeMeasureSortKeys, IReadOnlyList<PlanOperation> contextTables)
			: this(member, includeSortByMeasureKeysAtMeasureScope, null, excludeMeasureSortKeys, contextTables)
		{
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00048780 File Offset: 0x00046980
		internal PlanGroupByMember(DataMember member, bool includeSortByMeasureKeysAtMeasureScope, string subtotalIndicatorColumnName, bool excludeMeasureSortKeys, IReadOnlyList<PlanOperation> contextTables)
		{
			this.m_member = member;
			this.m_subtotalIndicatorColumnName = subtotalIndicatorColumnName;
			this.m_includeSortByMeasureKeysAtMeasureScope = includeSortByMeasureKeysAtMeasureScope;
			this.m_excludeMeasureSortKeys = excludeMeasureSortKeys;
			this.m_contextTables = contextTables;
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001220 RID: 4640 RVA: 0x000487AD File Offset: 0x000469AD
		public DataMember Member
		{
			get
			{
				return this.m_member;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x000487B5 File Offset: 0x000469B5
		public bool RequiresRollupGroup
		{
			get
			{
				return !string.IsNullOrEmpty(this.m_subtotalIndicatorColumnName);
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x000487C5 File Offset: 0x000469C5
		public string SubtotalIndicatorColumnName
		{
			get
			{
				return this.m_subtotalIndicatorColumnName;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x000487CD File Offset: 0x000469CD
		public bool IncludeSortByMeasureKeysAtMeasureScope
		{
			get
			{
				return this.m_includeSortByMeasureKeysAtMeasureScope;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x000487D5 File Offset: 0x000469D5
		public bool ExcludeMeasureSortKeys
		{
			get
			{
				return this.m_excludeMeasureSortKeys;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x000487DD File Offset: 0x000469DD
		public IReadOnlyList<PlanOperation> ContextTables
		{
			get
			{
				return this.m_contextTables;
			}
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x000487E5 File Offset: 0x000469E5
		internal override void Accept(IPlanGroupByItemVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x000487EE File Offset: 0x000469EE
		protected override int GetHashCodeInternal()
		{
			return this.m_member.GetHashCode();
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x000487FC File Offset: 0x000469FC
		public override bool Equals(PlanGroupByItem other)
		{
			PlanGroupByMember planGroupByMember = other as PlanGroupByMember;
			return planGroupByMember != null && this.SubtotalIndicatorColumnName == planGroupByMember.SubtotalIndicatorColumnName && this.Member == planGroupByMember.Member && this.IncludeSortByMeasureKeysAtMeasureScope == planGroupByMember.IncludeSortByMeasureKeysAtMeasureScope && this.ExcludeMeasureSortKeys == planGroupByMember.ExcludeMeasureSortKeys && this.ContextTables.SequenceEqualReadOnly(planGroupByMember.ContextTables);
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x00048864 File Offset: 0x00046A64
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("GroupByMember");
			builder.WriteAttribute<string>("Member", this.m_member.Id.Value, false, false);
			builder.WriteAttribute<string>("SubtotalIndicatorColumn", this.m_subtotalIndicatorColumnName, false, false);
			builder.WriteAttribute<bool>("IncludeSortByMeasureKeysAtMeasureScope", this.m_includeSortByMeasureKeysAtMeasureScope, false, false);
			builder.WriteAttribute<bool>("ExcludeMeasureSortKeys", this.m_excludeMeasureSortKeys, false, false);
			if (this.m_contextTables != null)
			{
				builder.WriteProperty<IReadOnlyList<PlanOperation>>("ContextTables", this.m_contextTables, false);
			}
			builder.EndObject();
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x000488F2 File Offset: 0x00046AF2
		internal PlanGroupByMember DisableSortByMeasureKeysAndSubtotals()
		{
			return new PlanGroupByMember(this.Member, false, null, true, this.ContextTables);
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00048908 File Offset: 0x00046B08
		internal PlanGroupByMember RemoveContextTables()
		{
			return new PlanGroupByMember(this.Member, this.m_includeSortByMeasureKeysAtMeasureScope, this.m_subtotalIndicatorColumnName, this.m_excludeMeasureSortKeys, null);
		}

		// Token: 0x0400082F RID: 2095
		private readonly DataMember m_member;

		// Token: 0x04000830 RID: 2096
		private readonly string m_subtotalIndicatorColumnName;

		// Token: 0x04000831 RID: 2097
		private readonly bool m_includeSortByMeasureKeysAtMeasureScope;

		// Token: 0x04000832 RID: 2098
		private readonly bool m_excludeMeasureSortKeys;

		// Token: 0x04000833 RID: 2099
		private readonly IReadOnlyList<PlanOperation> m_contextTables;
	}
}
