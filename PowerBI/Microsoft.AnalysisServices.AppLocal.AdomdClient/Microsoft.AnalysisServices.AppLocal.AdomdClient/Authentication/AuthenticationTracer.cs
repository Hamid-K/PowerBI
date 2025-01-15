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
		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x00032C78 File Offset: 0x00030E78
		public static bool IsTracingEnabled
		{
			get
			{
				return AuthenticationTracer.traceWriter != null;
			}
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x00032C82 File Offset: 0x00030E82
		public static IDisposable StartScope(string scope)
		{
			return new AuthenticationTracer.TraceScope(scope);
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x00032C8A File Offset: 0x00030E8A
		public static void TraceError(string format, params object[] args)
		{
			if (AuthenticationTracer.traceWriter != null)
			{
				AuthenticationTracer.WriteTraceEvent(AuthenticationTracer.TraceEventInfoMask.Default, TraceEventType.Error, format, args);
			}
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x00032C9D File Offset: 0x00030E9D
		public static void TraceWarning(string format, params object[] args)
		{
			if (AuthenticationTracer.traceWriter != null)
			{
				AuthenticationTracer.WriteTraceEvent(AuthenticationTracer.TraceEventInfoMask.Default, TraceEventType.Warning, format, args);
			}
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00032CB0 File Offset: 0x00030EB0
		public static void TraceInformation(string format, params object[] args)
		{
			if (AuthenticationTracer.traceWriter != null)
			{
				AuthenticationTracer.WriteTraceEvent(AuthenticationTracer.TraceEventInfoMask.Default, TraceEventType.Information, format, args);
			}
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x00032CC3 File Offset: 0x00030EC3
		[Conditional("DEBUG")]
		public static void TraceDebug(string format, params object[] args)
		{
			if (AuthenticationTracer.traceWriter != null)
			{
				AuthenticationTracer.WriteTraceEvent(AuthenticationTracer.TraceEventInfoMask.Default, TraceEventType.Verbose, format, args);
			}
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x00032CD8 File Offset: 0x00030ED8
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

		// Token: 0x06000F04 RID: 3844 RVA: 0x00032D94 File Offset: 0x00030F94
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

		// Token: 0x06000F05 RID: 3845 RVA: 0x00032DE0 File Offset: 0x00030FE0
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

		// Token: 0x06000F06 RID: 3846 RVA: 0x00032F24 File Offset: 0x00031124
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

		// Token: 0x06000F07 RID: 3847 RVA: 0x00032F84 File Offset: 0x00031184
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

		// Token: 0x0400089D RID: 2205
		private const string AppDomainKey_TraceLock = "MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_TRACE_LOCK";

		// Token: 0x0400089E RID: 2206
		private const string AppDomainKey_TraceWriter = "MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_TRACE_WRITER";

		// Token: 0x0400089F RID: 2207
		private const string AppDomainKey_TraceScopeId = "MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_TRACE_SCOPE_ID";

		// Token: 0x040008A0 RID: 2208
		private const string EnvironmentVariableName_Logging = "MS_AS_AADAUTHENTICATOR_LOG";

		// Token: 0x040008A1 RID: 2209
		private const string EnvironmentVariableName_LogFile = "MS_AS_AADAUTHENTICATOR_LOGFILE";

		// Token: 0x040008A2 RID: 2210
		private const string DefaultLogFileName = "Microsoft.AnalysisServices.Authentication.log";

		// Token: 0x040008A3 RID: 2211
		private static TextWriter traceWriter = AuthenticationTracer.Initialize();

		// Token: 0x040008A4 RID: 2212
		[ThreadStatic]
		private static int currentScopeId = -1;

		// Token: 0x020001D3 RID: 467
		[Flags]
		private enum TraceEventInfoMask
		{
			// Token: 0x04000E3B RID: 3643
			None = 0,
			// Token: 0x04000E3C RID: 3644
			EventType = 1,
			// Token: 0x04000E3D RID: 3645
			CurrentTime = 16,
			// Token: 0x04000E3E RID: 3646
			ScopeId = 32,
			// Token: 0x04000E3F RID: 3647
			ThreadId = 64,
			// Token: 0x04000E40 RID: 3648
			Default = 49,
			// Token: 0x04000E41 RID: 3649
			All = 113
		}

		// Token: 0x020001D4 RID: 468
		private sealed class TraceScope : Disposable
		{
			// Token: 0x060013FC RID: 5116 RVA: 0x000455B9 File Offset: 0x000437B9
			public TraceScope(string scope)
			{
				this.scope = scope;
				this.previousScopeId = AuthenticationTracer.StartScopeImpl(scope);
			}

			// Token: 0x060013FD RID: 5117 RVA: 0x000455D4 File Offset: 0x000437D4
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					AuthenticationTracer.CompleteScopeImpl(this.scope, this.previousScopeId);
				}
				base.Dispose(disposing);
			}

			// Token: 0x04000E42 RID: 3650
			private string scope;

			// Token: 0x04000E43 RID: 3651
			private int previousScopeId;
		}

		// Token: 0x020001D5 RID: 469
		private sealed class TraceStreamWriter : StreamWriter
		{
			// Token: 0x060013FE RID: 5118 RVA: 0x000455F4 File Offset: 0x000437F4
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

			// Token: 0x060013FF RID: 5119 RVA: 0x00045674 File Offset: 0x00043874
			private static string GetTracePath()
			{
				string text = Environment.GetEnvironmentVariable("MS_AS_AADAUTHENTICATOR_LOGFILE");
				if (string.IsNullOrEmpty(text))
				{
					text = Path.Combine(Path.GetTempPath(), "Microsoft.AnalysisServices.Authentication.log");
				}
				return text;
			}

			// Token: 0x06001400 RID: 5120 RVA: 0x000456A5 File Offset: 0x000438A5
			private void Cleanup()
			{
				base.WriteLine("========================================================================");
				base.Flush();
			}
		}
	}
}
