using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.DataIntegration.FuzzyMatchingCommon.IO;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000066 RID: 102
	[Serializable]
	internal class ByteSegmentAllocator
	{
		// Token: 0x060003E0 RID: 992 RVA: 0x0001A6C8 File Offset: 0x000188C8
		public ByteSegmentAllocator()
		{
			this.m_blocks.Add(new ByteSegmentAllocator.Block
			{
				Count = 0,
				Array = new byte[this.BlockLength]
			});
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0001A720 File Offset: 0x00018920
		public ArraySegment<byte> GetSegment(long position)
		{
			ByteSegmentAllocator.Block block = this.m_blocks[(int)(position / (long)this.BlockLength)];
			int num = -1;
			int num2 = (int)(position % (long)this.BlockLength);
			if (this.StoreSegmentLength)
			{
				num = StreamUtilities.ReadInt32(block.Array, num2);
				num2 += 4;
			}
			return new ArraySegment<byte>(block.Array, num2, num);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0001A778 File Offset: 0x00018978
		public long New(int length)
		{
			ArraySegment<byte> arraySegment;
			return this.New(length, out arraySegment);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0001A790 File Offset: 0x00018990
		public long New(int requestedLength, out ArraySegment<byte> segment)
		{
			int num = 0;
			int num2 = requestedLength;
			if (this.StoreSegmentLength)
			{
				num = 4;
				num2 += num;
			}
			if (num2 > this.BlockLength)
			{
				throw new ArgumentOutOfRangeException("Length must be less than block length minus any per segment overhead.");
			}
			ByteSegmentAllocator.Block block;
			int num4;
			int num5;
			do
			{
				int num3 = this.m_blocks.Count - 1;
				block = this.m_blocks[num3];
				num4 = block.Count;
				num5 = this.BlockLength * num3;
				if (num2 > block.Array.Length - num4)
				{
					List<ByteSegmentAllocator.Block> blocks = this.m_blocks;
					lock (blocks)
					{
						if (block != this.m_blocks[this.m_blocks.Count - 1])
						{
							continue;
						}
						block = new ByteSegmentAllocator.Block
						{
							Count = num2,
							Array = new byte[this.BlockLength]
						};
						num4 = 0;
						num5 = this.BlockLength * this.m_blocks.Count;
						this.m_blocks.Add(block);
					}
					break;
				}
			}
			while (num4 != Interlocked.CompareExchange(ref block.Count, num4 + num2, num4));
			if (this.StoreSegmentLength)
			{
				StreamUtilities.WriteInt32(block.Array, num4, requestedLength);
			}
			segment = new ArraySegment<byte>(block.Array, num4 + num, requestedLength);
			return (long)(num5 + num4);
		}

		// Token: 0x040000A0 RID: 160
		private int BlockLength = 16777216;

		// Token: 0x040000A1 RID: 161
		private List<ByteSegmentAllocator.Block> m_blocks = new List<ByteSegmentAllocator.Block>();

		// Token: 0x040000A2 RID: 162
		private bool StoreSegmentLength = true;

		// Token: 0x020000EA RID: 234
		[Serializable]
		private class Block
		{
			// Token: 0x0400024D RID: 589
			public byte[] Array;

			// Token: 0x0400024E RID: 590
			public int Count;
		}
	}
}
