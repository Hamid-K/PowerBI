using System;
using System.Threading.Tasks;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200029C RID: 668
	public class ColdTask
	{
		// Token: 0x06001217 RID: 4631 RVA: 0x0003F328 File Offset: 0x0003D528
		private ColdTask(Func<Task> creator)
		{
			this._creator = creator;
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x0003F342 File Offset: 0x0003D542
		public Task Task
		{
			get
			{
				this._task = this._task ?? this.CreateTask();
				return this._task;
			}
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x0003F360 File Offset: 0x0003D560
		public static ColdTask Create(Func<Task> creator)
		{
			return new ColdTask(creator);
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x0003F368 File Offset: 0x0003D568
		public Task StartAsync()
		{
			this._kickoff.SetResult(true);
			return this.Task;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x0003F37C File Offset: 0x0003D57C
		private async Task CreateTask()
		{
			await this._kickoff.Task;
			await this._creator();
		}

		// Token: 0x040006B1 RID: 1713
		private Func<Task> _creator;

		// Token: 0x040006B2 RID: 1714
		private Task _task;

		// Token: 0x040006B3 RID: 1715
		private TaskCompletionSource<bool> _kickoff = new TaskCompletionSource<bool>();
	}
}
