using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using NLog.Common;
using NLog.Config;
using NLog.Internal;

namespace NLog
{
	// Token: 0x02000011 RID: 17
	public static class LogManager
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00007F4E File Offset: 0x0000614E
		public static LogFactory LogFactory
		{
			get
			{
				return LogManager.factory;
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060003FA RID: 1018 RVA: 0x00007F55 File Offset: 0x00006155
		// (remove) Token: 0x060003FB RID: 1019 RVA: 0x00007F62 File Offset: 0x00006162
		public static event EventHandler<LoggingConfigurationChangedEventArgs> ConfigurationChanged
		{
			add
			{
				LogManager.factory.ConfigurationChanged += value;
			}
			remove
			{
				LogManager.factory.ConfigurationChanged -= value;
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060003FC RID: 1020 RVA: 0x00007F6F File Offset: 0x0000616F
		// (remove) Token: 0x060003FD RID: 1021 RVA: 0x00007F7C File Offset: 0x0000617C
		public static event EventHandler<LoggingConfigurationReloadedEventArgs> ConfigurationReloaded
		{
			add
			{
				LogManager.factory.ConfigurationReloaded += value;
			}
			remove
			{
				LogManager.factory.ConfigurationReloaded -= value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00007F89 File Offset: 0x00006189
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x00007F95 File Offset: 0x00006195
		public static bool ThrowExceptions
		{
			get
			{
				return LogManager.factory.ThrowExceptions;
			}
			set
			{
				LogManager.factory.ThrowExceptions = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00007FA2 File Offset: 0x000061A2
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x00007FAE File Offset: 0x000061AE
		public static bool? ThrowConfigExceptions
		{
			get
			{
				return LogManager.factory.ThrowConfigExceptions;
			}
			set
			{
				LogManager.factory.ThrowConfigExceptions = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00007FBB File Offset: 0x000061BB
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x00007FC7 File Offset: 0x000061C7
		public static bool KeepVariablesOnReload
		{
			get
			{
				return LogManager.factory.KeepVariablesOnReload;
			}
			set
			{
				LogManager.factory.KeepVariablesOnReload = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x00007FD4 File Offset: 0x000061D4
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x00007FE0 File Offset: 0x000061E0
		public static LoggingConfiguration Configuration
		{
			get
			{
				return LogManager.factory.Configuration;
			}
			set
			{
				LogManager.factory.Configuration = value;
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00007FED File Offset: 0x000061ED
		public static LogFactory LoadConfiguration(string configFile)
		{
			LogManager.factory.LoadConfiguration(configFile);
			return LogManager.factory;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x00008000 File Offset: 0x00006200
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x0000800C File Offset: 0x0000620C
		public static LogLevel GlobalThreshold
		{
			get
			{
				return LogManager.factory.GlobalThreshold;
			}
			set
			{
				LogManager.factory.GlobalThreshold = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x00008019 File Offset: 0x00006219
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x0000803A File Offset: 0x0000623A
		[Obsolete("Use Configuration.DefaultCultureInfo property instead. Marked obsolete before v4.3.11")]
		public static LogManager.GetCultureInfo DefaultCultureInfo
		{
			get
			{
				return () => LogManager.factory.DefaultCultureInfo ?? CultureInfo.CurrentCulture;
			}
			set
			{
				throw new NotSupportedException("Setting the DefaultCultureInfo delegate is no longer supported. Use the Configuration.DefaultCultureInfo property to change the default CultureInfo.");
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00008046 File Offset: 0x00006246
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Logger GetCurrentClassLogger()
		{
			return LogManager.factory.GetLogger(StackTraceUsageUtils.GetClassFullName());
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00008057 File Offset: 0x00006257
		internal static bool IsHiddenAssembly(Assembly assembly)
		{
			return LogManager._hiddenAssemblies != null && LogManager._hiddenAssemblies.Contains(assembly);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00008070 File Offset: 0x00006270
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void AddHiddenAssembly(Assembly assembly)
		{
			object obj = LogManager.lockObject;
			lock (obj)
			{
				if (LogManager._hiddenAssemblies != null && LogManager._hiddenAssemblies.Contains(assembly))
				{
					return;
				}
				IEnumerable<Assembly> hiddenAssemblies = LogManager._hiddenAssemblies;
				LogManager._hiddenAssemblies = new HashSet<Assembly>(hiddenAssemblies ?? Enumerable.Empty<Assembly>()) { assembly };
			}
			InternalLogger.Trace<string>("Assembly '{0}' will be hidden in callsite stacktrace", (assembly != null) ? assembly.FullName : null);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000080F8 File Offset: 0x000062F8
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Logger GetCurrentClassLogger(Type loggerType)
		{
			return LogManager.factory.GetLogger(StackTraceUsageUtils.GetClassFullName(), loggerType);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000810A File Offset: 0x0000630A
		[CLSCompliant(false)]
		public static Logger CreateNullLogger()
		{
			return LogManager.factory.CreateNullLogger();
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00008116 File Offset: 0x00006316
		[CLSCompliant(false)]
		public static Logger GetLogger(string name)
		{
			return LogManager.factory.GetLogger(name);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00008123 File Offset: 0x00006323
		[CLSCompliant(false)]
		public static Logger GetLogger(string name, Type loggerType)
		{
			return LogManager.factory.GetLogger(name, loggerType);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00008131 File Offset: 0x00006331
		public static void ReconfigExistingLoggers()
		{
			LogManager.factory.ReconfigExistingLoggers();
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000813D File Offset: 0x0000633D
		public static void Flush()
		{
			LogManager.factory.Flush();
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00008149 File Offset: 0x00006349
		public static void Flush(TimeSpan timeout)
		{
			LogManager.factory.Flush(timeout);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00008156 File Offset: 0x00006356
		public static void Flush(int timeoutMilliseconds)
		{
			LogManager.factory.Flush(timeoutMilliseconds);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00008163 File Offset: 0x00006363
		public static void Flush(AsyncContinuation asyncContinuation)
		{
			LogManager.factory.Flush(asyncContinuation);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00008170 File Offset: 0x00006370
		public static void Flush(AsyncContinuation asyncContinuation, TimeSpan timeout)
		{
			LogManager.factory.Flush(asyncContinuation, timeout);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000817E File Offset: 0x0000637E
		public static void Flush(AsyncContinuation asyncContinuation, int timeoutMilliseconds)
		{
			LogManager.factory.Flush(asyncContinuation, timeoutMilliseconds);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000818C File Offset: 0x0000638C
		public static IDisposable DisableLogging()
		{
			return LogManager.factory.SuspendLogging();
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00008198 File Offset: 0x00006398
		public static void EnableLogging()
		{
			LogManager.factory.ResumeLogging();
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x000081A4 File Offset: 0x000063A4
		public static bool IsLoggingEnabled()
		{
			return LogManager.factory.IsLoggingEnabled();
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000081B0 File Offset: 0x000063B0
		public static void Shutdown()
		{
			LogManager.factory.Shutdown();
		}

		// Token: 0x0400003F RID: 63
		internal static readonly LogFactory factory = new LogFactory();

		// Token: 0x04000040 RID: 64
		private static ICollection<Assembly> _hiddenAssemblies;

		// Token: 0x04000041 RID: 65
		private static readonly object lockObject = new object();

		// Token: 0x02000216 RID: 534
		// (Invoke) Token: 0x060014C4 RID: 5316
		[Obsolete("Marked obsolete before v4.3.11")]
		public delegate CultureInfo GetCultureInfo();
	}
}
