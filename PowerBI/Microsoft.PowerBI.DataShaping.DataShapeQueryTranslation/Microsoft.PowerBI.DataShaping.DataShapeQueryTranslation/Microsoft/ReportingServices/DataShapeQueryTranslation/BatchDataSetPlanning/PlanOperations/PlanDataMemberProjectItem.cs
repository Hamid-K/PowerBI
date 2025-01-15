using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x0200021D RID: 541
	internal sealed class PlanDataMemberProjectItem : PlanProjectItem
	{
		// Token: 0x060012B0 RID: 4784 RVA: 0x0004947F File Offset: 0x0004767F
		internal PlanDataMemberProjectItem(DataMember dataMember, bool excludeMeasureSortKeys)
		{
			this.DataMember = dataMember;
			this.ExcludeMeasureSortKeys = excludeMeasureSortKeys;
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060012B1 RID: 4785 RVA: 0x00049495 File Offset: 0x00047695
		public DataMember DataMember { get; }

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x0004949D File Offset: 0x0004769D
		public bool ExcludeMeasureSortKeys { get; }

		// Token: 0x060012B3 RID: 4787 RVA: 0x000494A5 File Offset: 0x000476A5
		public override void Accept(IPlanProjectItemVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x000494B0 File Offset: 0x000476B0
		protected override int GetHashCodeInternal()
		{
			return Hashing.CombineHash(this.DataMember.GetHashCode(), this.ExcludeMeasureSortKeys.GetHashCode());
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x000494DC File Offset: 0x000476DC
		public override bool Equals(PlanProjectItem other)
		{
			PlanDataMemberProjectItem planDataMemberProjectItem = other as PlanDataMemberProjectItem;
			return planDataMemberProjectItem != null && this.DataMember == planDataMemberProjectItem.DataMember && this.ExcludeMeasureSortKeys == planDataMemberProjectItem.ExcludeMeasureSortKeys;
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00049514 File Offset: 0x00047714
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("DataMemberProjectItem");
			builder.WriteAttribute<string>("DataMember", this.DataMember.Id.Value, false, false);
			builder.WriteAttribute<bool>("ExcludeMeasureSortKeys", this.ExcludeMeasureSortKeys, false, false);
			builder.EndObject();
		}
	}
}
