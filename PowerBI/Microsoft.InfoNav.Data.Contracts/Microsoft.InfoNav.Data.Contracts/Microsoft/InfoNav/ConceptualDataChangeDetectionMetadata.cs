using System;
using System.ComponentModel;

namespace Microsoft.InfoNav
{
	// Token: 0x0200001C RID: 28
	[ImmutableObject(true)]
	public sealed class ConceptualDataChangeDetectionMetadata
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00002598 File Offset: 0x00000798
		internal ConceptualDataChangeDetectionMetadata(string refreshInterval)
		{
			this.RefreshInterval = refreshInterval;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000025A7 File Offset: 0x000007A7
		public string RefreshInterval { get; }
	}
}
