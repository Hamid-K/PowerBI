using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.ReportProcessing.Utilities;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x020005A4 RID: 1444
	[DataContract]
	internal sealed class Relationship
	{
		// Token: 0x0600520C RID: 21004 RVA: 0x0015A512 File Offset: 0x00158712
		internal Relationship(string parentScopeName, bool naturalJoin, IEnumerable<JoinCondition> joinConditions)
		{
			this.m_parentScopeName = parentScopeName;
			this.m_naturalJoin = naturalJoin;
			if (joinConditions != null)
			{
				this.m_joinConditions = joinConditions.ToReadOnlyCollection<JoinCondition>();
			}
		}

		// Token: 0x17001E8C RID: 7820
		// (get) Token: 0x0600520D RID: 21005 RVA: 0x0015A537 File Offset: 0x00158737
		internal string ParentScopeName
		{
			get
			{
				return this.m_parentScopeName;
			}
		}

		// Token: 0x17001E8D RID: 7821
		// (get) Token: 0x0600520E RID: 21006 RVA: 0x0015A53F File Offset: 0x0015873F
		internal bool NaturalJoin
		{
			get
			{
				return this.m_naturalJoin;
			}
		}

		// Token: 0x17001E8E RID: 7822
		// (get) Token: 0x0600520F RID: 21007 RVA: 0x0015A547 File Offset: 0x00158747
		internal IEnumerable<JoinCondition> JoinConditions
		{
			get
			{
				return this.m_joinConditions;
			}
		}

		// Token: 0x04002971 RID: 10609
		[DataMember(Name = "ParentScope", Order = 1)]
		private readonly string m_parentScopeName;

		// Token: 0x04002972 RID: 10610
		[DataMember(Name = "NaturalJoin", Order = 2)]
		private readonly bool m_naturalJoin;

		// Token: 0x04002973 RID: 10611
		[DataMember(Name = "JoinConditions", Order = 3)]
		private readonly IEnumerable<JoinCondition> m_joinConditions;
	}
}
