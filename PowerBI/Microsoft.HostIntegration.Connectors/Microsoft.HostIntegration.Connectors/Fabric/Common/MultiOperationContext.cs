using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003F7 RID: 1015
	internal sealed class MultiOperationContext<T> : OperationContext where T : class
	{
		// Token: 0x0600238A RID: 9098 RVA: 0x0006CFEA File Offset: 0x0006B1EA
		public MultiOperationContext(AsyncCallback callback, object state, TimeSpan timeout, int numOps)
			: base(callback, state, timeout)
		{
			this.Initialize(numOps);
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x0006D00F File Offset: 0x0006B20F
		private void Initialize(int numOps)
		{
			this.m_numCompleted = 0;
			this.m_asyncOps = new List<IAsyncResult>(numOps);
			this.m_asyncTargets = new List<T>(numOps);
			this.m_asyncException = null;
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x0006D037 File Offset: 0x0006B237
		public void AsyncOperationBegan(T asyncTarget, IAsyncResult ar)
		{
			ReleaseAssert.IsTrue(this.m_asyncTargets.Count < this.m_asyncTargets.Capacity);
			this.m_asyncTargets.Add(asyncTarget);
			this.m_asyncOps.Add(ar);
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x0006D070 File Offset: 0x0006B270
		public void AllAsyncOperationsBegan()
		{
			this.m_asyncTargets.TrimExcess();
			this.m_asyncOps.TrimExcess();
			int count = this.m_asyncTargets.Count;
			if (Interlocked.Add(ref this.m_numCompleted, count) == 0)
			{
				base.OperationCompleted(true, null);
			}
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x0006D0B8 File Offset: 0x0006B2B8
		public void AsyncOperationEnded(int index, Exception asyncException)
		{
			ReleaseAssert.IsTrue(index < this.m_asyncTargets.Capacity);
			this.m_asyncOps[index] = null;
			if (asyncException == null)
			{
				this.m_asyncTargets[index] = default(T);
				return;
			}
			if (this.m_asyncException == null)
			{
				this.m_asyncException = asyncException;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x0600238F RID: 9103 RVA: 0x0006D10D File Offset: 0x0006B30D
		public List<IAsyncResult> AsyncOps
		{
			get
			{
				return this.m_asyncOps;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06002390 RID: 9104 RVA: 0x0006D115 File Offset: 0x0006B315
		public List<T> AsyncTargets
		{
			get
			{
				return this.m_asyncTargets;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06002391 RID: 9105 RVA: 0x0006D11D File Offset: 0x0006B31D
		// (set) Token: 0x06002392 RID: 9106 RVA: 0x0006D125 File Offset: 0x0006B325
		public Exception AsyncException
		{
			get
			{
				return this.m_asyncException;
			}
			set
			{
				this.m_asyncException = value;
			}
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x0006D12E File Offset: 0x0006B32E
		public void Reinitialize(AsyncCallback callback, object state, TimeSpan timeout, int numOps)
		{
			base.Reinitialize(callback, state, timeout);
			this.Initialize(numOps);
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x0006D144 File Offset: 0x0006B344
		private static void StaticOperationCompletedCallback(IAsyncResult ar)
		{
			MultiOperationContext<T> multiOperationContext = (MultiOperationContext<T>)ar.AsyncState;
			multiOperationContext.OperationCompletedCallback(ar);
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x0006D164 File Offset: 0x0006B364
		private void OperationCompletedCallback(IAsyncResult ar)
		{
			if (Interlocked.Decrement(ref this.m_numCompleted) == 0)
			{
				base.OperationCompleted(ar.CompletedSynchronously, null);
			}
		}

		// Token: 0x04001618 RID: 5656
		public AsyncCallback OperationCompletionCallback = new AsyncCallback(MultiOperationContext<T>.StaticOperationCompletedCallback);

		// Token: 0x04001619 RID: 5657
		private int m_numCompleted;

		// Token: 0x0400161A RID: 5658
		private List<IAsyncResult> m_asyncOps;

		// Token: 0x0400161B RID: 5659
		private List<T> m_asyncTargets;

		// Token: 0x0400161C RID: 5660
		private Exception m_asyncException;
	}
}
