using System;
using System.Collections.Generic;
using System.Linq;

namespace Azure.Identity
{
	// Token: 0x0200005C RID: 92
	internal class EnvironmentVariables
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000A738 File Offset: 0x00008938
		public static string Username
		{
			get
			{
				return EnvironmentVariables.GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("AZURE_USERNAME"));
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000A749 File Offset: 0x00008949
		public static string Password
		{
			get
			{
				return EnvironmentVariables.GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("AZURE_PASSWORD"));
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000A75A File Offset: 0x0000895A
		public static string TenantId
		{
			get
			{
				return EnvironmentVariables.GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("AZURE_TENANT_ID"));
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000A76B File Offset: 0x0000896B
		public static List<string> AdditionallyAllowedTenants
		{
			get
			{
				return (Environment.GetEnvironmentVariable("AZURE_ADDITIONALLY_ALLOWED_TENANTS") ?? string.Empty).Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000A796 File Offset: 0x00008996
		public static string ClientId
		{
			get
			{
				return EnvironmentVariables.GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("AZURE_CLIENT_ID"));
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000A7A7 File Offset: 0x000089A7
		public static string ClientSecret
		{
			get
			{
				return EnvironmentVariables.GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("AZURE_CLIENT_SECRET"));
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000A7B8 File Offset: 0x000089B8
		public static string ClientCertificatePath
		{
			get
			{
				return EnvironmentVariables.GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("AZURE_CLIENT_CERTIFICATE_PATH"));
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000A7C9 File Offset: 0x000089C9
		public static string ClientCertificatePassword
		{
			get
			{
				return EnvironmentVariables.GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("AZURE_CLIENT_CERTIFICATE_PASSWORD"));
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000A7DA File Offset: 0x000089DA
		public static bool ClientSendCertificateChain
		{
			get
			{
				return EnvironmentVariables.EnvironmentVariableToBool(Environment.GetEnvironmentVariable("AZURE_CLIENT_SEND_CERTIFICATE_CHAIN"));
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000A7EB File Offset: 0x000089EB
		public static string IdentityEndpoint
		{
			get
			{
				return EnvironmentVariables.GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("IDENTITY_ENDPOINT"));
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000A7FC File Offset: 0x000089FC
		public static string IdentityHeader
		{
			get
			{
				return EnvironmentVariables.GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("IDENTITY_HEADER"));
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000A80D File Offset: 0x00008A0D
		public static string MsiEndpoint
		{
			get
			{
				return EnvironmentVariables.GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("MSI_ENDPOINT"));
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000A81E File Offset: 0x00008A1E
		public static string MsiSecret
		{
			get
			{
				return EnvironmentVariables.GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("MSI_SECRET"));
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000A82F File Offset: 0x00008A2F
		public static string ImdsEndpoint
		{
			get
			{
				return EnvironmentVariables.GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("IMDS_ENDPOINT"));
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000A840 File Offset: 0x00008A40
		public static string IdentityServerThumbprint
		{
			get
			{
				return EnvironmentVariables.GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("IDENTITY_SERVER_THUMBPRINT"));
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000A851 File Offset: 0x00008A51
		public static string PodIdentityEndpoint
		{
			get
			{
				return EnvironmentVariables.GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("AZURE_POD_IDENTITY_AUTHORITY_HOST"));
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000A862 File Offset: 0x00008A62
		public static string Path
		{
			get
			{
				return EnvironmentVariables.GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("PATH"));
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000A873 File Offset: 0x00008A73
		public static string ProgramFilesX86
		{
			get
			{
				return EnvironmentVariables.GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("ProgramFiles(x86)"));
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000A884 File Offset: 0x00008A84
		public static string ProgramFiles
		{
			get
			{
				return EnvironmentVariables.GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("ProgramFiles"));
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000A895 File Offset: 0x00008A95
		public static string AuthorityHost
		{
			get
			{
				return EnvironmentVariables.GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("AZURE_AUTHORITY_HOST"));
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000A8A6 File Offset: 0x00008AA6
		public static string AzureRegionalAuthorityName
		{
			get
			{
				return EnvironmentVariables.GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("AZURE_REGIONAL_AUTHORITY_NAME"));
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000A8B7 File Offset: 0x00008AB7
		public static string AzureFederatedTokenFile
		{
			get
			{
				return EnvironmentVariables.GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("AZURE_FEDERATED_TOKEN_FILE"));
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000A8C8 File Offset: 0x00008AC8
		private static string GetNonEmptyStringOrNull(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return null;
			}
			return str;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000A8D5 File Offset: 0x00008AD5
		private static bool EnvironmentVariableToBool(string str)
		{
			return string.Equals(bool.TrueString, str, StringComparison.OrdinalIgnoreCase) || string.Equals("1", str, StringComparison.OrdinalIgnoreCase);
		}
	}
}
