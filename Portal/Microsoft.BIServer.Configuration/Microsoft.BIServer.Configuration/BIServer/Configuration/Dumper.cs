using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using Microsoft.BIServer.HostingEnvironment;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x02000016 RID: 22
	public sealed class Dumper
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00003AA4 File Offset: 0x00001CA4
		private Dumper(StaticConfig config, IDumperAdapter dumperAdapter)
		{
			this._dumperAdapter = dumperAdapter;
			dumperAdapter.Prepare();
			Process.GetCurrentProcess().Id.ToString(CultureInfo.InvariantCulture);
			this._dumpLocation = config.Get("Dumper.path");
			this._serviceName = config.Get("Name");
			this._dumperFlags = Dumper.ParseFlags(config.Get("Dumper.flags"));
			string text = config.Get("Dumper.preventIfContains");
			this._errorsExcluded = Dumper.ParseList(text);
			Logger.Info("Do not dump on: {0}", new object[] { text });
			Logger.Trace("Setting unhandled exception handler", Array.Empty<object>());
			AppDomain.CurrentDomain.UnhandledException += this.DefaultExceptionHandler;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003B68 File Offset: 0x00001D68
		public static void Create(StaticConfig config, IDumperAdapter dumperAdapter)
		{
			object lockObj = Dumper._lockObj;
			lock (lockObj)
			{
				if (Dumper._dumper == null)
				{
					if (config == null)
					{
						throw new ArgumentNullException("config");
					}
					if (dumperAdapter == null)
					{
						throw new ArgumentNullException("dumperAdapter");
					}
					Dumper._dumper = new Dumper(config, dumperAdapter);
				}
				else
				{
					Logger.Error("Dumper must not be created multiple times.", Array.Empty<object>());
				}
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003BE4 File Offset: 0x00001DE4
		public static void DumpHere(Exception optionalException)
		{
			if (Dumper._dumper != null)
			{
				Dumper._dumper.GenerateDump(optionalException, false);
				return;
			}
			Logger.Error("Dumper must be created before triggering a dump.", Array.Empty<object>());
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003C09 File Offset: 0x00001E09
		public static void Reset()
		{
			Dumper._dumper = null;
			Dumper._dumpHashCache = new Dictionary<int, byte[]>();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003C1B File Offset: 0x00001E1B
		private void DefaultExceptionHandler(object sender, UnhandledExceptionEventArgs args)
		{
			this.GenerateDump(args.ExceptionObject as Exception, true);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003C30 File Offset: 0x00001E30
		private void GenerateDump(Exception optionalException, bool unhandledException)
		{
			using (Dumper.RecursionGuard recursionGuard = new Dumper.RecursionGuard())
			{
				if (!recursionGuard.InRecursion)
				{
					using (ImpersonationContext.EnterServiceAccountContext())
					{
						if (unhandledException && optionalException != null)
						{
							Dumper.LogUnhandledException(optionalException, unhandledException);
						}
						if (this.ShouldDump(optionalException))
						{
							DumpInstructions dumpInstructions = new DumpInstructions
							{
								Location = this._dumpLocation,
								Flags = this._dumperFlags,
								InstanceName = ConfigReader.Current.InstanceId,
								ServiceName = this._serviceName,
								FramesToInclude = 16
							};
							string text;
							if (Logger.TryGetFileTargetName(out text))
							{
								dumpInstructions.LogFileToInclude = text;
								dumpInstructions.SizeOfLogFileToInclude = 524288;
							}
							if (optionalException != null)
							{
								dumpInstructions.ErrorText = Dumper.CreateDumperErrorText(optionalException);
							}
							try
							{
								Logger.Error("Report server dump occurred: {0}", new object[] { dumpInstructions.ErrorText });
								string text2 = this._dumperAdapter.Dump(dumpInstructions);
								Logger.Info("Dump result: {0}.", new object[] { text2 });
							}
							catch (Exception ex)
							{
								Logger.Error("Failed to generate a dump. Error: {0}.", new object[] { ex.ToString() });
							}
						}
					}
				}
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003D9C File Offset: 0x00001F9C
		private static void LogUnhandledException(Exception optionalException, bool unhandledException)
		{
			if (optionalException is AppDomainUnloadedException || optionalException is OutOfMemoryException || optionalException is ThreadAbortException || (optionalException is NullReferenceException && Dumper.IsOwinException(optionalException)))
			{
				Logger.Trace("Unhandled exception in Appdomain {0}, Exception {1}: ", new object[]
				{
					optionalException,
					AppDomain.CurrentDomain.FriendlyName
				});
				return;
			}
			Logger.Error("Unhandled exception in Appdomain {0}, Exception {1}: ", new object[]
			{
				optionalException,
				AppDomain.CurrentDomain.FriendlyName
			});
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003E14 File Offset: 0x00002014
		private static bool IsOwinException(Exception owinException)
		{
			return owinException.StackTrace.Contains("System.Net.HttpListener.EndGetContext");
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003E28 File Offset: 0x00002028
		private static string CreateDumperErrorText(Exception optionalException)
		{
			string text = optionalException.StackTrace ?? ((optionalException.InnerException != null) ? optionalException.InnerException.StackTrace : null);
			string text2 = optionalException.GetType().ToString() + " at \r\n ";
			if (string.IsNullOrWhiteSpace(text))
			{
				return string.Format(CultureInfo.InvariantCulture, "Exception message: {0}:{1}\r\n", optionalException.GetType().ToString(), optionalException.Message);
			}
			return text2 + text;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003E9C File Offset: 0x0000209C
		private bool ContainsExcludedException(Exception root)
		{
			if (root == null)
			{
				return false;
			}
			Exception ex = root;
			while (!this._errorsExcluded.Contains(ex.GetType().ToString()))
			{
				ex = ex.InnerException;
				if (ex == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003ED8 File Offset: 0x000020D8
		private bool ShouldDump(Exception optionalException)
		{
			if (optionalException == null)
			{
				return true;
			}
			if (!this.ContainsExcludedException(optionalException))
			{
				int num;
				if (!this.ExistsInCache(optionalException, out num))
				{
					return true;
				}
				Logger.Trace("Skipped creating a dump file for the error {0}, because a dump with the identical stack trace (with signature {1}) was already created.", new object[]
				{
					(optionalException != null) ? optionalException.GetType().Name : string.Empty,
					(uint)num
				});
			}
			return false;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003F34 File Offset: 0x00002134
		private bool ExistsInCache(Exception optionalException, out int hash)
		{
			hash = 0;
			if (optionalException == null)
			{
				return false;
			}
			bool flag = false;
			byte[] exceptionFrames = Dumper.GetExceptionFrames(optionalException, out hash);
			if (exceptionFrames != null && hash != 0)
			{
				byte[] array = null;
				object lockObj = Dumper._lockObj;
				lock (lockObj)
				{
					if (Dumper._dumpHashCache.ContainsKey(hash))
					{
						array = Dumper._dumpHashCache[hash];
					}
					else
					{
						Dumper._dumpHashCache.Add(hash, exceptionFrames);
					}
				}
				flag = array != null && exceptionFrames.SequenceEqual(array);
			}
			return flag;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003FC4 File Offset: 0x000021C4
		private static DumperFlags ParseFlags(string flags)
		{
			DumperFlags dumperFlags = DumperFlags.Default;
			if (!string.IsNullOrWhiteSpace(flags))
			{
				foreach (string text in flags.Split(new char[] { ',' }))
				{
					DumperFlags dumperFlags2;
					if (Enum.TryParse<DumperFlags>(text, out dumperFlags2))
					{
						dumperFlags |= dumperFlags2;
					}
					else
					{
						Logger.Error("Invalid Dumper flag: {0}", new object[] { text });
					}
				}
			}
			else
			{
				Logger.Info("No flag is configured for the Dumper. Using Default settings.", Array.Empty<object>());
			}
			if (!Dumper.IsPublicBuild())
			{
				dumperFlags &= ~DumperFlags.SendToWatson;
			}
			return dumperFlags;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004048 File Offset: 0x00002248
		private static ISet<string> ParseList(string list)
		{
			IEnumerable<string> enumerable;
			if (!string.IsNullOrWhiteSpace(list))
			{
				enumerable = from p in list.Split(new char[] { ',' })
					select p.Trim();
			}
			else
			{
				enumerable = Enumerable.Empty<string>();
			}
			return new SortedSet<string>(enumerable);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000409E File Offset: 0x0000229E
		private static bool IsPublicBuild()
		{
			return false;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000040A4 File Offset: 0x000022A4
		private static byte[] GetExceptionFrames(Exception e, out int exceptionHash)
		{
			exceptionHash = 0;
			if (e == null)
			{
				return null;
			}
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
			exceptionHash = Convert.ToBase64String(array).GetHashCode();
			return array;
		}

		// Token: 0x040000AD RID: 173
		private const string FolderKey = "Dumper.path";

		// Token: 0x040000AE RID: 174
		private const string FlagsKey = "Dumper.flags";

		// Token: 0x040000AF RID: 175
		private const string ExclusionKey = "Dumper.preventIfContains";

		// Token: 0x040000B0 RID: 176
		private const char ItemSeparator = ',';

		// Token: 0x040000B1 RID: 177
		private const int LogFileSize = 524288;

		// Token: 0x040000B2 RID: 178
		private const int HashFrames = 16;

		// Token: 0x040000B3 RID: 179
		private static Dumper _dumper;

		// Token: 0x040000B4 RID: 180
		private static object _lockObj = new object();

		// Token: 0x040000B5 RID: 181
		private static Dictionary<int, byte[]> _dumpHashCache = new Dictionary<int, byte[]>();

		// Token: 0x040000B6 RID: 182
		private readonly IDumperAdapter _dumperAdapter;

		// Token: 0x040000B7 RID: 183
		private string _dumpLocation;

		// Token: 0x040000B8 RID: 184
		private DumperFlags _dumperFlags;

		// Token: 0x040000B9 RID: 185
		private string _serviceName;

		// Token: 0x040000BA RID: 186
		private ISet<string> _errorsExcluded;

		// Token: 0x040000BB RID: 187
		private int _recursionGuard;

		// Token: 0x0200004B RID: 75
		private class RecursionGuard : IDisposable
		{
			// Token: 0x06000214 RID: 532 RVA: 0x00008BA7 File Offset: 0x00006DA7
			public RecursionGuard()
			{
				this.InRecursion = Interlocked.Increment(ref Dumper._dumper._recursionGuard) > 1;
			}

			// Token: 0x17000061 RID: 97
			// (get) Token: 0x06000215 RID: 533 RVA: 0x00008BC7 File Offset: 0x00006DC7
			// (set) Token: 0x06000216 RID: 534 RVA: 0x00008BCF File Offset: 0x00006DCF
			public bool InRecursion { get; private set; }

			// Token: 0x06000217 RID: 535 RVA: 0x00008BD8 File Offset: 0x00006DD8
			void IDisposable.Dispose()
			{
				Interlocked.Decrement(ref Dumper._dumper._recursionGuard);
			}
		}
	}
}
