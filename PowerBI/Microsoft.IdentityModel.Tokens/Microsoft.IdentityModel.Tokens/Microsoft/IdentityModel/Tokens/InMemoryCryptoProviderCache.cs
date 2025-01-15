using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000156 RID: 342
	public class InMemoryCryptoProviderCache : CryptoProviderCache, IDisposable
	{
		// Token: 0x06000FE8 RID: 4072 RVA: 0x0003E0E9 File Offset: 0x0003C2E9
		public InMemoryCryptoProviderCache()
			: this(new CryptoProviderCacheOptions())
		{
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x0003E0F6 File Offset: 0x0003C2F6
		// (set) Token: 0x06000FEA RID: 4074 RVA: 0x0003E0FE File Offset: 0x0003C2FE
		internal CryptoProviderFactory CryptoProviderFactory { get; set; }

		// Token: 0x06000FEB RID: 4075 RVA: 0x0003E108 File Offset: 0x0003C308
		public InMemoryCryptoProviderCache(CryptoProviderCacheOptions cryptoProviderCacheOptions)
		{
			if (cryptoProviderCacheOptions == null)
			{
				throw LogHelper.LogArgumentNullException("cryptoProviderCacheOptions");
			}
			this._cryptoProviderCacheOptions = cryptoProviderCacheOptions;
			EventBasedLRUCache<string, SignatureProvider> eventBasedLRUCache = new EventBasedLRUCache<string, SignatureProvider>(cryptoProviderCacheOptions.SizeLimit, TaskCreationOptions.None, StringComparer.Ordinal, false, 300, false);
			eventBasedLRUCache.OnItemRemoved = delegate(SignatureProvider signatureProvider)
			{
				signatureProvider.CryptoProviderCache = null;
			};
			this._signingSignatureProviders = eventBasedLRUCache;
			EventBasedLRUCache<string, SignatureProvider> eventBasedLRUCache2 = new EventBasedLRUCache<string, SignatureProvider>(cryptoProviderCacheOptions.SizeLimit, TaskCreationOptions.None, StringComparer.Ordinal, false, 300, false);
			eventBasedLRUCache2.OnItemRemoved = delegate(SignatureProvider signatureProvider)
			{
				signatureProvider.CryptoProviderCache = null;
			};
			this._verifyingSignatureProviders = eventBasedLRUCache2;
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x0003E1B8 File Offset: 0x0003C3B8
		internal InMemoryCryptoProviderCache(CryptoProviderCacheOptions cryptoProviderCacheOptions, TaskCreationOptions options, int tryTakeTimeout = 500)
		{
			if (cryptoProviderCacheOptions == null)
			{
				throw LogHelper.LogArgumentNullException("cryptoProviderCacheOptions");
			}
			if (tryTakeTimeout <= 0)
			{
				throw LogHelper.LogArgumentException<ArgumentException>("tryTakeTimeout", "tryTakeTimeout must be greater than zero");
			}
			this._cryptoProviderCacheOptions = cryptoProviderCacheOptions;
			EventBasedLRUCache<string, SignatureProvider> eventBasedLRUCache = new EventBasedLRUCache<string, SignatureProvider>(cryptoProviderCacheOptions.SizeLimit, options, StringComparer.Ordinal, false, 300, false);
			eventBasedLRUCache.OnItemRemoved = delegate(SignatureProvider signatureProvider)
			{
				signatureProvider.CryptoProviderCache = null;
			};
			this._signingSignatureProviders = eventBasedLRUCache;
			EventBasedLRUCache<string, SignatureProvider> eventBasedLRUCache2 = new EventBasedLRUCache<string, SignatureProvider>(cryptoProviderCacheOptions.SizeLimit, options, StringComparer.Ordinal, false, 300, false);
			eventBasedLRUCache2.OnItemRemoved = delegate(SignatureProvider signatureProvider)
			{
				signatureProvider.CryptoProviderCache = null;
			};
			this._verifyingSignatureProviders = eventBasedLRUCache2;
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0003E27A File Offset: 0x0003C47A
		protected override string GetCacheKey(SignatureProvider signatureProvider)
		{
			if (signatureProvider == null)
			{
				throw LogHelper.LogArgumentNullException("signatureProvider");
			}
			return InMemoryCryptoProviderCache.GetCacheKeyPrivate(signatureProvider.Key, signatureProvider.Algorithm, signatureProvider.GetType().ToString());
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0003E2A6 File Offset: 0x0003C4A6
		protected override string GetCacheKey(SecurityKey securityKey, string algorithm, string typeofProvider)
		{
			if (securityKey == null)
			{
				throw LogHelper.LogArgumentNullException("securityKey");
			}
			if (string.IsNullOrEmpty(algorithm))
			{
				throw LogHelper.LogArgumentNullException("algorithm");
			}
			if (string.IsNullOrEmpty(typeofProvider))
			{
				throw LogHelper.LogArgumentNullException("typeofProvider");
			}
			return InMemoryCryptoProviderCache.GetCacheKeyPrivate(securityKey, algorithm, typeofProvider);
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0003E2E4 File Offset: 0x0003C4E4
		private static string GetCacheKeyPrivate(SecurityKey securityKey, string algorithm, string typeofProvider)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}-{1}-{2}-{3}", new object[]
			{
				securityKey.GetType(),
				securityKey.InternalId,
				algorithm,
				typeofProvider
			});
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0003E318 File Offset: 0x0003C518
		public override bool TryAdd(SignatureProvider signatureProvider)
		{
			if (signatureProvider == null)
			{
				throw LogHelper.LogArgumentNullException("signatureProvider");
			}
			string cacheKey = this.GetCacheKey(signatureProvider);
			EventBasedLRUCache<string, SignatureProvider> eventBasedLRUCache;
			if (signatureProvider.WillCreateSignatures)
			{
				eventBasedLRUCache = this._signingSignatureProviders;
			}
			else
			{
				eventBasedLRUCache = this._verifyingSignatureProviders;
			}
			if (!eventBasedLRUCache.Contains(cacheKey))
			{
				eventBasedLRUCache.SetValue(cacheKey, signatureProvider);
				signatureProvider.CryptoProviderCache = this;
				return true;
			}
			return false;
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0003E370 File Offset: 0x0003C570
		public override bool TryGetSignatureProvider(SecurityKey securityKey, string algorithm, string typeofProvider, bool willCreateSignatures, out SignatureProvider signatureProvider)
		{
			if (securityKey == null)
			{
				throw LogHelper.LogArgumentNullException("securityKey");
			}
			if (string.IsNullOrEmpty(algorithm))
			{
				throw LogHelper.LogArgumentNullException("algorithm");
			}
			if (string.IsNullOrEmpty(typeofProvider))
			{
				throw LogHelper.LogArgumentNullException("typeofProvider");
			}
			string cacheKeyPrivate = InMemoryCryptoProviderCache.GetCacheKeyPrivate(securityKey, algorithm, typeofProvider);
			if (willCreateSignatures)
			{
				return this._signingSignatureProviders.TryGetValue(cacheKeyPrivate, out signatureProvider);
			}
			return this._verifyingSignatureProviders.TryGetValue(cacheKeyPrivate, out signatureProvider);
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0003E3DC File Offset: 0x0003C5DC
		public override bool TryRemove(SignatureProvider signatureProvider)
		{
			if (signatureProvider == null)
			{
				throw LogHelper.LogArgumentNullException("signatureProvider");
			}
			if (signatureProvider.CryptoProviderCache != this)
			{
				return false;
			}
			string cacheKey = this.GetCacheKey(signatureProvider);
			EventBasedLRUCache<string, SignatureProvider> eventBasedLRUCache;
			if (signatureProvider.WillCreateSignatures)
			{
				eventBasedLRUCache = this._signingSignatureProviders;
			}
			else
			{
				eventBasedLRUCache = this._verifyingSignatureProviders;
			}
			bool flag;
			try
			{
				SignatureProvider signatureProvider2;
				flag = eventBasedLRUCache.TryRemove(cacheKey, out signatureProvider2);
			}
			catch (Exception ex)
			{
				LogHelper.LogWarning(LogHelper.FormatInvariant("IDX10699: Unable to remove SignatureProvider with cache key: {0} from the InMemoryCryptoProviderCache. Exception: '{1}'.", new object[] { cacheKey, ex }), Array.Empty<object>());
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0003E46C File Offset: 0x0003C66C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0003E47B File Offset: 0x0003C67B
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				this._signingSignatureProviders.StopEventQueueTask();
				this._verifyingSignatureProviders.StopEventQueueTask();
				this._disposed = true;
			}
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0003E4A2 File Offset: 0x0003C6A2
		internal long LinkedListCountSigning()
		{
			return this._signingSignatureProviders.LinkedListCount;
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0003E4AF File Offset: 0x0003C6AF
		internal long LinkedListCountVerifying()
		{
			return this._verifyingSignatureProviders.LinkedListCount;
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0003E4BC File Offset: 0x0003C6BC
		internal long MapCountSigning()
		{
			return this._signingSignatureProviders.MapCount;
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0003E4C9 File Offset: 0x0003C6C9
		internal long MapCountVerifying()
		{
			return this._verifyingSignatureProviders.MapCount;
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0003E4D6 File Offset: 0x0003C6D6
		internal long EventQueueCountSigning()
		{
			return this._signingSignatureProviders.EventQueueCount;
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0003E4E3 File Offset: 0x0003C6E3
		internal long EventQueueCountVerifying()
		{
			return this._verifyingSignatureProviders.EventQueueCount;
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000FFB RID: 4091 RVA: 0x0003E4F0 File Offset: 0x0003C6F0
		internal long TaskCount
		{
			get
			{
				return (long)(this._signingSignatureProviders.TaskCount + this._verifyingSignatureProviders.TaskCount);
			}
		}

		// Token: 0x0400051B RID: 1307
		internal CryptoProviderCacheOptions _cryptoProviderCacheOptions;

		// Token: 0x0400051C RID: 1308
		private bool _disposed;

		// Token: 0x0400051D RID: 1309
		private readonly EventBasedLRUCache<string, SignatureProvider> _signingSignatureProviders;

		// Token: 0x0400051E RID: 1310
		private readonly EventBasedLRUCache<string, SignatureProvider> _verifyingSignatureProviders;
	}
}
