using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Microsoft.ApplicationInsights.Extensibility.Implementation.External;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x0200006A RID: 106
	internal static class ExceptionConverter
	{
		// Token: 0x06000343 RID: 835 RVA: 0x0000F1B4 File Offset: 0x0000D3B4
		internal static ExceptionDetails ConvertToExceptionDetails(Exception exception, ExceptionDetails parentExceptionDetails)
		{
			ExceptionDetails exceptionDetails = ExceptionDetails.CreateWithoutStackInfo(exception, parentExceptionDetails);
			Tuple<List<Microsoft.ApplicationInsights.Extensibility.Implementation.External.StackFrame>, bool> tuple = ExceptionConverter.SanitizeStackFrame<global::System.Diagnostics.StackFrame, Microsoft.ApplicationInsights.Extensibility.Implementation.External.StackFrame>(new StackTrace(exception, true).GetFrames(), new Func<global::System.Diagnostics.StackFrame, int, Microsoft.ApplicationInsights.Extensibility.Implementation.External.StackFrame>(ExceptionConverter.GetStackFrame), new Func<Microsoft.ApplicationInsights.Extensibility.Implementation.External.StackFrame, int>(ExceptionConverter.GetStackFrameLength));
			exceptionDetails.parsedStack = tuple.Item1;
			exceptionDetails.hasFullStack = tuple.Item2;
			return exceptionDetails;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000F20C File Offset: 0x0000D40C
		internal static Microsoft.ApplicationInsights.Extensibility.Implementation.External.StackFrame GetStackFrame(global::System.Diagnostics.StackFrame stackFrame, int frameId)
		{
			Microsoft.ApplicationInsights.Extensibility.Implementation.External.StackFrame stackFrame2 = new Microsoft.ApplicationInsights.Extensibility.Implementation.External.StackFrame
			{
				level = frameId
			};
			MethodBase method = stackFrame.GetMethod();
			string text;
			string text2;
			if (method == null)
			{
				text = "unknown";
				text2 = "unknown";
			}
			else
			{
				text2 = method.Module.Assembly.FullName;
				if (method.DeclaringType != null)
				{
					text = method.DeclaringType.FullName + "." + method.Name;
				}
				else
				{
					text = method.Name;
				}
			}
			stackFrame2.method = text;
			stackFrame2.assembly = text2;
			stackFrame2.fileName = stackFrame.GetFileName();
			int fileLineNumber = stackFrame.GetFileLineNumber();
			if (fileLineNumber != 0)
			{
				stackFrame2.line = fileLineNumber;
			}
			return stackFrame2;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000F2B8 File Offset: 0x0000D4B8
		internal static int GetStackFrameLength(Microsoft.ApplicationInsights.Extensibility.Implementation.External.StackFrame stackFrame)
		{
			return ((stackFrame.method == null) ? 0 : stackFrame.method.Length) + ((stackFrame.assembly == null) ? 0 : stackFrame.assembly.Length) + ((stackFrame.fileName == null) ? 0 : stackFrame.fileName.Length);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000F30C File Offset: 0x0000D50C
		private static Tuple<List<TOutput>, bool> SanitizeStackFrame<TInput, TOutput>(IList<TInput> inputList, Func<TInput, int, TOutput> converter, Func<TOutput, int> lengthGetter)
		{
			List<TOutput> list = new List<TOutput>();
			bool flag = true;
			if (inputList != null && inputList.Count > 0)
			{
				int num = 0;
				for (int i = 0; i < inputList.Count; i++)
				{
					int num2 = ((i % 2 == 0) ? (inputList.Count - 1 - i / 2) : (i / 2));
					TOutput toutput = converter(inputList[num2], num2);
					num += lengthGetter(toutput);
					if (num > 32768)
					{
						flag = false;
						break;
					}
					list.Insert(list.Count / 2, toutput);
				}
			}
			return new Tuple<List<TOutput>, bool>(list, flag);
		}

		// Token: 0x04000163 RID: 355
		public const int MaxParsedStackLength = 32768;
	}
}
