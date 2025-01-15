using System;
using System.Collections.Generic;
using Microsoft.Identity.Client;

namespace Azure.Identity
{
	// Token: 0x0200005B RID: 91
	public struct DeviceCodeInfo
	{
		// Token: 0x06000344 RID: 836 RVA: 0x0000A653 File Offset: 0x00008853
		internal DeviceCodeInfo(DeviceCodeResult deviceCode)
		{
			this = new DeviceCodeInfo(deviceCode.UserCode, deviceCode.DeviceCode, new Uri(deviceCode.VerificationUrl), deviceCode.ExpiresOn, deviceCode.Message, deviceCode.ClientId, deviceCode.Scopes);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000A68A File Offset: 0x0000888A
		internal DeviceCodeInfo(string userCode, string deviceCode, Uri verificationUri, DateTimeOffset expiresOn, string message, string clientId, IReadOnlyCollection<string> scopes)
		{
			this.UserCode = userCode;
			this.DeviceCode = deviceCode;
			this.VerificationUri = verificationUri;
			this.ExpiresOn = expiresOn;
			this.Message = message;
			this.ClientId = clientId;
			this.Scopes = scopes;
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000A6C1 File Offset: 0x000088C1
		// (set) Token: 0x06000347 RID: 839 RVA: 0x0000A6C9 File Offset: 0x000088C9
		public string UserCode { readonly get; private set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000A6D2 File Offset: 0x000088D2
		// (set) Token: 0x06000349 RID: 841 RVA: 0x0000A6DA File Offset: 0x000088DA
		public string DeviceCode { readonly get; private set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000A6E3 File Offset: 0x000088E3
		// (set) Token: 0x0600034B RID: 843 RVA: 0x0000A6EB File Offset: 0x000088EB
		public Uri VerificationUri { readonly get; private set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000A6F4 File Offset: 0x000088F4
		// (set) Token: 0x0600034D RID: 845 RVA: 0x0000A6FC File Offset: 0x000088FC
		public DateTimeOffset ExpiresOn { readonly get; private set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000A705 File Offset: 0x00008905
		// (set) Token: 0x0600034F RID: 847 RVA: 0x0000A70D File Offset: 0x0000890D
		public string Message { readonly get; private set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000A716 File Offset: 0x00008916
		// (set) Token: 0x06000351 RID: 849 RVA: 0x0000A71E File Offset: 0x0000891E
		public string ClientId { readonly get; private set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000A727 File Offset: 0x00008927
		// (set) Token: 0x06000353 RID: 851 RVA: 0x0000A72F File Offset: 0x0000892F
		public IReadOnlyCollection<string> Scopes { readonly get; private set; }
	}
}
