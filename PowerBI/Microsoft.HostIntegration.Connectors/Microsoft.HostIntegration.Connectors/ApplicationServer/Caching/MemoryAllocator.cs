using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000369 RID: 873
	internal class MemoryAllocator
	{
		// Token: 0x06001EB1 RID: 7857 RVA: 0x0005E1C0 File Offset: 0x0005C3C0
		private int FindPoolIndex(long bufferId)
		{
			if (this.numberOfPools == 1)
			{
				return 0;
			}
			int num = (int)Math.Floor((double)bufferId / (double)this.processorPoolSize);
			if (num >= this.numberOfPools)
			{
				return this.numberOfPools - 1;
			}
			return num;
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x0005E1FC File Offset: 0x0005C3FC
		private long ConvertBufferIndexToPoolIndex(long bufferId)
		{
			if (this.numberOfPools == 1)
			{
				return bufferId;
			}
			int num = (int)Math.Floor((double)bufferId / (double)this.processorPoolSize);
			if (num >= this.numberOfPools)
			{
				return (long)this.processorPoolSize + bufferId % (long)this.processorPoolSize;
			}
			return bufferId % (long)this.processorPoolSize;
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x0005E249 File Offset: 0x0005C449
		private long ConvertPoolIndexIntoBufferIndex(long bufferId, int poolIndex)
		{
			if (this.numberOfPools == 1)
			{
				return bufferId;
			}
			return (long)(poolIndex * this.processorPoolSize) + bufferId;
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x0005E264 File Offset: 0x0005C464
		public MemoryAllocator(long tableSize, int recordSize)
		{
			this._maxMemoryCapacity = tableSize;
			this._bufferSize = recordSize;
			int num;
			if ((long)this._bufferSize >= 1024L)
			{
				num = 1 << (int)this.BufferBits;
			}
			else
			{
				int num2 = (1 << (int)this.BufferBits) * 1024;
				if ((this._bufferSize & (this._bufferSize - 1)) != 0)
				{
					int i;
					for (i = 1; i <= this._bufferSize; i += i)
					{
					}
					if (Provider.IsEnabled(TraceLevel.Info))
					{
						EventLogWriter.WriteInfo("RecordTable", "bufferSize set to {0} bytes. Input value was {1} bytes", new object[] { i, this._bufferSize });
					}
					this._bufferSize = i;
				}
				num = num2 / this._bufferSize;
				this.BufferBits = (byte)Math.Log((double)num, 2.0);
			}
			this._bufferNumberMask = (long)(num - 1);
			int num3 = (int)(tableSize / (long)this._bufferSize);
			int num4 = num3 / num;
			if (num3 < num)
			{
				num4 = 1;
			}
			this._blockList = new byte[num4][];
			this.processorPoolSize = (int)Math.Floor((double)(num3 / this.numberOfPools));
			this._freeBufferList = new PoolQueue<long>[this.numberOfPools];
			int num5 = 0;
			for (int j = 0; j < this.numberOfPools; j++)
			{
				int num6 = this.processorPoolSize;
				if (j == this.numberOfPools - 1)
				{
					num6 = num3 - num5;
				}
				this._freeBufferList[j] = new PoolQueue<long>((long)num6, -1L);
				num5 += num6;
			}
			for (long num7 = 0L; num7 < (long)this._blockList.Length; num7 += 1L)
			{
				this._blockList[(int)(checked((IntPtr)num7))] = new byte[num * this._bufferSize];
				int num8 = num;
				if (num3 < num)
				{
					num8 = num3;
				}
				for (int k = 0; k < num8; k++)
				{
					long num9 = (num7 << (int)this.BufferBits) + (long)k;
					int num10 = this.FindPoolIndex(num9);
					this._freeBufferList[num10].Enqueue(this.ConvertBufferIndexToPoolIndex(num9));
				}
			}
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x0005E46C File Offset: 0x0005C66C
		private int PoolNumberToUse()
		{
			int num;
			if (this.numberOfPools == 1)
			{
				num = 0;
			}
			else
			{
				num = NativeMethods.GetCurrentProcessorNumber();
			}
			return num;
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001EB6 RID: 7862 RVA: 0x0005E48D File Offset: 0x0005C68D
		public int BufferCapacity
		{
			get
			{
				return this._bufferSize - 8;
			}
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x0005E498 File Offset: 0x0005C698
		public long AllocBuffer()
		{
			if (this.FreeBufferCount() == 0L)
			{
				throw new DataCacheException("MemoryPool", 18001, string.Format(CultureInfo.CurrentUICulture, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 18001), new object[] { "MemoryPool" }));
			}
			int num = this.PoolNumberToUse();
			long num2 = this._freeBufferList[num].Dequeue();
			if (num2 == -1L)
			{
				for (int i = 0; i < this.numberOfPools; i++)
				{
					num2 = this._freeBufferList[i].Dequeue();
					if (num2 != -1L)
					{
						return this.ConvertPoolIndexIntoBufferIndex(num2, i);
					}
				}
				throw new DataCacheException("MemoryPool", 18001, string.Format(CultureInfo.CurrentUICulture, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 18001), new object[] { "MemoryPool" }));
			}
			return this.ConvertPoolIndexIntoBufferIndex(num2, num);
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x0005E578 File Offset: 0x0005C778
		public void FreeBuffer(long BufferId)
		{
			int num = this.FindPoolIndex(BufferId);
			this._freeBufferList[num].Enqueue(this.ConvertBufferIndexToPoolIndex(BufferId));
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x0005E5A4 File Offset: 0x0005C7A4
		public Buffer AccessBuffer(long BufferId)
		{
			long num = BufferId >> (int)this.BufferBits;
			long num2 = BufferId & this._bufferNumberMask;
			return new Buffer(BufferId, this._blockList[(int)(checked((IntPtr)num))], num2 * (long)this._bufferSize, this._bufferSize);
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x0005E5E4 File Offset: 0x0005C7E4
		public long FreeBufferCount()
		{
			long num = 0L;
			for (int i = 0; i < this.numberOfPools; i++)
			{
				num += this._freeBufferList[i].Count;
			}
			return num;
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x0005E616 File Offset: 0x0005C816
		public long AvailableMemory()
		{
			return this.FreeBufferCount() * (long)this._bufferSize;
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x0005E626 File Offset: 0x0005C826
		public long MemoryUsage()
		{
			return this._maxMemoryCapacity - this.AvailableMemory();
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x0005E635 File Offset: 0x0005C835
		public long MaxMemoryCapacity()
		{
			return this._maxMemoryCapacity;
		}

		// Token: 0x0400115E RID: 4446
		private const long DefaultIndex = -1L;

		// Token: 0x0400115F RID: 4447
		private byte BufferBits = 8;

		// Token: 0x04001160 RID: 4448
		private byte[][] _blockList;

		// Token: 0x04001161 RID: 4449
		private int numberOfPools = 1;

		// Token: 0x04001162 RID: 4450
		private PoolQueue<long>[] _freeBufferList;

		// Token: 0x04001163 RID: 4451
		private readonly long _maxMemoryCapacity;

		// Token: 0x04001164 RID: 4452
		private readonly int _bufferSize;

		// Token: 0x04001165 RID: 4453
		private long _bufferNumberMask;

		// Token: 0x04001166 RID: 4454
		private int processorPoolSize;
	}
}
