using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200014C RID: 332
	public sealed class AsyncProcess : IDisposable
	{
		// Token: 0x060008A3 RID: 2211 RVA: 0x0001E2D4 File Offset: 0x0001C4D4
		private AsyncProcess(ProcessStartInfo startInfo, AsyncProcessOptions options)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ProcessStartInfo>(startInfo, "startInfo");
			this.m_options = options;
			this.m_process = new Process();
			this.m_process.StartInfo = startInfo;
			this.m_exited = new TaskCompletionSource<bool>();
			this.m_process.EnableRaisingEvents = true;
			this.m_process.Exited += this.OnProcessExited;
			this.m_process.Start();
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x0001E34A File Offset: 0x0001C54A
		public Process Process
		{
			get
			{
				return this.m_process;
			}
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0001E352 File Offset: 0x0001C552
		public static AsyncProcess Start(ProcessStartInfo startInfo, AsyncProcessOptions options = AsyncProcessOptions.None)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ProcessStartInfo>(startInfo, "startInfo");
			return new AsyncProcess(startInfo, options);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0001E368 File Offset: 0x0001C568
		public async Task<bool> WaitForExitAsync()
		{
			return await this.m_exited.Task;
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0001E3B0 File Offset: 0x0001C5B0
		public async Task<bool> WaitForExitAsync(TimeSpan timeout)
		{
			TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
			bool flag;
			using (CancellationTokenSource cts = new CancellationTokenSource())
			{
				Task<bool> exitedTask = this.m_exited.Task;
				Task task;
				using (cts.Token.Register(delegate(object e)
				{
					((TaskCompletionSource<bool>)e).SetResult(true);
				}, taskCompletionSource))
				{
					cts.CancelAfter(timeout);
					task = await Task.WhenAny<bool>(new Task<bool>[] { taskCompletionSource.Task, exitedTask });
				}
				CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
				if (task == exitedTask)
				{
					flag = true;
				}
				else
				{
					if (this.m_options.HasFlag(AsyncProcessOptions.KillProcessOnTimeout))
					{
						this.m_process.Kill();
					}
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0001E400 File Offset: 0x0001C600
		public async Task<bool> WaitForExitAsync(int timeoutInMilliseconds)
		{
			return await this.WaitForExitAsync(TimeSpan.FromMilliseconds((double)timeoutInMilliseconds));
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0001E44D File Offset: 0x0001C64D
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0001E458 File Offset: 0x0001C658
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.m_process.EnableRaisingEvents = false;
				this.m_process.Exited -= this.OnProcessExited;
				this.m_exited.TrySetException(new ObjectDisposedException("AsyncProcess"));
				this.m_process.Dispose();
			}
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0001E4AC File Offset: 0x0001C6AC
		private void OnProcessExited(object sender, EventArgs e)
		{
			this.m_exited.TrySetResult(true);
		}

		// Token: 0x04000348 RID: 840
		private readonly AsyncProcessOptions m_options;

		// Token: 0x04000349 RID: 841
		private readonly Process m_process;

		// Token: 0x0400034A RID: 842
		private readonly TaskCompletionSource<bool> m_exited;
	}
}
