using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading;
using JetBrains.Annotations;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.Internal.Fakeables;
using NLog.Targets;

namespace NLog
{
	// Token: 0x0200000D RID: 13
	public class LogFactory : IDisposable
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060001CD RID: 461 RVA: 0x00002E94 File Offset: 0x00001094
		// (remove) Token: 0x060001CE RID: 462 RVA: 0x00002ECC File Offset: 0x000010CC
		public event EventHandler<LoggingConfigurationChangedEventArgs> ConfigurationChanged;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060001CF RID: 463 RVA: 0x00002F04 File Offset: 0x00001104
		// (remove) Token: 0x060001D0 RID: 464 RVA: 0x00002F3C File Offset: 0x0000113C
		public event EventHandler<LoggingConfigurationReloadedEventArgs> ConfigurationReloaded;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060001D1 RID: 465 RVA: 0x00002F74 File Offset: 0x00001174
		// (remove) Token: 0x060001D2 RID: 466 RVA: 0x00002FA8 File Offset: 0x000011A8
		private static event EventHandler<EventArgs> LoggerShutdown;

		// Token: 0x060001D3 RID: 467 RVA: 0x00002FDB File Offset: 0x000011DB
		static LogFactory()
		{
			LogFactory.RegisterEvents(LogFactory.CurrentAppDomain);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00002FFA File Offset: 0x000011FA
		public LogFactory()
			: this(new LoggingConfigurationWatchableFileLoader())
		{
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00003007 File Offset: 0x00001207
		public LogFactory(LoggingConfiguration config)
			: this()
		{
			this.Configuration = config;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00003018 File Offset: 0x00001218
		internal LogFactory(ILoggingConfigurationLoader configLoader)
		{
			this._configLoader = configLoader;
			LogFactory.LoggerShutdown += this.OnStopLogging;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00003064 File Offset: 0x00001264
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x0000307F File Offset: 0x0000127F
		public static IAppDomain CurrentAppDomain
		{
			get
			{
				IAppDomain appDomain;
				if ((appDomain = LogFactory.currentAppDomain) == null)
				{
					appDomain = (LogFactory.currentAppDomain = new AppDomainWrapper(AppDomain.CurrentDomain));
				}
				return appDomain;
			}
			set
			{
				LogFactory.UnregisterEvents(LogFactory.currentAppDomain);
				LogFactory.UnregisterEvents(value);
				LogFactory.RegisterEvents(value);
				LogFactory.currentAppDomain = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000309D File Offset: 0x0000129D
		// (set) Token: 0x060001DA RID: 474 RVA: 0x000030A5 File Offset: 0x000012A5
		public bool ThrowExceptions { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001DB RID: 475 RVA: 0x000030AE File Offset: 0x000012AE
		// (set) Token: 0x060001DC RID: 476 RVA: 0x000030B6 File Offset: 0x000012B6
		public bool? ThrowConfigExceptions { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001DD RID: 477 RVA: 0x000030BF File Offset: 0x000012BF
		// (set) Token: 0x060001DE RID: 478 RVA: 0x000030C7 File Offset: 0x000012C7
		public bool KeepVariablesOnReload { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001DF RID: 479 RVA: 0x000030D0 File Offset: 0x000012D0
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00003190 File Offset: 0x00001390
		public LoggingConfiguration Configuration
		{
			get
			{
				if (this._configLoaded)
				{
					return this._config;
				}
				object syncRoot = this._syncRoot;
				LoggingConfiguration loggingConfiguration;
				lock (syncRoot)
				{
					if (this._configLoaded || this._isDisposing)
					{
						loggingConfiguration = this._config;
					}
					else
					{
						LoggingConfiguration loggingConfiguration2 = this._configLoader.Load(this);
						if (loggingConfiguration2 != null)
						{
							try
							{
								this._config = loggingConfiguration2;
								this._configLoader.Activated(this, this._config);
								this._config.Dump();
								this.ReconfigExistingLoggers();
								LogFactory.LogConfigurationInitialized();
							}
							finally
							{
								this._configLoaded = true;
							}
						}
						loggingConfiguration = this._config;
					}
				}
				return loggingConfiguration;
			}
			set
			{
				object syncRoot = this._syncRoot;
				lock (syncRoot)
				{
					LoggingConfiguration config = this._config;
					if (config != null)
					{
						InternalLogger.Info("Closing old configuration.");
						this.Flush();
						config.Close();
					}
					this._config = value;
					if (this._config == null)
					{
						this._configLoaded = false;
						this._configLoader.Activated(this, this._config);
					}
					else
					{
						try
						{
							this._configLoader.Activated(this, this._config);
							this._config.Dump();
							this.ReconfigExistingLoggers();
						}
						finally
						{
							this._configLoaded = true;
						}
					}
					this.OnConfigurationChanged(new LoggingConfigurationChangedEventArgs(value, config));
				}
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000325C File Offset: 0x0000145C
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x00003264 File Offset: 0x00001464
		public LogLevel GlobalThreshold
		{
			get
			{
				return this._globalThreshold;
			}
			set
			{
				object syncRoot = this._syncRoot;
				lock (syncRoot)
				{
					this._globalThreshold = value;
					this.ReconfigExistingLoggers();
				}
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x000032AC File Offset: 0x000014AC
		[CanBeNull]
		public CultureInfo DefaultCultureInfo
		{
			get
			{
				LoggingConfiguration configuration = this.Configuration;
				if (configuration == null)
				{
					return null;
				}
				return configuration.DefaultCultureInfo;
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000032C0 File Offset: 0x000014C0
		internal static void LogConfigurationInitialized()
		{
			InternalLogger.Info("Configuration initialized.");
			try
			{
				InternalLogger.LogAssemblyVersion(typeof(ILogger).GetAssembly());
			}
			catch (SecurityException ex)
			{
				InternalLogger.Debug(ex, "Not running in full trust");
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000330C File Offset: 0x0000150C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000331B File Offset: 0x0000151B
		public Logger CreateNullLogger()
		{
			return new NullLogger(this);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00003324 File Offset: 0x00001524
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Logger GetCurrentClassLogger()
		{
			string classFullName = StackTraceUsageUtils.GetClassFullName(new StackFrame(1, false));
			return this.GetLogger(classFullName);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00003348 File Offset: 0x00001548
		[MethodImpl(MethodImplOptions.NoInlining)]
		public T GetCurrentClassLogger<T>() where T : Logger
		{
			string classFullName = StackTraceUsageUtils.GetClassFullName(new StackFrame(1, false));
			return (T)((object)this.GetLogger(classFullName, typeof(T)));
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00003378 File Offset: 0x00001578
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Logger GetCurrentClassLogger(Type loggerType)
		{
			string classFullName = StackTraceUsageUtils.GetClassFullName(new StackFrame(1, false));
			return this.GetLoggerThreadSafe(classFullName, loggerType);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000339A File Offset: 0x0000159A
		public Logger GetLogger(string name)
		{
			return this.GetLoggerThreadSafe(name, Logger.DefaultLoggerType);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000033A8 File Offset: 0x000015A8
		public T GetLogger<T>(string name) where T : Logger
		{
			return (T)((object)this.GetLoggerThreadSafe(name, typeof(T)));
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000033C0 File Offset: 0x000015C0
		public Logger GetLogger(string name, Type loggerType)
		{
			return this.GetLoggerThreadSafe(name, loggerType);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000033CC File Offset: 0x000015CC
		public void ReconfigExistingLoggers()
		{
			object syncRoot = this._syncRoot;
			List<Logger> loggers;
			lock (syncRoot)
			{
				LoggingConfiguration config = this._config;
				if (config != null)
				{
					config.InitializeAll();
				}
				loggers = this._loggerCache.GetLoggers();
			}
			foreach (Logger logger in loggers)
			{
				logger.SetConfiguration(this.GetConfigurationForLogger(logger.Name, this._config));
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00003474 File Offset: 0x00001674
		public void Flush()
		{
			this.Flush(LogFactory.DefaultFlushTimeout);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00003484 File Offset: 0x00001684
		public void Flush(TimeSpan timeout)
		{
			try
			{
				AsyncHelpers.RunSynchronously(delegate(AsyncContinuation cb)
				{
					this.Flush(cb, timeout);
				});
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "Error with flush.");
				if (ex.MustBeRethrown())
				{
					throw;
				}
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000034E0 File Offset: 0x000016E0
		public void Flush(int timeoutMilliseconds)
		{
			this.Flush(TimeSpan.FromMilliseconds((double)timeoutMilliseconds));
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000034EF File Offset: 0x000016EF
		public void Flush(AsyncContinuation asyncContinuation)
		{
			this.Flush(asyncContinuation, TimeSpan.MaxValue);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000034FD File Offset: 0x000016FD
		public void Flush(AsyncContinuation asyncContinuation, int timeoutMilliseconds)
		{
			this.Flush(asyncContinuation, TimeSpan.FromMilliseconds((double)timeoutMilliseconds));
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00003510 File Offset: 0x00001710
		public void Flush(AsyncContinuation asyncContinuation, TimeSpan timeout)
		{
			try
			{
				InternalLogger.Trace<TimeSpan>("LogFactory.Flush({0})", timeout);
				object syncRoot = this._syncRoot;
				LoggingConfiguration config;
				lock (syncRoot)
				{
					config = this._config;
				}
				if (config != null)
				{
					config.FlushAllTargets(AsyncHelpers.WithTimeout(asyncContinuation, timeout));
				}
				else
				{
					asyncContinuation(null);
				}
			}
			catch (Exception ex)
			{
				if (this.ThrowExceptions)
				{
					throw;
				}
				InternalLogger.Error(ex, "Error with flush.");
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000359C File Offset: 0x0000179C
		[Obsolete("Use SuspendLogging() instead. Marked obsolete on NLog 4.0")]
		public IDisposable DisableLogging()
		{
			return this.SuspendLogging();
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000035A4 File Offset: 0x000017A4
		[Obsolete("Use ResumeLogging() instead. Marked obsolete on NLog 4.0")]
		public void EnableLogging()
		{
			this.ResumeLogging();
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000035AC File Offset: 0x000017AC
		public IDisposable SuspendLogging()
		{
			object syncRoot = this._syncRoot;
			lock (syncRoot)
			{
				this._logsEnabled--;
				if (this._logsEnabled == -1)
				{
					this.ReconfigExistingLoggers();
				}
			}
			return new LogFactory.LogEnabler(this);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000360C File Offset: 0x0000180C
		public void ResumeLogging()
		{
			object syncRoot = this._syncRoot;
			lock (syncRoot)
			{
				this._logsEnabled++;
				if (this._logsEnabled == 0)
				{
					this.ReconfigExistingLoggers();
				}
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00003664 File Offset: 0x00001864
		public bool IsLoggingEnabled()
		{
			return this._logsEnabled >= 0;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00003672 File Offset: 0x00001872
		protected virtual void OnConfigurationChanged(LoggingConfigurationChangedEventArgs e)
		{
			EventHandler<LoggingConfigurationChangedEventArgs> configurationChanged = this.ConfigurationChanged;
			if (configurationChanged == null)
			{
				return;
			}
			configurationChanged(this, e);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00003686 File Offset: 0x00001886
		protected virtual void OnConfigurationReloaded(LoggingConfigurationReloadedEventArgs e)
		{
			EventHandler<LoggingConfigurationReloadedEventArgs> configurationReloaded = this.ConfigurationReloaded;
			if (configurationReloaded == null)
			{
				return;
			}
			configurationReloaded(this, e);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000369A File Offset: 0x0000189A
		internal void NotifyConfigurationReloaded(LoggingConfigurationReloadedEventArgs eventArgs)
		{
			this.OnConfigurationReloaded(eventArgs);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000036A4 File Offset: 0x000018A4
		private bool GetTargetsByLevelForLogger(string name, List<LoggingRule> loggingRules, TargetWithFilterChain[] targetsByLevel, TargetWithFilterChain[] lastTargetsByLevel, bool[] suppressedLevels)
		{
			bool flag = false;
			foreach (LoggingRule loggingRule in loggingRules)
			{
				if (loggingRule.NameMatches(name))
				{
					for (int i = 0; i <= LogLevel.MaxLevel.Ordinal; i++)
					{
						if (i >= this.GlobalThreshold.Ordinal && !suppressedLevels[i] && loggingRule.IsLoggingEnabledForLevel(LogLevel.FromOrdinal(i)))
						{
							if (loggingRule.Final)
							{
								suppressedLevels[i] = true;
							}
							foreach (Target target in loggingRule.GetTargetsThreadSafe())
							{
								flag = true;
								TargetWithFilterChain targetWithFilterChain = new TargetWithFilterChain(target, loggingRule.Filters, loggingRule.DefaultFilterResult);
								if (lastTargetsByLevel[i] != null)
								{
									lastTargetsByLevel[i].NextInChain = targetWithFilterChain;
								}
								else
								{
									targetsByLevel[i] = targetWithFilterChain;
								}
								lastTargetsByLevel[i] = targetWithFilterChain;
							}
						}
					}
					if (loggingRule.ChildRules.Count != 0)
					{
						flag = this.GetTargetsByLevelForLogger(name, loggingRule.GetChildRulesThreadSafe(), targetsByLevel, lastTargetsByLevel, suppressedLevels) || flag;
					}
				}
			}
			for (int j = 0; j <= LogLevel.MaxLevel.Ordinal; j++)
			{
				TargetWithFilterChain targetWithFilterChain2 = targetsByLevel[j];
				if (targetWithFilterChain2 != null)
				{
					targetWithFilterChain2.PrecalculateStackTraceUsage();
				}
			}
			return flag;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00003808 File Offset: 0x00001A08
		internal LoggerConfiguration GetConfigurationForLogger(string name, LoggingConfiguration configuration)
		{
			TargetWithFilterChain[] array = new TargetWithFilterChain[LogLevel.MaxLevel.Ordinal + 1];
			TargetWithFilterChain[] array2 = new TargetWithFilterChain[LogLevel.MaxLevel.Ordinal + 1];
			bool[] array3 = new bool[LogLevel.MaxLevel.Ordinal + 1];
			bool flag = false;
			if (configuration != null && this.IsLoggingEnabled())
			{
				List<LoggingRule> loggingRulesThreadSafe = configuration.GetLoggingRulesThreadSafe();
				flag = this.GetTargetsByLevelForLogger(name, loggingRulesThreadSafe, array, array2, array3);
			}
			if (InternalLogger.IsDebugEnabled)
			{
				if (flag)
				{
					InternalLogger.Debug<string>("Targets for {0} by level:", name);
					for (int i = 0; i <= LogLevel.MaxLevel.Ordinal; i++)
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0} =>", new object[] { LogLevel.FromOrdinal(i) });
						for (TargetWithFilterChain targetWithFilterChain = array[i]; targetWithFilterChain != null; targetWithFilterChain = targetWithFilterChain.NextInChain)
						{
							stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " {0}", new object[] { targetWithFilterChain.Target.Name });
							if (targetWithFilterChain.FilterChain.Count > 0)
							{
								stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " ({0} filters)", new object[] { targetWithFilterChain.FilterChain.Count });
							}
						}
						InternalLogger.Debug(stringBuilder.ToString());
					}
				}
				else
				{
					InternalLogger.Debug<string>("Targets not configured for logger: {0}", name);
				}
			}
			bool flag2 = configuration != null && configuration.ExceptionLoggingOldStyle;
			return new LoggerConfiguration(array, flag2);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00003978 File Offset: 0x00001B78
		private void Close(TimeSpan flushTimeout)
		{
			if (this._isDisposing)
			{
				return;
			}
			this._isDisposing = true;
			LogFactory.LoggerShutdown -= this.OnStopLogging;
			this.ConfigurationReloaded = null;
			if (Monitor.TryEnter(this._syncRoot, 500))
			{
				try
				{
					this._configLoader.Dispose();
					LoggingConfiguration config = this._config;
					if (this._configLoaded && config != null)
					{
						this.CloseOldConfig(flushTimeout, config);
					}
				}
				finally
				{
					Monitor.Exit(this._syncRoot);
				}
			}
			this.ConfigurationChanged = null;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00003A0C File Offset: 0x00001C0C
		private void CloseOldConfig(TimeSpan flushTimeout, LoggingConfiguration oldConfig)
		{
			try
			{
				bool flag = true;
				if (flushTimeout != TimeSpan.Zero && !PlatformDetector.IsMono && !PlatformDetector.IsUnix)
				{
					ManualResetEvent flushCompleted = new ManualResetEvent(false);
					oldConfig.FlushAllTargets(delegate(Exception ex)
					{
						flushCompleted.Set();
					});
					flag = flushCompleted.WaitOne(flushTimeout);
				}
				this._config = null;
				this.ReconfigExistingLoggers();
				if (!flag)
				{
					InternalLogger.Warn("Target flush timeout. One or more targets did not complete flush operation, skipping target close.");
				}
				else
				{
					oldConfig.Close();
					this.OnConfigurationChanged(new LoggingConfigurationChangedEventArgs(null, oldConfig));
				}
			}
			catch (Exception ex)
			{
				Exception ex2;
				InternalLogger.Error(ex2, "Error with close.");
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00003AB4 File Offset: 0x00001CB4
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close(TimeSpan.Zero);
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00003AC4 File Offset: 0x00001CC4
		internal void Shutdown()
		{
			InternalLogger.Info("Shutdown() called. Logger closing...");
			if (!this._isDisposing && this._configLoaded)
			{
				object syncRoot = this._syncRoot;
				lock (syncRoot)
				{
					if (this._isDisposing || !this._configLoaded)
					{
						return;
					}
					this.Configuration = null;
					this._configLoaded = true;
					this.ReconfigExistingLoggers();
				}
			}
			InternalLogger.Info("Logger has been closed down.");
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00003B48 File Offset: 0x00001D48
		public IEnumerable<string> GetCandidateConfigFilePaths()
		{
			if (this._candidateConfigFilePaths != null)
			{
				return this._candidateConfigFilePaths.AsReadOnly();
			}
			return this._configLoader.GetDefaultCandidateConfigFilePaths();
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00003B69 File Offset: 0x00001D69
		public void SetCandidateConfigFilePaths(IEnumerable<string> filePaths)
		{
			this._candidateConfigFilePaths = new List<string>();
			if (filePaths != null)
			{
				this._candidateConfigFilePaths.AddRange(filePaths);
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00003B85 File Offset: 0x00001D85
		public void ResetCandidateConfigFilePath()
		{
			this._candidateConfigFilePaths = null;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00003B90 File Offset: 0x00001D90
		private Logger GetLoggerThreadSafe(string name, Type loggerType)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name", "Name of logger cannot be null");
			}
			LogFactory.LoggerCacheKey loggerCacheKey = new LogFactory.LoggerCacheKey(name, loggerType ?? typeof(Logger));
			object syncRoot = this._syncRoot;
			Logger logger2;
			lock (syncRoot)
			{
				Logger logger = this._loggerCache.Retrieve(loggerCacheKey);
				if (logger != null)
				{
					logger2 = logger;
				}
				else
				{
					Logger logger3 = this.CreateNewLogger(loggerCacheKey.ConcreteType);
					if (logger3 == null)
					{
						loggerCacheKey = new LogFactory.LoggerCacheKey(loggerCacheKey.Name, typeof(Logger));
						logger3 = new Logger();
					}
					logger3.Initialize(name, this.GetConfigurationForLogger(name, this.Configuration), this);
					this._loggerCache.InsertOrUpdate(loggerCacheKey, logger3);
					logger2 = logger3;
				}
			}
			return logger2;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00003C64 File Offset: 0x00001E64
		internal Logger CreateNewLogger(Type loggerType)
		{
			if (loggerType != null && loggerType != typeof(Logger))
			{
				try
				{
					return this.CreateCustomLoggerType(loggerType);
				}
				catch (Exception ex)
				{
					InternalLogger.Error(ex, "GetLogger / GetCurrentClassLogger. Cannot create instance of type '{0}'. It should have an default contructor.", new object[] { loggerType });
					if (ex.MustBeRethrown())
					{
						throw;
					}
					return null;
				}
			}
			return new Logger();
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00003CD4 File Offset: 0x00001ED4
		private Logger CreateCustomLoggerType(Type customLoggerType)
		{
			if (customLoggerType.IsStaticClass())
			{
				string text = string.Format("GetLogger / GetCurrentClassLogger is '{0}' as loggerType is static class and should instead inherit from Logger", customLoggerType);
				InternalLogger.Error(text);
				if (this.ThrowExceptions)
				{
					throw new NLogRuntimeException(text);
				}
				return null;
			}
			else
			{
				Logger logger = FactoryHelper.CreateInstance(customLoggerType) as Logger;
				if (logger != null)
				{
					return logger;
				}
				string text2 = string.Format("GetLogger / GetCurrentClassLogger got '{0}' as loggerType doesn't inherit from Logger", customLoggerType);
				InternalLogger.Error(text2);
				if (this.ThrowExceptions)
				{
					throw new NLogRuntimeException(text2);
				}
				return null;
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00003D40 File Offset: 0x00001F40
		public LogFactory LoadConfiguration(string configFile)
		{
			LoggingConfigurationFileLoader loggingConfigurationFileLoader;
			if ((loggingConfigurationFileLoader = this._configLoader as LoggingConfigurationFileLoader) != null)
			{
				this.Configuration = loggingConfigurationFileLoader.Load(this, configFile);
			}
			return this;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00003D6B File Offset: 0x00001F6B
		internal void ResetLoggerCache()
		{
			this._loggerCache.Reset();
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00003D78 File Offset: 0x00001F78
		private static void RegisterEvents(IAppDomain appDomain)
		{
			if (appDomain == null)
			{
				return;
			}
			try
			{
				appDomain.ProcessExit += LogFactory.OnLoggerShutdown;
				appDomain.DomainUnload += LogFactory.OnLoggerShutdown;
			}
			catch (Exception ex)
			{
				InternalLogger.Warn(ex, "Error setting up termination events.");
				if (ex.MustBeRethrown())
				{
					throw;
				}
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00003DD8 File Offset: 0x00001FD8
		private static void UnregisterEvents(IAppDomain appDomain)
		{
			if (appDomain == null)
			{
				return;
			}
			appDomain.DomainUnload -= LogFactory.OnLoggerShutdown;
			appDomain.ProcessExit -= LogFactory.OnLoggerShutdown;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00003E04 File Offset: 0x00002004
		private static void OnLoggerShutdown(object sender, EventArgs args)
		{
			try
			{
				EventHandler<EventArgs> loggerShutdown = LogFactory.LoggerShutdown;
				if (loggerShutdown != null)
				{
					loggerShutdown(sender, args);
				}
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
				InternalLogger.Error(ex, "LogFactory failed to shut down properly.");
			}
			finally
			{
				LogFactory.LoggerShutdown = null;
				if (LogFactory.currentAppDomain != null)
				{
					LogFactory.CurrentAppDomain = null;
				}
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00003E6C File Offset: 0x0000206C
		private void OnStopLogging(object sender, EventArgs args)
		{
			try
			{
				InternalLogger.Info("AppDomain Shutting down. Logger closing...");
				this.Close(TimeSpan.FromMilliseconds(1500.0));
				InternalLogger.Info("Logger has been shut down.");
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
				InternalLogger.Error(ex, "Logger failed to shut down properly.");
			}
		}

		// Token: 0x04000015 RID: 21
		private static readonly TimeSpan DefaultFlushTimeout = TimeSpan.FromSeconds(15.0);

		// Token: 0x04000016 RID: 22
		private static IAppDomain currentAppDomain;

		// Token: 0x04000017 RID: 23
		internal readonly object _syncRoot = new object();

		// Token: 0x04000018 RID: 24
		internal LoggingConfiguration _config;

		// Token: 0x04000019 RID: 25
		private LogLevel _globalThreshold = LogLevel.MinLevel;

		// Token: 0x0400001A RID: 26
		private bool _configLoaded;

		// Token: 0x0400001B RID: 27
		private int _logsEnabled;

		// Token: 0x0400001C RID: 28
		private readonly LogFactory.LoggerCache _loggerCache = new LogFactory.LoggerCache();

		// Token: 0x0400001D RID: 29
		private List<string> _candidateConfigFilePaths;

		// Token: 0x0400001E RID: 30
		private readonly ILoggingConfigurationLoader _configLoader;

		// Token: 0x04000025 RID: 37
		private bool _isDisposing;

		// Token: 0x0200020A RID: 522
		private struct LoggerCacheKey : IEquatable<LogFactory.LoggerCacheKey>
		{
			// Token: 0x060014A5 RID: 5285 RVA: 0x00036CEC File Offset: 0x00034EEC
			public LoggerCacheKey(string name, Type concreteType)
			{
				this.Name = name;
				this.ConcreteType = concreteType;
			}

			// Token: 0x060014A6 RID: 5286 RVA: 0x00036CFC File Offset: 0x00034EFC
			public override int GetHashCode()
			{
				return this.ConcreteType.GetHashCode() ^ this.Name.GetHashCode();
			}

			// Token: 0x060014A7 RID: 5287 RVA: 0x00036D18 File Offset: 0x00034F18
			public override bool Equals(object obj)
			{
				if (obj is LogFactory.LoggerCacheKey)
				{
					LogFactory.LoggerCacheKey loggerCacheKey = (LogFactory.LoggerCacheKey)obj;
					return this.Equals(loggerCacheKey);
				}
				return false;
			}

			// Token: 0x060014A8 RID: 5288 RVA: 0x00036D3F File Offset: 0x00034F3F
			public bool Equals(LogFactory.LoggerCacheKey other)
			{
				return this.ConcreteType == other.ConcreteType && string.Equals(other.Name, this.Name, StringComparison.Ordinal);
			}

			// Token: 0x040005A3 RID: 1443
			public readonly string Name;

			// Token: 0x040005A4 RID: 1444
			public readonly Type ConcreteType;
		}

		// Token: 0x0200020B RID: 523
		private class LoggerCache
		{
			// Token: 0x060014A9 RID: 5289 RVA: 0x00036D68 File Offset: 0x00034F68
			public void InsertOrUpdate(LogFactory.LoggerCacheKey cacheKey, Logger logger)
			{
				this._loggerCache[cacheKey] = new WeakReference(logger);
			}

			// Token: 0x060014AA RID: 5290 RVA: 0x00036D7C File Offset: 0x00034F7C
			public Logger Retrieve(LogFactory.LoggerCacheKey cacheKey)
			{
				WeakReference weakReference;
				if (this._loggerCache.TryGetValue(cacheKey, out weakReference))
				{
					return weakReference.Target as Logger;
				}
				return null;
			}

			// Token: 0x060014AB RID: 5291 RVA: 0x00036DA8 File Offset: 0x00034FA8
			public List<Logger> GetLoggers()
			{
				List<Logger> list = new List<Logger>(this._loggerCache.Count);
				using (Dictionary<LogFactory.LoggerCacheKey, WeakReference>.ValueCollection.Enumerator enumerator = this._loggerCache.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Logger logger;
						if ((logger = enumerator.Current.Target as Logger) != null)
						{
							list.Add(logger);
						}
					}
				}
				return list;
			}

			// Token: 0x060014AC RID: 5292 RVA: 0x00036E20 File Offset: 0x00035020
			public void Reset()
			{
				this._loggerCache.Clear();
			}

			// Token: 0x040005A5 RID: 1445
			private readonly Dictionary<LogFactory.LoggerCacheKey, WeakReference> _loggerCache = new Dictionary<LogFactory.LoggerCacheKey, WeakReference>();
		}

		// Token: 0x0200020C RID: 524
		private class LogEnabler : IDisposable
		{
			// Token: 0x060014AE RID: 5294 RVA: 0x00036E40 File Offset: 0x00035040
			public LogEnabler(LogFactory factory)
			{
				this._factory = factory;
			}

			// Token: 0x060014AF RID: 5295 RVA: 0x00036E4F File Offset: 0x0003504F
			void IDisposable.Dispose()
			{
				this._factory.ResumeLogging();
			}

			// Token: 0x040005A6 RID: 1446
			private readonly LogFactory _factory;
		}
	}
}
