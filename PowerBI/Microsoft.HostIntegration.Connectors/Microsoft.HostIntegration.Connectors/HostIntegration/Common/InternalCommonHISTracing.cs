using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.Win32;

namespace Microsoft.HostIntegration.Common
{
	// Token: 0x020004F1 RID: 1265
	public class InternalCommonHISTracing
	{
		// Token: 0x06002AD1 RID: 10961 RVA: 0x00093CC8 File Offset: 0x00091EC8
		static InternalCommonHISTracing()
		{
			if (InternalCommonHISTracing.rkCedar == null)
			{
				return;
			}
			InternalCommonHISTracing.traceDir = InternalCommonHISTracing.rkCedar.GetValue("TraceDir") as string;
			InternalCommonHISTracing.tempTracingOptions = InternalCommonHISTracing.rkCedar.GetValue("TraceLevel");
			object value = InternalCommonHISTracing.rkCedar.GetValue("TraceFileSize");
			if (value != null)
			{
				InternalCommonHISTracing.maximumTraceFileSize = (int)value * 1024;
			}
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x00093D60 File Offset: 0x00091F60
		~InternalCommonHISTracing()
		{
			if (InternalCommonHISTracing.traceFile != null)
			{
				this.CloseTraceFile();
			}
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x00093D94 File Offset: 0x00091F94
		internal void CloseTraceFile()
		{
			if (InternalCommonHISTracing.traceFile != null)
			{
				object obj = InternalCommonHISTracing.lockTraceFile;
				lock (obj)
				{
					if (InternalCommonHISTracing.traceFile != null)
					{
						InternalCommonHISTracing.traceFile.Flush();
						bool flag2 = InternalCommonHISTracing.isTraceFileSizeExceeded;
						long num = (long)InternalCommonHISTracing.currentTraceFileSize;
						InternalCommonHISTracing.currentTraceFileSize = 0;
						InternalCommonHISTracing.isTraceFileSizeExceeded = false;
						string text = "HISTGBL0001 Tracing file is being closed.                                                      \n";
						for (long num2 = num % 6144L; num2 < 6144L; num2 += 96L)
						{
							text += "                                                                                               \n";
						}
						this.HISTraceEntry(text);
						InternalCommonHISTracing.traceFile.Flush();
						InternalCommonHISTracing.traceFile = null;
					}
				}
			}
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x00093E44 File Offset: 0x00092044
		internal long Refresh()
		{
			InternalCommonHISTracing.tempTracingOptions = InternalCommonHISTracing.rkCedar.GetValue("TraceLevel");
			long num = (long)((InternalCommonHISTracing.tempTracingOptions != null) ? ((int)InternalCommonHISTracing.tempTracingOptions) : 0);
			InternalCommonHISTracing.tempTracingOptions = InternalCommonHISTracing.rkCedar.GetValue("TraceLevel2");
			long num2 = ((InternalCommonHISTracing.tempTracingOptions != null) ? ((long)((int)InternalCommonHISTracing.tempTracingOptions) << 32) : 0L);
			return num + num2;
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x00093EAC File Offset: 0x000920AC
		internal void HISTraceEntry(string traceData)
		{
			object obj = InternalCommonHISTracing.lockTraceFile;
			lock (obj)
			{
				if (InternalCommonHISTracing.traceFile == null && InternalCommonHISTracing.traceFile == null)
				{
					this.CreateTraceFile();
				}
				int managedThreadId = Thread.CurrentThread.ManagedThreadId;
				string text = InternalCommonHISTracing.pid.Value.ToString("D5");
				text += " ";
				text += managedThreadId.ToString("X8");
				text += " ";
				text += DateTime.Now.ToString("MMM dd yyyy HH:mm:ss.fff");
				text += "\t";
				if (InternalCommonHISTracing.maximumTraceFileSize > 0 && InternalCommonHISTracing.currentTraceFileSize + text.Length + traceData.Length > InternalCommonHISTracing.maximumTraceFileSize)
				{
					if (!InternalCommonHISTracing.isTraceFileSizeExceeded)
					{
						InternalCommonHISTracing.isTraceFileSizeExceeded = true;
						text += "HISTGBL0000 Tracing terminated as file size will be exceeded";
						Interlocked.Add(ref InternalCommonHISTracing.currentTraceFileSize, text.Length);
						InternalCommonHISTracing.traceFile.WriteLine(text);
						InternalCommonHISTracing.traceFile.Flush();
					}
					return;
				}
				text += traceData;
				Interlocked.Add(ref InternalCommonHISTracing.currentTraceFileSize, text.Length);
				InternalCommonHISTracing.traceFile.WriteLine(text);
			}
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x00094004 File Offset: 0x00092204
		private void CreateTraceFile()
		{
			if (InternalCommonHISTracing.traceFile == null)
			{
				InternalCommonHISTracing.pid = new int?(Process.GetCurrentProcess().Id);
				InternalCommonHISTracing.realFile = new FileStream(InternalCommonHISTracing.traceDir + "\\PID" + InternalCommonHISTracing.pid.Value.ToString("D5") + ".AITF", FileMode.Append, FileAccess.Write, FileShare.ReadWrite, 256, FileOptions.WriteThrough);
				InternalCommonHISTracing.traceFile = new StreamWriter(InternalCommonHISTracing.realFile);
				GC.SuppressFinalize(InternalCommonHISTracing.realFile);
				GC.SuppressFinalize(InternalCommonHISTracing.realFile.SafeFileHandle);
			}
		}

		// Token: 0x040019FC RID: 6652
		private static int? pid;

		// Token: 0x040019FD RID: 6653
		private static object lockTraceFile = new object();

		// Token: 0x040019FE RID: 6654
		private static RegistryKey rkCedar = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Cedar", RegistryKeyPermissionCheck.ReadSubTree);

		// Token: 0x040019FF RID: 6655
		private static StreamWriter traceFile;

		// Token: 0x04001A00 RID: 6656
		private static FileStream realFile;

		// Token: 0x04001A01 RID: 6657
		private static string traceDir;

		// Token: 0x04001A02 RID: 6658
		private static int maximumTraceFileSize = 0;

		// Token: 0x04001A03 RID: 6659
		private static int currentTraceFileSize = 0;

		// Token: 0x04001A04 RID: 6660
		private static bool isTraceFileSizeExceeded = false;

		// Token: 0x04001A05 RID: 6661
		private static object tempTracingOptions;
	}
}
