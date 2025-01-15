using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x020005A6 RID: 1446
	[DataContract]
	internal sealed class ScopeIdDefinition
	{
		// Token: 0x06005213 RID: 21011 RVA: 0x0015A575 File Offset: 0x00158775
		internal ScopeIdDefinition(ScopeValueDefinition[] scopeValues, bool omitScopeIdFromDataShapeResult)
		{
			this.m_scopeValues = scopeValues;
			this.m_omitScopeIdFromDataShapeResult = omitScopeIdFromDataShapeResult;
		}

		// Token: 0x17001E91 RID: 7825
		// (get) Token: 0x06005214 RID: 21012 RVA: 0x0015A58B File Offset: 0x0015878B
		internal IEnumerable<ScopeValueDefinition> Values
		{
			get
			{
				return this.m_scopeValues;
			}
		}

		// Token: 0x17001E92 RID: 7826
		// (get) Token: 0x06005215 RID: 21013 RVA: 0x0015A593 File Offset: 0x00158793
		internal bool OmitScopeIdFromDataShapeResult
		{
			get
			{
				return this.m_omitScopeIdFromDataShapeResult;
			}
		}

		// Token: 0x04002976 RID: 10614
		[DataMember(Name = "Values", Order = 1)]
		private readonly IEnumerable<ScopeValueDefinition> m_scopeValues;

		// Token: 0x04002977 RID: 10615
		[DataMember(Name = "OmitScopeIdFromDataShapeResult", Order = 2, EmitDefaultValue = false)]
		private readonly bool m_omitScopeIdFromDataShapeResult;
	}
}
