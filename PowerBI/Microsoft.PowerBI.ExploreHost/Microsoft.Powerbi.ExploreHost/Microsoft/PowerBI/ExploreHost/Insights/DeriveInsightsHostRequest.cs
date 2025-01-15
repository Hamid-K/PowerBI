using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Experimental.Insights;
using Microsoft.InfoNav.Experimental.Insights.ServiceContracts.Internal;

namespace Microsoft.PowerBI.ExploreHost.Insights
{
	// Token: 0x0200007C RID: 124
	[DataContract]
	public sealed class DeriveInsightsHostRequest
	{
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000ABA4 File Offset: 0x00008DA4
		// (set) Token: 0x0600035A RID: 858 RVA: 0x0000ABAC File Offset: 0x00008DAC
		[Required]
		[DataMember(IsRequired = true, Order = 10, Name = "Analysis")]
		public AnalysisDefinitionContainer Analysis { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000ABB5 File Offset: 0x00008DB5
		// (set) Token: 0x0600035C RID: 860 RVA: 0x0000ABBD File Offset: 0x00008DBD
		[DataMember(IsRequired = false, Order = 20, Name = "AnchorTime")]
		public string AnchorTime { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000ABC6 File Offset: 0x00008DC6
		// (set) Token: 0x0600035E RID: 862 RVA: 0x0000ABCE File Offset: 0x00008DCE
		[DataMember(IsRequired = false, Order = 30, Name = "ExecutionMetricsKind")]
		public ExecutionMetricsKind ExecutionMetricsKind { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000ABD7 File Offset: 0x00008DD7
		// (set) Token: 0x06000360 RID: 864 RVA: 0x0000ABDF File Offset: 0x00008DDF
		[DataMember(IsRequired = false, Order = 40, Name = "JobId")]
		public Guid? JobId { get; set; }
	}
}
