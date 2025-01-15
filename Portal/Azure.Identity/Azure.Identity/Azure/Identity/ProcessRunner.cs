using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.Identity
{
	// Token: 0x02000079 RID: 121
	internal sealed class ProcessRunner : IDisposable
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000CB7F File Offset: 0x0000AD7F
		public int ExitCode
		{
			get
			{
				return this._process.ExitCode;
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000CB8C File Offset: 0x0000AD8C
		public ProcessRunner(IProcess process, TimeSpan timeout, bool logPII, CancellationToken cancellationToken)
		{
			this._logPII = logPII;
			this._process = process;
			this._timeout = timeout;
			if (this._logPII)
			{
				AzureIdentityEventSource.Singleton.ProcessRunnerInformational("Running process `" + process.StartInfo.FileName + "' with arguments " + string.Join(", ", new string[] { process.StartInfo.Arguments }));
			}
			this._outputData = new List<string>();
			this._errorData = new List<string>();
			this._outputTcs = new TaskCompletionSource<ICollection<string>>(TaskCreationOptions.RunContinuationsAsynchronously);
			this._errorTcs = new TaskCompletionSource<ICollection<string>>(TaskCreationOptions.RunContinuationsAsynchronously);
			this._tcs = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);
			if (timeout.TotalMilliseconds >= 0.0)
			{
				this._timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(new CancellationToken[] { cancellationToken });
				this._cancellationToken = this._timeoutCts.Token;
				return;
			}
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000CC81 File Offset: 0x0000AE81
		public Task<string> RunAsync()
		{
			this.StartProcess();
			return this._tcs.Task;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000CC94 File Offset: 0x0000AE94
		public string Run()
		{
			this.StartProcess();
			return this._tcs.Task.GetAwaiter().GetResult();
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000CCC0 File Offset: 0x0000AEC0
		private void StartProcess()
		{
			if (this.TrySetCanceled() || this._tcs.Task.IsCompleted)
			{
				return;
			}
			this._process.StartInfo.UseShellExecute = false;
			this._process.StartInfo.RedirectStandardOutput = true;
			this._process.StartInfo.RedirectStandardError = true;
			this._process.OutputDataReceived += delegate(object sender, DataReceivedEventArgs args)
			{
				ProcessRunner.OnDataReceived(args, this._outputData, this._outputTcs);
			};
			this._process.ErrorDataReceived += delegate(object sender, DataReceivedEventArgs args)
			{
				ProcessRunner.OnDataReceived(args, this._errorData, this._errorTcs);
			};
			this._process.Exited += delegate(object o, EventArgs e)
			{
				this.HandleExitAsync();
			};
			CancellationTokenSource timeoutCts = this._timeoutCts;
			if (timeoutCts != null)
			{
				timeoutCts.CancelAfter(this._timeout);
			}
			if (!this._process.Start())
			{
				this.TrySetException(new InvalidOperationException("Failed to start process '" + this._process.StartInfo.FileName + "'"));
			}
			this._process.BeginOutputReadLine();
			this._process.BeginErrorReadLine();
			this._ctRegistration = this._cancellationToken.Register(new Action(this.HandleCancel), false);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000CDE8 File Offset: 0x0000AFE8
		private async ValueTask HandleExitAsync()
		{
			if (this._process.ExitCode == 0)
			{
				ICollection<string> collection = await this._outputTcs.Task.ConfigureAwait(false);
				this.TrySetResult(string.Join(Environment.NewLine, collection));
			}
			else
			{
				ICollection<string> collection2 = await this._errorTcs.Task.ConfigureAwait(false);
				this.TrySetException(new InvalidOperationException(string.Join(Environment.NewLine, collection2)));
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000CE2C File Offset: 0x0000B02C
		private void HandleCancel()
		{
			if (this._tcs.Task.IsCompleted)
			{
				return;
			}
			if (!this._process.HasExited)
			{
				try
				{
					this._process.Kill();
				}
				catch (Exception ex)
				{
					this.TrySetException(ex);
					return;
				}
			}
			this.TrySetCanceled();
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000CE88 File Offset: 0x0000B088
		private static void OnDataReceived(DataReceivedEventArgs args, ICollection<string> data, TaskCompletionSource<ICollection<string>> tcs)
		{
			if (args.Data != null)
			{
				data.Add(args.Data);
				return;
			}
			tcs.SetResult(data);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000CEA6 File Offset: 0x0000B0A6
		private void TrySetResult(string result)
		{
			this._tcs.TrySetResult(result);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000CEB8 File Offset: 0x0000B0B8
		private bool TrySetCanceled()
		{
			if (this._cancellationToken.IsCancellationRequested)
			{
				this._tcs.TrySetCanceled(this._cancellationToken);
			}
			return this._cancellationToken.IsCancellationRequested;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000CEF5 File Offset: 0x0000B0F5
		private void TrySetException(Exception exception)
		{
			if (this._logPII)
			{
				AzureIdentityEventSource.Singleton.ProcessRunnerError(exception.ToString());
			}
			this._tcs.TrySetException(exception);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000CF1C File Offset: 0x0000B11C
		public void Dispose()
		{
			this._tcs.TrySetCanceled();
			this._process.Dispose();
			this._ctRegistration.Dispose();
			CancellationTokenSource timeoutCts = this._timeoutCts;
			if (timeoutCts == null)
			{
				return;
			}
			timeoutCts.Dispose();
		}

		// Token: 0x04000259 RID: 601
		private readonly IProcess _process;

		// Token: 0x0400025A RID: 602
		private readonly TimeSpan _timeout;

		// Token: 0x0400025B RID: 603
		private readonly TaskCompletionSource<string> _tcs;

		// Token: 0x0400025C RID: 604
		private readonly TaskCompletionSource<ICollection<string>> _outputTcs;

		// Token: 0x0400025D RID: 605
		private readonly TaskCompletionSource<ICollection<string>> _errorTcs;

		// Token: 0x0400025E RID: 606
		private readonly ICollection<string> _outputData;

		// Token: 0x0400025F RID: 607
		private readonly ICollection<string> _errorData;

		// Token: 0x04000260 RID: 608
		private readonly CancellationToken _cancellationToken;

		// Token: 0x04000261 RID: 609
		private readonly CancellationTokenSource _timeoutCts;

		// Token: 0x04000262 RID: 610
		private CancellationTokenRegistration _ctRegistration;

		// Token: 0x04000263 RID: 611
		private bool _logPII;
	}
}
