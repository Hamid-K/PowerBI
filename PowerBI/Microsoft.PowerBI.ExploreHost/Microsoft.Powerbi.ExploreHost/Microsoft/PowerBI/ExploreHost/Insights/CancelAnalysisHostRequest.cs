using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.ExploreHost.Insights
{
	// Token: 0x0200007D RID: 125
	[DataContract]
	public sealed class CancelAnalysisHostRequest
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000ABF0 File Offset: 0x00008DF0
		// (set) Token: 0x06000363 RID: 867 RVA: 0x0000ABF8 File Offset: 0x00008DF8
		[DataMember(IsRequired = true, Order = 10, Name = "JobId")]
		public Guid JobId { get; set; }
	}
}
