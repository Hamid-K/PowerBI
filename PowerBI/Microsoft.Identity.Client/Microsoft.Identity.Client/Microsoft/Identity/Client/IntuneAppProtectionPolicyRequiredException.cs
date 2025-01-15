using System;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000153 RID: 339
	public class IntuneAppProtectionPolicyRequiredException : MsalServiceException
	{
		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x0003B79F File Offset: 0x0003999F
		// (set) Token: 0x060010CC RID: 4300 RVA: 0x0003B7A7 File Offset: 0x000399A7
		public string Upn { get; set; }

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x0003B7B0 File Offset: 0x000399B0
		// (set) Token: 0x060010CE RID: 4302 RVA: 0x0003B7B8 File Offset: 0x000399B8
		public string AccountUserId { get; set; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x0003B7C1 File Offset: 0x000399C1
		// (set) Token: 0x060010D0 RID: 4304 RVA: 0x0003B7C9 File Offset: 0x000399C9
		public string TenantId { get; set; }

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x0003B7D2 File Offset: 0x000399D2
		// (set) Token: 0x060010D2 RID: 4306 RVA: 0x0003B7DA File Offset: 0x000399DA
		public string AuthorityUrl { get; set; }

		// Token: 0x060010D3 RID: 4307 RVA: 0x0003B7E3 File Offset: 0x000399E3
		public IntuneAppProtectionPolicyRequiredException(string errorCode, string errorMessage)
			: base(errorCode, errorMessage, null)
		{
		}
	}
}
