using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.SqlServer.SqlDumper;
using Microsoft.Win32;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001E6 RID: 486
	public sealed class Dumper : IDumper
	{
		// Token: 0x06000CAA RID: 3242 RVA: 0x0002C200 File Offset: 0x0002A400
		public void InitializeDumper()
		{
			TopLevelHandler.RunNoDump(this, TopLevelHandlerOption.SwallowBenign, delegate
			{
				if (this.m_isInitialized)
				{
					return;
				}
				object obj = Dumper.s_lockObj;
				lock (obj)
				{
					if (!this.m_isInitialized)
					{
						this.InitializeDumpLocation();
						this.InitializeLocalCache();
						this.InitializeExceptionShouldNotDumpList();
						if (!ExtendedEnvironment.IsTest)
						{
							this.InitializeSqlDumperClient();
						}
						this.m_isInitialized = true;
					}
				}
			});
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0002C218 File Offset: 0x0002A418
		public void CreateMemoryDump(Exception e, bool fatal, string errorText)
		{
			if (!Dumper.s_inCall)
			{
				Dumper.s_inCall = true;
				try
				{
					TopLevelHandler.RunNoDump(this, TopLevelHandlerOption.SwallowNonfatal, delegate
					{
						if (!this.m_isInitialized)
						{
							this.Implementation.InitializeDumper();
						}
						if (this.IsExceptionProcessedByDumper(e) || this.IsDumpCreated(e))
						{
							return;
						}
						if (!fatal && this.IsShouldNotDumpListException(e))
						{
							TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "Dumper: non-fatal whitelisted exception: {0}.", new object[] { e.ToString() });
							this.SetExceptionProcessedByDumper(e);
							return;
						}
						if (!Dumper.s_disabled)
						{
							object obj = Dumper.s_lockObj;
							lock (obj)
							{
								if (!Dumper.s_disabled)
								{
									Dumper.s_disabled = fatal && !ExtendedEnvironment.IsTest;
									this.CreateMemoryDumpInternal(e, fatal, errorText);
								}
							}
						}
					});
					return;
				}
				finally
				{
					Dumper.s_inCall = false;
				}
			}
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "Dumper: Ignoring re-entrant call. Error: {0}.", new object[] { e.ToString() });
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0002C2A8 File Offset: 0x0002A4A8
		public bool IsDumpHashInCache(string hash)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(hash, "hash");
			DateTime minValue = DateTime.MinValue;
			return this.m_localCache.TryGetValue(hash, out minValue) && (minValue == DateTime.MinValue || minValue > DateTime.UtcNow);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0002C2F4 File Offset: 0x0002A4F4
		public void AddDumpHashToCache(bool fatal, string hash, DateTime expiryDate)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(hash, "hash");
			this.m_localCache.AddOrUpdate(hash, expiryDate, (string k, DateTime d) => expiryDate);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0002C338 File Offset: 0x0002A538
		public void RemoveDumpHashFromCache(string hash)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(hash, "hash");
			DateTime dateTime;
			this.m_localCache.TryRemove(hash, out dateTime);
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0002C360 File Offset: 0x0002A560
		public void NotifyDumpOccured(string dumpName, Exception e, bool fatal, bool inCache, string dumpUid)
		{
			if (fatal)
			{
				if (inCache)
				{
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "Dumper.NotifyFatalDuplicateDumpOccured: {0}. Exception message: {1}", new object[] { dumpName, e.Message });
					return;
				}
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "Dumper.NotifyFatalUniqueDumpOccured: {0}. PBIDumpUID: {1}. Exception message: {2}", new object[] { dumpName, dumpUid, e.Message });
				return;
			}
			else
			{
				if (inCache)
				{
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "Dumper.NotifyNonFatalDuplicateDumpOccured: {0}. Exception message: {1}", new object[] { dumpName, e.Message });
					return;
				}
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "Dumper.NotifyNonFatalUniqueDumpOccured: {0}. PBIDumpUID: {1}. Exception message: {2}", new object[] { dumpName, dumpUid, e.Message });
				return;
			}
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0002C414 File Offset: 0x0002A614
		public bool IsDumpCreated(Exception e)
		{
			for (Exception ex = e; ex != null; ex = ex.InnerException)
			{
				if (ex.Data != null && ex.Data.Contains("Microsoft.Cloud.Platform.Utils.DumpCreated"))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0002C44C File Offset: 0x0002A64C
		private Dumper()
		{
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x0002C4A0 File Offset: 0x0002A6A0
		public static IDumper Current
		{
			get
			{
				return Dumper.s_Dumper.Implementation;
			}
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0002C4AC File Offset: 0x0002A6AC
		public static IDumper SetImplementation(IDumper implementation)
		{
			Dumper.s_Dumper.Implementation = implementation;
			return Dumper.s_Dumper;
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x0002C4BE File Offset: 0x0002A6BE
		// (set) Token: 0x06000CB5 RID: 3253 RVA: 0x0002C4CB File Offset: 0x0002A6CB
		private IDumper Implementation
		{
			get
			{
				return this.m_implementation ?? this;
			}
			set
			{
				this.m_implementation = value;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x0002C4D4 File Offset: 0x0002A6D4
		private static string ProcessMemoryDumpsDirectory
		{
			get
			{
				return Dumper.s_processMemoryDumpDirectoryTweak.Value;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x0002C4E0 File Offset: 0x0002A6E0
		private static string ExceptionShouldNotDumpList
		{
			get
			{
				return Dumper.s_exceptionShouldNotDumpListTweak.Value;
			}
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0002C4EC File Offset: 0x0002A6EC
		private bool IsExceptionProcessedByDumper(Exception e)
		{
			for (Exception ex = e; ex != null; ex = ex.InnerException)
			{
				if (ex.Data != null && ex.Data.Contains("Microsoft.Cloud.Platform.Utils.ExceptionProcessedByDumper"))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x0002C524 File Offset: 0x0002A724
		private void CreateMemoryDumpInternal(Exception e, bool fatal, string errorText)
		{
			string text = this.CalculateHash(e);
			string dumpName = this.GetDumpName(fatal, text);
			string dumpIdentity = this.GetDumpIdentity(dumpName);
			bool flag = this.Implementation.IsDumpHashInCache(text);
			this.Implementation.NotifyDumpOccured(dumpName, e, fatal, flag, this.dumpUid);
			this.SetDumpCreated(e);
			DumpContextStack.Current.PushDumpContext(e.GetType(), e.StackTrace, text, fatal, flag);
			if (!flag)
			{
				if (!ExtendedEnvironment.IsTest)
				{
					this.SqlDumperCreateMemoryDump(dumpName, dumpIdentity, fatal, errorText);
				}
				this.Implementation.AddDumpHashToCache(fatal, text, DateTime.MinValue);
			}
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0002C5B4 File Offset: 0x0002A7B4
		private string GetDumpIdentity(string dumpName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<Identity>");
			string text = "<Element key=\"{0}\" val=\"{1}\" />";
			stringBuilder.Append(string.Format(text, "DumpType", "Managed"));
			stringBuilder.Append(string.Format(text, "OriginalFileName", dumpName));
			stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, text, new object[]
			{
				"ActivityID",
				UtilsContext.Current.Activity.ActivityId
			}));
			stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, text, new object[]
			{
				"RootActivityID",
				UtilsContext.Current.Activity.RootActivityId
			}));
			stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, text, new object[]
			{
				"ClientActivityID",
				UtilsContext.Current.Activity.ClientActivityId
			}));
			stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, text, new object[]
			{
				"ActivityStack",
				UtilsContext.GetActivityStackAsStrings(UtilsContext.Current.GetActivityStack())
			}));
			this.dumpUid = Guid.NewGuid().ToString();
			stringBuilder.Append(string.Format(text, "PBIDumpUID", this.dumpUid));
			stringBuilder.Append("</Identity>");
			return stringBuilder.ToString();
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0002C718 File Offset: 0x0002A918
		private string GetWerLocalDumpsDirectory()
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\Windows Error Reporting\\LocalDumps");
			if (registryKey != null)
			{
				object value = registryKey.GetValue("DumpFolder");
				if (value != null && Path.IsPathRooted(value.ToString()))
				{
					return value.ToString();
				}
			}
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "WindowErrorReporting LocalDumps is not configured.");
			return string.Empty;
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0002C770 File Offset: 0x0002A970
		private void InitializeDumpLocation()
		{
			string text = (string.IsNullOrEmpty(Dumper.ProcessMemoryDumpsDirectory) ? this.GetWerLocalDumpsDirectory() : Dumper.ProcessMemoryDumpsDirectory);
			if (!Directory.Exists(text))
			{
				text = "c:\\temp\\LocalCrashDumps";
				Directory.CreateDirectory(text);
			}
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "Dump location: " + text);
			this.m_dumpLocation = text;
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0002C7CC File Offset: 0x0002A9CC
		private void InitializeLocalCache()
		{
			foreach (string text in Directory.GetFiles(this.m_dumpLocation, "*.mdmp"))
			{
				Match match = Dumper.s_dumpNameParser.Match(text);
				if (match.Success)
				{
					string value = match.Groups["hash"].Value;
					this.AddDumpHashToCache(true, value, DateTime.MinValue);
				}
			}
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0002C835 File Offset: 0x0002AA35
		private void InitializeExceptionShouldNotDumpList()
		{
			if (!string.IsNullOrEmpty(Dumper.ExceptionShouldNotDumpList))
			{
				this.m_exceptionShouldNotDumpList.AddRange(Dumper.ExceptionShouldNotDumpList.Split(new char[] { ';' }));
			}
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0002C863 File Offset: 0x0002AA63
		private void InitializeSqlDumperClient()
		{
			DumpClient.Initialize();
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0002C86A File Offset: 0x0002AA6A
		private void SetDumpCreated(Exception e)
		{
			this.MarkException(e, "Microsoft.Cloud.Platform.Utils.DumpCreated");
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0002C878 File Offset: 0x0002AA78
		private void SetExceptionProcessedByDumper(Exception e)
		{
			this.MarkException(e, "Microsoft.Cloud.Platform.Utils.ExceptionProcessedByDumper");
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0002C886 File Offset: 0x0002AA86
		private void MarkException(Exception e, string key)
		{
			while (e != null && e.Data != null && !e.Data.Contains(key))
			{
				e.Data[key] = string.Empty;
				e = e.InnerException;
			}
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0002C8BC File Offset: 0x0002AABC
		private bool IsShouldNotDumpListException(Exception e)
		{
			while (e != null)
			{
				if (this.m_exceptionShouldNotDumpList.Exists(delegate(string whiteListExceptionTypeString)
				{
					Type type = e.GetType();
					if (string.Compare(whiteListExceptionTypeString, type.ToString(), StringComparison.Ordinal) == 0)
					{
						return true;
					}
					Type whiteListExceptionType = null;
					TopLevelHandler.RunNoDump(this, TopLevelHandlerOption.SwallowNonfatal, delegate
					{
						whiteListExceptionType = Type.GetType(whiteListExceptionTypeString, false);
					});
					return whiteListExceptionType != null && whiteListExceptionType.IsAssignableFrom(type);
				}))
				{
					return true;
				}
				e = e.InnerException;
			}
			return false;
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0002C914 File Offset: 0x0002AB14
		private string CalculateHash(Exception e)
		{
			string text;
			using (MD5 md = MD5.Create())
			{
				text = BitConverter.ToString(md.ComputeHash(this.GetStackBytes(e))).Replace("-", string.Empty);
			}
			return text;
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0002C968 File Offset: 0x0002AB68
		private byte[] GetStackBytes(Exception e)
		{
			List<StackTrace> list = new List<StackTrace>();
			int num = 0;
			while (e != null)
			{
				StackTrace stackTrace = new StackTrace(e);
				if (stackTrace.FrameCount == 0)
				{
					stackTrace = new StackTrace(false);
				}
				num += stackTrace.FrameCount;
				list.Add(stackTrace);
				e = e.InnerException;
			}
			int num2 = 0;
			byte[] array = new byte[4 * num];
			foreach (StackTrace stackTrace2 in list)
			{
				if (stackTrace2.FrameCount > 0)
				{
					StackFrame[] frames = stackTrace2.GetFrames();
					for (int i = 0; i < frames.Length; i++)
					{
						BitConverter.GetBytes(frames[i].GetNativeOffset()).CopyTo(array, num2);
						num2 += 4;
					}
				}
			}
			return array;
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0002CA40 File Offset: 0x0002AC40
		private void SqlDumperCreateMemoryDump(string dumpName, string dumpIdentity, bool fatal, string errorText)
		{
			using (Dumper dumper = DumpClient.GetDumper())
			{
				dumper.SetDirectory(this.m_dumpLocation);
				dumper.SetMiniDumpFlags(289, 0);
				dumper.SetFlags(67781744, 0);
				dumper.SetBucket(16, 0);
				dumper.SetFileName(Path.Combine(this.m_dumpLocation, dumpName));
				dumper.SetDumpIdentity(dumpIdentity);
				if (!string.IsNullOrEmpty(errorText))
				{
					dumper.SetErrorText(errorText);
				}
				dumper.Dump();
				string dumpResultText = dumper.GetDumpResultText();
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "Dump result: {0}.", new object[] { dumpResultText });
			}
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0002CAF0 File Offset: 0x0002ACF0
		private string GetDumpName(bool fatal, string hash)
		{
			return (fatal ? "{0}.{1}.{2}.exe.mdmp" : "non-fatal.{0}.{1}.{2}.exe.mdmp").FormatWithInvariantCulture(new object[]
			{
				Process.GetCurrentProcess().ProcessName,
				Process.GetCurrentProcess().Id,
				hash
			});
		}

		// Token: 0x040004DF RID: 1247
		[ThreadStatic]
		private static bool s_inCall = false;

		// Token: 0x040004E0 RID: 1248
		private const string c_managedDumpExtension = "*.mdmp";

		// Token: 0x040004E1 RID: 1249
		private const string c_exceptionProcessedByDumper = "Microsoft.Cloud.Platform.Utils.ExceptionProcessedByDumper";

		// Token: 0x040004E2 RID: 1250
		private const string c_dumpCreatedKey = "Microsoft.Cloud.Platform.Utils.DumpCreated";

		// Token: 0x040004E3 RID: 1251
		private const string c_defaultDumpFolder = "c:\\temp\\LocalCrashDumps";

		// Token: 0x040004E4 RID: 1252
		private const string c_werLocalDumpRegKey = "SOFTWARE\\Microsoft\\Windows\\Windows Error Reporting\\LocalDumps";

		// Token: 0x040004E5 RID: 1253
		private const string c_werLocalDumpFolderValueName = "DumpFolder";

		// Token: 0x040004E6 RID: 1254
		private const MiniDumpFlags c_miniDumpFlags = 289;

		// Token: 0x040004E7 RID: 1255
		private const MiniDumpFlags c_unsetMiniDumpFlags = 0;

		// Token: 0x040004E8 RID: 1256
		private const DumperFlags c_dumperFlags = 67781744;

		// Token: 0x040004E9 RID: 1257
		private const DumperFlags c_unsetDumperFlags = 0;

		// Token: 0x040004EA RID: 1258
		private const string c_fatalDumpMask = "{0}.{1}.{2}.exe.mdmp";

		// Token: 0x040004EB RID: 1259
		private const string c_nonFatalDumpMask = "non-fatal.{0}.{1}.{2}.exe.mdmp";

		// Token: 0x040004EC RID: 1260
		private const string c_hashCaptureName = "hash";

		// Token: 0x040004ED RID: 1261
		private static readonly Regex s_dumpNameParser = new Regex("(non-fatal.|)[\\w]+.[0-9]+.(?<hash>[\\dA-F]{32}).exe.mdmp", RegexOptions.Compiled);

		// Token: 0x040004EE RID: 1262
		private const int c_hashFrames = 16;

		// Token: 0x040004EF RID: 1263
		private bool m_isInitialized;

		// Token: 0x040004F0 RID: 1264
		private string m_dumpLocation = string.Empty;

		// Token: 0x040004F1 RID: 1265
		private IDumper m_implementation;

		// Token: 0x040004F2 RID: 1266
		private List<string> m_exceptionShouldNotDumpList = new List<string> { typeof(MonitoredException).FullName };

		// Token: 0x040004F3 RID: 1267
		private ConcurrentDictionary<string, DateTime> m_localCache = new ConcurrentDictionary<string, DateTime>();

		// Token: 0x040004F4 RID: 1268
		private static bool s_disabled = false;

		// Token: 0x040004F5 RID: 1269
		private string dumpUid = string.Empty;

		// Token: 0x040004F6 RID: 1270
		public const string ProcessMemoryDumpDirectoryTweakName = "Microsoft.Cloud.Platform.Utils.ProcessMemoryDumpDirectory";

		// Token: 0x040004F7 RID: 1271
		public const string ExceptionShouldNotDumpListTweakName = "Microsoft.Cloud.Platform.Utils.ExceptionShouldNotDumpList";

		// Token: 0x040004F8 RID: 1272
		private static readonly Tweak<string> s_processMemoryDumpDirectoryTweak = Anchor.Tweaks.RegisterTweak<string>("Microsoft.Cloud.Platform.Utils.ProcessMemoryDumpDirectory", "The directory path in which process memory dumps are stored on crashes", string.Empty);

		// Token: 0x040004F9 RID: 1273
		private static readonly Tweak<string> s_exceptionShouldNotDumpListTweak = Anchor.Tweaks.RegisterTweak<string>("Microsoft.Cloud.Platform.Utils.ExceptionShouldNotDumpList", "List of exceptions not to generate a dump if exception being checked or one of its inner exceptions is of a listed type", string.Empty);

		// Token: 0x040004FA RID: 1274
		private static readonly object s_lockObj = new object();

		// Token: 0x040004FB RID: 1275
		private static readonly Dumper s_Dumper = new Dumper();
	}
}
