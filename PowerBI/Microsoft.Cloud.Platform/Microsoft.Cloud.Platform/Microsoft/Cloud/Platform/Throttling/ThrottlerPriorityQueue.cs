using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Throttling
{
	// Token: 0x0200010B RID: 267
	internal class ThrottlerPriorityQueue<T> : IThrottlerQueue<T> where T : IPrioritizedObject
	{
		// Token: 0x06000758 RID: 1880 RVA: 0x00019F98 File Offset: 0x00018198
		private static int ComparePriorities(T arg1, T arg2)
		{
			return arg1.Priority.CompareTo(arg2.Priority);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00019FC7 File Offset: 0x000181C7
		public T Peek()
		{
			return this.m_queuedConcurrentRequests.Peek();
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00019FD4 File Offset: 0x000181D4
		public T Dequeue()
		{
			return this.m_queuedConcurrentRequests.Dequeue();
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x00019FE1 File Offset: 0x000181E1
		public int Count
		{
			get
			{
				return this.m_queuedConcurrentRequests.Count;
			}
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00019FEE File Offset: 0x000181EE
		public void Enqueue(T item)
		{
			this.m_queuedConcurrentRequests.Enqueue(item);
		}

		// Token: 0x0400029F RID: 671
		private readonly PriorityQueue<T> m_queuedConcurrentRequests = new PriorityQueue<T>(new Func<T, T, int>(ThrottlerPriorityQueue<T>.ComparePriorities));
	}
}
