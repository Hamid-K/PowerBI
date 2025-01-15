using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Throttling
{
	// Token: 0x0200010A RID: 266
	internal class ThrottlerQueue<T> : IThrottlerQueue<T>
	{
		// Token: 0x06000753 RID: 1875 RVA: 0x00019F4D File Offset: 0x0001814D
		public T Peek()
		{
			return this.m_queuedConcurrentRequests.Peek();
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00019F5A File Offset: 0x0001815A
		public T Dequeue()
		{
			return this.m_queuedConcurrentRequests.Dequeue();
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x00019F67 File Offset: 0x00018167
		public int Count
		{
			get
			{
				return this.m_queuedConcurrentRequests.Count;
			}
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00019F74 File Offset: 0x00018174
		public void Enqueue(T item)
		{
			this.m_queuedConcurrentRequests.Enqueue(item);
		}

		// Token: 0x0400029E RID: 670
		private readonly Queue<T> m_queuedConcurrentRequests = new Queue<T>();
	}
}
