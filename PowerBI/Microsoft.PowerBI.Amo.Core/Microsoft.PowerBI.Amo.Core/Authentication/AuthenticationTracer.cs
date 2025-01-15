using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices.Authentication
{
	// Token: 0x020000F8 RID: 248
	internal static class AuthenticationTracer
	{
		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x00035595 File Offset: 0x00033795
		public static bool IsTracingEnabled
		{
			get
			{
				return AuthenticationTracer.traceWriter != null;
			}
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0003559F File Offset: 0x0003379F
		public static IDisposable StartScope(string scope)
		{
			return new AuthenticationTracer.TraceScope(scope);
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x000355A7 File Offset: 0x000337A7
		public static void TraceError(string format, params object[] args)
		{
			if (AuthenticationTracer.traceWriter != null)
			{
				AuthenticationTracer.WriteTraceEvent(AuthenticationTracer.TraceEventInfoMask.Default, TraceEventType.Error, format, args);
			}
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x000355BA File Offset: 0x000337BA
		public static void TraceWarning(string format, params object[] args)
		{
			if (AuthenticationTracer.traceWriter != null)
			{
				AuthenticationTracer.WriteTraceEvent(AuthenticationTracer.TraceEventInfoMask.Default, TraceEventType.Warning, format, args);
			}
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x000355CD File Offset: 0x000337CD
		public static void TraceInformation(string format, params object[] args)
		{
			if (AuthenticationTracer.traceWriter != null)
			{
				AuthenticationTracer.WriteTraceEvent(AuthenticationTracer.TraceEventInfoMask.Default, TraceEventType.Information, format, args);
			}
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x000355E0 File Offset: 0x000337E0
		[Conditional("DEBUG")]
		public static void TraceDebug(string format, params object[] args)
		{
			if (AuthenticationTracer.traceWriter != null)
			{
				AuthenticationTracer.WriteTraceEvent(AuthenticationTracer.TraceEventInfoMask.Default, TraceEventType.Verbose, format, args);
			}
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x000355F4 File Offset: 0x000337F4
		internal static int StartScopeImpl(string scope)
		{
			if (AuthenticationTracer.traceWriter == null)
			{
				return -1;
			}
			int num;
			using (GlobalContext.CreateGlobalLockScope("MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_TRACE_LOCK"))
			{
				num = GlobalContext.GetGlobalObject<int>("MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_TRACE_SCOPE_ID") + 1;
				GlobalContext.SetGlobalObject("MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_TRACE_SCOPE_ID", num);
			}
			int num2 = AuthenticationTracer.currentScopeId;
			int num3;
			try
			{
				AuthenticationTracer.currentScopeId = num;
				string location = Assembly.GetExecutingAssembly().Location;
				AuthenticationTracer.WriteTraceEvent(AuthenticationTracer.TraceEventInfoMask.All, TraceEventType.Start, "Scope={0}, Assembly={1}, Version={2}", new object[]
				{
					scope,
					location,
					FileVersionInfo.GetVersionInfo(location).FileVersion
				});
				num3 = num;
			}
			catch (Exception)
			{
				AuthenticationTracer.currentScopeId = num2;
				throw;
			}
			return num3;
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x000356B0 File Offset: 0x000338B0
		internal static void CompleteScopeImpl(string scope, int previousScopeId)
		{
			if (AuthenticationTracer.traceWriter == null)
			{
				return;
			}
			try
			{
				AuthenticationTracer.WriteTraceEvent(AuthenticationTracer.TraceEventInfoMask.All, TraceEventType.Stop, "Scope={0}", new object[] { scope });
			}
			finally
			{
				AuthenticationTracer.currentScopeId = previousScopeId;
			}
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x000356FC File Offset: 0x000338FC
		private static void WriteTraceEvent(AuthenticationTracer.TraceEventInfoMask infoMask, TraceEventType eventType, string format, params object[] args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if ((infoMask & AuthenticationTracer.TraceEventInfoMask.CurrentTime) != AuthenticationTracer.TraceEventInfoMask.None)
			{
				stringBuilder.AppendFormat(", Time={0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
			}
			if ((infoMask & AuthenticationTracer.TraceEventInfoMask.ScopeId) != AuthenticationTracer.TraceEventInfoMask.None && AuthenticationTracer.currentScopeId != -1)
			{
				stringBuilder.AppendFormat(", Scope={0}", AuthenticationTracer.currentScopeId);
			}
			if ((infoMask & AuthenticationTracer.TraceEventInfoMask.ThreadId) != AuthenticationTracer.TraceEventInfoMask.None || ((infoMask & AuthenticationTracer.TraceEventInfoMask.ScopeId) != AuthenticationTracer.TraceEventInfoMask.None && AuthenticationTracer.currentScopeId == -1))
			{
				stringBuilder.AppendFormat(", Thread={0}", Thread.CurrentThread.ManagedThreadId);
			}
			if ((infoMask & AuthenticationTracer.TraceEventInfoMask.EventType) != AuthenticationTracer.TraceEventInfoMask.None)
			{
				stringBuilder.AppendFormat(", EventType={0}", AuthenticationTracer.TraceEventTypeToString(eventType));
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder = stringBuilder.Remove(0, 2);
			}
			stringBuilder.AppendFormat(" > {0}", (args != null && args.Length != 0) ? string.Format(format, args) : format);
			TextWriter textWriter = AuthenticationTracer.traceWriter;
			lock (textWriter)
			{
				if (eventType == TraceEventType.Start)
				{
					AuthenticationTracer.traceWriter.WriteLine("------------------------------------------------------------------------");
				}
				AuthenticationTracer.traceWriter.WriteLine(stringBuilder.ToString());
				if (eventType == TraceEventType.Stop)
				{
					AuthenticationTracer.traceWriter.WriteLine("------------------------------------------------------------------------");
					AuthenticationTracer.traceWriter.Flush();
				}
			}
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x00035840 File Offset: 0x00033A40
		private static string TraceEventTypeToString(TraceEventType eventType)
		{
			if (eventType <= TraceEventType.Information)
			{
				if (eventType == TraceEventType.Error)
				{
					return "Error";
				}
				if (eventType == TraceEventType.Warning)
				{
					return "Warning";
				}
				if (eventType == TraceEventType.Information)
				{
					return "Information";
				}
			}
			else
			{
				if (eventType == TraceEventType.Verbose)
				{
					return "Debug";
				}
				if (eventType == TraceEventType.Start)
				{
					return "Start";
				}
				if (eventType == TraceEventType.Stop)
				{
					return "End";
				}
			}
			return string.Empty;
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x000358A0 File Offset: 0x00033AA0
		private static TextWriter Initialize()
		{
			if (Environment.GetEnvironmentVariable("MS_AS_AADAUTHENTICATOR_LOG") != "1")
			{
				return null;
			}
			TextWriter textWriter;
			using (GlobalContext.CreateGlobalLockScope("MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_TRACE_LOCK"))
			{
				if (!GlobalContext.TryGetGlobalObject<TextWriter>("MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_TRACE_WRITER", out textWriter))
				{
					try
					{
						textWriter = new AuthenticationTracer.TraceStreamWriter();
					}
					finally
					{
						GlobalContext.SetGlobalObject("MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_TRACE_SCOPE_ID", -1);
						GlobalContext.SetGlobalObject("MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_TRACE_WRITER", textWriter);
					}
				}
			}
			return textWriter;
		}

		// Token: 0x04000856 RID: 2134
		private const string AppDomainKey_TraceLock = "MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_TRACE_LOCK";

		// Token: 0x04000857 RID: 2135
		private const string AppDomainKey_TraceWriter = "MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_TRACE_WRITER";

		// Token: 0x04000858 RID: 2136
		private const string AppDomainKey_TraceScopeId = "MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_TRACE_SCOPE_ID";

		// Token: 0x04000859 RID: 2137
		private const string EnvironmentVariableName_Logging = "MS_AS_AADAUTHENTICATOR_LOG";

		// Token: 0x0400085A RID: 2138
		private const string EnvironmentVariableName_LogFile = "MS_AS_AADAUTHENTICATOR_LOGFILE";

		// Token: 0x0400085B RID: 2139
		private const string DefaultLogFileName = "Microsoft.AnalysisServices.Authentication.log";

		// Token: 0x0400085C RID: 2140
		private static TextWriter traceWriter = AuthenticationTracer.Initialize();

		// Token: 0x0400085D RID: 2141
		[ThreadStatic]
		private static int currentScopeId = -1;

		// Token: 0x020001B0 RID: 432
		[Flags]
		private enum TraceEventInfoMask
		{
			// Token: 0x040010F8 RID: 4344
			None = 0,
			// Token: 0x040010F9 RID: 4345
			EventType = 1,
			// Token: 0x040010FA RID: 4346
			CurrentTime = 16,
			// Token: 0x040010FB RID: 4347
			ScopeId = 32,
			// Token: 0x040010FC RID: 4348
			ThreadId = 64,
			// Token: 0x040010FD RID: 4349
			Default = 49,
			// Token: 0x040010FE RID: 4350
			All = 113
		}

		// Token: 0x020001B1 RID: 433
		private sealed class TraceScope : Disposable
		{
			// Token: 0x06001358 RID: 4952 RVA: 0x00043805 File Offset: 0x00041A05
			public TraceScope(string scope)
			{
				this.scope = scope;
				this.previousScopeId = AuthenticationTracer.StartScopeImpl(scope);
			}

			// Token: 0x06001359 RID: 4953 RVA: 0x00043820 File Offset: 0x00041A20
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					AuthenticationTracer.CompleteScopeImpl(this.scope, this.previousScopeId);
				}
				base.Dispose(disposing);
			}

			// Token: 0x040010FF RID: 4351
			private string scope;

			// Token: 0x04001100 RID: 4352
			private int previousScopeId;
		}

		// Token: 0x020001B2 RID: 434
		private sealed class TraceStreamWriter : StreamWriter
		{
			// Token: 0x0600135A RID: 4954 RVA: 0x00043840 File Offset: 0x00041A40
			public TraceStreamWriter()
				: base(AuthenticationTracer.TraceStreamWriter.GetTracePath(), true)
			{
				base.WriteLine("========================================================================");
				base.WriteLine("Start of AAD AuthenticationTracer log");
				Process currentProcess = Process.GetCurrentProcess();
				base.WriteLine("OS={0} Process={1}, ProcessId={2}", Environment.OSVersion.ToString(), currentProcess.ProcessName, currentProcess.Id);
				if (!AppDomain.CurrentDomain.IsDefaultAppDomain())
				{
					AppDomain.CurrentDomain.DomainUnload += delegate(object sender, EventArgs args)
					{
						this.Cleanup();
					};
				}
			}

			// Token: 0x0600135B RID: 4955 RVA: 0x000438C0 File Offset: 0x00041AC0
			private static string GetTracePath()
			{
				string text = Environment.GetEnvironmentVariable("MS_AS_AADAUTHENTICATOR_LOGFILE");
				if (string.IsNullOrEmpty(text))
				{
					text = Path.Combine(Path.GetTempPath(), "Microsoft.AnalysisServices.Authentication.log");
				}
				return text;
			}

			// Token: 0x0600135C RID: 4956 RVA: 0x000438F1 File Offset: 0x00041AF1
			private void Cleanup()
			{
				base.WriteLine("========================================================================");
				base.Flush();
			}
		}
	}
}
