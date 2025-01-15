using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000235 RID: 565
	internal sealed class SubtotalColumnFilteringMetadata : IEquatable<SubtotalColumnFilteringMetadata>, IStructuredToString
	{
		// Token: 0x06001353 RID: 4947 RVA: 0x0004A69A File Offset: 0x0004889A
		public SubtotalColumnFilteringMetadata(DataMember dataMember, bool? filteringValue)
		{
			this.Member = dataMember;
			this.SubtotalColumnFilteringValue = filteringValue;
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001354 RID: 4948 RVA: 0x0004A6B0 File Offset: 0x000488B0
		public DataMember Member { get; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06001355 RID: 4949 RVA: 0x0004A6B8 File Offset: 0x000488B8
		public bool? SubtotalColumnFilteringValue { get; }

		// Token: 0x06001356 RID: 4950 RVA: 0x0004A6C0 File Offset: 0x000488C0
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SubtotalColumnFilteringMetadata);
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0004A6CE File Offset: 0x000488CE
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Member.GetHashCode(), Hashing.GetHashCode<bool?>(this.SubtotalColumnFilteringValue, null));
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0004A6EC File Offset: 0x000488EC
		public bool Equals(SubtotalColumnFilteringMetadata other)
		{
			if (other != null && this.Member.Equals(other.Member))
			{
				bool? subtotalColumnFilteringValue = this.SubtotalColumnFilteringValue;
				bool? subtotalColumnFilteringValue2 = other.SubtotalColumnFilteringValue;
				return (subtotalColumnFilteringValue.GetValueOrDefault() == subtotalColumnFilteringValue2.GetValueOrDefault()) & (subtotalColumnFilteringValue != null == (subtotalColumnFilteringValue2 != null));
			}
			return false;
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0004A740 File Offset: 0x00048940
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("SubtotalColumnFilteringMetadata");
			builder.WriteAttribute<DataMember>("Member", this.Member, false, false);
			builder.WriteAttribute<bool?>("SubtotalColumnFilteringValue", this.SubtotalColumnFilteringValue, false, false);
			builder.EndObject();
		}
	}
}
