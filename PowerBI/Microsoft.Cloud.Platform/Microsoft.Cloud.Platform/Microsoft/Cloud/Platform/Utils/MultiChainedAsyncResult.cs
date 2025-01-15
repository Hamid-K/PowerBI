using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200025B RID: 603
	public class MultiChainedAsyncResult<TWorkTicket> : AsyncResult where TWorkTicket : WorkTicket
	{
		// Token: 0x06000FF2 RID: 4082 RVA: 0x00036F60 File Offset: 0x00035160
		public MultiChainedAsyncResult(AsyncCallback callback, object context, TWorkTicket ticket)
			: base(callback, context)
		{
			this.m_ticket = ticket;
			this.m_innerResults = null;
			this.m_numberOfCompletedInnerResults = 0;
			this.m_isCompletionSignaled = false;
			this.m_lock = new object();
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x00036F91 File Offset: 0x00035191
		public TWorkTicket WorkTicket
		{
			get
			{
				return this.m_ticket;
			}
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x00036F9C File Offset: 0x0003519C
		public void BeginJoin(IEnumerable<IAsyncResult> resultsToJoin)
		{
			bool flag = false;
			object @lock = this.m_lock;
			lock (@lock)
			{
				this.m_innerResults = new List<IAsyncResult>(resultsToJoin);
				if (this.m_numberOfCompletedInnerResults == this.m_innerResults.Count)
				{
					flag = true;
					this.m_isCompletionSignaled = true;
				}
			}
			if (flag)
			{
				base.SignalCompletionInternal(true);
			}
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0003700C File Offset: 0x0003520C
		public ReadOnlyCollection<IAsyncResult> EndJoin()
		{
			base.EndInternal();
			return this.m_innerResults.AsReadOnly();
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0003701F File Offset: 0x0003521F
		public bool HasValidWorkTicket()
		{
			return this.m_ticket != null && this.m_ticket.IsValid();
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x00037040 File Offset: 0x00035240
		public void BeginAsyncFunctionCallback(IAsyncResult asyncResult)
		{
			bool flag = false;
			object @lock = this.m_lock;
			lock (@lock)
			{
				this.m_numberOfCompletedInnerResults++;
				if (this.m_innerResults == null || this.m_isCompletionSignaled)
				{
					return;
				}
				if (this.m_numberOfCompletedInnerResults == this.m_innerResults.Count)
				{
					flag = true;
					this.m_isCompletionSignaled = true;
				}
			}
			if (flag)
			{
				base.SignalCompletionInternal(false);
			}
		}

		// Token: 0x040005F6 RID: 1526
		private readonly TWorkTicket m_ticket;

		// Token: 0x040005F7 RID: 1527
		private List<IAsyncResult> m_innerResults;

		// Token: 0x040005F8 RID: 1528
		private int m_numberOfCompletedInnerResults;

		// Token: 0x040005F9 RID: 1529
		private bool m_isCompletionSignaled;

		// Token: 0x040005FA RID: 1530
		private object m_lock;
	}
}
