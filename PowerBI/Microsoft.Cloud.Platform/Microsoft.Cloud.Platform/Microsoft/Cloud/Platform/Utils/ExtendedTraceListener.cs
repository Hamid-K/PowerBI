using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001D7 RID: 471
	internal class ExtendedTraceListener : TraceListener
	{
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x0002AD60 File Offset: 0x00028F60
		// (set) Token: 0x06000C56 RID: 3158 RVA: 0x0002AD68 File Offset: 0x00028F68
		internal BehaviorOnAssertionFailure BehaviorOnAssertFailure { get; private set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x0002AD71 File Offset: 0x00028F71
		// (set) Token: 0x06000C58 RID: 3160 RVA: 0x0002AD79 File Offset: 0x00028F79
		internal string LogFileName { get; private set; }

		// Token: 0x06000C59 RID: 3161 RVA: 0x0002AD84 File Offset: 0x00028F84
		internal static void Initialize()
		{
			DefaultTraceListener defaultTraceListener = null;
			string text = null;
			BehaviorOnAssertionFailure behaviorOnAssertionFailure = BehaviorOnAssertionFailure.UiEnabled;
			foreach (object obj in Trace.Listeners)
			{
				defaultTraceListener = ((TraceListener)obj) as DefaultTraceListener;
				if (defaultTraceListener != null)
				{
					text = defaultTraceListener.LogFileName;
					behaviorOnAssertionFailure = (defaultTraceListener.AssertUiEnabled ? BehaviorOnAssertionFailure.UiEnabled : BehaviorOnAssertionFailure.Ignore);
					break;
				}
			}
			Anchor.Tweaks.RegisterTweaksFile(Tweaks.GetFileNameWithTweakExtension("diagnostics"));
			ExtendedTraceListener.s_assertBehaviorTweak = Anchor.Tweaks.RegisterTweak<string>("Microsoft.Cloud.Platform.Utils.ExtendedDiagnostics.BehaviorOnAssertionFailure", "Whether to show UI when diagnostics such as debug assertions fail, silently ignore the assertion or throw an exception", behaviorOnAssertionFailure.ToString());
			ExtendedTraceListener.s_logFileNameTweak = Anchor.Tweaks.RegisterTweak<string>("Microsoft.Cloud.Platform.Utils.ExtendedDiagnostics.LogFile", "The name of the log file to write diagnostics messages to", text);
			behaviorOnAssertionFailure = (BehaviorOnAssertionFailure)Enum.Parse(typeof(BehaviorOnAssertionFailure), ExtendedTraceListener.s_assertBehaviorTweak.Value, true);
			text = ExtendedTraceListener.s_logFileNameTweak.Value;
			DiagnosticsAlert.Initialize();
			ExtendedTraceListener.s_extendedTraceListener = new ExtendedTraceListener(behaviorOnAssertionFailure, text);
			Trace.Listeners.Add(ExtendedTraceListener.s_extendedTraceListener);
			if (defaultTraceListener != null)
			{
				Trace.Listeners.Remove(defaultTraceListener);
			}
			ExtendedTraceListener.s_writeTraceListener = defaultTraceListener;
			ExtendedTraceListener.s_writeTraceListenerEnabled = Debugger.IsAttached;
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0002AEBC File Offset: 0x000290BC
		public ExtendedTraceListener(BehaviorOnAssertionFailure behaviorOnAssertionFailure, string logFileName)
		{
			this.m_failTraceListener = new DefaultTraceListener();
			if (logFileName != null)
			{
				this.m_failTraceListener.LogFileName = logFileName;
			}
			this.BehaviorOnAssertFailure = behaviorOnAssertionFailure;
			this.LogFileName = this.m_failTraceListener.LogFileName;
			BehaviorOnAssertionFailure behaviorOnAssertFailure = this.BehaviorOnAssertFailure;
			if (behaviorOnAssertFailure == BehaviorOnAssertionFailure.UiEnabled)
			{
				this.m_failTraceListener.AssertUiEnabled = true;
				return;
			}
			if (behaviorOnAssertFailure - BehaviorOnAssertionFailure.Ignore > 1)
			{
				return;
			}
			this.m_failTraceListener.AssertUiEnabled = false;
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0002AF2B File Offset: 0x0002912B
		public static ExtendedTraceListener SetAndGetPrevious(ExtendedTraceListener etl)
		{
			ExtendedTraceListener extendedTraceListener = ExtendedTraceListener.s_extendedTraceListener;
			ExtendedTraceListener.s_extendedTraceListener = etl;
			return extendedTraceListener;
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0002AF38 File Offset: 0x00029138
		internal static bool SetWriteTraceListenerEnabled(bool val)
		{
			bool flag = ExtendedTraceListener.s_writeTraceListenerEnabled;
			ExtendedTraceListener.s_writeTraceListenerEnabled = val;
			return flag;
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x0002AF45 File Offset: 0x00029145
		public override bool IsThreadSafe
		{
			get
			{
				return !ExtendedTraceListener.s_writeTraceListenerEnabled || ExtendedTraceListener.s_writeTraceListener == null || ExtendedTraceListener.s_writeTraceListener.IsThreadSafe;
			}
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0002AF61 File Offset: 0x00029161
		public override void WriteLine(string message)
		{
			if (ExtendedTraceListener.s_writeTraceListenerEnabled && ExtendedTraceListener.s_writeTraceListener != null)
			{
				ExtendedTraceListener.s_writeTraceListener.WriteLine(message);
			}
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0002AF7C File Offset: 0x0002917C
		public override void WriteLine(string message, string category)
		{
			if (ExtendedTraceListener.s_writeTraceListenerEnabled && ExtendedTraceListener.s_writeTraceListener != null)
			{
				ExtendedTraceListener.s_writeTraceListener.WriteLine(message, category);
			}
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0002AF98 File Offset: 0x00029198
		public override void Write(string message)
		{
			if (ExtendedTraceListener.s_writeTraceListenerEnabled && ExtendedTraceListener.s_writeTraceListener != null)
			{
				ExtendedTraceListener.s_writeTraceListener.Write(message);
			}
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0002AFB3 File Offset: 0x000291B3
		public override void Write(string message, string category)
		{
			if (ExtendedTraceListener.s_writeTraceListenerEnabled && ExtendedTraceListener.s_writeTraceListener != null)
			{
				ExtendedTraceListener.s_writeTraceListener.Write(message, category);
			}
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0002AFCF File Offset: 0x000291CF
		[ContractAnnotation("=> halt")]
		public override void Fail(string message)
		{
			this.Fail(message, string.Empty);
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0002AFE0 File Offset: 0x000291E0
		[ContractAnnotation("=> halt")]
		public override void Fail(string message, string detailMessage)
		{
			string newLine = Environment.NewLine;
			detailMessage = detailMessage + newLine + Debugging.GetContext().StringJoin(newLine);
			detailMessage = detailMessage + newLine + Debugging.GetActivityStack().StringJoin(newLine);
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "Assertion failed! Message='{0}'", new object[] { message });
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "Assertion failed! DetailedMessage='{0}'", new object[] { detailMessage });
			this.m_failTraceListener.Fail(message, detailMessage);
			if (this.BehaviorOnAssertFailure == BehaviorOnAssertionFailure.UiEnabled && !Environment.UserInteractive)
			{
				switch (this.SendWTSAssertionFailedMessage(message, detailMessage))
				{
				case DiagnosticsAlert.WTSResponse.IDABORT:
					ExtendedDiagnostics.TerminateCurrentProcess(1);
					return;
				case DiagnosticsAlert.WTSResponse.IDRETRY:
					ExtendedDiagnostics.AlertDebugger(AlertDebuggerAction.TweakBased);
					return;
				case DiagnosticsAlert.WTSResponse.IDIGNORE:
					break;
				default:
					ExtendedTraceListener.ThrowAssertionFailedException(message, detailMessage);
					return;
				}
			}
			else if (this.BehaviorOnAssertFailure == BehaviorOnAssertionFailure.ThrowException)
			{
				ExtendedTraceListener.ThrowAssertionFailedException(message, detailMessage);
			}
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0002B0B0 File Offset: 0x000292B0
		[ContractAnnotation("=> halt")]
		private static void ThrowAssertionFailedException(string message, string detailMessage)
		{
			string text = message;
			if (!string.IsNullOrEmpty(detailMessage))
			{
				text = string.Format(CultureInfo.InvariantCulture, "{0}: {1}{2}", new object[]
				{
					message,
					Environment.NewLine,
					detailMessage
				});
			}
			throw new AssertionFailedException(text);
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0002B0F4 File Offset: 0x000292F4
		[ContractAnnotation("=> halt")]
		private static bool TryInvokeMstestAssertFail(string message, string detailMessage)
		{
			string combinedMessage = message + Environment.NewLine + detailMessage;
			bool ok = ExtendedTraceListener.TryInvokeStaticPublicMethodInAssembly("Microsoft.Cloud.Platform.UnitTestsUtils", "Microsoft.Cloud.Assert", "Fail", combinedMessage);
			if (ok)
			{
				return true;
			}
			UtilsContext.Current.RunWithClearContext(delegate
			{
				ok = ExtendedTraceListener.TryInvokeStaticPublicMethodInAssembly("Microsoft.VisualStudio.QualityTools.UnitTestFramework", "Microsoft.VisualStudio.TestTools.UnitTesting.Assert", "Fail", combinedMessage);
			});
			return ok;
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x0002B160 File Offset: 0x00029360
		private static bool TryInvokeStaticPublicMethodInAssembly(string assemblyName, string typeName, string methodName, object arg)
		{
			Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault((Assembly ass) => ass.GetName().Name.Equals(assemblyName, StringComparison.OrdinalIgnoreCase));
			if (assembly == null)
			{
				return false;
			}
			Type type = assembly.GetType(typeName);
			if (type == null)
			{
				return false;
			}
			MethodInfo method = type.GetMethod(methodName, new Type[] { typeof(string) });
			if (method == null)
			{
				return false;
			}
			method.Invoke(null, new object[] { arg });
			return true;
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x0002B1F0 File Offset: 0x000293F0
		private DiagnosticsAlert.WTSResponse SendWTSAssertionFailedMessage(string message, string detailMessage)
		{
			bool flag = true;
			StackTrace stackTrace = new StackTrace(6, flag);
			return DiagnosticsAlert.SendMessageToAllInteractiveSessions(string.Format(CultureInfo.InvariantCulture, "{0}: {1}{2}{3}{4}", new object[]
			{
				message,
				Environment.NewLine,
				detailMessage,
				Environment.NewLine,
				stackTrace
			}), "Assertion Failed: Abort=Quit, Retry=Debug, Ignore=Continue", 2, 30000);
		}

		// Token: 0x040004AA RID: 1194
		private const string c_messageBoxTitle = "Assertion Failed: Abort=Quit, Retry=Debug, Ignore=Continue";

		// Token: 0x040004AB RID: 1195
		private const int c_messageBoxTimeoutInSeconds = 30;

		// Token: 0x040004AC RID: 1196
		internal const string c_assertBehaviorTweakName = "Microsoft.Cloud.Platform.Utils.ExtendedDiagnostics.BehaviorOnAssertionFailure";

		// Token: 0x040004AD RID: 1197
		internal const string c_LogFileTweakName = "Microsoft.Cloud.Platform.Utils.ExtendedDiagnostics.LogFile";

		// Token: 0x040004AE RID: 1198
		private static Tweak<string> s_assertBehaviorTweak;

		// Token: 0x040004AF RID: 1199
		private static Tweak<string> s_logFileNameTweak;

		// Token: 0x040004B0 RID: 1200
		private static ExtendedTraceListener s_extendedTraceListener;

		// Token: 0x040004B1 RID: 1201
		private DefaultTraceListener m_failTraceListener;

		// Token: 0x040004B2 RID: 1202
		private static DefaultTraceListener s_writeTraceListener;

		// Token: 0x040004B3 RID: 1203
		private static bool s_writeTraceListenerEnabled;
	}
}
