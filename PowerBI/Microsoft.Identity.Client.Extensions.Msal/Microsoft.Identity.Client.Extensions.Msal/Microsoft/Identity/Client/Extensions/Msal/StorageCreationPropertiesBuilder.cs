using System;
using System.Collections.Generic;

namespace Microsoft.Identity.Client.Extensions.Msal
{
	// Token: 0x02000021 RID: 33
	public class StorageCreationPropertiesBuilder
	{
		// Token: 0x0600008E RID: 142 RVA: 0x000040E6 File Offset: 0x000022E6
		[Obsolete("Use StorageCreationPropertiesBuilder(string, string) instead. If you need to consume the CacheChanged event then also use WithCacheChangedEvent(string, string)", false)]
		public StorageCreationPropertiesBuilder(string cacheFileName, string cacheDirectory, string clientId)
		{
			this._cacheFileName = cacheFileName;
			this._cacheDirectory = cacheDirectory;
			this._clientId = clientId;
			this._authority = "https://login.microsoftonline.com/common";
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004121 File Offset: 0x00002321
		public StorageCreationPropertiesBuilder(string cacheFileName, string cacheDirectory)
		{
			this._cacheFileName = cacheFileName;
			this._cacheDirectory = cacheDirectory;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000414C File Offset: 0x0000234C
		public StorageCreationProperties Build()
		{
			return new StorageCreationProperties(this._cacheFileName, this._cacheDirectory, this._macKeyChainServiceName, this._macKeyChainAccountName, this._useLinuxPlaintextFallback, this._usePlaintextFallback, this._keyringSchemaName, this._keyringCollection, this._keyringSecretLabel, this._keyringAttribute1, this._keyringAttribute2, this._lockRetryDelay, this._lockRetryCount, this._clientId, this._authority);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000041B8 File Offset: 0x000023B8
		public StorageCreationPropertiesBuilder WithMacKeyChain(string serviceName, string accountName)
		{
			this._macKeyChainServiceName = serviceName;
			this._macKeyChainAccountName = accountName;
			return this;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000041C9 File Offset: 0x000023C9
		public StorageCreationPropertiesBuilder WithCacheChangedEvent(string clientId, string authority = "https://login.microsoftonline.com/common")
		{
			this._clientId = clientId;
			this._authority = authority;
			return this;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000041DA File Offset: 0x000023DA
		public StorageCreationPropertiesBuilder CustomizeLockRetry(int lockRetryDelay, int lockRetryCount)
		{
			if (lockRetryDelay < 1)
			{
				throw new ArgumentOutOfRangeException("lockRetryDelay");
			}
			if (lockRetryCount < 1)
			{
				throw new ArgumentOutOfRangeException("lockRetryCount");
			}
			this._lockRetryCount = lockRetryCount;
			this._lockRetryDelay = lockRetryDelay;
			return this;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004209 File Offset: 0x00002409
		public StorageCreationPropertiesBuilder WithLinuxKeyring(string schemaName, string collection, string secretLabel, KeyValuePair<string, string> attribute1, KeyValuePair<string, string> attribute2)
		{
			if (string.IsNullOrEmpty(schemaName))
			{
				throw new ArgumentNullException("schemaName");
			}
			this._keyringSchemaName = schemaName;
			this._keyringCollection = collection;
			this._keyringSecretLabel = secretLabel;
			this._keyringAttribute1 = attribute1;
			this._keyringAttribute2 = attribute2;
			return this;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004244 File Offset: 0x00002444
		public StorageCreationPropertiesBuilder WithLinuxUnprotectedFile()
		{
			this._useLinuxPlaintextFallback = true;
			return this;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000424E File Offset: 0x0000244E
		public StorageCreationPropertiesBuilder WithUnprotectedFile()
		{
			this._usePlaintextFallback = true;
			return this;
		}

		// Token: 0x04000088 RID: 136
		private readonly string _cacheFileName;

		// Token: 0x04000089 RID: 137
		private readonly string _cacheDirectory;

		// Token: 0x0400008A RID: 138
		private string _clientId;

		// Token: 0x0400008B RID: 139
		private string _authority;

		// Token: 0x0400008C RID: 140
		private string _macKeyChainServiceName;

		// Token: 0x0400008D RID: 141
		private string _macKeyChainAccountName;

		// Token: 0x0400008E RID: 142
		private string _keyringSchemaName;

		// Token: 0x0400008F RID: 143
		private string _keyringCollection;

		// Token: 0x04000090 RID: 144
		private string _keyringSecretLabel;

		// Token: 0x04000091 RID: 145
		private KeyValuePair<string, string> _keyringAttribute1;

		// Token: 0x04000092 RID: 146
		private KeyValuePair<string, string> _keyringAttribute2;

		// Token: 0x04000093 RID: 147
		private int _lockRetryDelay = 100;

		// Token: 0x04000094 RID: 148
		private int _lockRetryCount = 600;

		// Token: 0x04000095 RID: 149
		private bool _useLinuxPlaintextFallback;

		// Token: 0x04000096 RID: 150
		private bool _usePlaintextFallback;
	}
}
