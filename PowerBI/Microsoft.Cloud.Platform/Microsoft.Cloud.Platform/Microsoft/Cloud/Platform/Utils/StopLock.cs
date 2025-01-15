using System;
using System.Threading;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002A5 RID: 677
	internal struct StopLock
	{
		// Token: 0x0600125B RID: 4699 RVA: 0x000401E3 File Offset: 0x0003E3E3
		public StopLock([NotNull] Action onStopCompleted)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Action>(onStopCompleted, "onStopCompleted");
			this.m_state = 0L;
			this.m_callback = onStopCompleted;
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x000401FF File Offset: 0x0003E3FF
		public bool TryEnter()
		{
			if (Interlocked.Increment(ref this.m_state) > 0L)
			{
				return true;
			}
			this.Leave();
			return false;
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x00040219 File Offset: 0x0003E419
		public void Leave()
		{
			if (Interlocked.Decrement(ref this.m_state) == -2147483648L)
			{
				this.InvokeCallbackIfPossible();
			}
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x00040234 File Offset: 0x0003E434
		public void Stop()
		{
			if (!this.InterlockedAddToStateIfEqualsOrHigher(-2147483648L, 0L))
			{
				return;
			}
			this.InvokeCallbackIfPossible();
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x0004024D File Offset: 0x0003E44D
		private void InvokeCallbackIfPossible()
		{
			if (!this.InterlockedAddToStateIfEquals(-19327352832L, -2147483648L))
			{
				return;
			}
			this.m_callback();
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00040274 File Offset: 0x0003E474
		private bool InterlockedAddToStateIfEqualsOrHigher(long addend, long comparand)
		{
			return ExtendedInterlocked.ReadModifyWrite(ref this.m_state, (long currentState) => currentState < comparand, (long currentState) => currentState + addend);
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x000402B8 File Offset: 0x0003E4B8
		private bool InterlockedAddToStateIfEquals(long addend, long comparand)
		{
			return ExtendedInterlocked.ReadModifyWrite(ref this.m_state, (long currentState) => currentState != comparand, (long currentState) => currentState + addend);
		}

		// Token: 0x040006D0 RID: 1744
		private long m_state;

		// Token: 0x040006D1 RID: 1745
		private Action m_callback;

		// Token: 0x040006D2 RID: 1746
		private const long c_stopping = -2147483648L;

		// Token: 0x040006D3 RID: 1747
		private const long c_stopped = -21474836480L;

		// Token: 0x040006D4 RID: 1748
		private const long c_maxShared = 1073741823L;
	}
}
