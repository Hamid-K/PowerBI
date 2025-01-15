using System;

namespace Microsoft.Identity.Client.ManagedIdentity
{
	// Token: 0x0200021F RID: 543
	internal class EnvironmentVariables
	{
		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x0600166E RID: 5742 RVA: 0x0004AA18 File Offset: 0x00048C18
		public static string IdentityEndpoint
		{
			get
			{
				return Environment.GetEnvironmentVariable("IDENTITY_ENDPOINT");
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x0600166F RID: 5743 RVA: 0x0004AA24 File Offset: 0x00048C24
		public static string IdentityHeader
		{
			get
			{
				return Environment.GetEnvironmentVariable("IDENTITY_HEADER");
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001670 RID: 5744 RVA: 0x0004AA30 File Offset: 0x00048C30
		public static string PodIdentityEndpoint
		{
			get
			{
				return Environment.GetEnvironmentVariable("AZURE_POD_IDENTITY_AUTHORITY_HOST");
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x0004AA3C File Offset: 0x00048C3C
		public static string ImdsEndpoint
		{
			get
			{
				return Environment.GetEnvironmentVariable("IMDS_ENDPOINT");
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001672 RID: 5746 RVA: 0x0004AA48 File Offset: 0x00048C48
		public static string MsiEndpoint
		{
			get
			{
				return Environment.GetEnvironmentVariable("MSI_ENDPOINT");
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001673 RID: 5747 RVA: 0x0004AA54 File Offset: 0x00048C54
		public static string IdentityServerThumbprint
		{
			get
			{
				return Environment.GetEnvironmentVariable("IDENTITY_SERVER_THUMBPRINT");
			}
		}
	}
}
