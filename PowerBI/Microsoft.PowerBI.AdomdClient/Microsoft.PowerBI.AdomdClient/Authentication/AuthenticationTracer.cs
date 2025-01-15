using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.AnalysisServices.AdomdClient.Utilities;

namespace Microsoft.AnalysisServices.AdomdClient.Authentication
{
	// Token: 0x02000103 RID: 259
	internal static class AuthenticationTracer
	{
		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x00032948 File Offset: 0x00030B48
		public static bool IsTracingEnabled
		{
			get
			{
				return AuthenticationTracer.traceWriter != null;
			}
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00032952 File Offset: 0x00030B52
		public static IDisposable StartScope(string scope)
		{
			return new AuthenticationTracer.TraceScope(scope);
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0003295A File Offset: 0x00030B5A
		public static void TraceError(string format, params object[] args)
		{
			if (AuthenticationTracer.traceWriter != null)
			{
				AuthenticationTracer.WriteTraceEvent(AuthenticationTracer.TraceEventInfoMask.Default, TraceEventType.Error, format, args);
			}
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0003296D File Offset: 0x00030B6D
		public static void TraceWarning(string format, params object[] args)
		{
			if (AuthenticationTracer.traceWriter != null)
			{
				AuthenticationTracer.WriteTraceEvent(AuthenticationTracer.TraceEventInfoMask.Default, TraceEventType.Warning, format, args);
			}
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x00032980 File Offset: 0x00030B80
		public static void TraceInformation(string format, params object[] args)
		{
			if (AuthenticationTracer.traceWriter != null)
			{
				AuthenticationTracer.WriteTraceEvent(AuthenticationTracer.TraceEventInfoMask.Default, TraceEventType.Information, format, args);
			}
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x00032993 File Offset: 0x00030B93
		[Conditional("DEBUG")]
		public static void TraceDebug(string format, params object[] args)
		{
			if (AuthenticationTracer.traceWriter != null)
			{
				AuthenticationTracer.WriteTraceEvent(AuthenticationTracer.TraceEventInfoMask.Default, TraceEventType.Verbose, format, args);
			}
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x000329A8 File Offset: 0x00030BA8
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

		// Token: 0x06000EF7 RID: 3831 RVA: 0x00032A64 File Offset: 0x00030C64
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

		// Token: 0x06000EF8 RID: 3832 RVA: 0x00032AB0 File Offset: 0x00030CB0
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

		// Token: 0x06000EF9 RID: 3833 RVA: 0x00032BF4 File Offset: 0x00030DF4
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

		// Token: 0x06000EFA RID: 3834 RVA: 0x00032C54 File Offset: 0x00030E54
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

		// Token: 0x04000890 RID: 2192
		private const string AppDomainKey_TraceLock = "MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_TRACE_LOCK";

		// Token: 0x04000891 RID: 2193
		private const string AppDomainKey_TraceWriter = "MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_TRACE_WRITER";

		// Token: 0x04000892 RID: 2194
		private const string AppDomainKey_TraceScopeId = "MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_TRACE_SCOPE_ID";

		// Token: 0x04000893 RID: 2195
		private const string EnvironmentVariableName_Logging = "MS_AS_AADAUTHENTICATOR_LOG";

		// Token: 0x04000894 RID: 2196
		private const string EnvironmentVariableName_LogFile = "MS_AS_AADAUTHENTICATOR_LOGFILE";

		// Token: 0x04000895 RID: 2197
		private const string DefaultLogFileName = "Microsoft.AnalysisServices.Authentication.log";

		// Token: 0x04000896 RID: 2198
		private static TextWriter traceWriter = AuthenticationTracer.Initialize();

		// Token: 0x04000897 RID: 2199
		[ThreadStatic]
		private static int currentScopeId = -1;

		// Token: 0x020001D3 RID: 467
		[Flags]
		private enum TraceEventInfoMask
		{
			// Token: 0x04000E2A RID: 3626
			None = 0,
			// Token: 0x04000E2B RID: 3627
			EventType = 1,
			// Token: 0x04000E2C RID: 3628
			CurrentTime = 16,
			// Token: 0x04000E2D RID: 3629
			ScopeId = 32,
			// Token: 0x04000E2E RID: 3630
			ThreadId = 64,
			// Token: 0x04000E2F RID: 3631
			Default = 49,
			// Token: 0x04000E30 RID: 3632
			All = 113
		}

		// Token: 0x020001D4 RID: 468
		private sealed class TraceScope : Disposable
		{
			// Token: 0x060013EF RID: 5103 RVA: 0x0004507D File Offset: 0x0004327D
			public TraceScope(string scope)
			{
				this.scope = scope;
				this.previousScopeId = AuthenticationTracer.StartScopeImpl(scope);
			}

			// Token: 0x060013F0 RID: 5104 RVA: 0x00045098 File Offset: 0x00043298
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					AuthenticationTracer.CompleteScopeImpl(this.scope, this.previousScopeId);
				}
				base.Dispose(disposing);
			}

			// Token: 0x04000E31 RID: 3633
			private string scope;

			// Token: 0x04000E32 RID: 3634
			private int previousScopeId;
		}

		// Token: 0x020001D5 RID: 469
		private sealed class TraceStreamWriter : StreamWriter
		{
			// Token: 0x060013F1 RID: 5105 RVA: 0x000450B8 File Offset: 0x000432B8
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

			// Token: 0x060013F2 RID: 5106 RVA: 0x00045138 File Offset: 0x00043338
			private static string GetTracePath()
			{
				string text = Environment.GetEnvironmentVariable("MS_AS_AADAUTHENTICATOR_LOGFILE");
				if (string.IsNullOrEmpty(text))
				{
					text = Path.Combine(Path.GetTempPath(), "Microsoft.AnalysisServices.Authentication.log");
				}
				return text;
			}

			// Token: 0x060013F3 RID: 5107 RVA: 0x00045169 File Offset: 0x00043369
			private void Cleanup()
			{
				base.WriteLine("========================================================================");
				base.Flush();
			}
		}
	}
}
