using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.AnalysisServices.AzureClient.Utilities;

namespace Microsoft.AnalysisServices.AzureClient.Authentication
{
	// Token: 0x0200001C RID: 28
	internal static class AuthenticationTracer
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003D46 File Offset: 0x00001F46
		public static bool IsTracingEnabled
		{
			get
			{
				return AuthenticationTracer.traceWriter != null;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003D50 File Offset: 0x00001F50
		public static IDisposable StartScope(string scope)
		{
			return new AuthenticationTracer.TraceScope(scope);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003D58 File Offset: 0x00001F58
		public static void TraceError(string format, params object[] args)
		{
			if (AuthenticationTracer.traceWriter != null)
			{
				AuthenticationTracer.WriteTraceEvent(AuthenticationTracer.TraceEventInfoMask.Default, TraceEventType.Error, format, args);
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003D6B File Offset: 0x00001F6B
		public static void TraceWarning(string format, params object[] args)
		{
			if (AuthenticationTracer.traceWriter != null)
			{
				AuthenticationTracer.WriteTraceEvent(AuthenticationTracer.TraceEventInfoMask.Default, TraceEventType.Warning, format, args);
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003D7E File Offset: 0x00001F7E
		public static void TraceInformation(string format, params object[] args)
		{
			if (AuthenticationTracer.traceWriter != null)
			{
				AuthenticationTracer.WriteTraceEvent(AuthenticationTracer.TraceEventInfoMask.Default, TraceEventType.Information, format, args);
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003D91 File Offset: 0x00001F91
		[Conditional("DEBUG")]
		public static void TraceDebug(string format, params object[] args)
		{
			if (AuthenticationTracer.traceWriter != null)
			{
				AuthenticationTracer.WriteTraceEvent(AuthenticationTracer.TraceEventInfoMask.Default, TraceEventType.Verbose, format, args);
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003DA8 File Offset: 0x00001FA8
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

		// Token: 0x060000C7 RID: 199 RVA: 0x00003E64 File Offset: 0x00002064
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

		// Token: 0x060000C8 RID: 200 RVA: 0x00003EB0 File Offset: 0x000020B0
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

		// Token: 0x060000C9 RID: 201 RVA: 0x00003FF4 File Offset: 0x000021F4
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

		// Token: 0x060000CA RID: 202 RVA: 0x00004054 File Offset: 0x00002254
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

		// Token: 0x0400006D RID: 109
		private const string AppDomainKey_TraceLock = "MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_TRACE_LOCK";

		// Token: 0x0400006E RID: 110
		private const string AppDomainKey_TraceWriter = "MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_TRACE_WRITER";

		// Token: 0x0400006F RID: 111
		private const string AppDomainKey_TraceScopeId = "MICROSOFT_ANALYSISSERVICES_XMLA_LIBS_TRACE_SCOPE_ID";

		// Token: 0x04000070 RID: 112
		private const string EnvironmentVariableName_Logging = "MS_AS_AADAUTHENTICATOR_LOG";

		// Token: 0x04000071 RID: 113
		private const string EnvironmentVariableName_LogFile = "MS_AS_AADAUTHENTICATOR_LOGFILE";

		// Token: 0x04000072 RID: 114
		private const string DefaultLogFileName = "Microsoft.AnalysisServices.Authentication.log";

		// Token: 0x04000073 RID: 115
		private static TextWriter traceWriter = AuthenticationTracer.Initialize();

		// Token: 0x04000074 RID: 116
		[ThreadStatic]
		private static int currentScopeId = -1;

		// Token: 0x0200004D RID: 77
		[Flags]
		private enum TraceEventInfoMask
		{
			// Token: 0x04000198 RID: 408
			None = 0,
			// Token: 0x04000199 RID: 409
			EventType = 1,
			// Token: 0x0400019A RID: 410
			CurrentTime = 16,
			// Token: 0x0400019B RID: 411
			ScopeId = 32,
			// Token: 0x0400019C RID: 412
			ThreadId = 64,
			// Token: 0x0400019D RID: 413
			Default = 49,
			// Token: 0x0400019E RID: 414
			All = 113
		}

		// Token: 0x0200004E RID: 78
		private sealed class TraceScope : Disposable
		{
			// Token: 0x0600023C RID: 572 RVA: 0x0000AEFC File Offset: 0x000090FC
			public TraceScope(string scope)
			{
				this.scope = scope;
				this.previousScopeId = AuthenticationTracer.StartScopeImpl(scope);
			}

			// Token: 0x0600023D RID: 573 RVA: 0x0000AF17 File Offset: 0x00009117
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					AuthenticationTracer.CompleteScopeImpl(this.scope, this.previousScopeId);
				}
				base.Dispose(disposing);
			}

			// Token: 0x0400019F RID: 415
			private string scope;

			// Token: 0x040001A0 RID: 416
			private int previousScopeId;
		}

		// Token: 0x0200004F RID: 79
		private sealed class TraceStreamWriter : StreamWriter
		{
			// Token: 0x0600023E RID: 574 RVA: 0x0000AF34 File Offset: 0x00009134
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

			// Token: 0x0600023F RID: 575 RVA: 0x0000AFB4 File Offset: 0x000091B4
			private static string GetTracePath()
			{
				string text = Environment.GetEnvironmentVariable("MS_AS_AADAUTHENTICATOR_LOGFILE");
				if (string.IsNullOrEmpty(text))
				{
					text = Path.Combine(Path.GetTempPath(), "Microsoft.AnalysisServices.Authentication.log");
				}
				return text;
			}

			// Token: 0x06000240 RID: 576 RVA: 0x0000AFE5 File Offset: 0x000091E5
			private void Cleanup()
			{
				base.WriteLine("========================================================================");
				base.Flush();
			}
		}
	}
}
