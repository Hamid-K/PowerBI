using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004ED RID: 1261
	public static class ProcessUtils
	{
		// Token: 0x06001C0A RID: 7178 RVA: 0x00053E40 File Offset: 0x00052040
		public static int? RunProcess(string fileName, string arguments, out string output, out string error, string cwd = "", int timeoutInSeconds = -1)
		{
			ProcessUtils.OutputCapturer outputCapturer = new ProcessUtils.OutputCapturer();
			int? num = ProcessUtils.RunProcess(fileName, arguments, outputCapturer.OutputHandler, outputCapturer.ErrorHandler, cwd, timeoutInSeconds);
			output = outputCapturer.Output;
			error = outputCapturer.Error;
			return num;
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x00053E7C File Offset: 0x0005207C
		public static int? RunProcess(string fileName, string arguments, DataReceivedEventHandler outputHandler, DataReceivedEventHandler errorHandler, string cwd = "", int timeoutInSeconds = -1)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo
			{
				UseShellExecute = false,
				CreateNoWindow = true,
				RedirectStandardInput = true,
				RedirectStandardError = true,
				RedirectStandardOutput = true,
				FileName = fileName,
				Arguments = arguments,
				WorkingDirectory = cwd
			};
			int? num;
			using (Process process = new Process
			{
				StartInfo = processStartInfo
			})
			{
				process.OutputDataReceived += outputHandler;
				process.ErrorDataReceived += errorHandler;
				process.Start();
				process.BeginErrorReadLine();
				process.BeginOutputReadLine();
				if (!process.WaitForExit((timeoutInSeconds == -1) ? (-1) : ((10 + timeoutInSeconds) * 1000)))
				{
					string text = string.Join("\n", new string[] { arguments });
					try
					{
						process.Kill();
					}
					catch (InvalidOperationException)
					{
					}
					catch (Win32Exception ex)
					{
						throw new Exception("Process did not die after timeout: " + fileName + ". Arguments:\n" + text, ex);
					}
					num = null;
					num = num;
				}
				else
				{
					num = new int?(process.ExitCode);
				}
			}
			return num;
		}

		// Token: 0x04000DA8 RID: 3496
		private const int TimeoutAfterKill = 10;

		// Token: 0x020004EE RID: 1262
		private class OutputCapturer
		{
			// Token: 0x170004CE RID: 1230
			// (get) Token: 0x06001C0C RID: 7180 RVA: 0x00053F9C File Offset: 0x0005219C
			public DataReceivedEventHandler OutputHandler { get; }

			// Token: 0x170004CF RID: 1231
			// (get) Token: 0x06001C0D RID: 7181 RVA: 0x00053FA4 File Offset: 0x000521A4
			public DataReceivedEventHandler ErrorHandler { get; }

			// Token: 0x170004D0 RID: 1232
			// (get) Token: 0x06001C0E RID: 7182 RVA: 0x00053FAC File Offset: 0x000521AC
			public string Output
			{
				get
				{
					return this._outputBuilder.ToString();
				}
			}

			// Token: 0x170004D1 RID: 1233
			// (get) Token: 0x06001C0F RID: 7183 RVA: 0x00053FB9 File Offset: 0x000521B9
			public string Error
			{
				get
				{
					return this._errorBuilder.ToString();
				}
			}

			// Token: 0x06001C10 RID: 7184 RVA: 0x00053FC8 File Offset: 0x000521C8
			public OutputCapturer()
			{
				this.OutputHandler = delegate(object sender, DataReceivedEventArgs eventArgs)
				{
					if (eventArgs.Data != null)
					{
						this._outputBuilder.AppendLine(eventArgs.Data);
					}
				};
				this.ErrorHandler = delegate(object sender, DataReceivedEventArgs eventArgs)
				{
					if (eventArgs.Data != null)
					{
						this._errorBuilder.AppendLine(eventArgs.Data);
					}
				};
			}

			// Token: 0x04000DA9 RID: 3497
			private readonly StringBuilder _outputBuilder = new StringBuilder();

			// Token: 0x04000DAA RID: 3498
			private readonly StringBuilder _errorBuilder = new StringBuilder();
		}
	}
}
