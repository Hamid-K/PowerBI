using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200052D RID: 1325
	internal enum AtomicityReason
	{
		// Token: 0x04001FE7 RID: 8167
		Filters,
		// Token: 0x04001FE8 RID: 8168
		Sorts,
		// Token: 0x04001FE9 RID: 8169
		NonNaturalSorts,
		// Token: 0x04001FEA RID: 8170
		NonNaturalGroup,
		// Token: 0x04001FEB RID: 8171
		DomainScope,
		// Token: 0x04001FEC RID: 8172
		RecursiveParent,
		// Token: 0x04001FED RID: 8173
		Aggregates,
		// Token: 0x04001FEE RID: 8174
		RunningValues,
		// Token: 0x04001FEF RID: 8175
		Lookups,
		// Token: 0x04001FF0 RID: 8176
		PeerChildScopes
	}
}
