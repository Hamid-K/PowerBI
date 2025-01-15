using System;
using Microsoft.Identity.Extensions.Mac;

namespace Microsoft.Identity.Client.Extensions.Msal
{
	// Token: 0x02000014 RID: 20
	internal class MacKeychainAccessor : ICacheAccessor
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002AF0 File Offset: 0x00000CF0
		public MacKeychainAccessor(string cacheFilePath, string keyChainServiceName, string keyChainAccountName, TraceSourceLogger logger)
		{
			if (string.IsNullOrWhiteSpace(cacheFilePath))
			{
				throw new ArgumentNullException("cacheFilePath");
			}
			if (string.IsNullOrWhiteSpace(keyChainServiceName))
			{
				throw new ArgumentNullException("keyChainServiceName");
			}
			if (string.IsNullOrWhiteSpace(keyChainAccountName))
			{
				throw new ArgumentNullException("keyChainAccountName");
			}
			this._cacheFilePath = cacheFilePath;
			this._service = keyChainServiceName;
			this._account = keyChainAccountName;
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			this._logger = logger;
			this._keyChain = new MacOSKeychain(null);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002B74 File Offset: 0x00000D74
		public void Clear()
		{
			this._logger.LogInformation("Clearing cache");
			FileIOWithRetries.DeleteCacheFile(this._cacheFilePath, this._logger);
			this._logger.LogInformation("Before delete mac keychain service: " + this._service + " account " + this._account);
			this._keyChain.Remove(this._service, this._account);
			this._logger.LogInformation("After delete mac keychain service: " + this._service + " account " + this._account);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002C08 File Offset: 0x00000E08
		public byte[] Read()
		{
			this._logger.LogInformation("ReadDataCore, Before reading from mac keychain service: " + this._service + " account " + this._account);
			MacOSKeychainCredential macOSKeychainCredential = this._keyChain.Get(this._service, this._account);
			TraceSourceLogger logger = this._logger;
			string text = "ReadDataCore, After reading mac keychain {0} chars service: {1} account {2}";
			int? num;
			if (macOSKeychainCredential == null)
			{
				num = null;
			}
			else
			{
				byte[] password = macOSKeychainCredential.Password;
				num = ((password != null) ? new int?(password.Length) : null);
			}
			int? num2 = num;
			logger.LogInformation(string.Format(text, num2.GetValueOrDefault(), this._service, this._account));
			if (macOSKeychainCredential == null)
			{
				return null;
			}
			return macOSKeychainCredential.Password;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002CB8 File Offset: 0x00000EB8
		public void Write(byte[] data)
		{
			this._logger.LogInformation("Before write to mac keychain service: " + this._service + " account " + this._account);
			this._keyChain.AddOrUpdate(this._service, this._account, data);
			this._logger.LogInformation("After write to mac keychain service: " + this._service + " account " + this._account);
			FileIOWithRetries.TouchFile(this._cacheFilePath, this._logger);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002D3C File Offset: 0x00000F3C
		public ICacheAccessor CreateForPersistenceValidation()
		{
			return new MacKeychainAccessor(this._cacheFilePath + ".test", this._service + Guid.NewGuid().ToString(), this._account, this._logger);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002D88 File Offset: 0x00000F88
		public override string ToString()
		{
			return string.Concat(new string[] { "MacKeyChain accessor pointing to: service ", this._service, ", account ", this._account, ", file ", this._cacheFilePath });
		}

		// Token: 0x04000052 RID: 82
		private readonly string _cacheFilePath;

		// Token: 0x04000053 RID: 83
		private readonly string _service;

		// Token: 0x04000054 RID: 84
		private readonly string _account;

		// Token: 0x04000055 RID: 85
		private readonly TraceSourceLogger _logger;

		// Token: 0x04000056 RID: 86
		private readonly MacOSKeychain _keyChain;
	}
}
