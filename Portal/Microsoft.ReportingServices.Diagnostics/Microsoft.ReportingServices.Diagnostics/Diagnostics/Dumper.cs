using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.SqlServer.SqlDumper;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000045 RID: 69
	internal sealed class Dumper
	{
		// Token: 0x0600020A RID: 522 RVA: 0x00002E32 File Offset: 0x00001032
		private Dumper()
		{
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000A60D File Offset: 0x0000880D
		public static Dumper Current
		{
			get
			{
				return Dumper.m_Dumper;
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000A614 File Offset: 0x00008814
		public void Init()
		{
			if (this.m_isInitialized)
			{
				return;
			}
			if (Globals.CurrentApplication == RunningApplication.Unknown)
			{
				throw new InternalCatalogException("RS dumper class being used without being properly initialized.");
			}
			if (Globals.CurrentApplication != RunningApplication.WebService && Globals.CurrentApplication != RunningApplication.WindowsService && Globals.CurrentApplication != RunningApplication.ExcelAddin && Globals.CurrentApplication != RunningApplication.ReportServerWebApp && Globals.CurrentApplication != RunningApplication.PowerBIWebApp && Globals.CurrentApplication != RunningApplication.OfficeWebApp)
			{
				return;
			}
			if (!DumpClient.IsInitialized())
			{
				DumpClient.Initialize();
			}
			object lockObj = Dumper.m_lockObj;
			lock (lockObj)
			{
				this.InitializeSettings();
			}
			RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Setting unhandled exception handler");
			AppDomain.CurrentDomain.UnhandledException += this.DefaultExceptionHandler;
			RSException.ExceptionCreated += this.RSExceptionHandler;
			this.m_isInitialized = true;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000A6EC File Offset: 0x000088EC
		public void DumpHere(Exception optionalException)
		{
			this.DumpHere(optionalException, null, false);
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000A6F7 File Offset: 0x000088F7
		public static string ServiceName
		{
			get
			{
				return "Reporting Services";
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000A6FE File Offset: 0x000088FE
		public int SqlDumperFlags
		{
			get
			{
				return (int)Dumper.SqlDumperFlagRetriver;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000A705 File Offset: 0x00008905
		public int UnsetSqlDumperFlags
		{
			get
			{
				return (int)this.m_unsetSqlDumperFlags;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000A70D File Offset: 0x0000890D
		public int SqlMiniDumpFlags
		{
			get
			{
				return (int)this.m_sqlMiniDumpFlags;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00004F49 File Offset: 0x00003149
		public int UnsetSqlMiniDumpFlags
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000A715 File Offset: 0x00008915
		public string Location
		{
			get
			{
				return this.m_dumpLocation;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000A71D File Offset: 0x0000891D
		public static StringCollection TypesToDump
		{
			get
			{
				return Globals.Configuration.WatsonDumpOnExceptions;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000A729 File Offset: 0x00008929
		public static StringCollection TypesToNotDump
		{
			get
			{
				return Globals.Configuration.WatsonDumpExcludeIfContainsExceptions;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000A738 File Offset: 0x00008938
		public static DumperFlags SqlDumperFlagRetriver
		{
			get
			{
				DumperFlags dumperFlags = DumperFlags.Default;
				try
				{
					dumperFlags = (DumperFlags)Enum.Parse(typeof(DumperFlags), Globals.Configuration.WatsonFlags.ToString(CultureInfo.InvariantCulture));
				}
				catch (ArgumentException)
				{
					RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Invalid value for sqldumper flags: " + Globals.Configuration.WatsonFlags.ToString(CultureInfo.InvariantCulture));
				}
				catch (OverflowException)
				{
					RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Overflow parsing {0} to DumperFlags", new object[] { Globals.Configuration.WatsonFlags.ToString(CultureInfo.InvariantCulture) });
				}
				return dumperFlags;
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000A7F4 File Offset: 0x000089F4
		private void DefaultExceptionHandler(object sender, UnhandledExceptionEventArgs args)
		{
			Exception ex = (Exception)args.ExceptionObject;
			this.DumpHere(ex, null, true);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000A816 File Offset: 0x00008A16
		private void RSExceptionHandler(object sender, RSExceptionCreatedEventArgs e)
		{
			this.DumpHere(e.Exception);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000A824 File Offset: 0x00008A24
		internal void DumpHere(Exception optionalException, string assertionMessage, bool unhandledException)
		{
			Dumper.<>c__DisplayClass26_0 CS$<>8__locals1 = new Dumper.<>c__DisplayClass26_0();
			CS$<>8__locals1.unhandledException = unhandledException;
			CS$<>8__locals1.optionalException = optionalException;
			CS$<>8__locals1.assertionMessage = assertionMessage;
			CS$<>8__locals1.<>4__this = this;
			Interlocked.Increment(ref this.m_recursionGuard);
			CS$<>8__locals1.gcHandles = null;
			using (Dumper sqlDumper = DumpClient.GetDumper())
			{
				try
				{
					if (this.m_recursionGuard <= 1)
					{
						this.m_unsetSqlDumperFlags = DumperFlags.Default;
						sqlDumper.SetDirectory(this.m_dumpLocation);
						sqlDumper.SetMiniDumpFlags(this.m_sqlMiniDumpFlags, MiniDumpFlags.Normal);
						sqlDumper.SetFlags(Dumper.SqlDumperFlagRetriver, this.m_unsetSqlDumperFlags);
						if (!string.IsNullOrEmpty(RSTrace.CatalogTrace.TraceFileName))
						{
							sqlDumper.SetLogFile(RSTrace.CatalogTrace.TraceFileName, 524288);
						}
						sqlDumper.SetInstanceName(Globals.Configuration.InstanceName);
						sqlDumper.SetServiceName(Dumper.ServiceName);
						RevertImpersonationContext.RunFromRestrictedCasContext(delegate
						{
							if (CS$<>8__locals1.unhandledException && CS$<>8__locals1.optionalException != null && RSTrace.CatalogTrace.TraceError && RSTrace.CatalogTrace.TraceError)
							{
								if (CS$<>8__locals1.optionalException is AppDomainUnloadedException || CS$<>8__locals1.optionalException is OutOfMemoryException || CS$<>8__locals1.optionalException is ThreadAbortException)
								{
									if (RSTrace.CatalogTrace.TraceVerbose)
									{
										RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Unhandled exception in Appdomain {0}: " + CS$<>8__locals1.optionalException.ToString(), new object[] { AppDomain.CurrentDomain.FriendlyName });
									}
								}
								else
								{
									RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Unhandled exception in Appdomain {0}: " + CS$<>8__locals1.optionalException.ToString(), new object[] { AppDomain.CurrentDomain.FriendlyName });
								}
							}
							if (!Dumper.ShouldDump(CS$<>8__locals1.optionalException))
							{
								return;
							}
							RSEventProvider.Current.NotifyReportServerDumpEvent(CS$<>8__locals1.optionalException, CS$<>8__locals1.assertionMessage, CS$<>8__locals1.unhandledException);
							int num;
							if (CS$<>8__locals1.<>4__this.ExistsInCache(CS$<>8__locals1.optionalException, out num))
							{
								RSTrace.CatalogTrace.Trace(TraceLevel.Info, "Skipped creating a dump file for the error {0}, because a dump with the identical stack trace (with signature {1}) was already created.", new object[]
								{
									(CS$<>8__locals1.optionalException != null) ? CS$<>8__locals1.optionalException.GetType().Name : string.Empty,
									(uint)num
								});
								return;
							}
							StringBuilder stringBuilder = new StringBuilder(256);
							string text = null;
							RSEventProvider.Current.NotifyReportServerUniqueDumpEvent(CS$<>8__locals1.optionalException, CS$<>8__locals1.assertionMessage, CS$<>8__locals1.unhandledException);
							if (CS$<>8__locals1.optionalException != null)
							{
								if (CS$<>8__locals1.optionalException.StackTrace != null)
								{
									text = CS$<>8__locals1.optionalException.StackTrace;
								}
								else if (CS$<>8__locals1.optionalException.InnerException != null)
								{
									text = CS$<>8__locals1.optionalException.InnerException.StackTrace;
								}
								RSException ex = CS$<>8__locals1.optionalException as RSException;
								if (ex != null)
								{
									stringBuilder.AppendFormat("Exception message: {0}, additional message: {1}\r\n", ex.Message, ex.AdditionalTraceMessage);
									if (ex.ExceptionData != null)
									{
										CS$<>8__locals1.gcHandles = new GCHandle[ex.ExceptionData.Length];
										for (int j = 0; j < ex.ExceptionData.Length; j++)
										{
											object obj = ex.ExceptionData[j];
											int num2 = 0;
											if (obj is string)
											{
												num2 = ((string)obj).Length * 2;
											}
											else if (obj is Array && obj.GetType().GetElementType() != null && obj.GetType().GetElementType().IsPrimitive)
											{
												num2 = ((Array)obj).Length * Marshal.SizeOf(obj.GetType().GetElementType());
											}
											if (num2 != 0)
											{
												try
												{
													CS$<>8__locals1.gcHandles[j] = GCHandle.Alloc(obj, GCHandleType.Pinned);
													sqlDumper.AddMemoryRaw(CS$<>8__locals1.gcHandles[j].AddrOfPinnedObject(), num2);
												}
												catch (Exception)
												{
												}
											}
										}
									}
								}
								else
								{
									stringBuilder.AppendFormat("Exception message: {0}\r\n", CS$<>8__locals1.optionalException.Message);
								}
							}
							if (CS$<>8__locals1.assertionMessage != null)
							{
								stringBuilder.AppendFormat("Assertion failed: {0}\r\n", CS$<>8__locals1.assertionMessage);
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
								RSEventLog.Current.WriteError(Event.InternalError, new object[] { CS$<>8__locals1.optionalException });
								sqlDumper.Dump();
								string dumpResultText = sqlDumper.GetDumpResultText();
								RSTrace.CatalogTrace.Trace(TraceLevel.Info, "Dump result: {0}.", new object[] { dumpResultText });
							}
							catch (Exception ex2)
							{
								if (RSTrace.CatalogTrace.TraceError)
								{
									RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Failed to generate a dump. Error: {0}.", new object[] { ex2.ToString() });
								}
							}
						});
					}
				}
				finally
				{
					if (CS$<>8__locals1.gcHandles != null)
					{
						for (int i = 0; i < CS$<>8__locals1.gcHandles.Length; i++)
						{
							if (CS$<>8__locals1.gcHandles[i].IsAllocated)
							{
								CS$<>8__locals1.gcHandles[i].Free();
							}
						}
					}
					Interlocked.Decrement(ref this.m_recursionGuard);
				}
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000A9D8 File Offset: 0x00008BD8
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

		// Token: 0x0600021B RID: 539 RVA: 0x0000AA68 File Offset: 0x00008C68
		private static bool ShouldDump(Exception optionalException)
		{
			return (Globals.CurrentApplication == RunningApplication.WebService || Globals.CurrentApplication == RunningApplication.WindowsService || Globals.CurrentApplication == RunningApplication.ExcelAddin || Globals.CurrentApplication == RunningApplication.ReportServerWebApp || Globals.CurrentApplication == RunningApplication.PowerBIWebApp || Globals.CurrentApplication == RunningApplication.OfficeWebApp) && (Dumper.SqlDumperFlagRetriver & DumperFlags.NoMiniDump) != DumperFlags.NoMiniDump && Dumper.IsDumpException(optionalException);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000AABC File Offset: 0x00008CBC
		public static bool IsDumpException(Exception optionalException)
		{
			bool flag = false;
			if (optionalException == null)
			{
				flag = true;
			}
			else
			{
				RSException ex = optionalException as RSException;
				if (ex != null)
				{
					if (ErrorCode.rsInternalError == ex.Code || ErrorCode.rsUnexpectedError == ex.Code)
					{
						flag = true;
					}
				}
				else
				{
					flag = true;
				}
				if (!flag && Dumper.TypesToDump.Contains(optionalException.GetType().ToString()))
				{
					flag = true;
				}
				if (flag && Dumper.ContainsExceptedException(optionalException))
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000AB24 File Offset: 0x00008D24
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

		// Token: 0x0600021E RID: 542 RVA: 0x0000AB63 File Offset: 0x00008D63
		private void InitializeSettings()
		{
			RevertImpersonationContext.Run(delegate
			{
				this.m_pid = Globals.CurrentProcessId;
				this.InitializeDumperFlags();
				string text = null;
				foreach (string text2 in Dumper.TypesToDump)
				{
					text += text2;
				}
				RSTrace.CatalogTrace.Trace(TraceLevel.Info, "Dump on: {0}", new object[] { text });
				string text3 = null;
				foreach (string text4 in Dumper.TypesToNotDump)
				{
					text3 += text4;
				}
				RSTrace.CatalogTrace.Trace(TraceLevel.Info, "Do not dump on: {0}", new object[] { text3 });
				this.InitializeDumpLocation();
			});
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000AB78 File Offset: 0x00008D78
		private void InitializeDumpLocation()
		{
			string text = RSTraceInternal.Current.TraceDirectory;
			if (string.IsNullOrEmpty(text))
			{
				text = Path.GetTempPath();
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(text);
			this.m_dumpLocation = directoryInfo.FullName;
			if (!Directory.Exists(this.m_dumpLocation))
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Minidump location not found at: " + this.m_dumpLocation);
				return;
			}
			RSTrace.CatalogTrace.Trace(TraceLevel.Info, "Minidump location: " + this.m_dumpLocation);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000ABF8 File Offset: 0x00008DF8
		private void InitializeDumperFlags()
		{
			this.m_sqlMiniDumpFlags = MiniDumpFlags.DataSegs | MiniDumpFlags.UnloadedModules | MiniDumpFlags.ProcessThreadData;
			RSTrace.CatalogTrace.Trace(TraceLevel.Info, "SQLDump flags: {0}", new object[] { Dumper.SqlDumperFlagRetriver });
			RSTrace.CatalogTrace.Trace(TraceLevel.Info, "MiniDump flags: {0}", new object[] { this.m_sqlMiniDumpFlags });
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000AC58 File Offset: 0x00008E58
		private void ParseCommaDelimitedCollection(string configValue, StringCollection collection)
		{
			foreach (string text in configValue.Split(new char[] { ',' }))
			{
				collection.Add(text);
			}
		}

		// Token: 0x0400020C RID: 524
		private static readonly Dumper m_Dumper = new Dumper();

		// Token: 0x0400020D RID: 525
		private static object m_lockObj = new object();

		// Token: 0x0400020E RID: 526
		private static Dictionary<int, byte[]> m_dumpHashCache = new Dictionary<int, byte[]>();

		// Token: 0x0400020F RID: 527
		private bool m_isInitialized;

		// Token: 0x04000210 RID: 528
		private string m_dumpLocation;

		// Token: 0x04000211 RID: 529
		private string m_pid;

		// Token: 0x04000212 RID: 530
		private int m_recursionGuard;

		// Token: 0x04000213 RID: 531
		private MiniDumpFlags m_sqlMiniDumpFlags;

		// Token: 0x04000214 RID: 532
		private DumperFlags m_unsetSqlDumperFlags;

		// Token: 0x04000215 RID: 533
		private const int LogFileSize = 524288;

		// Token: 0x04000216 RID: 534
		private const int HashFrames = 16;
	}
}
