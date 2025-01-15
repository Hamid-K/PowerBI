using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using NLog.Config;

namespace NLog.Internal
{
	// Token: 0x02000142 RID: 322
	internal static class StackTraceUsageUtils
	{
		// Token: 0x06000FA7 RID: 4007 RVA: 0x00027ED5 File Offset: 0x000260D5
		internal static StackTraceUsage Max(StackTraceUsage u1, StackTraceUsage u2)
		{
			return (StackTraceUsage)Math.Max((int)u1, (int)u2);
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x00027EDE File Offset: 0x000260DE
		public static int GetFrameCount(this StackTrace strackTrace)
		{
			return strackTrace.FrameCount;
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x00027EE8 File Offset: 0x000260E8
		public static string GetStackFrameMethodName(MethodBase method, bool includeMethodInfo, bool cleanAsyncMoveNext, bool cleanAnonymousDelegates)
		{
			if (method == null)
			{
				return null;
			}
			string text = method.Name;
			Type declaringType = method.DeclaringType;
			if (cleanAsyncMoveNext && text == "MoveNext" && ((declaringType != null) ? declaringType.DeclaringType : null) != null && declaringType.Name.StartsWith("<"))
			{
				int num = declaringType.Name.IndexOf('>', 1);
				if (num > 1)
				{
					text = declaringType.Name.Substring(1, num - 1);
				}
			}
			if (cleanAnonymousDelegates && text.StartsWith("<") && text.Contains("__") && text.Contains(">"))
			{
				int num2 = text.IndexOf('<') + 1;
				int num3 = text.IndexOf('>');
				text = text.Substring(num2, num3 - num2);
			}
			if (includeMethodInfo && text == method.Name)
			{
				text = method.ToString();
			}
			return text;
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x00027FCC File Offset: 0x000261CC
		public static string GetStackFrameMethodClassName(MethodBase method, bool includeNameSpace, bool cleanAsyncMoveNext, bool cleanAnonymousDelegates)
		{
			if (method == null)
			{
				return null;
			}
			Type type = method.DeclaringType;
			if (cleanAsyncMoveNext && method.Name == "MoveNext" && ((type != null) ? type.DeclaringType : null) != null && type.Name.StartsWith("<") && type.Name.IndexOf('>', 1) > 1)
			{
				type = type.DeclaringType;
			}
			if (!includeNameSpace && ((type != null) ? type.DeclaringType : null) != null && type.IsNested && type.GetCustomAttribute<CompilerGeneratedAttribute>() != null)
			{
				return type.DeclaringType.Name;
			}
			string text = (includeNameSpace ? ((type != null) ? type.FullName : null) : ((type != null) ? type.Name : null));
			if (cleanAnonymousDelegates && text != null)
			{
				int num = text.IndexOf("+<>", StringComparison.Ordinal);
				if (num >= 0)
				{
					text = text.Substring(0, num);
				}
			}
			return text;
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x000280B0 File Offset: 0x000262B0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string GetClassFullName()
		{
			int num = 2;
			string empty = string.Empty;
			return StackTraceUsageUtils.GetClassFullName(new StackFrame(num, false));
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x000280C4 File Offset: 0x000262C4
		public static string GetClassFullName(StackFrame stackFrame)
		{
			string text = StackTraceUsageUtils.LookupClassNameFromStackFrame(stackFrame);
			if (string.IsNullOrEmpty(text))
			{
				text = StackTraceUsageUtils.GetClassFullName(new StackTrace(false));
			}
			return text;
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x000280F0 File Offset: 0x000262F0
		private static string GetClassFullName(StackTrace stackTrace)
		{
			StackFrame[] frames = stackTrace.GetFrames();
			for (int i = 0; i < frames.Length; i++)
			{
				string text = StackTraceUsageUtils.LookupClassNameFromStackFrame(frames[i]);
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
			}
			return string.Empty;
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0002812C File Offset: 0x0002632C
		public static Assembly LookupAssemblyFromStackFrame(StackFrame stackFrame)
		{
			MethodBase method = stackFrame.GetMethod();
			if (method == null)
			{
				return null;
			}
			Type declaringType = method.DeclaringType;
			Assembly assembly;
			if ((assembly = ((declaringType != null) ? declaringType.GetAssembly() : null)) == null)
			{
				Module module = method.Module;
				assembly = ((module != null) ? module.Assembly : null);
			}
			Assembly assembly2 = assembly;
			if (assembly2 == StackTraceUsageUtils.nlogAssembly)
			{
				return null;
			}
			if (assembly2 == StackTraceUsageUtils.mscorlibAssembly)
			{
				return null;
			}
			if (assembly2 == StackTraceUsageUtils.systemAssembly)
			{
				return null;
			}
			return assembly2;
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x000281A4 File Offset: 0x000263A4
		public static string LookupClassNameFromStackFrame(StackFrame stackFrame)
		{
			MethodBase method = stackFrame.GetMethod();
			if (method != null && StackTraceUsageUtils.LookupAssemblyFromStackFrame(stackFrame) != null)
			{
				string text = StackTraceUsageUtils.GetStackFrameMethodClassName(method, true, true, true) ?? method.Name;
				if (!string.IsNullOrEmpty(text) && !text.StartsWith("System.", StringComparison.Ordinal))
				{
					return text;
				}
			}
			return string.Empty;
		}

		// Token: 0x04000433 RID: 1075
		private static readonly Assembly nlogAssembly = typeof(StackTraceUsageUtils).GetAssembly();

		// Token: 0x04000434 RID: 1076
		private static readonly Assembly mscorlibAssembly = typeof(string).GetAssembly();

		// Token: 0x04000435 RID: 1077
		private static readonly Assembly systemAssembly = typeof(Debug).GetAssembly();
	}
}
