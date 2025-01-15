using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.InfoNav;
using Microsoft.PowerBI.Telemetry;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.SqlServer.SqlDumper;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000016 RID: 22
	internal sealed class Dumper
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002877 File Offset: 0x00000A77
		private Dumper()
		{
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0000288A File Offset: 0x00000A8A
		public static Dumper Current
		{
			get
			{
				return Dumper.m_Dumper;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002894 File Offset: 0x00000A94
		public void Init()
		{
			if (this.m_isInitialized)
			{
				return;
			}
			if (!DumpClient.IsInitialized())
			{
				DumpClient.Initialize();
				object lockObj = Dumper.m_lockObj;
				lock (lockObj)
				{
					this.InitializeSettings();
				}
			}
			TelemetryService.Instance.TraceVerbose("Setting unhandled exception handler");
			AppDomain.CurrentDomain.UnhandledException += this.DefaultExceptionHandler;
			this.m_isInitialized = true;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002914 File Offset: 0x00000B14
		public void DumpHere(Exception optionalException)
		{
			this.DumpHere(optionalException, null, false);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000291F File Offset: 0x00000B1F
		public static string ServiceName
		{
			get
			{
				return "Power BI";
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002926 File Offset: 0x00000B26
		public int SqlDumperFlags
		{
			get
			{
				return Dumper.SqlDumperFlagRetriver;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000292D File Offset: 0x00000B2D
		public int UnsetSqlDumperFlags
		{
			get
			{
				return this.m_unsetSqlDumperFlags;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002935 File Offset: 0x00000B35
		public int SqlMiniDumpFlags
		{
			get
			{
				return this.m_sqlMiniDumpFlags;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000293D File Offset: 0x00000B3D
		public int UnsetSqlMiniDumpFlags
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002940 File Offset: 0x00000B40
		public string Location
		{
			get
			{
				return this.m_dumpLocation;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002948 File Offset: 0x00000B48
		public static StringCollection TypesToNotDump
		{
			get
			{
				return Dumper._DefaultWatsonDumpExcludeIfContainsExceptions;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002950 File Offset: 0x00000B50
		public static DumperFlags SqlDumperFlagRetriver
		{
			get
			{
				DumperFlags dumperFlags = 0;
				try
				{
					dumperFlags = (DumperFlags)Enum.Parse(typeof(DumperFlags), 1064.ToString(CultureInfo.InvariantCulture));
				}
				catch (ArgumentException)
				{
					TelemetryService.Instance.TraceError("Invalid value for sqldumper flags: " + 1064.ToString(CultureInfo.InvariantCulture));
				}
				catch (OverflowException)
				{
					TelemetryService.Instance.TraceError(StringUtil.FormatInvariant("Overflow parsing {0} to DumperFlags", 1064.ToString(CultureInfo.InvariantCulture)));
				}
				return dumperFlags;
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000029F8 File Offset: 0x00000BF8
		private void DefaultExceptionHandler(object sender, UnhandledExceptionEventArgs args)
		{
			Exception ex = (Exception)args.ExceptionObject;
			this.DumpHere(ex, null, true);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002A1C File Offset: 0x00000C1C
		internal void DumpHere(Exception optionalException, string assertionMessage, bool unhandledException)
		{
			Interlocked.Increment(ref this.m_recursionGuard);
			GCHandle[] array = null;
			using (Dumper sqlDumper = DumpClient.GetDumper())
			{
				try
				{
					if (this.m_recursionGuard <= 1)
					{
						this.m_unsetSqlDumperFlags = 0;
						sqlDumper.SetDirectory(this.m_dumpLocation);
						sqlDumper.SetMiniDumpFlags(this.m_sqlMiniDumpFlags, 0);
						sqlDumper.SetFlags(Dumper.SqlDumperFlagRetriver, this.m_unsetSqlDumperFlags);
						sqlDumper.SetInstanceName(this.instanceName);
						sqlDumper.SetServiceName(Dumper.ServiceName);
						RevertImpersonationContext.RunFromRestrictedCasContext(delegate
						{
							if (unhandledException && optionalException != null)
							{
								if (optionalException is AppDomainUnloadedException || optionalException is OutOfMemoryException || optionalException is ThreadAbortException)
								{
									TelemetryService.Instance.TraceVerbose(StringUtil.FormatInvariant("Unhandled exception in Appdomain {0}: " + optionalException.ToString(), AppDomain.CurrentDomain.FriendlyName));
								}
								else
								{
									TelemetryService.Instance.TraceError(StringUtil.FormatInvariant("Unhandled exception in Appdomain {0}: " + optionalException.ToString(), AppDomain.CurrentDomain.FriendlyName));
								}
							}
							if (!Dumper.ShouldDump(optionalException))
							{
								return;
							}
							this.NotifyReportServerDumpEvent(optionalException, assertionMessage, unhandledException);
							int num;
							if (this.ExistsInCache(optionalException, out num))
							{
								TelemetryService.Instance.TraceInfo(StringUtil.FormatInvariant("Skipped creating a dump file for the error {0}, because a dump with the identical stack trace (with signature {1}) was already created.", (optionalException != null) ? optionalException.GetType().Name : string.Empty, (uint)num));
								return;
							}
							StringBuilder stringBuilder = new StringBuilder(256);
							string text = null;
							this.NotifyReportServerUniqueDumpEvent(optionalException, assertionMessage, unhandledException);
							if (optionalException != null)
							{
								if (optionalException.StackTrace != null)
								{
									text = optionalException.StackTrace;
								}
								else if (optionalException.InnerException != null)
								{
									text = optionalException.InnerException.StackTrace;
								}
								stringBuilder.AppendFormat("Exception message: {0}\r\n", optionalException.Message);
							}
							if (assertionMessage != null)
							{
								stringBuilder.AppendFormat("Assertion failed: {0}\r\n", assertionMessage);
							}
							if (0 < stringBuilder.Length)
							{
								sqlDumper.SetErrorText(stringBuilder.ToString());
							}
							if (text != null)
							{
								sqlDumper.SetErrorText(text);
							}
							sqlDumper.SetBucket(16, 0);
							try
							{
								sqlDumper.Dump();
								string dumpResultText = sqlDumper.GetDumpResultText();
								TelemetryService.Instance.TraceInfo(StringUtil.FormatInvariant("Dump result: {0}.", dumpResultText));
							}
							catch (Exception ex)
							{
								TelemetryService.Instance.TraceError(StringUtil.FormatInvariant("Failed to generate a dump. Error: {0}.", ex.ToString()));
							}
						});
					}
				}
				finally
				{
					if (array != null)
					{
						for (int i = 0; i < array.Length; i++)
						{
							if (array[i].IsAllocated)
							{
								array[i].Free();
							}
						}
					}
					Interlocked.Decrement(ref this.m_recursionGuard);
				}
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002B50 File Offset: 0x00000D50
		private void NotifyReportServerDumpEvent(Exception exception, string assertionMessage, bool unhandledException)
		{
			TelemetryService.Instance.TraceError(StringUtil.FormatInvariant("Report server dump occured. Exception: {0}, Message: {1}, Unhandled Exception: {2}", exception, assertionMessage, unhandledException));
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002B6E File Offset: 0x00000D6E
		private void NotifyReportServerUniqueDumpEvent(Exception exception, string assertionMessage, bool unhandledException)
		{
			TelemetryService.Instance.TraceError(StringUtil.FormatInvariant("Report server unique dump occured. Exception: {0}, Message: {1}, Unhandled Exception: {2}", exception, assertionMessage, unhandledException));
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002B8C File Offset: 0x00000D8C
		private bool ExistsInCache(Exception optionalException, out int hash)
		{
			hash = 0;
			if (optionalException == null)
			{
				return false;
			}
			bool flag = false;
			byte[] exceptionFrames = StackTraceUtil.GetExceptionFrames(optionalException, out hash);
			if (exceptionFrames != null && hash != 0)
			{
				byte[] array = null;
				object lockObj = Dumper.m_lockObj;
				lock (lockObj)
				{
					if (Dumper.m_dumpHashCache.ContainsKey(hash))
					{
						array = Dumper.m_dumpHashCache[hash];
					}
					else
					{
						Dumper.m_dumpHashCache.Add(hash, exceptionFrames);
					}
				}
				flag = array != null && exceptionFrames.SequenceEqual(array);
			}
			return flag;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002C1C File Offset: 0x00000E1C
		private static bool ShouldDump(Exception optionalException)
		{
			return (Dumper.SqlDumperFlagRetriver & 2) != 2 && Dumper.IsDumpException(optionalException);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002C30 File Offset: 0x00000E30
		public static bool IsDumpException(Exception optionalException)
		{
			bool flag = true;
			if (flag && Dumper.ContainsExceptedException(optionalException))
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002C50 File Offset: 0x00000E50
		private static bool ContainsExceptedException(Exception root)
		{
			if (root == null)
			{
				return false;
			}
			Exception ex = root;
			while (!Dumper.TypesToNotDump.Contains(ex.GetType().ToString()))
			{
				if (ex.InnerException == null)
				{
					return false;
				}
				ex = ex.InnerException;
			}
			return true;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002C8F File Offset: 0x00000E8F
		private void InitializeSettings()
		{
			RevertImpersonationContext.Run(delegate
			{
				this.InitializeDumperFlags();
				this.InitializeDumpLocation();
			});
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002CA4 File Offset: 0x00000EA4
		private void InitializeDumpLocation()
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetTempPath());
			this.m_dumpLocation = directoryInfo.FullName;
			if (!Directory.Exists(this.m_dumpLocation))
			{
				TelemetryService.Instance.TraceError("Minidump location not found at: " + this.m_dumpLocation);
				return;
			}
			TelemetryService.Instance.TraceInfo("Minidump location: " + this.m_dumpLocation);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002D0C File Offset: 0x00000F0C
		private void InitializeDumperFlags()
		{
			this.m_sqlMiniDumpFlags = 289;
			TelemetryService.Instance.TraceInfo(StringUtil.FormatInvariant("SQLDump flags: {0}", Dumper.SqlDumperFlagRetriver));
			TelemetryService.Instance.TraceInfo(StringUtil.FormatInvariant("MiniDump flags: {0}", this.m_sqlMiniDumpFlags));
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002D64 File Offset: 0x00000F64
		private void ParseCommaDelimitedCollection(string configValue, StringCollection collection)
		{
			foreach (string text in configValue.Split(new char[] { ',' }))
			{
				collection.Add(text);
			}
		}

		// Token: 0x0400008A RID: 138
		private const int _DefaultWatsonFlags = 1064;

		// Token: 0x0400008B RID: 139
		private static readonly StringCollection _DefaultWatsonDumpExcludeIfContainsExceptions = new StringCollection { "System.Threading.ThreadAbortException", "System.Web.UI.ViewStateException", "System.OutOfMemoryException", "System.Web.HttpException" };

		// Token: 0x0400008C RID: 140
		private readonly string instanceName = "Power BI";

		// Token: 0x0400008D RID: 141
		private static readonly Dumper m_Dumper = new Dumper();

		// Token: 0x0400008E RID: 142
		private static object m_lockObj = new object();

		// Token: 0x0400008F RID: 143
		private static Dictionary<int, byte[]> m_dumpHashCache = new Dictionary<int, byte[]>();

		// Token: 0x04000090 RID: 144
		private bool m_isInitialized;

		// Token: 0x04000091 RID: 145
		private string m_dumpLocation;

		// Token: 0x04000092 RID: 146
		private int m_recursionGuard;

		// Token: 0x04000093 RID: 147
		private MiniDumpFlags m_sqlMiniDumpFlags;

		// Token: 0x04000094 RID: 148
		private DumperFlags m_unsetSqlDumperFlags;

		// Token: 0x04000095 RID: 149
		private const int HashFrames = 16;
	}
}
