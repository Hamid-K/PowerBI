using System;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Json;

namespace Microsoft.Identity.Client.Internal
{
	// Token: 0x0200022E RID: 558
	[JsonObject]
	[Preserve(AllMembers = true)]
	internal class ClientInfo
	{
		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060016D4 RID: 5844 RVA: 0x0004B7AF File Offset: 0x000499AF
		// (set) Token: 0x060016D5 RID: 5845 RVA: 0x0004B7B7 File Offset: 0x000499B7
		[JsonProperty("uid")]
		public string UniqueObjectIdentifier { get; set; }

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060016D6 RID: 5846 RVA: 0x0004B7C0 File Offset: 0x000499C0
		// (set) Token: 0x060016D7 RID: 5847 RVA: 0x0004B7C8 File Offset: 0x000499C8
		[JsonProperty("utid")]
		public string UniqueTenantIdentifier { get; set; }

		// Token: 0x060016D8 RID: 5848 RVA: 0x0004B7D4 File Offset: 0x000499D4
		public static ClientInfo CreateFromJson(string clientInfo)
		{
			if (string.IsNullOrEmpty(clientInfo))
			{
				throw new MsalClientException("json_parse_failed", "client info is null");
			}
			ClientInfo clientInfo2;
			try
			{
				clientInfo2 = JsonHelper.DeserializeFromJson<ClientInfo>(Base64UrlHelpers.DecodeBytes(clientInfo));
			}
			catch (Exception ex)
			{
				throw new MsalClientException("json_parse_failed", "Failed to parse the returned client info.", ex);
			}
			return clientInfo2;
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x0004B82C File Offset: 0x00049A2C
		public string ToAccountIdentifier()
		{
			return this.UniqueObjectIdentifier + "." + this.UniqueTenantIdentifier;
		}
	}
}
