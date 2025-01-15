using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations
{
	// Token: 0x02000243 RID: 579
	internal sealed class DataMemberAnnotations
	{
		// Token: 0x060013C7 RID: 5063 RVA: 0x0004D101 File Offset: 0x0004B301
		internal DataMemberAnnotations(Dictionary<DataMember, DataMemberAnnotation> annotations)
		{
			this.m_annotations = annotations;
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x0004D110 File Offset: 0x0004B310
		public bool IsPrimaryMember(DataMember member)
		{
			return this.GetAnnotation(member).IsPrimaryMember;
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x0004D11E File Offset: 0x0004B31E
		public bool IsLeaf(DataMember member)
		{
			return this.GetLeafIndex(member) >= 0;
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0004D12D File Offset: 0x0004B32D
		public int GetLeafIndex(DataMember member)
		{
			return this.GetAnnotation(member).LeafIndex;
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x0004D13B File Offset: 0x0004B33B
		public bool AreContentsIncludedInOutput(DataMember member)
		{
			return this.GetAnnotation(member).AreContentsIncludedInOutput;
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x0004D149 File Offset: 0x0004B349
		public SortByMeasureInfoCollection GetSortByMeasureInfos(DataMember member)
		{
			return this.GetAnnotation(member).SortByMeasureInfos;
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0004D157 File Offset: 0x0004B357
		public bool HasSortByMeasureKeys(DataMember member)
		{
			return this.GetAnnotation(member).HasSortOnMeasureKeys;
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0004D165 File Offset: 0x0004B365
		public bool HasLimits(DataMember member)
		{
			return this.GetAnnotation(member).GetLimit() != null;
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x0004D176 File Offset: 0x0004B376
		public Limit GetLimitWithInnermostTarget(DataMember member)
		{
			return this.GetAnnotation(member).GetLimitWithInnermostTarget();
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x0004D184 File Offset: 0x0004B384
		private DataMemberAnnotation GetAnnotation(DataMember dataMember)
		{
			return this.m_annotations[dataMember];
		}

		// Token: 0x040008BD RID: 2237
		private readonly Dictionary<DataMember, DataMemberAnnotation> m_annotations;
	}
}
