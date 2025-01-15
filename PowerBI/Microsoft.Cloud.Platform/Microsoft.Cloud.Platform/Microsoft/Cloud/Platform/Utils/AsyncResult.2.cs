using System;
using System.Threading;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200018E RID: 398
	public class AsyncResult<TResult> : AsyncResult
	{
		// Token: 0x06000A44 RID: 2628 RVA: 0x00023A12 File Offset: 0x00021C12
		public AsyncResult(AsyncCallback callback, object context)
			: base(callback, context)
		{
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00023A1C File Offset: 0x00021C1C
		public AsyncResult(AsyncCallback callback, object context, TResult res)
			: base(callback, context)
		{
			this.m_result = res;
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00023A2D File Offset: 0x00021C2D
		public virtual void SignalCompletion(bool completedSynchronously, Exception ex)
		{
			base.SignalCompletionInternal(completedSynchronously, ex);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00023A37 File Offset: 0x00021C37
		public virtual void SignalCompletion(bool completedSynchronously, TResult res)
		{
			this.m_result = res;
			base.SignalCompletionInternal(completedSynchronously);
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00023A47 File Offset: 0x00021C47
		public virtual TResult End()
		{
			base.EndInternal();
			return this.m_result;
		}

		// Token: 0x04000406 RID: 1030
		private TResult m_result;

		// Token: 0x04000407 RID: 1031
		private AsyncResult<TResult>.TimeoutState m_timeoutState;

		// Token: 0x02000662 RID: 1634
		internal class TimeoutState
		{
			// Token: 0x06002D51 RID: 11601 RVA: 0x000A06F6 File Offset: 0x0009E8F6
			public TimeoutState(AsyncResult<TResult> owner, TResult key, int timeoutInMillisecond)
			{
				this.m_owner = owner;
				this.m_owner.m_timeoutState = this;
				this.Key = key;
				this.TimeoutInMillisecond = timeoutInMillisecond;
			}

			// Token: 0x17000725 RID: 1829
			// (get) Token: 0x06002D52 RID: 11602 RVA: 0x000A071F File Offset: 0x0009E91F
			public AsyncResult<TResult> AsyncResult
			{
				get
				{
					return this.m_owner;
				}
			}

			// Token: 0x17000726 RID: 1830
			// (get) Token: 0x06002D53 RID: 11603 RVA: 0x000A0727 File Offset: 0x0009E927
			// (set) Token: 0x06002D54 RID: 11604 RVA: 0x000A072F File Offset: 0x0009E92F
			public int TimeoutInMillisecond { get; private set; }

			// Token: 0x17000727 RID: 1831
			// (get) Token: 0x06002D55 RID: 11605 RVA: 0x000A0738 File Offset: 0x0009E938
			// (set) Token: 0x06002D56 RID: 11606 RVA: 0x000A0740 File Offset: 0x0009E940
			public TResult Key { get; private set; }

			// Token: 0x17000728 RID: 1832
			// (get) Token: 0x06002D57 RID: 11607 RVA: 0x000A0749 File Offset: 0x0009E949
			// (set) Token: 0x06002D58 RID: 11608 RVA: 0x000A0751 File Offset: 0x0009E951
			public Timer Timer { get; private set; }

			// Token: 0x06002D59 RID: 11609 RVA: 0x000A075A File Offset: 0x0009E95A
			public void SetTimer(Timer timer)
			{
				this.Timer = timer;
			}

			// Token: 0x04001201 RID: 4609
			private AsyncResult<TResult> m_owner;
		}
	}
}
