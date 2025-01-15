using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using NLog.Common;
using NLog.Config;
using NLog.Filters;
using NLog.Internal;
using NLog.Targets;

namespace NLog
{
	// Token: 0x0200000F RID: 15
	internal static class LoggerImpl
	{
		// Token: 0x060003CC RID: 972 RVA: 0x000076A4 File Offset: 0x000058A4
		internal static void Write([NotNull] Type loggerType, [NotNull] TargetWithFilterChain targetsForLevel, LogEventInfo logEvent, LogFactory factory)
		{
			StackTraceUsage stackTraceUsage = targetsForLevel.GetStackTraceUsage();
			if (stackTraceUsage != StackTraceUsage.None && !logEvent.HasStackTrace)
			{
				StackTrace stackTrace = new StackTrace(0, stackTraceUsage == StackTraceUsage.WithSource);
				StackFrame[] frames = stackTrace.GetFrames();
				int? num = LoggerImpl.FindCallingMethodOnStackTrace(frames, loggerType);
				int? num2 = ((num != null) ? new int?(LoggerImpl.SkipToUserStackFrameLegacy(frames, num.Value)) : null);
				logEvent.GetCallSiteInformationInternal().SetStackTrace(stackTrace, num ?? 0, num2);
			}
			AsyncContinuation asyncContinuation = delegate(Exception ex)
			{
			};
			if (factory.ThrowExceptions)
			{
				int originalThreadId = AsyncHelpers.GetManagedThreadId();
				asyncContinuation = delegate(Exception ex)
				{
					if (ex != null && AsyncHelpers.GetManagedThreadId() == originalThreadId)
					{
						throw new NLogRuntimeException("Exception occurred in NLog", ex);
					}
				};
			}
			if (targetsForLevel.NextInChain == null && logEvent.CanLogEventDeferMessageFormat())
			{
				logEvent.MessageFormatter = LogMessageTemplateFormatter.DefaultAutoSingleTarget.MessageFormatter;
			}
			IList<Filter> list = null;
			FilterResult filterResult = FilterResult.Neutral;
			for (TargetWithFilterChain targetWithFilterChain = targetsForLevel; targetWithFilterChain != null; targetWithFilterChain = targetWithFilterChain.NextInChain)
			{
				FilterResult filterResult2 = ((list == targetWithFilterChain.FilterChain) ? filterResult : LoggerImpl.GetFilterResult(targetWithFilterChain.FilterChain, logEvent, targetWithFilterChain.DefaultResult));
				if (!LoggerImpl.WriteToTargetWithFilterChain(targetWithFilterChain.Target, filterResult2, logEvent, asyncContinuation))
				{
					break;
				}
				filterResult = filterResult2;
				list = targetWithFilterChain.FilterChain;
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x000077EC File Offset: 0x000059EC
		internal static int? FindCallingMethodOnStackTrace(StackFrame[] stackFrames, [NotNull] Type loggerType)
		{
			if (stackFrames == null || stackFrames.Length == 0)
			{
				return null;
			}
			int? num = null;
			int? num2 = null;
			for (int i = 0; i < stackFrames.Length; i++)
			{
				StackFrame stackFrame = stackFrames[i];
				if (!LoggerImpl.SkipAssembly(stackFrame))
				{
					if (num2 == null)
					{
						num2 = new int?(i);
					}
					if (LoggerImpl.IsLoggerType(stackFrame, loggerType))
					{
						num = null;
					}
					else if (num == null)
					{
						num = new int?(i);
					}
				}
			}
			int? num3 = num;
			if (num3 == null)
			{
				return num2;
			}
			return num3;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000787C File Offset: 0x00005A7C
		internal static int SkipToUserStackFrameLegacy(StackFrame[] stackFrames, int firstUserStackFrame)
		{
			for (int i = firstUserStackFrame; i < stackFrames.Length; i++)
			{
				StackFrame stackFrame = stackFrames[i];
				if (!LoggerImpl.SkipAssembly(stackFrame))
				{
					MethodBase method = stackFrame.GetMethod();
					if (((method != null) ? method.Name : null) == "MoveNext" && stackFrames.Length > i)
					{
						MethodBase method2 = stackFrames[i + 1].GetMethod();
						Type type = ((method2 != null) ? method2.DeclaringType : null);
						if (type == typeof(AsyncTaskMethodBuilder) || type == typeof(AsyncTaskMethodBuilder<>))
						{
							goto IL_0071;
						}
					}
					return i;
				}
				IL_0071:;
			}
			return firstUserStackFrame;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00007908 File Offset: 0x00005B08
		private static bool SkipAssembly(StackFrame frame)
		{
			Assembly assembly = StackTraceUsageUtils.LookupAssemblyFromStackFrame(frame);
			return assembly == null || LogManager.IsHiddenAssembly(assembly);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00007930 File Offset: 0x00005B30
		private static bool IsLoggerType(StackFrame frame, Type loggerType)
		{
			MethodBase method = frame.GetMethod();
			Type type = ((method != null) ? method.DeclaringType : null);
			return type != null && (loggerType == type || type.IsSubclassOf(loggerType) || loggerType.IsAssignableFrom(type));
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00007978 File Offset: 0x00005B78
		private static bool WriteToTargetWithFilterChain(Target target, FilterResult result, LogEventInfo logEvent, AsyncContinuation onException)
		{
			if (result == FilterResult.Ignore || result == FilterResult.IgnoreFinal)
			{
				if (InternalLogger.IsDebugEnabled)
				{
					InternalLogger.Debug<string, LogLevel>("{0}.{1} Rejecting message because of a filter.", logEvent.LoggerName, logEvent.Level);
				}
				return result != FilterResult.IgnoreFinal;
			}
			target.WriteAsyncLogEvent(logEvent.WithContinuation(onException));
			return result != FilterResult.LogFinal;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000079C8 File Offset: 0x00005BC8
		private static FilterResult GetFilterResult(IList<Filter> filterChain, LogEventInfo logEvent, FilterResult defaultFilterResult)
		{
			if (filterChain == null || filterChain.Count == 0)
			{
				return defaultFilterResult;
			}
			FilterResult filterResult2;
			try
			{
				for (int i = 0; i < filterChain.Count; i++)
				{
					FilterResult filterResult = filterChain[i].GetFilterResult(logEvent);
					if (filterResult != FilterResult.Neutral)
					{
						return filterResult;
					}
				}
				filterResult2 = defaultFilterResult;
			}
			catch (Exception ex)
			{
				InternalLogger.Warn(ex, "Exception during filter evaluation. Message will be ignore.");
				if (ex.MustBeRethrown())
				{
					throw;
				}
				filterResult2 = FilterResult.Ignore;
			}
			return filterResult2;
		}

		// Token: 0x04000033 RID: 51
		private const int StackTraceSkipMethods = 0;
	}
}
