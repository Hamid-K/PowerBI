using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001DB RID: 475
	public static class MemoryDumper
	{
		// Token: 0x06000C6C RID: 3180 RVA: 0x0002B44C File Offset: 0x0002964C
		public static string DumpMemory(string dumpDirectory, string dumpFileName, DumpOptions dumpOptions)
		{
			string text2;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				string text = MemoryDumper.DumpMemory(currentProcess, CurrentProcess.Name, dumpDirectory, dumpFileName, dumpOptions);
				currentProcess.Close();
				text2 = text;
			}
			return text2;
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0002B494 File Offset: 0x00029694
		public static string DumpMemory(Process process, string processName, string dumpFilePath, string dumpFileName, DumpOptions dumpOptions)
		{
			return MemoryDumper.DumpMemory(process, processName, MemoryDumper.s_cdbPath, dumpFilePath, dumpFileName, dumpOptions);
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0002B4A8 File Offset: 0x000296A8
		public static string DumpMemory([NotNull] Process targetProcess, [NotNull] string targetName, [NotNull] string pathToDebugger, string dumpFilePath, [NotNull] string dumpFileName, DumpOptions dumpOptions)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Process>(targetProcess, "targetProcess");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(targetName, "targetName");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(pathToDebugger, "pathToDebugger");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(dumpFileName, "dumpFileName");
			if (Thread.VolatileRead(ref MemoryDumper.s_suppressionLevel) > 0)
			{
				throw new MemoryDumperException("Cannot take memory dumps when dumps are being actively suppressed");
			}
			if (string.IsNullOrEmpty(dumpFilePath))
			{
				dumpFilePath = Environment.GetEnvironmentVariable("TWEAKS_MEMORY_DUMPER_DEFAULT_DIRECTORY");
				if (string.IsNullOrEmpty(dumpFilePath))
				{
					dumpFilePath = Path.GetTempPath();
				}
			}
			if (!Directory.Exists(dumpFilePath))
			{
				string text = string.Format(CultureInfo.InvariantCulture, "Cannot take memory dump for process {0} as target dump directory {1} doesn't exist", new object[] { targetName, dumpFilePath });
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "{0}", new object[] { text });
				throw new MemoryDumperException(text, targetName);
			}
			if ((dumpOptions & DumpOptions.WatsonEnabled) != DumpOptions.None)
			{
				string text2 = "Illegal Dumper options: Dumper does not support Watson Enabled option";
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "{0}", new object[] { text2 });
				throw new MemoryDumperException(text2, targetName);
			}
			bool flag = false;
			if ((dumpOptions & DumpOptions.KillProcess) != DumpOptions.None)
			{
				flag = true;
			}
			string text3 = (flag ? "q" : ".detach;q");
			int id = targetProcess.Id;
			string text4 = "-noio";
			if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TWEAKS_MEMORY_DUMPER_NOIO_DISABLED")))
			{
				text4 = string.Empty;
			}
			string text12;
			try
			{
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(dumpFileName);
				string extension = Path.GetExtension(dumpFileName);
				string text5 = Path.Combine(dumpFilePath, dumpFileName);
				string text6 = Path.Combine(dumpFilePath, fileNameWithoutExtension + ".log");
				string text7 = Path.Combine(dumpFilePath, fileNameWithoutExtension + ".partial" + extension);
				string text8 = Path.Combine(dumpFilePath, fileNameWithoutExtension + ".partial.log");
				string text9 = Path.Combine(dumpFilePath, fileNameWithoutExtension + ".cdbscript");
				using (Process process = new Process())
				{
					using (StreamWriter streamWriter = File.CreateText(text9))
					{
						streamWriter.WriteLine(".logopen \"{0}\"", text8);
						streamWriter.WriteLine(".cordll -ve -u -l");
						streamWriter.WriteLine("~*kv");
						streamWriter.WriteLine(".load psscor2.dll");
						streamWriter.WriteLine("!threads -special");
						streamWriter.WriteLine("~*e !dso");
						streamWriter.WriteLine(".logclose");
						streamWriter.WriteLine(".dump /mFhutp \"{0}\"", text7);
						streamWriter.WriteLine(".logopen \"{0}\"", text6);
						streamWriter.WriteLine(".dump /ma \"{0}\"", text5);
						streamWriter.WriteLine(".logclose");
						streamWriter.WriteLine(text3);
						streamWriter.Flush();
					}
					string text10 = string.Format(CultureInfo.InvariantCulture, "{0} -p {1} -cf \"{2}\"", new object[] { text4, id, text9 });
					process.StartInfo.FileName = Path.Combine(pathToDebugger, "cdb.exe");
					process.StartInfo.Arguments = text10;
					process.StartInfo.UseShellExecute = false;
					process.StartInfo.WorkingDirectory = pathToDebugger;
					if (!process.Start())
					{
						throw new MemoryDumperException("Failed to start the debugger process. This should normally happen only if process resource was reused");
					}
					if (!process.WaitForExit(60000))
					{
						string text11 = string.Format(CultureInfo.InvariantCulture, "Waited {0} seconds to take dump of {1} and cdb.exe has not exited yet. Dump may still be created later under {2} or {3}", new object[] { 60, targetName, text5, text7 });
						TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "{0}", new object[] { text11 });
						process.Close();
						text12 = text5;
					}
					else
					{
						int exitCode = process.ExitCode;
						if (exitCode != 0 || !File.Exists(text5))
						{
							string text13 = string.Format(CultureInfo.InvariantCulture, "Failed taking memory dump of process {0}. cdb return code: {1}", new object[] { targetName, exitCode });
							TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "{0}", new object[] { text13 });
							process.Close();
							throw new MemoryDumperException(text13, targetName);
						}
						TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "Succeeded taking dump. Dump file outputted to {0}", new object[] { text5 });
						process.Close();
						text12 = text5;
					}
				}
			}
			catch (MemoryDumperException)
			{
				throw;
			}
			catch (Exception ex)
			{
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "Could not take memory dump of process {0}. Exception caught: {1}", new object[] { targetName, ex });
				if (ex.IsFatal())
				{
					throw;
				}
				throw new MemoryDumperException(targetName, "Exception Caught", ex);
			}
			return text12;
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0002B928 File Offset: 0x00029B28
		public static string CreateDumpFileName(string processIdentifier)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}_{1}.mdmp", new object[]
			{
				processIdentifier,
				Guid.NewGuid()
			});
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0002B950 File Offset: 0x00029B50
		public static IDisposable CreateSuppressScope()
		{
			Interlocked.Increment(ref MemoryDumper.s_suppressionLevel);
			return new DeferredDispose(delegate
			{
				Interlocked.Decrement(ref MemoryDumper.s_suppressionLevel);
			});
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0002B984 File Offset: 0x00029B84
		private static string CreateCdbPath()
		{
			string text;
			if (MemoryDumper.ProbePathForDebugger(Environment.GetEnvironmentVariable("TWEAKS_DEBUGGERS_PATH"), out text))
			{
				return text;
			}
			if (MemoryDumper.ProbePathForDebugger(Path.GetDirectoryName(ExtendedAssembly.GetExecutingAssembly(typeof(MemoryDumper)).Location), out text))
			{
				return text;
			}
			MemoryDumper.ProbePathForDebugger(Environment.GetEnvironmentVariable("SystemDrive"), out text);
			return text;
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0002B9E0 File Offset: 0x00029BE0
		private static bool ProbePathForDebugger(string path, out string res)
		{
			if (path == null)
			{
				res = null;
				return false;
			}
			string text = ((IntPtr.Size == 8) ? "64" : "86");
			res = path;
			if (path.EndsWith(text, StringComparison.Ordinal) && MemoryDumper.CdbExistsInDirectory(path))
			{
				return true;
			}
			res = "{0}\\Debuggers\\x{1}\\".FormatWithInvariantCulture(new object[] { path, text });
			if (MemoryDumper.CdbExistsInDirectory(res))
			{
				return true;
			}
			res = "{0}\\Debuggers{1}".FormatWithInvariantCulture(new object[] { path, text });
			if (MemoryDumper.CdbExistsInDirectory(res))
			{
				return true;
			}
			res = path;
			return MemoryDumper.CdbExistsInDirectory(res);
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0002BA7A File Offset: 0x00029C7A
		private static bool CdbExistsInDirectory(string path)
		{
			return File.Exists(Path.Combine(path, "cdb.exe"));
		}

		// Token: 0x040004C3 RID: 1219
		private static readonly string s_cdbPath = MemoryDumper.CreateCdbPath();

		// Token: 0x040004C4 RID: 1220
		private const int c_waitTimeout = 60000;

		// Token: 0x040004C5 RID: 1221
		private static int s_suppressionLevel = 0;
	}
}
