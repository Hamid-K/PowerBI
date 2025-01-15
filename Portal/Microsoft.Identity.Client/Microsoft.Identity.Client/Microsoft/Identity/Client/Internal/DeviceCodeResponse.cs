using System;
using System.Collections.Generic;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Json;

namespace Microsoft.Identity.Client.Internal
{
	// Token: 0x02000231 RID: 561
	[JsonObject]
	[Preserve(AllMembers = true)]
	internal class DeviceCodeResponse : OAuth2ResponseBase
	{
		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060016DF RID: 5855 RVA: 0x0004B898 File Offset: 0x00049A98
		// (set) Token: 0x060016E0 RID: 5856 RVA: 0x0004B8A0 File Offset: 0x00049AA0
		[JsonProperty("user_code")]
		public string UserCode { get; set; }

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060016E1 RID: 5857 RVA: 0x0004B8A9 File Offset: 0x00049AA9
		// (set) Token: 0x060016E2 RID: 5858 RVA: 0x0004B8B1 File Offset: 0x00049AB1
		[JsonProperty("device_code")]
		public string DeviceCode { get; set; }

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060016E3 RID: 5859 RVA: 0x0004B8BA File Offset: 0x00049ABA
		// (set) Token: 0x060016E4 RID: 5860 RVA: 0x0004B8C2 File Offset: 0x00049AC2
		[JsonProperty("verification_url")]
		public string VerificationUrl { get; set; }

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x0004B8CB File Offset: 0x00049ACB
		// (set) Token: 0x060016E6 RID: 5862 RVA: 0x0004B8D3 File Offset: 0x00049AD3
		[JsonProperty("verification_uri")]
		public string VerificationUri { get; set; }

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060016E7 RID: 5863 RVA: 0x0004B8DC File Offset: 0x00049ADC
		// (set) Token: 0x060016E8 RID: 5864 RVA: 0x0004B8E4 File Offset: 0x00049AE4
		[JsonProperty("expires_in")]
		public long ExpiresIn { get; set; }

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060016E9 RID: 5865 RVA: 0x0004B8ED File Offset: 0x00049AED
		// (set) Token: 0x060016EA RID: 5866 RVA: 0x0004B8F5 File Offset: 0x00049AF5
		[JsonProperty("interval")]
		public long Interval { get; set; }

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x0004B8FE File Offset: 0x00049AFE
		// (set) Token: 0x060016EC RID: 5868 RVA: 0x0004B906 File Offset: 0x00049B06
		[JsonProperty("message")]
		public string Message { get; set; }

		// Token: 0x060016ED RID: 5869 RVA: 0x0004B910 File Offset: 0x00049B10
		public DeviceCodeResult GetResult(string clientId, ISet<string> scopes)
		{
			string text = (string.IsNullOrWhiteSpace(this.VerificationUri) ? this.VerificationUrl : this.VerificationUri);
			return new DeviceCodeResult(this.UserCode, this.DeviceCode, text, DateTime.UtcNow.AddSeconds((double)this.ExpiresIn), this.Interval, this.Message, clientId, scopes);
		}
	}
}
