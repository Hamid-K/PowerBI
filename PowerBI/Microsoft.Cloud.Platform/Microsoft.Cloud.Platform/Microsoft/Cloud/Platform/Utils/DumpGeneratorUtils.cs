using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001E3 RID: 483
	public static class DumpGeneratorUtils
	{
		// Token: 0x06000C93 RID: 3219 RVA: 0x0002BEB0 File Offset: 0x0002A0B0
		public static string GetWerLocalDumpsDirectory()
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\Windows Error Reporting\\LocalDumps");
			if (registryKey != null)
			{
				object value = registryKey.GetValue("DumpFolder");
				if (value != null && Path.IsPathRooted(value.ToString()))
				{
					return value.ToString();
				}
			}
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "WindowErrorReporting LocalDumps is not configured.");
			return Path.GetTempPath();
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0002BF08 File Offset: 0x0002A108
		public static string DumpProcess(Process proc, string dumpFileShortName)
		{
			string werLocalDumpsDirectory = DumpGeneratorUtils.GetWerLocalDumpsDirectory();
			if (!Directory.Exists(werLocalDumpsDirectory))
			{
				Directory.CreateDirectory(werLocalDumpsDirectory);
			}
			return DumpGeneratorUtils.DumpProcess(proc, dumpFileShortName, werLocalDumpsDirectory);
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0002BF34 File Offset: 0x0002A134
		public static string DumpProcess(Process proc, string dumpFileShortName, string dumpFolder)
		{
			string text = Path.Combine(dumpFolder, dumpFileShortName);
			if (File.Exists(text))
			{
				int num = 1;
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(dumpFileShortName);
				string extension = Path.GetExtension(dumpFileShortName);
				do
				{
					text = Path.Combine(dumpFolder, "{0}({1}){2}".FormatWithInvariantCulture(new object[]
					{
						fileNameWithoutExtension,
						num++,
						extension
					}));
				}
				while (File.Exists(text));
			}
			ExtendedDiagnostics.EnsureOperation(!proc.HasExited, "proc is dead");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(dumpFileShortName, "dumpFileShortName");
			ExtendedDiagnostics.EnsureOperation(Directory.Exists(dumpFolder), "dump folder must exist");
			ExtendedDiagnostics.EnsureOperation(IntPtr.Size == 8, "current process must be 64bits");
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "Dump file location {0}".FormatWithInvariantCulture(new object[] { text }));
			Exception ex = null;
			using (FileStream fileStream = new FileStream(text, FileMode.Create))
			{
				DumpGeneratorUtils.MiniDumpType miniDumpType = (DumpGeneratorUtils.MiniDumpType)327;
				if (DumpGeneratorUtils.MiniDumpWriteDump(proc.Handle, proc.Id, fileStream.SafeFileHandle, miniDumpType, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero))
				{
					return text;
				}
				ex = new Win32Exception(Marshal.GetLastWin32Error());
			}
			if (File.Exists(text))
			{
				File.Delete(text);
			}
			throw ex;
		}

		// Token: 0x06000C96 RID: 3222
		[DllImport("dbghelp.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		private static extern bool MiniDumpWriteDump(IntPtr hProcess, int processId, SafeHandle hFile, DumpGeneratorUtils.MiniDumpType dumpType, IntPtr expParam, IntPtr userStreamParam, IntPtr callbackParam);

		// Token: 0x040004D3 RID: 1235
		private const string c_werLocalDumpRegKey = "SOFTWARE\\Microsoft\\Windows\\Windows Error Reporting\\LocalDumps";

		// Token: 0x040004D4 RID: 1236
		private const string c_werLocalDumpFolderValueName = "DumpFolder";

		// Token: 0x040004D5 RID: 1237
		private const int c_64BitsInBytes = 8;

		// Token: 0x02000693 RID: 1683
		private enum MiniDumpType
		{
			// Token: 0x04001299 RID: 4761
			None = 65536,
			// Token: 0x0400129A RID: 4762
			Normal = 0,
			// Token: 0x0400129B RID: 4763
			WithDataSegs,
			// Token: 0x0400129C RID: 4764
			WithFullMemory,
			// Token: 0x0400129D RID: 4765
			WithHandleData = 4,
			// Token: 0x0400129E RID: 4766
			FilterMemory = 8,
			// Token: 0x0400129F RID: 4767
			ScanMemory = 16,
			// Token: 0x040012A0 RID: 4768
			WithUnloadedModules = 32,
			// Token: 0x040012A1 RID: 4769
			WithIndirectlyReferencedMemory = 64,
			// Token: 0x040012A2 RID: 4770
			FilterModulePaths = 128,
			// Token: 0x040012A3 RID: 4771
			WithProcessThreadData = 256,
			// Token: 0x040012A4 RID: 4772
			WithPrivateReadWriteMemory = 512,
			// Token: 0x040012A5 RID: 4773
			WithoutOptionalData = 1024,
			// Token: 0x040012A6 RID: 4774
			WithFullMemoryInfo = 2048,
			// Token: 0x040012A7 RID: 4775
			WithThreadInfo = 4096,
			// Token: 0x040012A8 RID: 4776
			WithCodeSegs = 8192
		}
	}
}
