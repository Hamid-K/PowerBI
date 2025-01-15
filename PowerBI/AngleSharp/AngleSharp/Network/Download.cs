using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;

namespace AngleSharp.Network
{
	// Token: 0x0200009E RID: 158
	internal sealed class Download : IDownload, ICancellable<IResponse>, ICancellable
	{
		// Token: 0x060004AF RID: 1199 RVA: 0x0001E957 File Offset: 0x0001CB57
		public Download(Task<IResponse> task, CancellationTokenSource cts, Url target, INode originator)
		{
			this._task = task;
			this._cts = cts;
			this._target = target;
			this._originator = originator;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0001E97C File Offset: 0x0001CB7C
		public INode Originator
		{
			get
			{
				return this._originator;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0001E984 File Offset: 0x0001CB84
		public Url Target
		{
			get
			{
				return this._target;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0001E98C File Offset: 0x0001CB8C
		public Task<IResponse> Task
		{
			get
			{
				return this._task;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0001E994 File Offset: 0x0001CB94
		public bool IsRunning
		{
			get
			{
				return this._task.Status == TaskStatus.Running;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0001E9A4 File Offset: 0x0001CBA4
		public bool IsCompleted
		{
			get
			{
				return this._task.Status == TaskStatus.Faulted || this._task.Status == TaskStatus.RanToCompletion || this._task.Status == TaskStatus.Canceled;
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0001E9D2 File Offset: 0x0001CBD2
		public void Cancel()
		{
			this._cts.Cancel();
		}

		// Token: 0x040003BD RID: 957
		private readonly CancellationTokenSource _cts;

		// Token: 0x040003BE RID: 958
		private readonly Task<IResponse> _task;

		// Token: 0x040003BF RID: 959
		private readonly Url _target;

		// Token: 0x040003C0 RID: 960
		private readonly INode _originator;
	}
}
