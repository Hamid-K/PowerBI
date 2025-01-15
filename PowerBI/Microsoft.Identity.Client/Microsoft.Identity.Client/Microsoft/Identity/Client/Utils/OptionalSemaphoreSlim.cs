using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Identity.Client.Utils
{
	// Token: 0x020001CD RID: 461
	internal class OptionalSemaphoreSlim
	{
		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x0600144C RID: 5196 RVA: 0x00045101 File Offset: 0x00043301
		public int CurrentCount
		{
			get
			{
				if (!this._useRealSemaphore)
				{
					return this._noLockCurrentCount;
				}
				return this._semaphoreSlim.CurrentCount;
			}
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x0004511D File Offset: 0x0004331D
		public string GetCurrentCountLogMessage()
		{
			return string.Format("Real semaphore: {0}. Count: {1}", this._useRealSemaphore, this.CurrentCount);
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x0004513F File Offset: 0x0004333F
		public OptionalSemaphoreSlim(bool useRealSemaphore)
		{
			this._useRealSemaphore = useRealSemaphore;
			if (this._useRealSemaphore)
			{
				this._semaphoreSlim = new SemaphoreSlim(1, 1);
			}
			this._noLockCurrentCount = 1;
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x0004516A File Offset: 0x0004336A
		public void Release()
		{
			if (this._useRealSemaphore)
			{
				this._semaphoreSlim.Release();
				return;
			}
			Interlocked.Increment(ref this._noLockCurrentCount);
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x0004518D File Offset: 0x0004338D
		public Task WaitAsync(CancellationToken cancellationToken)
		{
			if (this._useRealSemaphore)
			{
				return this._semaphoreSlim.WaitAsync(cancellationToken);
			}
			Interlocked.Decrement(ref this._noLockCurrentCount);
			return Task.FromResult<bool>(true);
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x000451B6 File Offset: 0x000433B6
		public void Wait()
		{
			if (this._useRealSemaphore)
			{
				this._semaphoreSlim.Wait();
				return;
			}
			Interlocked.Decrement(ref this._noLockCurrentCount);
		}

		// Token: 0x0400084F RID: 2127
		private readonly bool _useRealSemaphore;

		// Token: 0x04000850 RID: 2128
		private int _noLockCurrentCount;

		// Token: 0x04000851 RID: 2129
		private SemaphoreSlim _semaphoreSlim;
	}
}
