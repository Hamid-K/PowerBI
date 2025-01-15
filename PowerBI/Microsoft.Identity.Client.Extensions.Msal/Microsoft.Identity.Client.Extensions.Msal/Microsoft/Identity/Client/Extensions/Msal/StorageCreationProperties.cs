using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Identity.Client.Extensions.Msal
{
	// Token: 0x02000020 RID: 32
	public class StorageCreationProperties
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00003F70 File Offset: 0x00002170
		internal StorageCreationProperties(string cacheFileName, string cacheDirectory, string macKeyChainServiceName, string macKeyChainAccountName, bool useLinuxPlaintextFallback, bool usePlaintextFallback, string keyringSchemaName, string keyringCollection, string keyringSecretLabel, KeyValuePair<string, string> keyringAttribute1, KeyValuePair<string, string> keyringAttribute2, int lockRetryDelay, int lockRetryCount, string clientId, string authority)
		{
			this.CacheFileName = cacheFileName;
			this.CacheDirectory = cacheDirectory;
			this.CacheFilePath = Path.Combine(this.CacheDirectory, this.CacheFileName);
			this.UseLinuxUnencryptedFallback = useLinuxPlaintextFallback;
			this.UseUnencryptedFallback = usePlaintextFallback;
			this.MacKeyChainServiceName = macKeyChainServiceName;
			this.MacKeyChainAccountName = macKeyChainAccountName;
			this.KeyringSchemaName = keyringSchemaName;
			this.KeyringCollection = keyringCollection;
			this.KeyringSecretLabel = keyringSecretLabel;
			this.KeyringAttribute1 = keyringAttribute1;
			this.KeyringAttribute2 = keyringAttribute2;
			this.ClientId = clientId;
			this.Authority = authority;
			this.LockRetryDelay = lockRetryDelay;
			this.LockRetryCount = lockRetryCount;
			this.Validate();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004018 File Offset: 0x00002218
		private void Validate()
		{
			if (this.UseLinuxUnencryptedFallback && this.UseUnencryptedFallback)
			{
				throw new ArgumentException("UseLinuxUnencryptedFallback and UseUnencryptedFallback are mutually exclusive. UseLinuxUnencryptedFallback is the safer option. ");
			}
			if ((this.UseLinuxUnencryptedFallback || this.UseUnencryptedFallback) && (!string.IsNullOrEmpty(this.KeyringSecretLabel) || !string.IsNullOrEmpty(this.KeyringSchemaName) || !string.IsNullOrEmpty(this.KeyringCollection)))
			{
				throw new ArgumentException("Using plaintext storage is mutually exclusive with other Linux storage options. ");
			}
			if (this.UseUnencryptedFallback && (!string.IsNullOrEmpty(this.MacKeyChainServiceName) || !string.IsNullOrEmpty(this.MacKeyChainAccountName)))
			{
				throw new ArgumentException("Using plaintext storage is mutually exclusive with other Mac storage options. ");
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000040AF File Offset: 0x000022AF
		public string CacheFilePath { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000040B7 File Offset: 0x000022B7
		public string ClientId { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000040BF File Offset: 0x000022BF
		public string Authority { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000040C7 File Offset: 0x000022C7
		internal bool IsCacheEventConfigured
		{
			get
			{
				return !string.IsNullOrEmpty(this.ClientId) && !string.IsNullOrEmpty(this.Authority);
			}
		}

		// Token: 0x04000079 RID: 121
		public readonly string CacheFileName;

		// Token: 0x0400007A RID: 122
		public readonly string CacheDirectory;

		// Token: 0x0400007B RID: 123
		public readonly string MacKeyChainServiceName;

		// Token: 0x0400007C RID: 124
		public readonly string MacKeyChainAccountName;

		// Token: 0x0400007D RID: 125
		public readonly string KeyringSchemaName;

		// Token: 0x0400007E RID: 126
		public readonly string KeyringCollection;

		// Token: 0x0400007F RID: 127
		public readonly string KeyringSecretLabel;

		// Token: 0x04000080 RID: 128
		public readonly KeyValuePair<string, string> KeyringAttribute1;

		// Token: 0x04000081 RID: 129
		public readonly KeyValuePair<string, string> KeyringAttribute2;

		// Token: 0x04000082 RID: 130
		public readonly int LockRetryDelay;

		// Token: 0x04000083 RID: 131
		public readonly bool UseLinuxUnencryptedFallback;

		// Token: 0x04000084 RID: 132
		public readonly bool UseUnencryptedFallback;

		// Token: 0x04000085 RID: 133
		public readonly int LockRetryCount;
	}
}
