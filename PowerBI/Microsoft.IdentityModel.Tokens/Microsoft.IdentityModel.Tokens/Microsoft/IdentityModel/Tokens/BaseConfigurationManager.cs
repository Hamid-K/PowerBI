using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens.Configuration;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000122 RID: 290
	public abstract class BaseConfigurationManager
	{
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x00039604 File Offset: 0x00037804
		// (set) Token: 0x06000E60 RID: 3680 RVA: 0x0003960C File Offset: 0x0003780C
		public TimeSpan AutomaticRefreshInterval
		{
			get
			{
				return this._automaticRefreshInterval;
			}
			set
			{
				if (value < BaseConfigurationManager.MinimumAutomaticRefreshInterval)
				{
					throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("value", LogHelper.FormatInvariant("IDX10108: When setting AutomaticRefreshInterval, the value must be greater than MinimumAutomaticRefreshInterval: '{0}'. value: '{1}'.", new object[]
					{
						LogHelper.MarkAsNonPII(BaseConfigurationManager.MinimumAutomaticRefreshInterval),
						LogHelper.MarkAsNonPII(value)
					})));
				}
				this._automaticRefreshInterval = value;
			}
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x0003966D File Offset: 0x0003786D
		public BaseConfigurationManager()
			: this(new LKGConfigurationCacheOptions())
		{
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x0003967C File Offset: 0x0003787C
		public BaseConfigurationManager(LKGConfigurationCacheOptions options)
		{
			if (options == null)
			{
				throw LogHelper.LogArgumentNullException("options");
			}
			this._lastKnownGoodConfigurationCache = new EventBasedLRUCache<BaseConfiguration, DateTime>(options.LastKnownGoodConfigurationSizeLimit, options.TaskCreationOptions, options.BaseConfigurationComparer, options.RemoveExpiredValues, 300, false);
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x000396EE File Offset: 0x000378EE
		public virtual Task<BaseConfiguration> GetBaseConfigurationAsync(CancellationToken cancel)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x000396F8 File Offset: 0x000378F8
		internal ICollection<BaseConfiguration> GetValidLkgConfigurations()
		{
			return (from x in this._lastKnownGoodConfigurationCache.ToArray()
				where x.Value.Value > DateTime.UtcNow
				select x.Key).ToArray<BaseConfiguration>();
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x0003975D File Offset: 0x0003795D
		// (set) Token: 0x06000E66 RID: 3686 RVA: 0x00039768 File Offset: 0x00037968
		public BaseConfiguration LastKnownGoodConfiguration
		{
			get
			{
				return this._lastKnownGoodConfiguration;
			}
			set
			{
				if (value == null)
				{
					throw LogHelper.LogArgumentNullException("value");
				}
				this._lastKnownGoodConfiguration = value;
				this._lastKnownGoodConfigFirstUse = new DateTime?(DateTime.UtcNow);
				this._lastKnownGoodConfigurationCache.SetValue(this._lastKnownGoodConfiguration, DateTime.UtcNow + this.LastKnownGoodLifetime, DateTime.UtcNow + this.LastKnownGoodLifetime);
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x000397CD File Offset: 0x000379CD
		// (set) Token: 0x06000E68 RID: 3688 RVA: 0x000397D5 File Offset: 0x000379D5
		public TimeSpan LastKnownGoodLifetime
		{
			get
			{
				return this._lastKnownGoodLifetime;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("value", LogHelper.FormatInvariant("IDX10110: When setting LastKnownGoodLifetime, the value must be greater than or equal to zero. value: '{0}'.", new object[] { value })));
				}
				this._lastKnownGoodLifetime = value;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x00039814 File Offset: 0x00037A14
		// (set) Token: 0x06000E6A RID: 3690 RVA: 0x0003981C File Offset: 0x00037A1C
		public string MetadataAddress { get; set; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x00039825 File Offset: 0x00037A25
		// (set) Token: 0x06000E6C RID: 3692 RVA: 0x00039830 File Offset: 0x00037A30
		public TimeSpan RefreshInterval
		{
			get
			{
				return this._refreshInterval;
			}
			set
			{
				if (value < BaseConfigurationManager.MinimumRefreshInterval)
				{
					throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("value", LogHelper.FormatInvariant("IDX10107: When setting RefreshInterval, the value must be greater than MinimumRefreshInterval: '{0}'. value: '{1}'.", new object[]
					{
						LogHelper.MarkAsNonPII(BaseConfigurationManager.MinimumRefreshInterval),
						LogHelper.MarkAsNonPII(value)
					})));
				}
				this._refreshInterval = value;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x00039891 File Offset: 0x00037A91
		// (set) Token: 0x06000E6E RID: 3694 RVA: 0x00039899 File Offset: 0x00037A99
		public bool UseLastKnownGoodConfiguration { get; set; } = true;

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x000398A4 File Offset: 0x00037AA4
		public bool IsLastKnownGoodValid
		{
			get
			{
				if (this._lastKnownGoodConfiguration == null)
				{
					return false;
				}
				if (this._lastKnownGoodConfigFirstUse != null)
				{
					DateTime utcNow = DateTime.UtcNow;
					return utcNow < this._lastKnownGoodConfigFirstUse + this.LastKnownGoodLifetime;
				}
				return true;
			}
		}

		// Token: 0x06000E70 RID: 3696
		public abstract void RequestRefresh();

		// Token: 0x04000496 RID: 1174
		private TimeSpan _automaticRefreshInterval = BaseConfigurationManager.DefaultAutomaticRefreshInterval;

		// Token: 0x04000497 RID: 1175
		private TimeSpan _refreshInterval = BaseConfigurationManager.DefaultRefreshInterval;

		// Token: 0x04000498 RID: 1176
		private TimeSpan _lastKnownGoodLifetime = BaseConfigurationManager.DefaultLastKnownGoodConfigurationLifetime;

		// Token: 0x04000499 RID: 1177
		private BaseConfiguration _lastKnownGoodConfiguration;

		// Token: 0x0400049A RID: 1178
		private DateTime? _lastKnownGoodConfigFirstUse;

		// Token: 0x0400049B RID: 1179
		internal EventBasedLRUCache<BaseConfiguration, DateTime> _lastKnownGoodConfigurationCache;

		// Token: 0x0400049C RID: 1180
		public static readonly TimeSpan DefaultAutomaticRefreshInterval = new TimeSpan(0, 12, 0, 0);

		// Token: 0x0400049D RID: 1181
		public static readonly TimeSpan DefaultLastKnownGoodConfigurationLifetime = new TimeSpan(0, 1, 0, 0);

		// Token: 0x0400049E RID: 1182
		public static readonly TimeSpan DefaultRefreshInterval = new TimeSpan(0, 0, 5, 0);

		// Token: 0x040004A0 RID: 1184
		public static readonly TimeSpan MinimumAutomaticRefreshInterval = new TimeSpan(0, 0, 5, 0);

		// Token: 0x040004A1 RID: 1185
		public static readonly TimeSpan MinimumRefreshInterval = new TimeSpan(0, 0, 0, 1);
	}
}
