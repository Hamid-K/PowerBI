using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.IdentityModel.Protocols
{
	// Token: 0x02000003 RID: 3
	public class ConfigurationManager<T> : BaseConfigurationManager, IConfigurationManager<T> where T : class
	{
		// Token: 0x06000013 RID: 19 RVA: 0x0000242D File Offset: 0x0000062D
		public ConfigurationManager(string metadataAddress, IConfigurationRetriever<T> configRetriever)
			: this(metadataAddress, configRetriever, new HttpDocumentRetriever(), new LastKnownGoodConfigurationCacheOptions())
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002441 File Offset: 0x00000641
		public ConfigurationManager(string metadataAddress, IConfigurationRetriever<T> configRetriever, HttpClient httpClient)
			: this(metadataAddress, configRetriever, new HttpDocumentRetriever(httpClient), new LastKnownGoodConfigurationCacheOptions())
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002456 File Offset: 0x00000656
		public ConfigurationManager(string metadataAddress, IConfigurationRetriever<T> configRetriever, IDocumentRetriever docRetriever)
			: this(metadataAddress, configRetriever, docRetriever, new LastKnownGoodConfigurationCacheOptions())
		{
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002468 File Offset: 0x00000668
		public ConfigurationManager(string metadataAddress, IConfigurationRetriever<T> configRetriever, IDocumentRetriever docRetriever, LastKnownGoodConfigurationCacheOptions lkgCacheOptions)
			: base(lkgCacheOptions)
		{
			if (string.IsNullOrWhiteSpace(metadataAddress))
			{
				throw LogHelper.LogArgumentNullException("metadataAddress");
			}
			if (configRetriever == null)
			{
				throw LogHelper.LogArgumentNullException("configRetriever");
			}
			if (docRetriever == null)
			{
				throw LogHelper.LogArgumentNullException("docRetriever");
			}
			base.MetadataAddress = metadataAddress;
			this._docRetriever = docRetriever;
			this._configRetriever = configRetriever;
			this._refreshLock = new SemaphoreSlim(1);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024FE File Offset: 0x000006FE
		public ConfigurationManager(string metadataAddress, IConfigurationRetriever<T> configRetriever, IDocumentRetriever docRetriever, IConfigurationValidator<T> configValidator)
			: this(metadataAddress, configRetriever, docRetriever, configValidator, new LastKnownGoodConfigurationCacheOptions())
		{
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002510 File Offset: 0x00000710
		public ConfigurationManager(string metadataAddress, IConfigurationRetriever<T> configRetriever, IDocumentRetriever docRetriever, IConfigurationValidator<T> configValidator, LastKnownGoodConfigurationCacheOptions lkgCacheOptions)
			: this(metadataAddress, configRetriever, docRetriever, lkgCacheOptions)
		{
			if (configValidator == null)
			{
				throw LogHelper.LogArgumentNullException("configValidator");
			}
			this._configValidator = configValidator;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002534 File Offset: 0x00000734
		public async Task<T> GetConfigurationAsync()
		{
			return await this.GetConfigurationAsync(CancellationToken.None).ConfigureAwait(false);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002578 File Offset: 0x00000778
		public async Task<T> GetConfigurationAsync(CancellationToken cancel)
		{
			T t;
			if (this._currentConfiguration != null && this._syncAfter > DateTimeOffset.UtcNow)
			{
				t = this._currentConfiguration;
			}
			else
			{
				await this._refreshLock.WaitAsync(cancel).ConfigureAwait(false);
				try
				{
					if (this._syncAfter <= DateTimeOffset.UtcNow)
					{
						try
						{
							T t2 = await this._configRetriever.GetConfigurationAsync(base.MetadataAddress, this._docRetriever, CancellationToken.None).ConfigureAwait(false);
							if (this._configValidator != null)
							{
								ConfigurationValidationResult configurationValidationResult = this._configValidator.Validate(t2);
								if (!configurationValidationResult.Succeeded)
								{
									throw LogHelper.LogExceptionMessage(new InvalidConfigurationException(LogHelper.FormatInvariant("IDX20810: Configuration validation failed, see inner exception for more details. Exception: '{0}'.", new object[] { configurationValidationResult.ErrorMessage })));
								}
							}
							this._lastRefresh = DateTimeOffset.UtcNow;
							this._syncAfter = DateTimeUtil.Add(DateTime.UtcNow, base.AutomaticRefreshInterval + TimeSpan.FromSeconds((double)new Random().Next((int)base.AutomaticRefreshInterval.TotalSeconds / 20)));
							this._currentConfiguration = t2;
						}
						catch (Exception ex)
						{
							this._fetchMetadataFailure = ex;
							if (this._currentConfiguration == null)
							{
								if (this._bootstrapRefreshInterval < base.RefreshInterval)
								{
									TimeSpan timeSpan = TimeSpan.FromSeconds((double)new Random().Next((int)this._bootstrapRefreshInterval.TotalSeconds));
									this._bootstrapRefreshInterval += this._bootstrapRefreshInterval;
									this._syncAfter = DateTimeUtil.Add(DateTime.UtcNow, timeSpan);
								}
								else
								{
									this._syncAfter = DateTimeUtil.Add(DateTime.UtcNow, (base.AutomaticRefreshInterval < base.RefreshInterval) ? base.AutomaticRefreshInterval : base.RefreshInterval);
								}
								throw LogHelper.LogExceptionMessage(new InvalidOperationException(LogHelper.FormatInvariant("IDX20803: Unable to obtain configuration from: '{0}'. Will retry at '{1}'. Exception: '{2}'.", new object[]
								{
									LogHelper.MarkAsNonPII(base.MetadataAddress ?? "null"),
									LogHelper.MarkAsNonPII(this._syncAfter),
									LogHelper.MarkAsNonPII(ex)
								}), ex));
							}
							this._syncAfter = DateTimeUtil.Add(DateTime.UtcNow, (base.AutomaticRefreshInterval < base.RefreshInterval) ? base.AutomaticRefreshInterval : base.RefreshInterval);
							LogHelper.LogExceptionMessage(new InvalidOperationException(LogHelper.FormatInvariant("IDX20806: Unable to obtain an updated configuration from: '{0}'. Returning the current configuration. Exception: '{1}.", new object[]
							{
								LogHelper.MarkAsNonPII(base.MetadataAddress ?? "null"),
								LogHelper.MarkAsNonPII(ex)
							}), ex));
						}
					}
					if (this._currentConfiguration == null)
					{
						throw LogHelper.LogExceptionMessage(new InvalidOperationException(LogHelper.FormatInvariant("IDX20803: Unable to obtain configuration from: '{0}'. Will retry at '{1}'. Exception: '{2}'.", new object[]
						{
							LogHelper.MarkAsNonPII(base.MetadataAddress ?? "null"),
							LogHelper.MarkAsNonPII(this._syncAfter),
							LogHelper.MarkAsNonPII(this._fetchMetadataFailure)
						}), this._fetchMetadataFailure));
					}
					t = this._currentConfiguration;
				}
				finally
				{
					this._refreshLock.Release();
				}
			}
			return t;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000025C4 File Offset: 0x000007C4
		public override async Task<BaseConfiguration> GetBaseConfigurationAsync(CancellationToken cancel)
		{
			T t = await this.GetConfigurationAsync(cancel).ConfigureAwait(false);
			BaseConfiguration baseConfiguration;
			if (t is BaseConfiguration)
			{
				baseConfiguration = t as BaseConfiguration;
			}
			else
			{
				baseConfiguration = null;
			}
			return baseConfiguration;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002610 File Offset: 0x00000810
		public override void RequestRefresh()
		{
			DateTimeOffset utcNow = DateTimeOffset.UtcNow;
			if (this._isFirstRefreshRequest)
			{
				this._syncAfter = utcNow;
				this._isFirstRefreshRequest = false;
				return;
			}
			if (utcNow >= DateTimeUtil.Add(this._lastRefresh.UtcDateTime, base.RefreshInterval))
			{
				this._syncAfter = utcNow;
			}
		}

		// Token: 0x04000007 RID: 7
		private DateTimeOffset _syncAfter = DateTimeOffset.MinValue;

		// Token: 0x04000008 RID: 8
		private DateTimeOffset _lastRefresh = DateTimeOffset.MinValue;

		// Token: 0x04000009 RID: 9
		private bool _isFirstRefreshRequest = true;

		// Token: 0x0400000A RID: 10
		private readonly SemaphoreSlim _refreshLock;

		// Token: 0x0400000B RID: 11
		private readonly IDocumentRetriever _docRetriever;

		// Token: 0x0400000C RID: 12
		private readonly IConfigurationRetriever<T> _configRetriever;

		// Token: 0x0400000D RID: 13
		private readonly IConfigurationValidator<T> _configValidator;

		// Token: 0x0400000E RID: 14
		private T _currentConfiguration;

		// Token: 0x0400000F RID: 15
		private Exception _fetchMetadataFailure;

		// Token: 0x04000010 RID: 16
		private TimeSpan _bootstrapRefreshInterval = TimeSpan.FromSeconds(1.0);

		// Token: 0x04000011 RID: 17
		public new static readonly TimeSpan DefaultAutomaticRefreshInterval = BaseConfigurationManager.DefaultAutomaticRefreshInterval;

		// Token: 0x04000012 RID: 18
		public new static readonly TimeSpan DefaultRefreshInterval = BaseConfigurationManager.DefaultRefreshInterval;

		// Token: 0x04000013 RID: 19
		public new static readonly TimeSpan MinimumAutomaticRefreshInterval = BaseConfigurationManager.MinimumAutomaticRefreshInterval;

		// Token: 0x04000014 RID: 20
		public new static readonly TimeSpan MinimumRefreshInterval = BaseConfigurationManager.MinimumRefreshInterval;
	}
}
