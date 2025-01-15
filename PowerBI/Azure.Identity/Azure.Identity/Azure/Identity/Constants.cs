using System;
using System.Collections.Generic;
using System.IO;

namespace Azure.Identity
{
	// Token: 0x0200002B RID: 43
	internal class Constants
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00004C2E File Offset: 0x00002E2E
		public static string SharedTokenCacheFilePath
		{
			get
			{
				return Path.Combine(Constants.DefaultMsalTokenCacheDirectory, "msal.cache");
			}
		}

		// Token: 0x0400009F RID: 159
		public const string OrganizationsTenantId = "organizations";

		// Token: 0x040000A0 RID: 160
		public const string AdfsTenantId = "adfs";

		// Token: 0x040000A1 RID: 161
		public const string DeveloperSignOnClientId = "04b07795-8ddb-461a-bbee-02f9e1bf7b46";

		// Token: 0x040000A2 RID: 162
		public const int SharedTokenCacheAccessRetryCount = 100;

		// Token: 0x040000A3 RID: 163
		public static readonly TimeSpan SharedTokenCacheAccessRetryDelay = TimeSpan.FromMilliseconds(600.0);

		// Token: 0x040000A4 RID: 164
		public const string DefaultRedirectUrl = "http://localhost";

		// Token: 0x040000A5 RID: 165
		public static readonly string DefaultMsalTokenCacheDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ".IdentityService");

		// Token: 0x040000A6 RID: 166
		public const string DefaultMsalTokenCacheKeychainService = "Microsoft.Developer.IdentityService";

		// Token: 0x040000A7 RID: 167
		public const string DefaultMsalTokenCacheKeychainAccount = "MSALCache";

		// Token: 0x040000A8 RID: 168
		public const string DefaultMsalTokenCacheKeyringLabel = "MSALCache";

		// Token: 0x040000A9 RID: 169
		public const string DefaultMsalTokenCacheKeyringSchema = "msal.cache";

		// Token: 0x040000AA RID: 170
		public const string DefaultMsalTokenCacheKeyringCollection = "default";

		// Token: 0x040000AB RID: 171
		public static readonly KeyValuePair<string, string> DefaultMsaltokenCacheKeyringAttribute1 = new KeyValuePair<string, string>("MsalClientID", "Microsoft.Developer.IdentityService");

		// Token: 0x040000AC RID: 172
		public static readonly KeyValuePair<string, string> DefaultMsaltokenCacheKeyringAttribute2 = new KeyValuePair<string, string>("Microsoft.Developer.IdentityService", "1.0.0.0");

		// Token: 0x040000AD RID: 173
		public const string DefaultMsalTokenCacheName = "msal.cache";

		// Token: 0x040000AE RID: 174
		public const string CaeEnabledCacheSuffix = ".cae";

		// Token: 0x040000AF RID: 175
		public const string CaeDisabledCacheSuffix = ".nocae";

		// Token: 0x040000B0 RID: 176
		public const string ManagedIdentityClientId = "client_id";

		// Token: 0x040000B1 RID: 177
		public const string ManagedIdentityResourceId = "mi_res_id";
	}
}
