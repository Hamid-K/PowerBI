using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000203 RID: 515
	internal sealed class PlanOperationFilteringMetadata : IEquatable<PlanOperationFilteringMetadata>, IStructuredToString
	{
		// Token: 0x060011ED RID: 4589 RVA: 0x00048090 File Offset: 0x00046290
		public PlanOperationFilteringMetadata(IReadOnlyList<SubtotalColumnFilteringMetadata> totalsMetadata, bool respectsInstanceFilters)
		{
			this.TotalsMetadata = totalsMetadata ?? Util.EmptyReadOnlyList<SubtotalColumnFilteringMetadata>();
			this.RespectsInstanceFilters = respectsInstanceFilters;
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x000480AF File Offset: 0x000462AF
		internal IReadOnlyList<SubtotalColumnFilteringMetadata> TotalsMetadata { get; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x000480B7 File Offset: 0x000462B7
		internal bool RespectsInstanceFilters { get; }

		// Token: 0x060011F0 RID: 4592 RVA: 0x000480BF File Offset: 0x000462BF
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PlanOperationFilteringMetadata);
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x000480D0 File Offset: 0x000462D0
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.TotalsMetadata.GetHashCode(), this.RespectsInstanceFilters.GetHashCode());
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x000480FB File Offset: 0x000462FB
		public bool Equals(PlanOperationFilteringMetadata other)
		{
			return other != null && this.TotalsMetadata.SequenceEqualReadOnly(other.TotalsMetadata) && this.RespectsInstanceFilters == other.RespectsInstanceFilters;
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x00048123 File Offset: 0x00046323
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("PlanOperationFilteringMetadata");
			builder.WriteProperty<IReadOnlyList<SubtotalColumnFilteringMetadata>>("TotalsMetadata", this.TotalsMetadata, false);
			builder.WriteProperty<bool>("RespectsInstanceFilters", this.RespectsInstanceFilters, false);
			builder.EndObject();
		}
	}
}
