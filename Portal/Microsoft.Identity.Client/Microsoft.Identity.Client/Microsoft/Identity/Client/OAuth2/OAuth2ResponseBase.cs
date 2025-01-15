using System;
using Microsoft.Identity.Json;

namespace Microsoft.Identity.Client.OAuth2
{
	// Token: 0x02000208 RID: 520
	[JsonObject]
	[Preserve(AllMembers = true)]
	internal class OAuth2ResponseBase
	{
		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x00048E04 File Offset: 0x00047004
		// (set) Token: 0x06001609 RID: 5641 RVA: 0x00048E0C File Offset: 0x0004700C
		[JsonProperty("error")]
		public string Error { get; set; }

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x00048E15 File Offset: 0x00047015
		// (set) Token: 0x0600160B RID: 5643 RVA: 0x00048E1D File Offset: 0x0004701D
		[JsonProperty("suberror")]
		public string SubError { get; set; }

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x0600160C RID: 5644 RVA: 0x00048E26 File Offset: 0x00047026
		// (set) Token: 0x0600160D RID: 5645 RVA: 0x00048E2E File Offset: 0x0004702E
		[JsonProperty("error_description")]
		public string ErrorDescription { get; set; }

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x0600160E RID: 5646 RVA: 0x00048E37 File Offset: 0x00047037
		// (set) Token: 0x0600160F RID: 5647 RVA: 0x00048E3F File Offset: 0x0004703F
		[JsonProperty("error_codes")]
		public string[] ErrorCodes { get; set; }

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001610 RID: 5648 RVA: 0x00048E48 File Offset: 0x00047048
		// (set) Token: 0x06001611 RID: 5649 RVA: 0x00048E50 File Offset: 0x00047050
		[JsonProperty("correlation_id")]
		public string CorrelationId { get; set; }

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001612 RID: 5650 RVA: 0x00048E59 File Offset: 0x00047059
		// (set) Token: 0x06001613 RID: 5651 RVA: 0x00048E61 File Offset: 0x00047061
		[JsonProperty("claims")]
		public string Claims { get; set; }
	}
}
