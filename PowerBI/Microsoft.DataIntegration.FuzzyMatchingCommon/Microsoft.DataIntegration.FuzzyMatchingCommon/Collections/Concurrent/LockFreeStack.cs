using System;
using System.Threading;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.Concurrent
{
	// Token: 0x020000AE RID: 174
	[Serializable]
	public sealed class LockFreeStack<T> where T : LockFreeStack<T>.Node
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x00028027 File Offset: 0x00026227
		public int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00028030 File Offset: 0x00026230
		public void Push(T node)
		{
			while (this.m_head != Interlocked.CompareExchange<T>(ref this.m_head, node, node.Next = this.m_head))
			{
			}
			Interlocked.Increment(ref this.m_count);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0002807C File Offset: 0x0002627C
		public T TryPop()
		{
			T head;
			do
			{
				head = this.m_head;
			}
			while (head != null && head != Interlocked.CompareExchange<T>(ref this.m_head, head.Next, head));
			if (head != null)
			{
				Interlocked.Decrement(ref this.m_count);
			}
			return head;
		}

		// Token: 0x0400017E RID: 382
		private T m_head;

		// Token: 0x0400017F RID: 383
		private int m_count;

		// Token: 0x02000147 RID: 327
		[Serializable]
		public class Node
		{
			// Token: 0x04000367 RID: 871
			public T Next;
		}
	}
}
