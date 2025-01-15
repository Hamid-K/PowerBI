using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.SqlServer.XEvent.Linq.Internal
{
	// Token: 0x020000C5 RID: 197
	internal class XEventEnumerator : IEnumerator<PublishedEvent>, IDisposable, IEnumerator
	{
		// Token: 0x06000267 RID: 615 RVA: 0x0001C074 File Offset: 0x0001C074
		public XEventEnumerator(IEventBufferStore store, XEventInteropMetadataManager metadata)
		{
			this.m_metadata = metadata;
			this.m_bufferStore = store;
			this.m_bufId = null;
			this.m_current = null;
			this.m_state = XEventEnumerator.EnumerationState.NextBuf;
			this.m_bufferProcessor = new XEventInteropBufferProcessor(this.m_metadata);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0001C0BC File Offset: 0x0001C0BC
		public bool MoveNext()
		{
			bool flag = false;
			if (this.m_state == XEventEnumerator.EnumerationState.NextEvent)
			{
				flag = this.m_bufferProcessor.MoveNext();
				if (!flag)
				{
					this.m_state = XEventEnumerator.EnumerationState.NextBuf;
				}
			}
			if (this.m_state == XEventEnumerator.EnumerationState.NextBuf)
			{
				while (this.m_bufferStore.GetNextBuffer(this.m_bufId, out this.m_bufId, out this.m_buf))
				{
					if (this.m_bufferProcessor.Reset(this.m_bufId, this.m_buf, 0))
					{
						this.m_state = XEventEnumerator.EnumerationState.NextEvent;
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				this.m_current = this.m_bufferProcessor.Current;
			}
			else if (this.m_bufferStore.LastException != null)
			{
				throw new EventEnumerationException(Resources.GetString("EventEnumerationExceptionString"), this.m_bufferStore.LastException);
			}
			return flag;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0001C178 File Offset: 0x0001C178
		public PublishedEvent Current
		{
			get
			{
				return this.m_current;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0001C18C File Offset: 0x0001C18C
		object IEnumerator.Current
		{
			get
			{
				return this.m_current;
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0001C1A0 File Offset: 0x0001C1A0
		public void Reset()
		{
			this.m_bufId = null;
			this.m_state = XEventEnumerator.EnumerationState.NextBuf;
			this.m_current = null;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0001C1C4 File Offset: 0x0001C1C4
		public void Dispose()
		{
			this.m_bufferProcessor.Dispose();
		}

		// Token: 0x0400027E RID: 638
		private PublishedEvent m_current;

		// Token: 0x0400027F RID: 639
		private byte[] m_buf;

		// Token: 0x04000280 RID: 640
		private BufferLocator m_bufId;

		// Token: 0x04000281 RID: 641
		private XEventInteropMetadataManager m_metadata;

		// Token: 0x04000282 RID: 642
		private XEventInteropBufferProcessor m_bufferProcessor;

		// Token: 0x04000283 RID: 643
		private IEventBufferStore m_bufferStore;

		// Token: 0x04000284 RID: 644
		private XEventEnumerator.EnumerationState m_state;

		// Token: 0x020000C7 RID: 199
		private enum EnumerationState
		{
			// Token: 0x04000289 RID: 649
			NextBuf,
			// Token: 0x0400028A RID: 650
			NextEvent
		}
	}
}
