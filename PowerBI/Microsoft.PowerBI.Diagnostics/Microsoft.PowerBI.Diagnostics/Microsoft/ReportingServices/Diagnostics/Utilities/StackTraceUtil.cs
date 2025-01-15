using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Security;
using System.Text;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000017 RID: 23
	internal static class StackTraceUtil
	{
		// Token: 0x06000077 RID: 119 RVA: 0x0000347C File Offset: 0x0000167C
		internal static void StackTraceToString(StackTrace trace, int firstStep, StringBuilder sb)
		{
			for (int i = firstStep; i <= trace.FrameCount - 1; i++)
			{
				StackTraceUtil.StackFrameToString(trace.GetFrame(i), sb);
			}
			sb.AppendLine();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000034B0 File Offset: 0x000016B0
		[SecuritySafeCritical]
		private static void StackFrameToString(StackFrame frame, StringBuilder sb)
		{
			if (frame == null)
			{
				return;
			}
			try
			{
				MethodBase method = frame.GetMethod();
				sb.AppendLine();
				sb.Append('\t');
				sb.Append((method.ReflectedType != null) ? method.ReflectedType.FullName : "DynamicClass");
				sb.Append('.');
				sb.Append(method.Name);
				sb.Append('(');
				ParameterInfo[] parameters = method.GetParameters();
				string text = string.Empty;
				foreach (ParameterInfo parameterInfo in parameters)
				{
					sb.Append(text);
					sb.Append(parameterInfo.ParameterType.Name);
					sb.Append(' ');
					sb.Append(parameterInfo.Name);
					text = ", ";
				}
				sb.Append(")  ");
				sb.Append(frame.GetFileName());
				int fileLineNumber = frame.GetFileLineNumber();
				if (fileLineNumber > 0)
				{
					sb.Append('(');
					sb.Append(fileLineNumber.ToString(CultureInfo.InvariantCulture));
					sb.Append(')');
				}
			}
			catch
			{
				sb.AppendLine("Failed to get full StackTrace from where the error occured.");
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000035F4 File Offset: 0x000017F4
		internal static byte[] GetExceptionFrames(Exception e, out int exceptionHash)
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
			exceptionHash = StackTraceUtil.GetInvariantHashCode<byte>(array);
			return array;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000036DC File Offset: 0x000018DC
		private static int GetInvariantHashCode<T>(IEnumerable<T> key) where T : IConvertible
		{
			if (key == null)
			{
				return 0;
			}
			uint num = 5381U;
			foreach (T t in key)
			{
				uint num2 = (num << 5) + num;
				ref T ptr = ref t;
				if (default(T) == null)
				{
					T t2 = t;
					ptr = ref t2;
				}
				num = num2 ^ (uint)ptr.ToChar(CultureInfo.InvariantCulture);
			}
			return (int)num;
		}
	}
}
