using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Identity.Client.Cache.Keys
{
	// Token: 0x020002B3 RID: 691
	internal class MsalCacheKeys
	{
		// Token: 0x060019D1 RID: 6609 RVA: 0x000547E4 File Offset: 0x000529E4
		public static string GetCredentialKey(string homeAccountId, string environment, string keyDescriptor, string clientId, string tenantId, string scopes, params string[] additionalKeys)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(homeAccountId ?? "");
			stringBuilder.Append('-');
			stringBuilder.Append(environment);
			stringBuilder.Append('-');
			stringBuilder.Append(keyDescriptor);
			stringBuilder.Append('-');
			stringBuilder.Append(clientId);
			stringBuilder.Append('-');
			stringBuilder.Append(tenantId ?? "");
			stringBuilder.Append('-');
			stringBuilder.Append(scopes ?? "");
			foreach (string text in ((IEnumerable<string>)(additionalKeys ?? Enumerable.Empty<string>())))
			{
				stringBuilder.Append('-');
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString().ToLowerInvariant();
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x000548D0 File Offset: 0x00052AD0
		public static string GetiOSAccountKey(string homeAccountId, string environment)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(homeAccountId ?? "");
			stringBuilder.Append('-');
			stringBuilder.Append(environment);
			return stringBuilder.ToString().ToLowerInvariant();
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x00054904 File Offset: 0x00052B04
		public static string GetiOSServiceKey(string keyDescriptor, string clientId, string tenantId, string scopes, params string[] extraKeyParts)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(keyDescriptor);
			stringBuilder.Append('-');
			stringBuilder.Append(clientId);
			stringBuilder.Append('-');
			stringBuilder.Append(tenantId ?? "");
			stringBuilder.Append('-');
			stringBuilder.Append(scopes ?? "");
			foreach (string text in ((IEnumerable<string>)(extraKeyParts ?? Enumerable.Empty<string>())))
			{
				stringBuilder.Append('-');
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString().ToLowerInvariant();
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x000549C0 File Offset: 0x00052BC0
		public static string GetiOSGenericKey(string keyDescriptor, string clientId, string tenantId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(keyDescriptor);
			stringBuilder.Append('-');
			stringBuilder.Append(clientId);
			stringBuilder.Append('-');
			stringBuilder.Append(tenantId ?? "");
			return stringBuilder.ToString().ToLowerInvariant();
		}

		// Token: 0x04000BB8 RID: 3000
		public const char CacheKeyDelimiter = '-';

		// Token: 0x04000BB9 RID: 3001
		internal static readonly Dictionary<string, int> iOSAuthorityTypeToAttrType = new Dictionary<string, int>
		{
			{
				CacheAuthorityType.AAD.ToString(),
				1001
			},
			{
				CacheAuthorityType.MSA.ToString(),
				1002
			},
			{
				CacheAuthorityType.MSSTS.ToString(),
				1003
			},
			{
				CacheAuthorityType.OTHER.ToString(),
				1004
			}
		};

		// Token: 0x02000522 RID: 1314
		internal enum iOSCredentialAttrType
		{
			// Token: 0x0400170D RID: 5901
			AccessToken = 2001,
			// Token: 0x0400170E RID: 5902
			RefreshToken,
			// Token: 0x0400170F RID: 5903
			IdToken,
			// Token: 0x04001710 RID: 5904
			Password,
			// Token: 0x04001711 RID: 5905
			AppMetadata = 3001
		}
	}
}
