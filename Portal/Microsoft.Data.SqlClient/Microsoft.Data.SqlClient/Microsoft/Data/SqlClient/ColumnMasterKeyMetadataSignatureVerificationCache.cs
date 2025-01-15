using System;
using System.Runtime.Caching;
using System.Text;
using System.Threading;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000048 RID: 72
	internal class ColumnMasterKeyMetadataSignatureVerificationCache
	{
		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x0001064C File Offset: 0x0000E84C
		internal static ColumnMasterKeyMetadataSignatureVerificationCache Instance
		{
			get
			{
				return ColumnMasterKeyMetadataSignatureVerificationCache._signatureVerificationCache;
			}
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00010653 File Offset: 0x0000E853
		private ColumnMasterKeyMetadataSignatureVerificationCache()
		{
			this._cache = new MemoryCache("ColumnMasterKeyMetadataSignatureVerificationCache", null);
			this._inTrim = 0;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00010674 File Offset: 0x0000E874
		internal bool? GetSignatureVerificationResult(string keyStoreName, string masterKeyPath, bool allowEnclaveComputations, byte[] signature)
		{
			this.ValidateStringArgumentNotNullOrEmpty(masterKeyPath, "masterKeyPath", "GetSignatureVerificationResult");
			this.ValidateStringArgumentNotNullOrEmpty(keyStoreName, "keyStoreName", "GetSignatureVerificationResult");
			this.ValidateSignatureNotNullOrEmpty(signature, "GetSignatureVerificationResult");
			string cacheLookupKey = this.GetCacheLookupKey(masterKeyPath, allowEnclaveComputations, signature, keyStoreName);
			return this._cache.Get(cacheLookupKey, null) as bool?;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x000106D4 File Offset: 0x0000E8D4
		internal void AddSignatureVerificationResult(string keyStoreName, string masterKeyPath, bool allowEnclaveComputations, byte[] signature, bool result)
		{
			this.ValidateStringArgumentNotNullOrEmpty(masterKeyPath, "masterKeyPath", "AddSignatureVerificationResult");
			this.ValidateStringArgumentNotNullOrEmpty(keyStoreName, "keyStoreName", "AddSignatureVerificationResult");
			this.ValidateSignatureNotNullOrEmpty(signature, "AddSignatureVerificationResult");
			string cacheLookupKey = this.GetCacheLookupKey(masterKeyPath, allowEnclaveComputations, signature, keyStoreName);
			this.TrimCacheIfNeeded();
			this._cache.Set(cacheLookupKey, result, DateTimeOffset.UtcNow.AddDays(10.0), null);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0001074C File Offset: 0x0000E94C
		private void ValidateSignatureNotNullOrEmpty(byte[] signature, string methodName)
		{
			if (signature != null && signature.Length != 0)
			{
				return;
			}
			if (signature == null)
			{
				throw SQL.NullArgumentInternal("signature", "ColumnMasterKeyMetadataSignatureVerificationCache", methodName);
			}
			throw SQL.EmptyArgumentInternal("signature", "ColumnMasterKeyMetadataSignatureVerificationCache", methodName);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0001077A File Offset: 0x0000E97A
		private void ValidateStringArgumentNotNullOrEmpty(string stringArgValue, string stringArgName, string methodName)
		{
			if (!string.IsNullOrWhiteSpace(stringArgValue))
			{
				return;
			}
			if (stringArgValue == null)
			{
				throw SQL.NullArgumentInternal(stringArgName, "ColumnMasterKeyMetadataSignatureVerificationCache", methodName);
			}
			throw SQL.EmptyArgumentInternal(stringArgName, "ColumnMasterKeyMetadataSignatureVerificationCache", methodName);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x000107A4 File Offset: 0x0000E9A4
		private void TrimCacheIfNeeded()
		{
			long count = this._cache.GetCount(null);
			if (count > 2300L && Interlocked.CompareExchange(ref this._inTrim, 1, 0) == 0)
			{
				try
				{
					this._cache.Trim((int)((double)(count - 2000L) / (double)count * 100.0));
				}
				finally
				{
					Interlocked.CompareExchange(ref this._inTrim, 0, 1);
				}
			}
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0001081C File Offset: 0x0000EA1C
		private string GetCacheLookupKey(string masterKeyPath, bool allowEnclaveComputations, byte[] signature, string keyStoreName)
		{
			StringBuilder stringBuilder = new StringBuilder(keyStoreName, keyStoreName.Length + masterKeyPath.Length + SqlSecurityUtility.GetBase64LengthFromByteLength(signature.Length) + 3 + 10);
			stringBuilder.Append(":");
			stringBuilder.Append(masterKeyPath);
			stringBuilder.Append(":");
			stringBuilder.Append(allowEnclaveComputations);
			stringBuilder.Append(":");
			stringBuilder.Append(Convert.ToBase64String(signature));
			stringBuilder.Append(":");
			return stringBuilder.ToString();
		}

		// Token: 0x040000E8 RID: 232
		private const int _cacheSize = 2000;

		// Token: 0x040000E9 RID: 233
		private const int _cacheTrimThreshold = 300;

		// Token: 0x040000EA RID: 234
		private const string _className = "ColumnMasterKeyMetadataSignatureVerificationCache";

		// Token: 0x040000EB RID: 235
		private const string _getSignatureVerificationResultMethodName = "GetSignatureVerificationResult";

		// Token: 0x040000EC RID: 236
		private const string _addSignatureVerificationResultMethodName = "AddSignatureVerificationResult";

		// Token: 0x040000ED RID: 237
		private const string _masterkeypathArgumentName = "masterKeyPath";

		// Token: 0x040000EE RID: 238
		private const string _keyStoreNameArgumentName = "keyStoreName";

		// Token: 0x040000EF RID: 239
		private const string _signatureName = "signature";

		// Token: 0x040000F0 RID: 240
		private const string _cacheLookupKeySeparator = ":";

		// Token: 0x040000F1 RID: 241
		private static readonly ColumnMasterKeyMetadataSignatureVerificationCache _signatureVerificationCache = new ColumnMasterKeyMetadataSignatureVerificationCache();

		// Token: 0x040000F2 RID: 242
		private readonly MemoryCache _cache;

		// Token: 0x040000F3 RID: 243
		private int _inTrim;
	}
}
