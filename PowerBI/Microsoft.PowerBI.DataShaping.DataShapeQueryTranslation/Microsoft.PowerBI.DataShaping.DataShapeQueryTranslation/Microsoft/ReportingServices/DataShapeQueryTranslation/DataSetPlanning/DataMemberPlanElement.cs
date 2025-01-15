using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000E4 RID: 228
	[DebuggerDisplay("[Member] Id={Scope.Id} [Projected={IsProjected}]")]
	internal sealed class DataMemberPlanElement : ScopePlanElement
	{
		// Token: 0x0600094B RID: 2379 RVA: 0x00023A56 File Offset: 0x00021C56
		internal DataMemberPlanElement(DataMember dataMember, IList<NestedPlanElement> nestedElements, bool isProjected, RollupRequirement rollup = null, FilterCondition filterCondition = null, Limit limit = null, bool requiresReversedSortDirection = false, bool omitStartAt = false)
			: base(nestedElements, isProjected, filterCondition, limit)
		{
			this.m_dataMember = dataMember;
			this.m_rollup = rollup;
			this.m_requiresReversedSortDirection = requiresReversedSortDirection;
			this.m_omitStartAt = omitStartAt;
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x00023A83 File Offset: 0x00021C83
		public RollupRequirement RollupInfo
		{
			get
			{
				return this.m_rollup;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x00023A8B File Offset: 0x00021C8B
		public DataMember DataMember
		{
			get
			{
				return this.m_dataMember;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x00023A93 File Offset: 0x00021C93
		public bool RequiresReversedSortDirection
		{
			get
			{
				return this.m_requiresReversedSortDirection;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x00023A9B File Offset: 0x00021C9B
		public override IScope Scope
		{
			get
			{
				return this.m_dataMember;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x00023AA3 File Offset: 0x00021CA3
		public bool OmitStartAt
		{
			get
			{
				return this.m_omitStartAt;
			}
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00023AAB File Offset: 0x00021CAB
		public override ScopePlanElement OmitProjection()
		{
			if (!base.IsProjected)
			{
				return this;
			}
			return new DataMemberPlanElement(this.DataMember, base.GetNestedElementsForOmitProjection(), false, null, base.FilterCondition, base.Limit, this.m_requiresReversedSortDirection, this.OmitStartAt);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00023AE2 File Offset: 0x00021CE2
		public override ScopePlanElement OmitNestedElements()
		{
			return new DataMemberPlanElement(this.DataMember, null, base.IsProjected, this.RollupInfo, base.FilterCondition, base.Limit, this.RequiresReversedSortDirection, this.OmitStartAt);
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00023B14 File Offset: 0x00021D14
		public override ScopePlanElement AddNestedPlanElement(NestedPlanElement expression)
		{
			return new DataMemberPlanElement(this.DataMember, base.AddToNestedElementCollection(expression), base.IsProjected, this.RollupInfo, base.FilterCondition, base.Limit, this.RequiresReversedSortDirection, this.OmitStartAt);
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00023B4C File Offset: 0x00021D4C
		public override void Accept(DataSetPlanElementVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00023B58 File Offset: 0x00021D58
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("DataMemberPlanElement");
			builder.WriteAttribute<DataMember>("DataMember", this.m_dataMember, false, false);
			builder.WriteAttribute<bool>("OmitStartAt", this.m_omitStartAt, false, false);
			builder.WriteAttribute<bool>("RequiresReversedSortDirection", this.m_requiresReversedSortDirection, false, false);
			base.WriteToBase(builder);
			builder.WriteProperty<RollupRequirement>("Rollup", this.m_rollup, false);
			builder.EndObject();
		}

		// Token: 0x04000468 RID: 1128
		private readonly DataMember m_dataMember;

		// Token: 0x04000469 RID: 1129
		private readonly RollupRequirement m_rollup;

		// Token: 0x0400046A RID: 1130
		private readonly bool m_omitStartAt;

		// Token: 0x0400046B RID: 1131
		private readonly bool m_requiresReversedSortDirection;
	}
}
