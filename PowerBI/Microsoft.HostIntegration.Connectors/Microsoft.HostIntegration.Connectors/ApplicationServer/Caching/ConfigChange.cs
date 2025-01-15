using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000F0 RID: 240
	internal struct ConfigChange
	{
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0001AAC6 File Offset: 0x00018CC6
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x0001AACE File Offset: 0x00018CCE
		internal bool ChangeHost
		{
			get
			{
				return this._hostChange;
			}
			set
			{
				this._hostChange = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0001AAD7 File Offset: 0x00018CD7
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x0001AADF File Offset: 0x00018CDF
		internal bool ChangeCluster
		{
			get
			{
				return this._clusterChange;
			}
			set
			{
				this._clusterChange = value;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0001AAE8 File Offset: 0x00018CE8
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x0001AAF0 File Offset: 0x00018CF0
		internal CacheConfigChange ChangeCache
		{
			get
			{
				return this._cacheChange;
			}
			set
			{
				this._cacheChange = value;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0001AAF9 File Offset: 0x00018CF9
		// (set) Token: 0x060006C6 RID: 1734 RVA: 0x0001AB01 File Offset: 0x00018D01
		internal AdvancePropertiesChange ChangeAdvanceProperties
		{
			get
			{
				return this._advChange;
			}
			set
			{
				this._advChange = value;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0001AB0A File Offset: 0x00018D0A
		// (set) Token: 0x060006C8 RID: 1736 RVA: 0x0001AB12 File Offset: 0x00018D12
		internal DeploymentSettingsChange ChangeDeploymentSettings
		{
			get
			{
				return this._deplChange;
			}
			set
			{
				this._deplChange = value;
			}
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001AB1B File Offset: 0x00018D1B
		public void ChangeAll(bool on)
		{
			this._hostChange = on;
			this._clusterChange = on;
			this._cacheChange.ChangeAll(on);
			this._advChange[AdvanceChanges.ChangeAll] = on;
			this._deplChange[DeploymentChanges.ChangeAll] = on;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0001AB56 File Offset: 0x00018D56
		public bool Changed
		{
			get
			{
				return this._cacheChange.Changed || this._advChange.Changed || this._hostChange || this._clusterChange || this._deplChange.Changed;
			}
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001AB90 File Offset: 0x00018D90
		public bool CanUpdateConfigDynamically()
		{
			return !this.ChangeHost && !this.ChangeCluster && !this.ChangeCache.Changed && !this.ChangeDeploymentSettings.Changed && this.ChangeAdvanceProperties.CanUpdateConfigDynamically();
		}

		// Token: 0x04000435 RID: 1077
		private bool _hostChange;

		// Token: 0x04000436 RID: 1078
		private bool _clusterChange;

		// Token: 0x04000437 RID: 1079
		private CacheConfigChange _cacheChange;

		// Token: 0x04000438 RID: 1080
		private AdvancePropertiesChange _advChange;

		// Token: 0x04000439 RID: 1081
		private DeploymentSettingsChange _deplChange;
	}
}
