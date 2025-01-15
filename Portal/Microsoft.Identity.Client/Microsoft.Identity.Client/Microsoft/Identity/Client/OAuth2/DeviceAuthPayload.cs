using System;
using Microsoft.Identity.Json;

namespace Microsoft.Identity.Client.OAuth2
{
	// Token: 0x02000202 RID: 514
	[JsonObject]
	[Preserve(AllMembers = true)]
	internal class DeviceAuthPayload
	{
		// Token: 0x060015BB RID: 5563 RVA: 0x00047DF8 File Offset: 0x00045FF8
		public DeviceAuthPayload(string audience, string nonce)
		{
			this.Nonce = nonce;
			this.Audience = audience;
			this.Iat = this._defaultDeviceAuthJWTTimeSpan.Value;
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060015BC RID: 5564 RVA: 0x00047E54 File Offset: 0x00046054
		// (set) Token: 0x060015BD RID: 5565 RVA: 0x00047E5C File Offset: 0x0004605C
		[JsonProperty("iat")]
		public long Iat { get; set; }

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x00047E65 File Offset: 0x00046065
		// (set) Token: 0x060015BF RID: 5567 RVA: 0x00047E6D File Offset: 0x0004606D
		[JsonProperty("aud")]
		public string Audience { get; set; }

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x00047E76 File Offset: 0x00046076
		// (set) Token: 0x060015C1 RID: 5569 RVA: 0x00047E7E File Offset: 0x0004607E
		[JsonProperty("nonce")]
		public string Nonce { get; private set; }

		// Token: 0x040008EC RID: 2284
		private readonly Lazy<long> _defaultDeviceAuthJWTTimeSpan = new Lazy<long>(() => (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds);
	}
}
