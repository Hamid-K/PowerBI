using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000DE RID: 222
	internal class ClusterConfigPoller : IDisposable
	{
		// Token: 0x06000645 RID: 1605 RVA: 0x0001934C File Offset: 0x0001754C
		private ClusterConfigPoller(ServiceConfigurationManager confMan, TimeSpan cacheConfigPeriod, TimeSpan versionPropsPeriod)
		{
			this.serviceConfig = confMan;
			this.CacheConfigUpdatePeriod = cacheConfigPeriod;
			this.VersionPropsUpdatePeriod = versionPropsPeriod;
			this.versionPropertiesTimer = new global::System.Threading.Timer(new TimerCallback(this.PollForClusterPropertiesUpdate), null, -1, -1);
			this.cacheConfigurationsTimer = new global::System.Threading.Timer(new TimerCallback(this.PollForCacheConfigurationsUpdate), null, -1, -1);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x000193F3 File Offset: 0x000175F3
		internal static ClusterConfigPoller GetClusterConfigPoller(ServiceConfigurationManager confMan, TimeSpan cacheConfigPeriod, TimeSpan versionPropsPeriod)
		{
			if (ConfigManager.IsStoreVersionHigherThan2000(confMan.StoreVersion))
			{
				return new ClusterConfigPoller(confMan, cacheConfigPeriod, versionPropsPeriod);
			}
			return null;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001940C File Offset: 0x0001760C
		internal bool Start()
		{
			lock (this._lockObject)
			{
				if (!this._started && !this._disposed)
				{
					this.versionSet = this.serviceConfig.GetAllVersions();
					this._started = true;
					this.deploymentSettingsVersion = this.serviceConfig.GetDeploymentVersion();
					TimeSpan timeSpan = new TimeSpan(0, 5, 0);
					this.versionPropertiesTimer.Change(timeSpan, this.VersionPropsUpdatePeriod);
					this.cacheConfigurationsTimer.Change(timeSpan, this.CacheConfigUpdatePeriod);
					if (Provider.IsEnabled(TraceLevel.Info))
					{
						EventLogWriter.WriteInfo("Velocity.ClusterConfigPoller", "Poller started at {0}", new object[] { DateTime.UtcNow });
						EventLogWriter.WriteInfo("Velocity.ClusterConfigPoller", "Polling interval for CacheConfig poller = {0} and for VersionProperties poller = {1}", new object[]
						{
							this.CacheConfigUpdatePeriod.TotalMinutes,
							this.VersionPropsUpdatePeriod.TotalMinutes
						});
					}
				}
			}
			return this._started;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00019530 File Offset: 0x00017730
		internal void Stop()
		{
			lock (this._lockObject)
			{
				if (this._started && !this._disposed)
				{
					this._started = false;
					this.versionPropertiesTimer.Change(-1, -1);
					this.cacheConfigurationsTimer.Change(-1, -1);
				}
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<DateTime, bool>("Velocity.ClusterConfigPoller", "Poller stop called at {0}, IsStarted={1}", DateTime.UtcNow, this._started);
			}
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x000195C0 File Offset: 0x000177C0
		private void PollForDeploymentSettingsUpdate()
		{
			this.serviceConfig.ReloadDeploymentSettings(ref this.deploymentSettingsVersion);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x000195D4 File Offset: 0x000177D4
		private void PollForVersionPropertiesUpdate()
		{
			long versionPropertiesId = this.versionSet.VersionPropertiesId;
			this.serviceConfig.ReloadVersionProperties(ref this.versionSet);
			if (versionPropertiesId < this.versionSet.VersionPropertiesId)
			{
				VersionProperties versionProperties = this.serviceConfig.AdvancedProperties.VersionProperties;
				ClientVersionInfo.Singleton.EditAllowedVersions(versionProperties.BeginClientVersion, versionProperties.EndClientVersion, VersioningUtility.GetOtherSupportedClientVersions(versionProperties.BeginServerVersion, versionProperties.EndServerVersion));
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo("Velocity.ClusterConfigPoller", "Updated version properties at {0}", new object[] { DateTime.UtcNow });
					return;
				}
			}
			else if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<DateTime>("Velocity.ClusterConfigPoller", "Version properties update not available at {0}", DateTime.UtcNow);
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00019690 File Offset: 0x00017890
		private void PollForClusterPropertiesUpdate(object stateInfo)
		{
			try
			{
				this.PollForVersionPropertiesUpdate();
				this.PollForDeploymentSettingsUpdate();
			}
			catch (ConfigStoreException ex)
			{
				if (Provider.IsEnabled(TraceLevel.Warning))
				{
					EventLogWriter.WriteWarning("Velocity.ClusterConfigPoller", "PollClusterPropertiesUpdate failed with {0}.", new object[] { ex });
				}
			}
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x000196E4 File Offset: 0x000178E4
		private void PollForCacheConfigurationsUpdate(object stateInfo)
		{
			try
			{
				this.OnCacheConfigChanged(this.serviceConfig.GetCachesToReload());
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo("Velocity.ClusterConfigPoller", "Updated cache configurations at {0}", new object[] { DateTime.UtcNow });
				}
			}
			catch (ConfigStoreException ex)
			{
				if (Provider.IsEnabled(TraceLevel.Warning))
				{
					EventLogWriter.WriteWarning("Velocity.ClusterConfigPoller", "PollForClusterPropertiesUpdate failed with {0}", new object[] { ex });
				}
			}
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0001976C File Offset: 0x0001796C
		public void RegisterCallback(OnCacheConfigChanged onCacheConfigChangedCallback)
		{
			this.OnCacheConfigChanged = onCacheConfigChangedCallback;
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<DateTime>("Velocity.ClusterConfigPoller", "Callback registered for OnCacheConfigChanged in ClusterConfigPoller at {0}", DateTime.UtcNow);
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00019794 File Offset: 0x00017994
		public void Dispose()
		{
			lock (this._lockObject)
			{
				if (!this._disposed)
				{
					this.versionPropertiesTimer.Dispose();
					this.cacheConfigurationsTimer.Dispose();
					this._disposed = true;
					GC.SuppressFinalize(this);
				}
			}
		}

		// Token: 0x040003D8 RID: 984
		private const string LogSource = "Velocity.ClusterConfigPoller";

		// Token: 0x040003D9 RID: 985
		internal const string CacheConfigUpdatePeriodSetting = "cacheConfigUpdatePeriod";

		// Token: 0x040003DA RID: 986
		internal const string VersionPropertiesUpdatePeriodSetting = "versionPropsUpdatePeriod";

		// Token: 0x040003DB RID: 987
		private OnCacheConfigChanged OnCacheConfigChanged;

		// Token: 0x040003DC RID: 988
		private ServiceConfigurationManager serviceConfig;

		// Token: 0x040003DD RID: 989
		private LastPolledVersions versionSet = default(LastPolledVersions);

		// Token: 0x040003DE RID: 990
		private LastPolledVersions deploymentSettingsVersion = default(LastPolledVersions);

		// Token: 0x040003DF RID: 991
		private global::System.Threading.Timer versionPropertiesTimer;

		// Token: 0x040003E0 RID: 992
		private readonly TimeSpan VersionPropsUpdatePeriod = TimeSpan.FromMinutes(45.0);

		// Token: 0x040003E1 RID: 993
		private global::System.Threading.Timer cacheConfigurationsTimer;

		// Token: 0x040003E2 RID: 994
		private readonly TimeSpan CacheConfigUpdatePeriod = TimeSpan.FromMinutes(45.0);

		// Token: 0x040003E3 RID: 995
		private bool _disposed;

		// Token: 0x040003E4 RID: 996
		private bool _started;

		// Token: 0x040003E5 RID: 997
		private object _lockObject = new object();
	}
}
