using System;

namespace Microsoft.SqlServer.XEvent.Linq.Internal
{
	// Token: 0x020000DC RID: 220
	internal class XEventCachedBuffer
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0001CDB0 File Offset: 0x0001CDB0
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x0001CDC4 File Offset: 0x0001CDC4
		public BufferLocator BufferId { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0001CDD8 File Offset: 0x0001CDD8
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0001CDEC File Offset: 0x0001CDEC
		public int BufferSize { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0001CE00 File Offset: 0x0001CE00
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x0001CE14 File Offset: 0x0001CE14
		public XEventCachedBuffer Next { get; set; }

		// Token: 0x060002F8 RID: 760 RVA: 0x0001CE28 File Offset: 0x0001CE28
		public XEventCachedBuffer(BufferLocator bufId, byte[] buffer, IEventBufferStore store)
		{
			this.BufferId = bufId;
			this.m_backingStore = store;
			this.m_buffer = new WeakReference(buffer);
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0001CE58 File Offset: 0x0001CE58
		public bool IsAlive
		{
			get
			{
				return this.m_buffer.IsAlive;
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0001CE70 File Offset: 0x0001CE70
		public byte[] GetBuffer()
		{
			byte[] array = null;
			lock (this)
			{
				if (this.m_buffer.IsAlive)
				{
					array = (byte[])this.m_buffer.Target;
				}
				if (array == null && this.m_backingStore.GetBufferDirect(this.BufferId, this.BufferSize, out array))
				{
					this.m_buffer.Target = array;
				}
			}
			return array;
		}

		// Token: 0x040002B6 RID: 694
		private WeakReference m_buffer;

		// Token: 0x040002B7 RID: 695
		private IEventBufferStore m_backingStore;
	}
}
