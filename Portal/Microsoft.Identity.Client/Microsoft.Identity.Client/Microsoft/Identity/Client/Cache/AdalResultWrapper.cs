using System;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Json;

namespace Microsoft.Identity.Client.Cache
{
	// Token: 0x0200029E RID: 670
	[JsonObject]
	[Preserve(AllMembers = true)]
	internal class AdalResultWrapper
	{
		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x0600194A RID: 6474 RVA: 0x0005307D File Offset: 0x0005127D
		// (set) Token: 0x0600194B RID: 6475 RVA: 0x00053085 File Offset: 0x00051285
		[JsonProperty]
		public AdalResult Result { get; set; }

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x0600194C RID: 6476 RVA: 0x0005308E File Offset: 0x0005128E
		// (set) Token: 0x0600194D RID: 6477 RVA: 0x00053096 File Offset: 0x00051296
		[JsonProperty]
		public string RawClientInfo { get; set; }

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x0600194E RID: 6478 RVA: 0x0005309F File Offset: 0x0005129F
		// (set) Token: 0x0600194F RID: 6479 RVA: 0x000530A7 File Offset: 0x000512A7
		[JsonProperty]
		public string RefreshToken { get; set; }

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001950 RID: 6480 RVA: 0x000530B0 File Offset: 0x000512B0
		internal bool IsMultipleResourceRefreshToken
		{
			get
			{
				return !string.IsNullOrWhiteSpace(this.RefreshToken) && !string.IsNullOrWhiteSpace(this.ResourceInResponse);
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001951 RID: 6481 RVA: 0x000530CF File Offset: 0x000512CF
		// (set) Token: 0x06001952 RID: 6482 RVA: 0x000530D7 File Offset: 0x000512D7
		[JsonProperty]
		internal string ResourceInResponse { get; set; }

		// Token: 0x06001953 RID: 6483 RVA: 0x000530E0 File Offset: 0x000512E0
		public static AdalResultWrapper Deserialize(string serializedObject)
		{
			return JsonHelper.DeserializeFromJson<AdalResultWrapper>(serializedObject);
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x000530E8 File Offset: 0x000512E8
		public string Serialize()
		{
			return JsonHelper.SerializeToJson<AdalResultWrapper>(this);
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001955 RID: 6485 RVA: 0x000530F0 File Offset: 0x000512F0
		// (set) Token: 0x06001956 RID: 6486 RVA: 0x000530F8 File Offset: 0x000512F8
		[JsonProperty]
		public string UserAssertionHash { get; set; }

		// Token: 0x06001957 RID: 6487 RVA: 0x00053101 File Offset: 0x00051301
		internal AdalResultWrapper Clone()
		{
			return AdalResultWrapper.Deserialize(this.Serialize());
		}
	}
}
