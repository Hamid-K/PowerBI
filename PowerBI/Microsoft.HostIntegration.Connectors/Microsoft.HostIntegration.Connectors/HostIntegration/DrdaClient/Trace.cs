using System;
using System.Diagnostics;
using System.Reflection;
using Microsoft.HostIntegration.Drda.Requester;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.Tracing.DrdaClient;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A34 RID: 2612
	internal class Trace
	{
		// Token: 0x060051B7 RID: 20919 RVA: 0x0014DA02 File Offset: 0x0014BC02
		public static DrdaClientTracePoint GetTracePoint(DrdaConnection conn)
		{
			if (conn == null)
			{
				return Trace._tracePoint;
			}
			return conn.TracePoint;
		}

		// Token: 0x060051B8 RID: 20920 RVA: 0x0014DA13 File Offset: 0x0014BC13
		public static void ApiEnterTrace()
		{
			Trace.ApiEnterTrace(Trace._tracePoint);
		}

		// Token: 0x060051B9 RID: 20921 RVA: 0x0014DA20 File Offset: 0x0014BC20
		public static void ApiEnterTrace(DrdaClientTracePoint tracePoint)
		{
			if (tracePoint.IsEnabled(TraceFlags.Information))
			{
				Trace.GetDebugInformation(new StackFrame(1, true), delegate(string fileName, int line, string methodName)
				{
					tracePoint.Trace(TraceFlags.Information, string.Format("{0} Enter at File: {1}, Line: {2}", methodName, fileName, line));
				});
			}
		}

		// Token: 0x060051BA RID: 20922 RVA: 0x0014DA60 File Offset: 0x0014BC60
		public static void ApiEnterTrace(string methodName)
		{
			Trace.ApiEnterTrace(Trace._tracePoint, methodName);
		}

		// Token: 0x060051BB RID: 20923 RVA: 0x0014DA70 File Offset: 0x0014BC70
		public static void ApiEnterTrace(DrdaClientTracePoint tracePoint, string methodName)
		{
			if (tracePoint.IsEnabled(TraceFlags.Information))
			{
				Trace.GetDebugInformation(new StackFrame(1, true), delegate(string fileName, int line, string stackMethodName)
				{
					tracePoint.Trace(TraceFlags.Information, string.Format("{0} - {1} Enter at File: {2}, Line: {3}", new object[] { stackMethodName, methodName, fileName, line }));
				});
			}
		}

		// Token: 0x060051BC RID: 20924 RVA: 0x0014DAB7 File Offset: 0x0014BCB7
		public static void ApiExitTrace()
		{
			Trace.ApiExitTrace(Trace._tracePoint);
		}

		// Token: 0x060051BD RID: 20925 RVA: 0x0014DAC4 File Offset: 0x0014BCC4
		public static void ApiExitTrace(DrdaClientTracePoint tracePoint)
		{
			if (tracePoint.IsEnabled(TraceFlags.Information))
			{
				Trace.GetDebugInformation(new StackFrame(1, true), delegate(string fileName, int line, string methodName)
				{
					tracePoint.Trace(TraceFlags.Information, string.Format("{0} Exit at File: {1}, Line: {2}", methodName, fileName, line));
				});
			}
		}

		// Token: 0x060051BE RID: 20926 RVA: 0x0014DB04 File Offset: 0x0014BD04
		private static void InternalTrace(DrdaClientTracePoint tracePoint, TraceFlags flags, string message)
		{
			if (tracePoint.IsEnabled(flags))
			{
				Trace.GetDebugInformation(new StackFrame(2, true), delegate(string fileName, int line, string methodName)
				{
					tracePoint.Trace(flags, string.Format("File: {0}, Line: {1}, method: {2}. --- {3} ", new object[] { fileName, line, methodName, message }));
				});
			}
		}

		// Token: 0x060051BF RID: 20927 RVA: 0x0014DB58 File Offset: 0x0014BD58
		private static void GetDebugInformation(StackFrame caller, Action<string, int, string> customAction)
		{
			string text = caller.GetFileName();
			if (!string.IsNullOrEmpty(text))
			{
				int num = text.LastIndexOf('\\');
				if (num >= 0)
				{
					text = text.Substring(num + 1);
				}
			}
			int fileLineNumber = caller.GetFileLineNumber();
			MethodBase method = caller.GetMethod();
			string text2 = method.ToString();
			if (method.DeclaringType != null)
			{
				text2 = method.DeclaringType.ToString();
				int num2 = text2.LastIndexOf('.');
				if (num2 >= 0)
				{
					text2 = text2.Substring(num2 + 1);
					text2 = text2 + "." + method.Name;
				}
			}
			customAction(text, fileLineNumber, text2);
		}

		// Token: 0x060051C0 RID: 20928 RVA: 0x0014DBF2 File Offset: 0x0014BDF2
		public static void MessageTrace(string traceString)
		{
			Trace.InternalTrace(Trace._tracePoint, TraceFlags.Information, traceString);
		}

		// Token: 0x060051C1 RID: 20929 RVA: 0x0014DC00 File Offset: 0x0014BE00
		public static void MessageTrace(DrdaClientTracePoint tracePoint, string traceString)
		{
			Trace.InternalTrace(tracePoint, TraceFlags.Information, traceString);
		}

		// Token: 0x060051C2 RID: 20930 RVA: 0x0014DC0A File Offset: 0x0014BE0A
		public static void MessageTrace(string traceString, object param1)
		{
			Trace.InternalTrace(Trace._tracePoint, TraceFlags.Information, string.Format(traceString, param1));
		}

		// Token: 0x060051C3 RID: 20931 RVA: 0x0014DC1E File Offset: 0x0014BE1E
		public static void MessageTrace(DrdaClientTracePoint tracePoint, string traceString, object param1)
		{
			Trace.InternalTrace(tracePoint, TraceFlags.Information, string.Format(traceString, param1));
		}

		// Token: 0x060051C4 RID: 20932 RVA: 0x0014DC2E File Offset: 0x0014BE2E
		public static void MessageVerboseTrace(string traceString)
		{
			Trace.InternalTrace(Trace._tracePoint, TraceFlags.Verbose, traceString);
		}

		// Token: 0x060051C5 RID: 20933 RVA: 0x0014DC3D File Offset: 0x0014BE3D
		public static void MessageVerboseTrace(DrdaClientTracePoint tracePoint, string traceString)
		{
			Trace.InternalTrace(tracePoint, TraceFlags.Verbose, traceString);
		}

		// Token: 0x060051C6 RID: 20934 RVA: 0x0014DC48 File Offset: 0x0014BE48
		public static void MessageVerboseTrace(string traceString, object param1)
		{
			Trace.InternalTrace(Trace._tracePoint, TraceFlags.Verbose, string.Format(traceString, param1));
		}

		// Token: 0x060051C7 RID: 20935 RVA: 0x0014DC5D File Offset: 0x0014BE5D
		public static void MessageVerboseTrace(DrdaClientTracePoint tracePoint, string traceString, object param1)
		{
			Trace.InternalTrace(tracePoint, TraceFlags.Verbose, string.Format(traceString, param1));
		}

		// Token: 0x060051C8 RID: 20936 RVA: 0x0014DC6E File Offset: 0x0014BE6E
		public static void MessageVerboseTrace(string traceString, object param1, object param2)
		{
			Trace.InternalTrace(Trace._tracePoint, TraceFlags.Verbose, string.Format(traceString, param1, param2));
		}

		// Token: 0x060051C9 RID: 20937 RVA: 0x0014DC84 File Offset: 0x0014BE84
		public static void MessageVerboseTrace(DrdaClientTracePoint tracePoint, string traceString, object param1, object param2)
		{
			Trace.InternalTrace(tracePoint, TraceFlags.Verbose, string.Format(traceString, param1, param2));
		}

		// Token: 0x060051CA RID: 20938 RVA: 0x0014DC96 File Offset: 0x0014BE96
		public static void MessageVerboseTrace(string traceString, object param1, object param2, object param3)
		{
			Trace.InternalTrace(Trace._tracePoint, TraceFlags.Verbose, string.Format(traceString, param1, param2, param3));
		}

		// Token: 0x060051CB RID: 20939 RVA: 0x0014DCAD File Offset: 0x0014BEAD
		public static void MessageVerboseTrace(string traceString, object[] parameters)
		{
			Trace.InternalTrace(Trace._tracePoint, TraceFlags.Verbose, string.Format(traceString, parameters));
		}

		// Token: 0x04004054 RID: 16468
		public static DrdaClientTracePoint _tracePoint = new DrdaClientTracePoint(RequesterFactory.Container, 7);
	}
}
