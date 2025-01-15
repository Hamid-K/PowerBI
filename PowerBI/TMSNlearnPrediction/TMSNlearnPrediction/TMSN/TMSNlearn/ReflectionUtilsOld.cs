using System;
using System.Diagnostics;
using System.IO;
using Microsoft.MachineLearning;
using Microsoft.MachineLearning.CommandLine;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004D9 RID: 1241
	public static class ReflectionUtilsOld
	{
		// Token: 0x06001963 RID: 6499 RVA: 0x0008F728 File Offset: 0x0008D928
		public static TRes CreateInstanceOld<TRes, TSig>(SubComponent comp, string extraSettings, params object[] extra) where TRes : class
		{
			string[] array = ((!string.IsNullOrWhiteSpace(extraSettings)) ? new string[] { extraSettings } : null);
			string text = PredictionUtil.CombineSettings(comp.Settings, array);
			TRes tres;
			if (ComponentCatalog.TryCreateInstance<TRes, TSig>(ref tres, comp.Kind, text, extra))
			{
				return tres;
			}
			throw Contracts.Except("Unknown loadable class: {0}", new object[] { comp.Kind });
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0008F78B File Offset: 0x0008D98B
		public static TRes CreateInstanceOld<TRes, TSig>(SubComponent<TRes, TSig> comp, string extraSettings, params object[] extra) where TRes : class
		{
			return ReflectionUtilsOld.CreateInstanceOld<TRes, TSig>(comp, extraSettings, extra);
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0008F798 File Offset: 0x0008D998
		public static TRes CreateInstanceOld<TRes, TSig>(SubComponent comp, string[] extraSettings, params object[] extra) where TRes : class
		{
			string text = PredictionUtil.CombineSettings(comp.Settings, extraSettings);
			TRes tres;
			if (ComponentCatalog.TryCreateInstance<TRes, TSig>(ref tres, comp.Kind, text, extra))
			{
				return tres;
			}
			throw Contracts.Except("Unknown loadable class: {0}", new object[] { comp.Kind });
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x0008F820 File Offset: 0x0008DA20
		public static void RunProcess(string exeFilename, string[] args, string workingDir, TextWriter standardOutputWriter = null, TextWriter standardErrorWriter = null)
		{
			Process process = new Process
			{
				StartInfo = 
				{
					UseShellExecute = false,
					CreateNoWindow = true,
					FileName = exeFilename,
					Arguments = ((args == null) ? "" : string.Join(" ", args))
				}
			};
			if (workingDir != null)
			{
				process.StartInfo.WorkingDirectory = workingDir;
			}
			if (standardOutputWriter != null)
			{
				process.StartInfo.RedirectStandardOutput = true;
				process.OutputDataReceived += delegate(object s, DataReceivedEventArgs a)
				{
					if (a.Data != null)
					{
						standardOutputWriter.WriteLine(a.Data);
					}
				};
			}
			if (standardErrorWriter != null)
			{
				process.StartInfo.RedirectStandardError = true;
				process.ErrorDataReceived += delegate(object s, DataReceivedEventArgs a)
				{
					if (a.Data != null)
					{
						standardErrorWriter.WriteLine(a.Data);
					}
				};
			}
			process.Start();
			if (standardOutputWriter != null)
			{
				process.BeginOutputReadLine();
			}
			if (standardErrorWriter != null)
			{
				process.BeginErrorReadLine();
			}
			process.WaitForExit();
			if (standardOutputWriter != null)
			{
				standardOutputWriter.Flush();
				standardOutputWriter.Close();
			}
			if (standardErrorWriter != null)
			{
				standardErrorWriter.Flush();
				standardErrorWriter.Close();
			}
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x0008F96C File Offset: 0x0008DB6C
		public static bool IsNumericType(Type type)
		{
			return type == typeof(double) || type == typeof(float) || type == typeof(int) || type == typeof(long) || type == typeof(short) || type == typeof(uint) || type == typeof(ulong) || type == typeof(ushort);
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x0008FA09 File Offset: 0x0008DC09
		public static bool IsSimpleType(Type type)
		{
			return ReflectionUtilsOld.IsNumericType(type) || type == typeof(bool) || type == typeof(string) || type == typeof(char);
		}
	}
}
