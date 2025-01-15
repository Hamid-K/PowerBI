using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x0200009C RID: 156
	[Serializable]
	public class BlockedSegmentArray64<T> : IMemoryUsage
	{
		// Token: 0x060006A9 RID: 1705 RVA: 0x00023B76 File Offset: 0x00021D76
		public BlockedSegmentArray64()
			: this(1, 262144)
		{
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00023B84 File Offset: 0x00021D84
		public BlockedSegmentArray64(int baseTypePerElement, int elementsPerBlock)
		{
			this.m_baseTypePerElement = baseTypePerElement;
			this.m_elementsPerBlock = (long)elementsPerBlock;
			this.m_baseTypePerBlock = (long)(baseTypePerElement * elementsPerBlock);
			this.m_blocks.Add(new T[this.m_baseTypePerBlock]);
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x00023BD2 File Offset: 0x00021DD2
		public long Count
		{
			get
			{
				return this.m_nextElementIndex;
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00023BDA File Offset: 0x00021DDA
		public void Reset()
		{
			this.m_nextElementIndex = 0L;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00023BE4 File Offset: 0x00021DE4
		public long New(int numElements)
		{
			if (numElements > 65535 || (long)numElements > this.m_elementsPerBlock)
			{
				throw new ArgumentOutOfRangeException(string.Format("Segments can contain at most {0} elements.", Math.Min(65535L, this.m_elementsPerBlock)));
			}
			if (numElements == 0)
			{
				throw new ArgumentException("Length must be > 0.");
			}
			int num = (int)(this.m_nextElementIndex * (long)this.m_baseTypePerElement % this.m_baseTypePerBlock) + numElements * this.m_baseTypePerElement;
			if ((long)num >= this.m_baseTypePerBlock)
			{
				int num2 = (int)(this.m_nextElementIndex * (long)this.m_baseTypePerElement / this.m_baseTypePerBlock) + 1;
				if (num2 == this.m_blocks.Count)
				{
					this.m_blocks.Add(new T[this.m_baseTypePerBlock]);
				}
				if ((long)num > this.m_baseTypePerBlock)
				{
					this.m_nextElementIndex = (long)num2 * this.m_elementsPerBlock;
				}
			}
			long num3 = ((long)numElements << 48) | this.m_nextElementIndex;
			this.m_nextElementIndex += (long)numElements;
			return num3;
		}

		// Token: 0x17000111 RID: 273
		public ArraySegment<T> this[long segmentReference]
		{
			get
			{
				ushort num = (ushort)((ulong)segmentReference >> 48);
				long num2 = segmentReference & 281474976710655L;
				return new ArraySegment<T>(this.m_blocks[(int)(num2 / this.m_elementsPerBlock)], (int)(num2 * (long)this.m_baseTypePerElement % this.m_baseTypePerBlock), (int)num * this.m_baseTypePerElement);
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x00023D25 File Offset: 0x00021F25
		public long MemoryUsage
		{
			get
			{
				return (long)this.m_blocks.Count * this.m_baseTypePerBlock * (long)Marshal.SizeOf(typeof(T));
			}
		}

		// Token: 0x0400014F RID: 335
		private int m_baseTypePerElement;

		// Token: 0x04000150 RID: 336
		private long m_baseTypePerBlock;

		// Token: 0x04000151 RID: 337
		private long m_elementsPerBlock;

		// Token: 0x04000152 RID: 338
		private List<T[]> m_blocks = new List<T[]>();

		// Token: 0x04000153 RID: 339
		private long m_nextElementIndex;
	}
}
