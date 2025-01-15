using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.ReportProcessing.Utilities;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x02000591 RID: 1425
	[DataContract]
	internal sealed class DefaultRelationship
	{
		// Token: 0x060051C3 RID: 20931 RVA: 0x0015A0CF File Offset: 0x001582CF
		internal DefaultRelationship(string relatedDataSet, bool naturalJoin, IEnumerable<JoinCondition> joinConditions)
		{
			this.m_relatedDataSet = relatedDataSet;
			this.m_naturalJoin = naturalJoin;
			if (joinConditions != null)
			{
				this.m_joinConditions = joinConditions.ToReadOnlyCollection<JoinCondition>();
			}
		}

		// Token: 0x17001E63 RID: 7779
		// (get) Token: 0x060051C4 RID: 20932 RVA: 0x0015A0F4 File Offset: 0x001582F4
		internal string RelatedDataSet
		{
			get
			{
				return this.m_relatedDataSet;
			}
		}

		// Token: 0x17001E64 RID: 7780
		// (get) Token: 0x060051C5 RID: 20933 RVA: 0x0015A0FC File Offset: 0x001582FC
		internal bool NaturalJoin
		{
			get
			{
				return this.m_naturalJoin;
			}
		}

		// Token: 0x17001E65 RID: 7781
		// (get) Token: 0x060051C6 RID: 20934 RVA: 0x0015A104 File Offset: 0x00158304
		internal IEnumerable<JoinCondition> JoinConditions
		{
			get
			{
				return this.m_joinConditions;
			}
		}

		// Token: 0x04002948 RID: 10568
		[DataMember(Name = "RelatedDataSet", Order = 1)]
		private readonly string m_relatedDataSet;

		// Token: 0x04002949 RID: 10569
		[DataMember(Name = "NaturalJoin", Order = 2)]
		private readonly bool m_naturalJoin;

		// Token: 0x0400294A RID: 10570
		[DataMember(Name = "JoinConditions", Order = 3)]
		private readonly IEnumerable<JoinCondition> m_joinConditions;
	}
}
