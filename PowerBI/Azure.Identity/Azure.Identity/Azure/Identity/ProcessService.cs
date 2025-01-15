using System;
using System.Diagnostics;

namespace Azure.Identity
{
	// Token: 0x0200007A RID: 122
	internal class ProcessService : IProcessService
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0000CF81 File Offset: 0x0000B181
		public static IProcessService Default { get; } = new ProcessService();

		// Token: 0x06000436 RID: 1078 RVA: 0x0000CF88 File Offset: 0x0000B188
		private ProcessService()
		{
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000CF90 File Offset: 0x0000B190
		public IProcess Create(ProcessStartInfo startInfo)
		{
			return new ProcessService.ProcessWrapper(startInfo);
		}

		// Token: 0x0200011D RID: 285
		private class ProcessWrapper : IProcess, IDisposable
		{
			// Token: 0x060005F1 RID: 1521 RVA: 0x0001A8FA File Offset: 0x00018AFA
			public ProcessWrapper(ProcessStartInfo processStartInfo)
			{
				this._process = new Process
				{
					StartInfo = processStartInfo,
					EnableRaisingEvents = true
				};
			}

			// Token: 0x17000156 RID: 342
			// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0001A91B File Offset: 0x00018B1B
			public bool HasExited
			{
				get
				{
					return this._process.HasExited;
				}
			}

			// Token: 0x17000157 RID: 343
			// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0001A928 File Offset: 0x00018B28
			public int ExitCode
			{
				get
				{
					return this._process.ExitCode;
				}
			}

			// Token: 0x17000158 RID: 344
			// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0001A935 File Offset: 0x00018B35
			// (set) Token: 0x060005F5 RID: 1525 RVA: 0x0001A942 File Offset: 0x00018B42
			public ProcessStartInfo StartInfo
			{
				get
				{
					return this._process.StartInfo;
				}
				set
				{
					this._process.StartInfo = value;
				}
			}

			// Token: 0x14000004 RID: 4
			// (add) Token: 0x060005F6 RID: 1526 RVA: 0x0001A950 File Offset: 0x00018B50
			// (remove) Token: 0x060005F7 RID: 1527 RVA: 0x0001A95E File Offset: 0x00018B5E
			public event EventHandler Exited
			{
				add
				{
					this._process.Exited += value;
				}
				remove
				{
					this._process.Exited -= value;
				}
			}

			// Token: 0x14000005 RID: 5
			// (add) Token: 0x060005F8 RID: 1528 RVA: 0x0001A96C File Offset: 0x00018B6C
			// (remove) Token: 0x060005F9 RID: 1529 RVA: 0x0001A97A File Offset: 0x00018B7A
			public event DataReceivedEventHandler OutputDataReceived
			{
				add
				{
					this._process.OutputDataReceived += value;
				}
				remove
				{
					this._process.OutputDataReceived -= value;
				}
			}

			// Token: 0x14000006 RID: 6
			// (add) Token: 0x060005FA RID: 1530 RVA: 0x0001A988 File Offset: 0x00018B88
			// (remove) Token: 0x060005FB RID: 1531 RVA: 0x0001A996 File Offset: 0x00018B96
			public event DataReceivedEventHandler ErrorDataReceived
			{
				add
				{
					this._process.ErrorDataReceived += value;
				}
				remove
				{
					this._process.ErrorDataReceived -= value;
				}
			}

			// Token: 0x060005FC RID: 1532 RVA: 0x0001A9A4 File Offset: 0x00018BA4
			public bool Start()
			{
				return this._process.Start();
			}

			// Token: 0x060005FD RID: 1533 RVA: 0x0001A9B1 File Offset: 0x00018BB1
			public void Kill()
			{
				this._process.Kill();
			}

			// Token: 0x060005FE RID: 1534 RVA: 0x0001A9BE File Offset: 0x00018BBE
			public void BeginOutputReadLine()
			{
				this._process.BeginOutputReadLine();
			}

			// Token: 0x060005FF RID: 1535 RVA: 0x0001A9CB File Offset: 0x00018BCB
			public void BeginErrorReadLine()
			{
				this._process.BeginErrorReadLine();
			}

			// Token: 0x06000600 RID: 1536 RVA: 0x0001A9D8 File Offset: 0x00018BD8
			public void Dispose()
			{
				this._process.Dispose();
			}

			// Token: 0x0400063F RID: 1599
			private readonly Process _process;
		}
	}
}
