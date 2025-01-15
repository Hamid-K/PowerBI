using System;
using System.Data.Entity.Resources;
using System.Threading;

namespace System.Data.Entity.Internal
{
	// Token: 0x0200012D RID: 301
	internal class ThrowingMonitor
	{
		// Token: 0x060014A4 RID: 5284 RVA: 0x00035F1A File Offset: 0x0003411A
		public void Enter()
		{
			if (Interlocked.CompareExchange(ref this._isInCriticalSection, 1, 0) != 0)
			{
				throw new NotSupportedException(Strings.ConcurrentMethodInvocation);
			}
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x00035F36 File Offset: 0x00034136
		public void Exit()
		{
			Interlocked.Exchange(ref this._isInCriticalSection, 0);
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x00035F45 File Offset: 0x00034145
		public void EnsureNotEntered()
		{
			Thread.MemoryBarrier();
			if (this._isInCriticalSection != 0)
			{
				throw new NotSupportedException(Strings.ConcurrentMethodInvocation);
			}
		}

		// Token: 0x040009B5 RID: 2485
		private int _isInCriticalSection;
	}
}
