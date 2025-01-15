using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000B5 RID: 181
	internal class RSServiceTraceInternal : RSTraceInternal, IDisposable
	{
		// Token: 0x060005D6 RID: 1494 RVA: 0x00011400 File Offset: 0x0000F600
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		internal RSServiceTraceInternal()
		{
			string shortAppDomainName = this.GetShortAppDomainName(AppDomain.CurrentDomain.FriendlyName);
			this.m_cachedAppDomainNamePrefix = string.Format("{0}:", shortAppDomainName);
			RSServiceTraceInternal.m_traceInternalObject = NativeLoggingMethods.CreateNativeLoggingObject(shortAppDomainName);
			if (RSServiceTraceInternal.m_traceInternalObject != null)
			{
				RSTraceInternal.m_traceInitialized = 1;
			}
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001147C File Offset: 0x0000F67C
		private string GetShortAppDomainName(string appDomainName)
		{
			if (string.IsNullOrEmpty(appDomainName))
			{
				return "UnknownDomain";
			}
			Match match = Regex.Match(appDomainName, "^(?<part1>[a-zA-Z]+)_.+(?<part2>_\\d+-\\d+)-");
			if (match.Success)
			{
				return match.Groups["part1"].Value + match.Groups["part2"].Value;
			}
			return appDomainName;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x000114DC File Offset: 0x0000F6DC
		public override void Trace(string componentName, string message)
		{
			if (this.IsTracingEnabled(TraceLevel.Info, componentName))
			{
				this.TraceInternal(TraceLevel.Info, componentName, message, false, false, true);
			}
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x000114F4 File Offset: 0x0000F6F4
		public override void Trace(TraceLevel traceLevel, string componentName, string message)
		{
			if (this.IsTracingEnabled(traceLevel, componentName))
			{
				this.TraceInternal(traceLevel, componentName, message, false, false, true);
			}
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0001150C File Offset: 0x0000F70C
		public override void TraceException(TraceLevel traceLevel, string componentName, string message)
		{
			if (this.IsTracingEnabled(traceLevel, componentName))
			{
				this.TraceInternal(traceLevel, componentName, message, false, true, true);
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00011524 File Offset: 0x0000F724
		public override void Trace(string componentName, string format, params object[] arg)
		{
			if (this.IsTracingEnabled(TraceLevel.Info, componentName))
			{
				this.TraceInternal(TraceLevel.Info, componentName, string.Format(CultureInfo.InvariantCulture, format, arg), false, false, true);
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00011547 File Offset: 0x0000F747
		public override void Trace(TraceLevel traceLevel, string componentName, string format, params object[] arg)
		{
			if (this.IsTracingEnabled(traceLevel, componentName))
			{
				this.TraceInternal(traceLevel, componentName, string.Format(CultureInfo.InvariantCulture, format, arg), false, false, true);
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001156B File Offset: 0x0000F76B
		public override void TraceWithNoEventLog(TraceLevel traceLevel, string componentName, string format, params object[] arg)
		{
			if (this.IsTracingEnabled(traceLevel, componentName))
			{
				this.TraceInternal(traceLevel, componentName, string.Format(CultureInfo.InvariantCulture, format, arg), false, false, false);
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001158F File Offset: 0x0000F78F
		public override void Fail(string componentName)
		{
			this.FailInternal(componentName, "Un-named assertion fired for component " + componentName);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x000115A3 File Offset: 0x0000F7A3
		public override void Fail(string componentName, string message)
		{
			this.FailInternal(componentName, message);
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x000115AD File Offset: 0x0000F7AD
		// (set) Token: 0x060005E1 RID: 1505 RVA: 0x000115B5 File Offset: 0x0000F7B5
		public override bool AutoFlush
		{
			get
			{
				return this.m_IsAutoFlush;
			}
			set
			{
				this.m_IsAutoFlush = value;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x000115BE File Offset: 0x0000F7BE
		// (set) Token: 0x060005E3 RID: 1507 RVA: 0x000115C8 File Offset: 0x0000F7C8
		public override bool BufferOutput
		{
			get
			{
				return this.m_bufferOutput;
			}
			set
			{
				this.m_bufferOutput = value;
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x000115D4 File Offset: 0x0000F7D4
		public override void ClearBuffer()
		{
			object bufferLockObject = this.m_bufferLockObject;
			lock (bufferLockObject)
			{
				this.m_buffer.Clear();
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001161C File Offset: 0x0000F81C
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public override void WriteBuffer()
		{
			try
			{
				object bufferLockObject = this.m_bufferLockObject;
				lock (bufferLockObject)
				{
					foreach (RSServiceTraceInternal.BufferedOutput bufferedOutput in this.m_buffer)
					{
						NativeLoggingMethods.NativeLoggingTrace(RSServiceTraceInternal.m_traceInternalObject, bufferedOutput.m_traceLevel, bufferedOutput.m_componentName, bufferedOutput.m_message, bufferedOutput.m_isAssert, bufferedOutput.m_isException, bufferedOutput.m_allowEventlogWrite);
					}
					this.ClearBuffer();
				}
			}
			catch
			{
				RSEventLog.Current.WriteWarning(Event.FailedTraceWrite, Array.Empty<object>());
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x000116E8 File Offset: 0x0000F8E8
		public override string TraceDirectory
		{
			[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
			get
			{
				return Marshal.PtrToStringUni(NativeLoggingMethods.GetNativeLoggingTraceDirectory(RSServiceTraceInternal.m_traceInternalObject));
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x000116FC File Offset: 0x0000F8FC
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public override bool GetTraceLevel(string componentName, out TraceLevel componentLevel)
		{
			if (!NativeLoggingMethods.GetNativeTraceLevel(RSServiceTraceInternal.m_traceInternalObject, componentName, out componentLevel) && !NativeLoggingMethods.GetNativeTraceLevel(RSServiceTraceInternal.m_traceInternalObject, "all", out componentLevel))
			{
				int num;
				if (int.TryParse(this.GetDefaultTraceLevel(), NumberStyles.None, CultureInfo.InvariantCulture, out num) && num > -1 && num < 4)
				{
					componentLevel = (TraceLevel)num;
				}
				else
				{
					componentLevel = TraceLevel.Error;
				}
			}
			return true;
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0001174F File Offset: 0x0000F94F
		public override string CurrentTraceFilePath
		{
			[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
			get
			{
				return Marshal.PtrToStringUni(NativeLoggingMethods.GetNativeLoggingTracePath(RSServiceTraceInternal.m_traceInternalObject));
			}
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00011760 File Offset: 0x0000F960
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public override string GetDefaultTraceLevel()
		{
			return Marshal.PtrToStringUni(NativeLoggingMethods.GetDefaultTraceLevel(RSServiceTraceInternal.m_traceInternalObject));
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00011771 File Offset: 0x0000F971
		public override void EnsureTraceInitializedCorrectly()
		{
			if (!this.m_createdFileTraceCorrectly)
			{
				RSEventLog.Current.WriteError(Event.CouldNotCreateTraceFile, new object[]
				{
					RSEventLog.Current.SourceName,
					this.CurrentTraceFilePath
				});
			}
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x000117A3 File Offset: 0x0000F9A3
		private bool IsTracingEnabled(TraceLevel traceLevel, string componentName)
		{
			this.GetTraceLevel(componentName, out this.m_traceLevel);
			return traceLevel <= this.m_traceLevel;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x000117C0 File Offset: 0x0000F9C0
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		private void TraceInternal(TraceLevel traceLevel, string componentName, string message, bool isAssert, bool isException, bool allowEventlogWrite)
		{
			try
			{
				if (RSServiceTraceInternal.m_traceInternalObject != null)
				{
					if (this.m_bufferOutput)
					{
						RSServiceTraceInternal.BufferedOutput bufferedOutput = new RSServiceTraceInternal.BufferedOutput(traceLevel, componentName, message, isAssert, isException, allowEventlogWrite);
						object bufferLockObject = this.m_bufferLockObject;
						lock (bufferLockObject)
						{
							this.m_buffer.Add(bufferedOutput);
							goto IL_005D;
						}
					}
					NativeLoggingMethods.NativeLoggingTrace(RSServiceTraceInternal.m_traceInternalObject, traceLevel, componentName, message, isAssert, isException, allowEventlogWrite);
				}
				IL_005D:;
			}
			catch
			{
				if (allowEventlogWrite)
				{
					RSEventLog.Current.WriteWarning(Event.FailedTraceWrite, Array.Empty<object>());
				}
			}
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00011860 File Offset: 0x0000FA60
		private TraceEventType MapTraceLevel(TraceLevel level)
		{
			switch (level)
			{
			case TraceLevel.Error:
				return TraceEventType.Error;
			case TraceLevel.Warning:
				return TraceEventType.Warning;
			case TraceLevel.Info:
				return TraceEventType.Information;
			case TraceLevel.Verbose:
				return TraceEventType.Verbose;
			default:
				return TraceEventType.Information;
			}
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00011888 File Offset: 0x0000FA88
		private void FailInternal(string componentName, string message)
		{
			if (this.IsTracingEnabled(TraceLevel.Error, componentName))
			{
				StackTrace stackTrace = new StackTrace(true);
				StringBuilder stringBuilder = new StringBuilder(message, 1024);
				stringBuilder.Append("   Call stack:");
				StackTraceUtil.StackTraceToString(stackTrace, 3, stringBuilder);
				string text = stringBuilder.ToString();
				this.TraceInternal(TraceLevel.Error, componentName, text, true, false, true);
			}
			throw new InternalCatalogException(message);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x000118DD File Offset: 0x0000FADD
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x000118E6 File Offset: 0x0000FAE6
		private void Dispose(bool disposing)
		{
			if (disposing && RSServiceTraceInternal.m_traceInternalObject != null && !RSServiceTraceInternal.m_traceInternalObject.IsInvalid)
			{
				RSServiceTraceInternal.m_traceInternalObject.Dispose();
				RSServiceTraceInternal.m_traceInternalObject = null;
				RSTraceInternal.m_traceInitialized = 0;
			}
		}

		// Token: 0x04000331 RID: 817
		internal volatile bool m_bufferOutput;

		// Token: 0x04000332 RID: 818
		internal List<RSServiceTraceInternal.BufferedOutput> m_buffer = new List<RSServiceTraceInternal.BufferedOutput>();

		// Token: 0x04000333 RID: 819
		private readonly object m_bufferLockObject = new object();

		// Token: 0x04000334 RID: 820
		private static SafeNativeLoggingPointer m_traceInternalObject;

		// Token: 0x04000335 RID: 821
		internal const string m_DebugWindow = "debugwindow";

		// Token: 0x04000336 RID: 822
		internal const string m_File = "file";

		// Token: 0x04000337 RID: 823
		internal const string m_StdOut = "stdout";

		// Token: 0x04000338 RID: 824
		internal const string m_Unique = "unique";

		// Token: 0x04000339 RID: 825
		private const string m_allComponents = "all";

		// Token: 0x0400033A RID: 826
		private bool m_IsAutoFlush = true;

		// Token: 0x0400033B RID: 827
		private bool m_createdFileTraceCorrectly = true;

		// Token: 0x0400033C RID: 828
		private TraceLevel m_traceLevel = TraceLevel.Warning;

		// Token: 0x0400033D RID: 829
		private string m_cachedAppDomainNamePrefix;

		// Token: 0x02000105 RID: 261
		internal struct BufferedOutput
		{
			// Token: 0x0600082A RID: 2090 RVA: 0x000160A4 File Offset: 0x000142A4
			internal BufferedOutput(TraceLevel traceLevel, string componentName, string message, bool isAssert, bool isException, bool allowEventlogWrite)
			{
				this.m_traceLevel = traceLevel;
				this.m_componentName = componentName;
				this.m_message = message;
				this.m_isAssert = isAssert;
				this.m_isException = isException;
				this.m_allowEventlogWrite = allowEventlogWrite;
			}

			// Token: 0x0400053A RID: 1338
			internal TraceLevel m_traceLevel;

			// Token: 0x0400053B RID: 1339
			internal string m_componentName;

			// Token: 0x0400053C RID: 1340
			internal string m_message;

			// Token: 0x0400053D RID: 1341
			internal bool m_isAssert;

			// Token: 0x0400053E RID: 1342
			internal bool m_isException;

			// Token: 0x0400053F RID: 1343
			internal bool m_allowEventlogWrite;
		}
	}
}
