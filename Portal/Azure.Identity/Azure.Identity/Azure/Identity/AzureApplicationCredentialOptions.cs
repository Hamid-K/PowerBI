using System;

namespace Azure.Identity
{
	// Token: 0x02000031 RID: 49
	internal class AzureApplicationCredentialOptions : TokenCredentialOptions
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600012D RID: 301 RVA: 0x000053DF File Offset: 0x000035DF
		// (set) Token: 0x0600012E RID: 302 RVA: 0x000053E7 File Offset: 0x000035E7
		public string ManagedIdentityClientId { get; set; } = AzureApplicationCredentialOptions.GetNonEmptyStringOrNull(EnvironmentVariables.ClientId);

		// Token: 0x0600012F RID: 303 RVA: 0x000053F0 File Offset: 0x000035F0
		private static string GetNonEmptyStringOrNull(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return null;
			}
			return str;
		}
	}
}
