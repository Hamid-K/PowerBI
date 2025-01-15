using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001D5 RID: 469
	public static class ExtendedDiagnostics
	{
		// Token: 0x06000C15 RID: 3093 RVA: 0x0002A223 File Offset: 0x00028423
		[ContractAnnotation("=> halt")]
		public static void TerminateCurrentProcess(int exitCode)
		{
			ExtendedDiagnostics.NativeMethods.TerminateProcess(ExtendedDiagnostics.NativeMethods.GetCurrentProcess(), exitCode);
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0002A234 File Offset: 0x00028434
		public static bool TrySetProcessWideSymbolPath(Assembly pivot)
		{
			string text = Environment.GetEnvironmentVariable("_NT_SYMBOL_PATH");
			if (!string.IsNullOrWhiteSpace(text))
			{
				return true;
			}
			string location = pivot.Location;
			string fileName = Path.GetFileName(location);
			string directoryName = Path.GetDirectoryName(location);
			string text2 = Path.GetFileNameWithoutExtension(location) + ".pdb";
			if (File.Exists(Path.Combine(directoryName, text2)))
			{
				return true;
			}
			text = Path.Combine(directoryName, "..", "symbols");
			string text3 = Path.GetExtension(fileName);
			if (text3.StartsWith(".", StringComparison.Ordinal))
			{
				text3 = text3.Remove(0, 1);
			}
			if (File.Exists(Path.Combine(text, text3, text2)))
			{
				Environment.SetEnvironmentVariable("_NT_SYMBOL_PATH", text);
				return true;
			}
			return false;
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x0002A2DB File Offset: 0x000284DB
		public static string LeakDetectionAssemblyExclusionListTweakName
		{
			get
			{
				return "Microsoft.Cloud.Platform.Utils.ExtendedDiagnostics.LeakDetectionAssemblyExclusionList";
			}
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0002A2E2 File Offset: 0x000284E2
		public static bool IsDebuggerAttached()
		{
			return ExtendedDiagnostics.NativeMethods.IsDebuggerPresent() != 0 || Debugger.IsAttached;
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0002A2F4 File Offset: 0x000284F4
		public static void AlertDebugger(AlertDebuggerAction alertDebuggerAction)
		{
			if ((alertDebuggerAction == AlertDebuggerAction.LaunchManagedDebugger || (alertDebuggerAction == AlertDebuggerAction.TweakBased && ExtendedDiagnostics.s_attachDebuggerIfNeededTweak != null && ExtendedDiagnostics.s_attachDebuggerIfNeededTweak.Value)) && ExtendedDiagnostics.NativeMethods.IsDebuggerPresent() == 0 && !Debugger.IsAttached)
			{
				if (Environment.UserInteractive)
				{
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "Starting a debugger to debug this process.");
					Debugger.Launch();
				}
				else
				{
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "Not starting a debugger to debug this process, as it is not user interactive; please attach directly");
					DiagnosticsAlert.SendMessageToAllInteractiveSessions(string.Concat(new object[]
					{
						"Process ",
						CurrentProcess.Name,
						" with ID ",
						CurrentProcess.Id,
						" is attempting to alert a debugger, but none is attached; please attach a debugger"
					}), CurrentProcess.Name + " alert", 0, (int)TimeSpan.FromMinutes(1.0).TotalMilliseconds);
				}
			}
			while (ExtendedDiagnostics.NativeMethods.IsDebuggerPresent() == 0 && !Debugger.IsAttached)
			{
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "Waiting for the debugger to attach....");
				Thread.Sleep(1000);
			}
			if (Debugger.IsAttached)
			{
				Debugger.Break();
				return;
			}
			ExtendedDiagnostics.NativeMethods.DebugBreak();
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0002A40B File Offset: 0x0002860B
		public static void AlertDebuggerIfAttached()
		{
			if (Debugger.IsAttached)
			{
				Debugger.Break();
				return;
			}
			if (ExtendedDiagnostics.NativeMethods.IsDebuggerPresent() != 0)
			{
				ExtendedDiagnostics.NativeMethods.DebugBreak();
			}
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0002A428 File Offset: 0x00028628
		public static void Initialize()
		{
			ExtendedDiagnostics.TrySetProcessWideSymbolPath(Assembly.GetAssembly(typeof(ExtendedDiagnostics)));
			ExtendedTraceListener.Initialize();
			ExtendedDiagnostics.OnLeakDetectionAssemblyExclusionListTweakChanged(Anchor.Tweaks.RegisterTweak<string>("Microsoft.Cloud.Platform.Utils.ExtendedDiagnostics.LeakDetectionAssemblyExclusionList", "When set, leaks will not be checked for in the listed assemblies", new Action<Tweak>(ExtendedDiagnostics.OnLeakDetectionAssemblyExclusionListTweakChanged), string.Empty));
			ExtendedDiagnostics.s_attachDebuggerIfNeededTweak = Anchor.Tweaks.RegisterTweak<bool>("Microsoft.Cloud.Platform.Utils.ExtendedDiagnostics.AttachDebuggerIfNeeded", "When set, the ExtendedDiagnostics.AlertDebugger will attempt to launch a managed debugger if no debugger is already attached", true);
			AppDomain.CurrentDomain.UnhandledException += ExtendedDiagnostics.OnUnhandledException;
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0002A4A9 File Offset: 0x000286A9
		private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			ExtendedDiagnostics.s_hasUnhandledExceptionOccurred.InterlockedWrite(true);
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0002A4B8 File Offset: 0x000286B8
		private static void OnLeakDetectionAssemblyExclusionListTweakChanged(Tweak tweak)
		{
			string value = ((Tweak<string>)tweak).Value;
			Regex regex = (string.IsNullOrEmpty(value) ? null : new Regex(value, RegexOptions.Compiled));
			ExtendedDiagnostics.s_leakDetectionAssemblyExclusionListExpression.VolatileWrite(regex);
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0002A4F0 File Offset: 0x000286F0
		internal static bool IsLeakReportEnabled(StackTrace creationCallStack)
		{
			if (ExtendedDiagnostics.s_hasUnhandledExceptionOccurred.InterlockedRead())
			{
				return false;
			}
			if (Environment.HasShutdownStarted)
			{
				return false;
			}
			if (creationCallStack == null)
			{
				return true;
			}
			bool flag = true;
			Regex regex = ExtendedDiagnostics.s_leakDetectionAssemblyExclusionListExpression.VolatileRead();
			if (regex != null && creationCallStack != null)
			{
				StackFrame[] frames = creationCallStack.GetFrames();
				for (int i = 0; i < frames.Length; i++)
				{
					string name = frames[i].GetMethod().Module.Name;
					if (regex.Match(name).Success)
					{
						flag = false;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x00009B3B File Offset: 0x00007D3B
		[Conditional("DEBUG")]
		private static void AssertLeakIfEnabled(string shortFormat, string longFormat, string arg0, string arg1, CallStackRef callStack)
		{
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0002A569 File Offset: 0x00028769
		[Conditional("DEBUG")]
		public static void AssertDisposeNotInvoked(string @class, CallStackRef callStack)
		{
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "{0}.Dispose() should have been called prior to {1}.Finalize().", new object[] { @class, @class });
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0002A589 File Offset: 0x00028789
		[Conditional("DEBUG")]
		public static void AssertShutdownNotInvoked(string @class, CallStackRef callStack)
		{
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "{0}.Shutdown() should have been called prior to {1}.Finalize().", new object[] { @class, @class });
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0002A5AC File Offset: 0x000287AC
		[ContractAnnotation("=> halt")]
		public static void EnsureInvalidSwitchValue<T>(T value)
		{
			string text = ExtendedDiagnostics.AssertInvalidSwitchValue<T>(value, 2);
			throw new ArgumentOutOfRangeException("?", value, text);
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x00009B3B File Offset: 0x00007D3B
		[Conditional("DEBUG")]
		[ContractAnnotation("=> halt")]
		public static void AssertInvalidSwitchValue<T>(T value)
		{
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0002A5D4 File Offset: 0x000287D4
		[ContractAnnotation("=> halt")]
		private static string AssertInvalidSwitchValue<T>(T value, int stackFramesToSkip)
		{
			StackFrame stackFrame = new StackFrame(stackFramesToSkip, true);
			string fullyQualifiedMemberName = ExtendedReflection.GetFullyQualifiedMemberName(stackFrame.GetMethod());
			string text = string.Format(CultureInfo.InvariantCulture, "Invalid switch/case: Value '{0}' of type '{1}' is illegal in method '{2}' ({3})", new object[]
			{
				value,
				typeof(T).Name,
				fullyQualifiedMemberName,
				stackFrame
			});
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[] { text });
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[]
			{
				new StackTrace(0, true)
			});
			return text;
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0002A66C File Offset: 0x0002886C
		[ContractAnnotation("findResult:notnull=>halt")]
		public static void EnsureArgumentNotInCollection<T>(T findResult, [InvokerParameterName] string argName, string collectionName) where T : class
		{
			if (findResult == null)
			{
				return;
			}
			StackFrame stackFrame = new StackFrame(1, true);
			string fullyQualifiedMemberName = ExtendedReflection.GetFullyQualifiedMemberName(stackFrame.GetMethod());
			string text = string.Format(CultureInfo.InvariantCulture, "Argument '{0}' of value '{1}' cannot be added to collection '{2}' by method '{3}' as it already exists in the collection ({4})", new object[] { argName, findResult, collectionName, fullyQualifiedMemberName, stackFrame });
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[] { text });
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[]
			{
				new StackTrace(0, true)
			});
			throw new ArgumentException(text, argName);
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0002A706 File Offset: 0x00028906
		[ContractAnnotation("value:null=> halt")]
		public static void EnsureArgumentNotNull<T>([NoEnumeration] T value, [InvokerParameterName] string argName)
		{
			Ensure.ArgNotNull<T>(value, argName);
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0002A70F File Offset: 0x0002890F
		[ContractAnnotation("value:null=> halt")]
		public static void EnsureNotNull<T>([NoEnumeration] T value, string who) where T : class
		{
			Ensure.IsNotNull<T>(value, who);
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0002A718 File Offset: 0x00028918
		[ContractAnnotation("value:notnull=> halt")]
		public static void EnsureNull<T>([NoEnumeration] T value, string who) where T : class
		{
			Ensure.IsNull<T>(value, who);
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0002A721 File Offset: 0x00028921
		[ContractAnnotation("value:null=> halt")]
		public static void EnsureArrayNotNullOrEmpty(Array value, [InvokerParameterName] string argName)
		{
			Ensure.IsNotNull<Array>(value, argName);
			ExtendedDiagnostics.EnsureArgumentIsPositive((long)value.Length, argName, 2);
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0002A738 File Offset: 0x00028938
		[ContractAnnotation("value:null=> halt")]
		public static void EnsureCollectionNotNullOrEmpty<T>(ICollection<T> value, [InvokerParameterName] string argName)
		{
			Ensure.IsNotNull<ICollection<T>>(value, argName);
			ExtendedDiagnostics.EnsureArgumentIsPositive((long)value.Count, argName, 2);
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0002A750 File Offset: 0x00028950
		[ContractAnnotation("value:null=> halt")]
		public static void EnsureEnumerableNotNullOrEmpty<T>(IEnumerable<T> value, [InvokerParameterName] string argName)
		{
			Ensure.IsNotNull<IEnumerable<T>>(value, argName);
			if (value.None<T>())
			{
				string text = "Constraint: '{0}' is not empty has been violated.".FormatWithInvariantCulture(new object[] { argName });
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[] { text });
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[]
				{
					new StackTrace(0, true)
				});
				throw new ArgumentException(argName, text);
			}
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0002A7C3 File Offset: 0x000289C3
		[ContractAnnotation("value:null=> halt")]
		public static void EnsureStringNotNullOrEmpty(string value, [InvokerParameterName] string argName)
		{
			Ensure.ArgNotNullOrEmpty(value, argName);
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0002A7CC File Offset: 0x000289CC
		public static void EnsureAllStringsNotNullOrEmpty(IList<string> values, [InvokerParameterName] string argName)
		{
			if (values == null)
			{
				return;
			}
			for (int i = 0; i < values.Count; i++)
			{
				ExtendedDiagnostics.EnsureStringNotNullOrEmpty(values[i], "{0}[{1}]".FormatWithInvariantCulture(new object[] { argName, i }));
			}
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x0002A818 File Offset: 0x00028A18
		public static void EnsureAllNotNull<T>(IReadOnlyList<T> values, [InvokerParameterName] string argName) where T : class
		{
			if (values == null)
			{
				return;
			}
			for (int i = 0; i < values.Count; i++)
			{
				ExtendedDiagnostics.EnsureNotNull<T>(values[i], "{0}[{1}]".FormatWithInvariantCulture(new object[] { argName, i }));
			}
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0002A863 File Offset: 0x00028A63
		[ContractAnnotation("condition:false=> halt")]
		public static void EnsureOperation(bool condition, string message)
		{
			Ensure.IsTrue(condition, message);
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0002A86C File Offset: 0x00028A6C
		[ContractAnnotation("condition:false=> halt")]
		public static void EnsureArgument(TimeSpan value, [InvokerParameterName] string argName, bool condition)
		{
			if (!condition)
			{
				ExtendedDiagnostics.EnsureArgument(value.TotalMilliseconds.ToString(), argName, condition);
			}
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x0002A892 File Offset: 0x00028A92
		[ContractAnnotation("condition:false=> halt")]
		public static void EnsureArgument(short value, [InvokerParameterName] string argName, bool condition)
		{
			if (!condition)
			{
				ExtendedDiagnostics.EnsureArgument(value.ToString(CultureInfo.InvariantCulture), argName, condition);
			}
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0002A8AA File Offset: 0x00028AAA
		[ContractAnnotation("condition:false=> halt")]
		public static void EnsureArgument(int value, [InvokerParameterName] string argName, bool condition)
		{
			if (!condition)
			{
				ExtendedDiagnostics.EnsureArgument(value.ToString(CultureInfo.InvariantCulture), argName, condition);
			}
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0002A8C2 File Offset: 0x00028AC2
		[ContractAnnotation("condition:false=> halt")]
		public static void EnsureArgument(long value, [InvokerParameterName] string argName, bool condition)
		{
			if (!condition)
			{
				ExtendedDiagnostics.EnsureArgument(value.ToString(CultureInfo.InvariantCulture), argName, condition);
			}
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0002A8DA File Offset: 0x00028ADA
		[ContractAnnotation("condition:false=> halt")]
		public static void EnsureArgument(double value, [InvokerParameterName] string argName, bool condition)
		{
			if (!condition)
			{
				ExtendedDiagnostics.EnsureArgument(value.ToString(CultureInfo.InvariantCulture), argName, condition);
			}
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x0002A8F2 File Offset: 0x00028AF2
		[ContractAnnotation("condition:false=> halt")]
		public static void EnsureArgument(string value, [InvokerParameterName] string argName, bool condition)
		{
			Ensure.ArgSatisfiesCondition(value, argName, condition);
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0002A8FC File Offset: 0x00028AFC
		[ContractAnnotation("condition:false=> halt")]
		public static void EnsureArgument([InvokerParameterName] string argName, bool condition, string msg)
		{
			Ensure.ArgSatisfiesCondition(argName, condition, msg);
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0002A908 File Offset: 0x00028B08
		[Conditional("DEBUG")]
		[ContractAnnotation("=> halt")]
		public static void AssertFail(string message)
		{
			StackFrame stackFrame = new StackFrame(1, true);
			string fullyQualifiedMemberName = ExtendedReflection.GetFullyQualifiedMemberName(stackFrame.GetMethod());
			string text = string.Format(CultureInfo.InvariantCulture, "Failure was found: '{0}' in method '{1}' ({2})", new object[] { message, fullyQualifiedMemberName, stackFrame });
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[] { text });
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[]
			{
				new StackTrace(0, true)
			});
			ExtendedEnvironment.FailSlow("ExtendedDiagnostics.AssertFail", message);
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0002A990 File Offset: 0x00028B90
		public static void EnsureArgumentIsNotNegative(short value, [InvokerParameterName] string argName)
		{
			ExtendedDiagnostics.EnsureArgumentIsNotNegative((long)value, argName, 2);
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0002A990 File Offset: 0x00028B90
		public static void EnsureArgumentIsNotNegative(int value, [InvokerParameterName] string argName)
		{
			ExtendedDiagnostics.EnsureArgumentIsNotNegative((long)value, argName, 2);
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0002A99B File Offset: 0x00028B9B
		[ContractAnnotation("throwOnNull:true,value:null=> halt")]
		public static void EnsureArgumentIsNotNegative(int? value, [InvokerParameterName] string argName, bool throwOnNull)
		{
			if (throwOnNull)
			{
				ExtendedDiagnostics.EnsureArgumentNotNull<int?>(value, argName);
			}
			if (value != null)
			{
				ExtendedDiagnostics.EnsureArgumentIsNotNegative((long)value.Value, argName, 2);
			}
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0002A9BF File Offset: 0x00028BBF
		public static void EnsureArgumentIsNotNegative(long value, [InvokerParameterName] string argName)
		{
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(value, argName, 2);
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0002A9C9 File Offset: 0x00028BC9
		private static void EnsureArgumentIsNotNegative(long value, [InvokerParameterName] string argName, int stackFramesToSkip)
		{
			Ensure.ArgIsNotNegative(value, argName, stackFramesToSkip + 1);
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0002A9D5 File Offset: 0x00028BD5
		public static void EnsureArgumentIsNotNegative(TimeSpan value, [InvokerParameterName] string argName)
		{
			Ensure.ArgIsNotNegative(value, argName);
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0002A9DE File Offset: 0x00028BDE
		public static void EnsureArgumentIsBetween(short value, short begin, short end, [InvokerParameterName] string argName)
		{
			ExtendedDiagnostics.EnsureArgumentIsBetween((float)value, (float)begin, (float)end, argName, 2);
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0002A9DE File Offset: 0x00028BDE
		public static void EnsureArgumentIsBetween(int value, int begin, int end, [InvokerParameterName] string argName)
		{
			ExtendedDiagnostics.EnsureArgumentIsBetween((float)value, (float)begin, (float)end, argName, 2);
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0002A9DE File Offset: 0x00028BDE
		public static void EnsureArgumentIsBetween(long value, long begin, long end, [InvokerParameterName] string argName)
		{
			ExtendedDiagnostics.EnsureArgumentIsBetween((float)value, (float)begin, (float)end, argName, 2);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0002A9ED File Offset: 0x00028BED
		public static void EnsureArgumentIsBetween(float value, float begin, float end, [InvokerParameterName] string argName)
		{
			ExtendedDiagnostics.EnsureArgumentIsBetween(value, begin, end, argName, 2);
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0002A9F9 File Offset: 0x00028BF9
		public static void EnsureArgumentIsBetween(double value, double begin, double end, [InvokerParameterName] string argName)
		{
			Ensure.ArgIsInRange<double>(value, begin, end, argName, 2);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0002AA05 File Offset: 0x00028C05
		public static void EnsureArgumentIsBetween(DateTime value, DateTime begin, DateTime end, [InvokerParameterName] string argName)
		{
			Ensure.ArgIsInRange<DateTime>(value, begin, end, argName, 2);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0002AA11 File Offset: 0x00028C11
		public static void EnsureArgumentIsBetween(TimeSpan value, TimeSpan begin, TimeSpan end, [InvokerParameterName] string argName)
		{
			Ensure.ArgIsInRange<TimeSpan>(value, begin, end, argName, 2);
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0002AA1D File Offset: 0x00028C1D
		private static void EnsureArgumentIsBetween(float value, float begin, float end, [InvokerParameterName] string argName, int stackFramesToSkip)
		{
			Ensure.ArgIsInRange<float>(value, begin, end, argName, stackFramesToSkip + 1);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x0002AA2C File Offset: 0x00028C2C
		public static void EnsureArgumentIsPositive(short value, [InvokerParameterName] string argName)
		{
			ExtendedDiagnostics.EnsureArgumentIsPositive((long)value, argName, 2);
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0002AA2C File Offset: 0x00028C2C
		public static void EnsureArgumentIsPositive(int value, [InvokerParameterName] string argName)
		{
			ExtendedDiagnostics.EnsureArgumentIsPositive((long)value, argName, 2);
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0002AA37 File Offset: 0x00028C37
		public static void EnsureArgumentIsPositive(long value, [InvokerParameterName] string argName)
		{
			ExtendedDiagnostics.EnsureArgumentIsPositive(value, argName, 2);
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0002AA41 File Offset: 0x00028C41
		public static void EnsureArgumentIsPositive(TimeSpan value, [InvokerParameterName] string argName)
		{
			Ensure.ArgIsPositive(value, argName);
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0002AA4A File Offset: 0x00028C4A
		public static void EnsureArgumentIsOfType(object value, Type type, [InvokerParameterName] string argName)
		{
			Ensure.ArgSatisfiesCondition(argName, value.GetType().Equals(type), "Argument '{0}' is expected to be of type '{1}'. Actual type: '{2}'".FormatWithInvariantCulture(new object[]
			{
				argName,
				type,
				value.GetType()
			}));
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0002AA7F File Offset: 0x00028C7F
		public static void EnsureArgumentIsOfType<T>(object value, [InvokerParameterName] string argName)
		{
			Ensure.ArgSatisfiesCondition(argName, value is T, "Argument '{0}' is expected to be of type '{1}'. Actual type: '{2}'".FormatWithInvariantCulture(new object[]
			{
				argName,
				typeof(T).Name,
				value.GetType()
			}));
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0002AAC0 File Offset: 0x00028CC0
		public static void EnsureArgumentIsNotOfType(object value, Type type, [InvokerParameterName] string argName)
		{
			Ensure.ArgSatisfiesCondition(argName, !value.GetType().IsSubclassOf(type) && !value.GetType().Equals(type), "Argument '{0}' is expected NOT to be of type '{1}'. Actual type: '{2}'".FormatWithInvariantCulture(new object[]
			{
				argName,
				type,
				value.GetType()
			}));
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0002AB14 File Offset: 0x00028D14
		public static void EnsureArgumentIsSubTypeOrImplementsInterfaceOf(object value, Type type, [InvokerParameterName] string argName)
		{
			Ensure.ArgSatisfiesCondition(argName, type.IsAssignableFrom(value.GetType()), "Argument '{0}' is expected to be a sub type or implement interface of type '{1}'. Actual type: '{2}'".FormatWithInvariantCulture(new object[]
			{
				argName,
				type,
				value.GetType()
			}));
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0002AB49 File Offset: 0x00028D49
		private static void EnsureArgumentIsPositive(long value, string argName, int stackFramesToSkip)
		{
			Ensure.ArgIsPositive(value, argName, stackFramesToSkip + 1);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0002AB55 File Offset: 0x00028D55
		public static void EnsureValueIsUTCKind(DateTime value, string argName)
		{
			Ensure.ArgSatisfiesCondition(argName, value.Kind == DateTimeKind.Utc, "{0}.Kind must be UTC".FormatWithInvariantCulture(new object[] { argName }));
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0002AB7B File Offset: 0x00028D7B
		[CLSCompliant(false)]
		public static void EnsureEnumIsDefinedSlow<T>(T value, string name) where T : struct, IComparable, IFormattable, IConvertible
		{
			Ensure.SlowEnumIsDefined<T>(value, name, 0);
		}

		// Token: 0x040004A2 RID: 1186
		internal const string c_attachDebuggerIfNeededTweakName = "Microsoft.Cloud.Platform.Utils.ExtendedDiagnostics.AttachDebuggerIfNeeded";

		// Token: 0x040004A3 RID: 1187
		internal const string c_leakDetectionAssemblyExclusionListTweakName = "Microsoft.Cloud.Platform.Utils.ExtendedDiagnostics.LeakDetectionAssemblyExclusionList";

		// Token: 0x040004A4 RID: 1188
		private static VolatileRef<Regex> s_leakDetectionAssemblyExclusionListExpression;

		// Token: 0x040004A5 RID: 1189
		private static Tweak<bool> s_attachDebuggerIfNeededTweak;

		// Token: 0x040004A6 RID: 1190
		private static InterlockedBool s_hasUnhandledExceptionOccurred;

		// Token: 0x02000688 RID: 1672
		internal static class NativeMethods
		{
			// Token: 0x06002DD6 RID: 11734
			[DllImport("kernel32.dll")]
			internal static extern int IsDebuggerPresent();

			// Token: 0x06002DD7 RID: 11735
			[DllImport("kernel32.dll")]
			internal static extern void DebugBreak();

			// Token: 0x06002DD8 RID: 11736
			[DllImport("kernel32")]
			internal static extern IntPtr GetCurrentProcess();

			// Token: 0x06002DD9 RID: 11737
			[DllImport("kernel32.dll")]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool TerminateProcess(IntPtr hProcess, int uExitCode);
		}
	}
}
