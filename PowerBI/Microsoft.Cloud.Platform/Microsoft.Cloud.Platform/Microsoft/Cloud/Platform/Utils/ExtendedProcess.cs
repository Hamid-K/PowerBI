using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000270 RID: 624
	public static class ExtendedProcess
	{
		// Token: 0x06001076 RID: 4214 RVA: 0x00038E9C File Offset: 0x0003709C
		public static int Run(string fileName, string arguments, out string output, out string error)
		{
			return ExtendedProcess.Run(fileName, arguments, int.MaxValue, ExtendedProcessOptions.None, out output, out error);
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x00038EAD File Offset: 0x000370AD
		public static int Run(string fileName, string arguments, int timeout, ExtendedProcessOptions options, out string output, out string error)
		{
			return ExtendedProcess.Run(ExtendedProcess.CreateDefaultProcessStartInfo(fileName, arguments), timeout, options, out output, out error);
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00038EC4 File Offset: 0x000370C4
		public static int Run([NotNull] ProcessStartInfo psi, int timeout, ExtendedProcessOptions options, string input, Action<string> outputHandler, Action<string> errorHandler)
		{
			ExtendedProcess.<>c__DisplayClass2_0 CS$<>8__locals1 = new ExtendedProcess.<>c__DisplayClass2_0();
			CS$<>8__locals1.outputHandler = outputHandler;
			CS$<>8__locals1.errorHandler = errorHandler;
			ExtendedDiagnostics.EnsureArgumentNotNull<ProcessStartInfo>(psi, "psi");
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(timeout, "timeout");
			CS$<>8__locals1.done = false;
			int num;
			using (Process p = new Process())
			{
				p.StartInfo = psi;
				if (psi.RedirectStandardOutput)
				{
					ExtendedDiagnostics.EnsureNotNull<Action<string>>(CS$<>8__locals1.outputHandler, "outputHandler");
					p.OutputDataReceived += delegate(object sender, DataReceivedEventArgs e)
					{
						Process p2 = p;
						lock (p2)
						{
							if (!CS$<>8__locals1.done && e.Data != null)
							{
								CS$<>8__locals1.outputHandler(e.Data + Environment.NewLine);
							}
						}
					};
				}
				if (psi.RedirectStandardError)
				{
					ExtendedDiagnostics.EnsureNotNull<Action<string>>(CS$<>8__locals1.errorHandler, "errorHandler");
					p.ErrorDataReceived += delegate(object sender, DataReceivedEventArgs e)
					{
						Process p3 = p;
						lock (p3)
						{
							if (!CS$<>8__locals1.done && e.Data != null)
							{
								CS$<>8__locals1.errorHandler(e.Data + Environment.NewLine);
							}
						}
					};
				}
				if (Tracing.RemovePersonallyIdentifiableInformationFromTraces)
				{
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "Running process: \"{0}\"", new object[] { psi.FileName });
				}
				else
				{
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "Running the command: \"{0} {1}\"", new object[] { psi.FileName, psi.Arguments });
				}
				p.Start();
				if (psi.RedirectStandardOutput)
				{
					p.BeginOutputReadLine();
				}
				if (psi.RedirectStandardError)
				{
					p.BeginErrorReadLine();
				}
				if (psi.RedirectStandardInput)
				{
					ExtendedDiagnostics.EnsureStringNotNullOrEmpty(input, "Input");
					p.StandardInput.Write(input);
					p.StandardInput.Flush();
				}
				if (p.WaitForExit(timeout) && (psi.RedirectStandardOutput || psi.RedirectStandardError))
				{
					p.WaitForExit();
				}
				if (psi.RedirectStandardOutput || psi.RedirectStandardError)
				{
					Process p4 = p;
					lock (p4)
					{
						CS$<>8__locals1.done = true;
					}
				}
				if (!p.HasExited)
				{
					string text;
					if (Tracing.RemovePersonallyIdentifiableInformationFromTraces)
					{
						text = string.Format(CultureInfo.InvariantCulture, "Process didn't stop after {0} milliseconds. Process was \"{1}\".", new object[] { timeout, psi.FileName });
					}
					else
					{
						text = string.Format(CultureInfo.InvariantCulture, "Process didn't stop after {0} milliseconds. Failed command was \"{1} {2}\".", new object[] { timeout, psi.FileName, psi.Arguments });
					}
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "{0}", new object[] { text });
					RunProcessException ex = new RunProcessTimeoutException(text);
					if (options.HasFlag(ExtendedProcessOptions.KillProcessOnTimeout))
					{
						if (options.HasFlag(ExtendedProcessOptions.DumpOnProcessTimeout))
						{
							try
							{
								MemoryDumper.DumpMemory(p, psi.FileName, Path.GetDirectoryName(psi.FileName), "{0}.Timeout.mdmp".FormatWithInvariantCulture(new object[] { psi.FileName }), DumpOptions.None);
							}
							catch (MemoryDumperException)
							{
							}
						}
						ExtendedProcess.KillProcess(p, text);
					}
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "Throwing exception: {0}", new object[] { ex });
					throw ex;
				}
				int exitCode = p.ExitCode;
				if (Tracing.RemovePersonallyIdentifiableInformationFromTraces)
				{
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "Finished running process: \"{0}\" successfuly. Exit code is: \"{1}\".", new object[] { psi.FileName, exitCode });
				}
				else
				{
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "Finished running the command: \"{0} {1}\" successfuly. Exit code is: \"{2}\".", new object[] { psi.FileName, psi.Arguments, exitCode });
				}
				num = exitCode;
			}
			return num;
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x000392A8 File Offset: 0x000374A8
		public static int Run([NotNull] ProcessStartInfo psi, int timeout, ExtendedProcessOptions options, out string output, out string error)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ProcessStartInfo>(psi, "psi");
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(timeout, "timeout");
			int num = 0;
			output = null;
			error = null;
			StringBuilder outputBuilder = new StringBuilder();
			StringBuilder errorBuilder = new StringBuilder();
			try
			{
				num = ExtendedProcess.Run(psi, timeout, options, null, delegate(string data)
				{
					ExtendedProcess.AddToProcessOutput(outputBuilder, data);
				}, delegate(string data)
				{
					ExtendedProcess.AddToProcessOutput(errorBuilder, data);
				});
			}
			finally
			{
				if (psi.RedirectStandardOutput)
				{
					StringBuilder stringBuilder = outputBuilder;
					lock (stringBuilder)
					{
						output = outputBuilder.ToString();
					}
				}
				if (psi.RedirectStandardError)
				{
					StringBuilder stringBuilder = errorBuilder;
					lock (stringBuilder)
					{
						error = errorBuilder.ToString();
					}
				}
			}
			if (!Tracing.RemovePersonallyIdentifiableInformationFromTraces)
			{
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "Additional details on command: \"{0}\": Args: \"{1}\", Exit code is: \"{2}\". STD out is: \"{3}\". STD err is \"{4}\"", new object[]
				{
					psi.FileName,
					psi.Arguments,
					num,
					psi.RedirectStandardOutput ? output : "Not redirected",
					psi.RedirectStandardError ? error : "Not redirected"
				});
			}
			return num;
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x00039404 File Offset: 0x00037604
		public static ProcessStartInfo CreateDefaultProcessStartInfo([NotNull] string fileName, string arguments)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(fileName, "fileName");
			return new ProcessStartInfo
			{
				Arguments = ((arguments == null) ? string.Empty : arguments),
				CreateNoWindow = true,
				FileName = fileName,
				RedirectStandardError = true,
				RedirectStandardInput = false,
				RedirectStandardOutput = true,
				UseShellExecute = false
			};
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0003945C File Offset: 0x0003765C
		public static Process Start(ProcessStartInfo psi, DataReceivedEventHandler outputDataReceived, DataReceivedEventHandler errorDataReceived)
		{
			return ExtendedProcess.Start(psi, outputDataReceived, errorDataReceived, null);
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x00039468 File Offset: 0x00037668
		public static Process Start(ProcessStartInfo psi, DataReceivedEventHandler outputDataReceived, DataReceivedEventHandler errorDataReceived, EventHandler processExited)
		{
			Process process = new Process
			{
				StartInfo = psi
			};
			Process process2;
			try
			{
				if (outputDataReceived != null)
				{
					process.OutputDataReceived += outputDataReceived;
				}
				if (errorDataReceived != null)
				{
					process.ErrorDataReceived += errorDataReceived;
				}
				if (processExited != null)
				{
					process.EnableRaisingEvents = true;
					process.Exited += processExited;
				}
				process.Start();
				if (outputDataReceived != null)
				{
					process.BeginOutputReadLine();
				}
				if (errorDataReceived != null)
				{
					process.BeginErrorReadLine();
				}
				process2 = process;
			}
			catch (Win32Exception ex)
			{
				RunProcessException ex2 = new RunProcessException("Cannot start process. See inner Exception for details", ex);
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "Caught exception: \"{0}\". Wrapping, and throwing Exception: \"{1}\"", new object[] { ex, ex2 });
				throw ex2;
			}
			catch (InvalidOperationException ex3)
			{
				RunProcessException ex4 = new RunProcessException("Cannot start process. See inner Exception for details", ex3);
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "Caught exception: \"{0}\". Wrapping, and throwing Exception: \"{1}\"", new object[] { ex3, ex4 });
				throw ex4;
			}
			return process2;
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x00039544 File Offset: 0x00037744
		private static void AddToProcessOutput(StringBuilder outputBuilder, string newLine)
		{
			if (!string.IsNullOrEmpty(newLine))
			{
				lock (outputBuilder)
				{
					outputBuilder.AppendLine(newLine);
				}
			}
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0003958C File Offset: 0x0003778C
		private static void KillProcess(Process p, string exceptionTextPrefix)
		{
			try
			{
				p.Kill();
			}
			catch (Win32Exception ex)
			{
				RunProcessException ex2 = new RunProcessException(exceptionTextPrefix + "Encountered error stopping the process. See inner Exception for details", ex);
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "Caught exception: \"{0}\". Wrapping, and throwing Exception: \"{1}\"", new object[] { ex, ex2 });
				throw ex2;
			}
			catch (InvalidOperationException ex3)
			{
				RunProcessException ex4 = new RunProcessException(exceptionTextPrefix + "Encountered error stopping the process. See inner Exception for details", ex3);
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "Caught exception: \"{0}\". Wrapping, and throwing Exception: \"{1}\"", new object[] { ex3, ex4 });
				throw ex4;
			}
		}
	}
}
