using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using NLog.Common;
using NLog.Internal;

namespace NLog.Config
{
	// Token: 0x02000193 RID: 403
	internal class LoggingConfigurationWatchableFileLoader : LoggingConfigurationFileLoader
	{
		// Token: 0x06001274 RID: 4724 RVA: 0x00031FB0 File Offset: 0x000301B0
		public override LoggingConfiguration Load(LogFactory logFactory)
		{
			LoggingConfiguration loggingConfiguration = this.TryLoadFromAppConfig();
			if (loggingConfiguration != null)
			{
				return loggingConfiguration;
			}
			return base.Load(logFactory);
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x00031FD0 File Offset: 0x000301D0
		public override void Activated(LogFactory logFactory, LoggingConfiguration config)
		{
			this._logFactory = logFactory;
			this.TryUnwatchConfigFile();
			if (config != null)
			{
				this.TryWachtingConfigFile(config);
			}
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00031FEC File Offset: 0x000301EC
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._isDisposing = true;
				if (this._watcher != null)
				{
					this._watcher.FileChanged -= new FileSystemEventHandler(this.ConfigFileChanged);
					this._watcher.StopWatching();
				}
				Timer reloadTimer = this._reloadTimer;
				if (reloadTimer != null)
				{
					this._reloadTimer = null;
					reloadTimer.WaitForDispose(TimeSpan.Zero);
				}
				MultiFileWatcher watcher = this._watcher;
				if (watcher != null)
				{
					watcher.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x00032064 File Offset: 0x00030264
		private LoggingConfiguration TryLoadFromAppConfig()
		{
			LoggingConfiguration loggingConfiguration;
			try
			{
				loggingConfiguration = XmlLoggingConfiguration.AppConfig;
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrown())
				{
					throw;
				}
				loggingConfiguration = null;
			}
			return loggingConfiguration;
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x00032098 File Offset: 0x00030298
		internal void ReloadConfigOnTimer(object state)
		{
			if (this._reloadTimer == null && this._isDisposing)
			{
				return;
			}
			LoggingConfiguration loggingConfiguration = (LoggingConfiguration)state;
			InternalLogger.Info("Reloading configuration...");
			object syncRoot = this._logFactory._syncRoot;
			lock (syncRoot)
			{
				LoggingConfiguration loggingConfiguration2;
				try
				{
					if (this._isDisposing)
					{
						return;
					}
					Timer reloadTimer = this._reloadTimer;
					if (reloadTimer != null)
					{
						this._reloadTimer = null;
						reloadTimer.WaitForDispose(TimeSpan.Zero);
					}
					if (this._logFactory._config != loggingConfiguration)
					{
						InternalLogger.Warn("NLog Config changed in between. Not reloading.");
						return;
					}
					loggingConfiguration2 = loggingConfiguration.ReloadNewConfig();
					if (loggingConfiguration2 == null)
					{
						return;
					}
				}
				catch (Exception ex)
				{
					if (ex.MustBeRethrownImmediately())
					{
						throw;
					}
					InternalLogger.Warn(ex, "NLog configuration failed to reload");
					LogFactory logFactory = this._logFactory;
					if (logFactory != null)
					{
						logFactory.NotifyConfigurationReloaded(new LoggingConfigurationReloadedEventArgs(false, ex));
					}
					return;
				}
				try
				{
					this.TryUnwatchConfigFile();
					this._logFactory.Configuration = loggingConfiguration2;
					LogFactory logFactory2 = this._logFactory;
					if (logFactory2 != null)
					{
						logFactory2.NotifyConfigurationReloaded(new LoggingConfigurationReloadedEventArgs(true));
					}
				}
				catch (Exception ex2)
				{
					if (ex2.MustBeRethrownImmediately())
					{
						throw;
					}
					InternalLogger.Warn(ex2, "NLog configuration reloaded, failed to be assigned");
					this._watcher.Watch(loggingConfiguration.FileNamesToWatch);
					LogFactory logFactory3 = this._logFactory;
					if (logFactory3 != null)
					{
						logFactory3.NotifyConfigurationReloaded(new LoggingConfigurationReloadedEventArgs(false, ex2));
					}
				}
			}
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x00032234 File Offset: 0x00030434
		private void ConfigFileChanged(object sender, EventArgs args)
		{
			InternalLogger.Info<int>("Configuration file change detected! Reloading in {0}ms...", 1000);
			object syncRoot = this._logFactory._syncRoot;
			lock (syncRoot)
			{
				if (!this._isDisposing)
				{
					if (this._reloadTimer == null)
					{
						LoggingConfiguration config = this._logFactory._config;
						if (config != null)
						{
							this._reloadTimer = new Timer(new TimerCallback(this.ReloadConfigOnTimer), config, 1000, -1);
						}
					}
					else
					{
						this._reloadTimer.Change(1000, -1);
					}
				}
			}
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x000322D8 File Offset: 0x000304D8
		private void TryWachtingConfigFile(LoggingConfiguration config)
		{
			try
			{
				IEnumerable<string> fileNamesToWatch = config.FileNamesToWatch;
				List<string> list = ((fileNamesToWatch != null) ? fileNamesToWatch.ToList<string>() : null);
				if (list != null && list.Count > 0)
				{
					if (this._watcher == null)
					{
						this._watcher = new MultiFileWatcher();
						this._watcher.FileChanged += new FileSystemEventHandler(this.ConfigFileChanged);
					}
					this._watcher.Watch(list);
				}
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrown())
				{
					throw;
				}
				InternalLogger.Warn(ex, "Cannot start file watching: {0}", new object[] { string.Join(",", this._logFactory._config.FileNamesToWatch.ToArray<string>()) });
			}
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x0003238C File Offset: 0x0003058C
		private void TryUnwatchConfigFile()
		{
			try
			{
				MultiFileWatcher watcher = this._watcher;
				if (watcher != null)
				{
					watcher.StopWatching();
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "Cannot stop file watching.");
				if (ex.MustBeRethrown())
				{
					throw;
				}
			}
		}

		// Token: 0x040004F1 RID: 1265
		private const int ReconfigAfterFileChangedTimeout = 1000;

		// Token: 0x040004F2 RID: 1266
		private Timer _reloadTimer;

		// Token: 0x040004F3 RID: 1267
		private MultiFileWatcher _watcher;

		// Token: 0x040004F4 RID: 1268
		private bool _isDisposing;

		// Token: 0x040004F5 RID: 1269
		private LogFactory _logFactory;
	}
}
