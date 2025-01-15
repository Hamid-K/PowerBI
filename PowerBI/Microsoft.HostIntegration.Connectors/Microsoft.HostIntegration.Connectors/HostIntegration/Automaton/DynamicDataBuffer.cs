using System;

namespace Microsoft.HostIntegration.Automaton
{
	// Token: 0x020004C8 RID: 1224
	public class DynamicDataBuffer
	{
		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x060029D6 RID: 10710 RVA: 0x0007E33A File Offset: 0x0007C53A
		// (set) Token: 0x060029D7 RID: 10711 RVA: 0x0007E342 File Offset: 0x0007C542
		public int UsedLength { get; set; }

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x060029D8 RID: 10712 RVA: 0x0007E34B File Offset: 0x0007C54B
		public byte[] Data
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x0007E353 File Offset: 0x0007C553
		public DynamicDataBuffer()
		{
			this.InternalConstructor(null, 0);
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x0007E363 File Offset: 0x0007C563
		public DynamicDataBuffer(int capacity)
		{
			this.InternalConstructor(null, capacity);
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x0007E373 File Offset: 0x0007C573
		public DynamicDataBuffer(IDynamicDataBufferOwner owner)
		{
			this.InternalConstructor(owner, 0);
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x0007E383 File Offset: 0x0007C583
		public DynamicDataBuffer(IDynamicDataBufferOwner owner, int capacity)
		{
			this.InternalConstructor(owner, capacity);
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x0007E394 File Offset: 0x0007C594
		private void InternalConstructor(IDynamicDataBufferOwner owner, int capacity)
		{
			if (capacity < 4096)
			{
				capacity = 4096;
			}
			if (capacity % 4096 != 0)
			{
				capacity = (capacity / 4096 + 1) * 4096;
			}
			this.buffer = new byte[capacity];
			this.UsedLength = 0;
			this.owner = owner;
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x0007E3E4 File Offset: 0x0007C5E4
		public void ReturnToOwner()
		{
			if (this.owner != null)
			{
				this.owner.ReturnDataBuffer(this);
			}
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x0007E3FA File Offset: 0x0007C5FA
		public void ClearOwner()
		{
			this.owner = null;
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x0007E403 File Offset: 0x0007C603
		public void Resize(int newCapacity)
		{
			if (newCapacity <= this.buffer.Length)
			{
				return;
			}
			if (newCapacity % 4096 != 0)
			{
				newCapacity = (newCapacity / 4096 + 1) * 4096;
			}
			Array.Resize<byte>(ref this.buffer, newCapacity);
		}

		// Token: 0x0400189B RID: 6299
		public const int MaxDynamicBuffers = 10;

		// Token: 0x0400189C RID: 6300
		public const int StandardCapacity = 4096;

		// Token: 0x0400189D RID: 6301
		private byte[] buffer;

		// Token: 0x0400189E RID: 6302
		private IDynamicDataBufferOwner owner;
	}
}
