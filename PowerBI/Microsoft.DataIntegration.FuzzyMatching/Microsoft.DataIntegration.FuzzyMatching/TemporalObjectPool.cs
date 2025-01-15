using System;
using System.Threading;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.Concurrent.DotNet4;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000035 RID: 53
	internal class TemporalObjectPool
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x00008324 File Offset: 0x00006524
		public TemporalObjectPool(TemporalObjectPool.CreateObjectDelegate createObjectDelegate)
		{
			this.m_createObjectDelegate = createObjectDelegate;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00008340 File Offset: 0x00006540
		public TemporalHandle TryGetObject(IObjectManager objectManager)
		{
			DateTime now = DateTime.Now;
			while ((DateTime.Now - now).Milliseconds < GlobalConfiguration.TemporalObjectPoolTimeout)
			{
				TemporalHandle temporalHandle;
				if (this.m_stack.TryPop(out temporalHandle))
				{
					if (temporalHandle.TryGetObject() != null)
					{
						return temporalHandle;
					}
					Interlocked.Decrement(ref this.m_allocatedObjectCount);
				}
				if (Interlocked.Increment(ref this.m_allocatedObjectCount) <= this.MaxCount)
				{
					return this.m_createObjectDelegate(objectManager);
				}
				Interlocked.Decrement(ref this.m_allocatedObjectCount);
				Thread.Sleep(0);
			}
			throw new Exception("Timed out waiting for a free object in the object pool. Increase the pool size or the pool timeout.");
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000083D0 File Offset: 0x000065D0
		public void ReturnObject(TemporalHandle item)
		{
			if (this.m_stack.Count > this.MaxCount)
			{
				throw new Exception("Object pool size exceed the expected max size.");
			}
			this.m_stack.Push(item);
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001CB RID: 459 RVA: 0x000083FC File Offset: 0x000065FC
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00008404 File Offset: 0x00006604
		public int MaxCount { get; set; }

		// Token: 0x04000072 RID: 114
		private ConcurrentStack<TemporalHandle> m_stack = new ConcurrentStack<TemporalHandle>();

		// Token: 0x04000073 RID: 115
		private TemporalObjectPool.CreateObjectDelegate m_createObjectDelegate;

		// Token: 0x04000074 RID: 116
		private int m_allocatedObjectCount;

		// Token: 0x02000127 RID: 295
		// (Invoke) Token: 0x06000BDF RID: 3039
		public delegate TemporalHandle CreateObjectDelegate(IObjectManager objectManager);
	}
}
