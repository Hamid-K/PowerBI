using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Diagnostics.Tracing.Internal;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200002A RID: 42
	[SecurityCritical]
	internal struct DataCollector
	{
		// Token: 0x0600015D RID: 349 RVA: 0x0000AEF4 File Offset: 0x000090F4
		internal unsafe void Enable(byte* scratch, int scratchSize, EventSource.EventData* datas, int dataCount, GCHandle* pins, int pinCount)
		{
			this.datasStart = datas;
			this.scratchEnd = scratch + scratchSize;
			this.datasEnd = datas + dataCount;
			this.pinsEnd = pins + pinCount;
			this.scratch = scratch;
			this.datas = datas;
			this.pins = pins;
			this.writingScalars = false;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000AF53 File Offset: 0x00009153
		internal void Disable()
		{
			this = default(DataCollector);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000AF5C File Offset: 0x0000915C
		internal unsafe EventSource.EventData* Finish()
		{
			this.ScalarsEnd();
			return this.datas;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000AF6C File Offset: 0x0000916C
		internal unsafe void AddScalar(void* value, int size)
		{
			if (this.bufferNesting != 0)
			{
				int num = this.bufferPos;
				int num2;
				checked
				{
					this.bufferPos += size;
					this.EnsureBuffer();
					num2 = 0;
				}
				while (num2 != size)
				{
					this.buffer[num] = ((byte*)value)[num2];
					num2++;
					num++;
				}
				return;
			}
			byte* ptr = this.scratch;
			byte* ptr2 = ptr + size;
			if (this.scratchEnd < ptr2)
			{
				throw new IndexOutOfRangeException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_AddScalarOutOfRange", Array.Empty<object>()));
			}
			this.ScalarsBegin();
			this.scratch = ptr2;
			for (int num3 = 0; num3 != size; num3++)
			{
				ptr[num3] = ((byte*)value)[num3];
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000B010 File Offset: 0x00009210
		internal unsafe void AddBinary(string value, int size)
		{
			if (size > 65535)
			{
				size = 65534;
			}
			if (this.bufferNesting != 0)
			{
				this.EnsureBuffer(size + 2);
			}
			this.AddScalar((void*)(&size), 2);
			if (size != 0)
			{
				if (this.bufferNesting == 0)
				{
					this.ScalarsEnd();
					this.PinArray(value, size);
					return;
				}
				int num = this.bufferPos;
				checked
				{
					this.bufferPos += size;
					this.EnsureBuffer();
				}
				fixed (string text = value)
				{
					void* ptr = text;
					if (ptr != null)
					{
						ptr = (void*)((byte*)ptr + RuntimeHelpers.OffsetToStringData);
					}
					Marshal.Copy((IntPtr)ptr, this.buffer, num, size);
				}
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000B0A1 File Offset: 0x000092A1
		internal void AddBinary(Array value, int size)
		{
			this.AddArray(value, size, 1);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000B0AC File Offset: 0x000092AC
		internal unsafe void AddArray(Array value, int length, int itemSize)
		{
			if (length > 65535)
			{
				length = 65535;
			}
			int num = length * itemSize;
			if (this.bufferNesting != 0)
			{
				this.EnsureBuffer(num + 2);
			}
			this.AddScalar((void*)(&length), 2);
			checked
			{
				if (length != 0)
				{
					if (this.bufferNesting == 0)
					{
						this.ScalarsEnd();
						this.PinArray(value, num);
						return;
					}
					int num2 = this.bufferPos;
					this.bufferPos += num;
					this.EnsureBuffer();
					Buffer.BlockCopy(value, 0, this.buffer, num2, num);
				}
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000B12B File Offset: 0x0000932B
		internal int BeginBufferedArray()
		{
			this.BeginBuffered();
			this.bufferPos += 2;
			return this.bufferPos;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000B147 File Offset: 0x00009347
		internal void EndBufferedArray(int bookmark, int count)
		{
			this.EnsureBuffer();
			this.buffer[bookmark - 2] = (byte)count;
			this.buffer[bookmark - 1] = (byte)(count >> 8);
			this.EndBuffered();
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000B16F File Offset: 0x0000936F
		internal void BeginBuffered()
		{
			this.ScalarsEnd();
			this.bufferNesting++;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000B185 File Offset: 0x00009385
		internal void EndBuffered()
		{
			this.bufferNesting--;
			if (this.bufferNesting == 0)
			{
				this.EnsureBuffer();
				this.PinArray(this.buffer, this.bufferPos);
				this.buffer = null;
				this.bufferPos = 0;
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000B1C4 File Offset: 0x000093C4
		private void EnsureBuffer()
		{
			int num = this.bufferPos;
			if (this.buffer == null || this.buffer.Length < num)
			{
				this.GrowBuffer(num);
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000B1F4 File Offset: 0x000093F4
		private void EnsureBuffer(int additionalSize)
		{
			int num = this.bufferPos + additionalSize;
			if (this.buffer == null || this.buffer.Length < num)
			{
				this.GrowBuffer(num);
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000B224 File Offset: 0x00009424
		private void GrowBuffer(int required)
		{
			int num = ((this.buffer == null) ? 64 : this.buffer.Length);
			do
			{
				num *= 2;
			}
			while (num < required);
			Array.Resize<byte>(ref this.buffer, num);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000B25C File Offset: 0x0000945C
		private unsafe void PinArray(object value, int size)
		{
			GCHandle* ptr = this.pins;
			if (this.pinsEnd == ptr)
			{
				throw new IndexOutOfRangeException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_PinArrayOutOfRange", Array.Empty<object>()));
			}
			EventSource.EventData* ptr2 = this.datas;
			if (this.datasEnd == ptr2)
			{
				throw new IndexOutOfRangeException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_DataDescriptorsOutOfRange", Array.Empty<object>()));
			}
			this.pins = ptr + 1;
			this.datas = ptr2 + 1;
			*ptr = GCHandle.Alloc(value, GCHandleType.Pinned);
			ptr2->m_Ptr = (long)(ulong)((UIntPtr)((void*)ptr->AddrOfPinnedObject()));
			ptr2->m_Size = size;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000B300 File Offset: 0x00009500
		private unsafe void ScalarsBegin()
		{
			if (!this.writingScalars)
			{
				EventSource.EventData* ptr = this.datas;
				if (this.datasEnd == ptr)
				{
					throw new IndexOutOfRangeException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_DataDescriptorsOutOfRange", Array.Empty<object>()));
				}
				ptr->m_Ptr = (long)(ulong)((UIntPtr)((void*)this.scratch));
				this.writingScalars = true;
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000B358 File Offset: 0x00009558
		private unsafe void ScalarsEnd()
		{
			if (this.writingScalars)
			{
				EventSource.EventData* ptr = this.datas;
				ptr->m_Size = (this.scratch - checked((UIntPtr)ptr->m_Ptr)) / 1;
				this.datas = ptr + 1;
				this.writingScalars = false;
			}
		}

		// Token: 0x040000B2 RID: 178
		[ThreadStatic]
		internal static DataCollector ThreadInstance;

		// Token: 0x040000B3 RID: 179
		private unsafe byte* scratchEnd;

		// Token: 0x040000B4 RID: 180
		private unsafe EventSource.EventData* datasEnd;

		// Token: 0x040000B5 RID: 181
		private unsafe GCHandle* pinsEnd;

		// Token: 0x040000B6 RID: 182
		private unsafe EventSource.EventData* datasStart;

		// Token: 0x040000B7 RID: 183
		private unsafe byte* scratch;

		// Token: 0x040000B8 RID: 184
		private unsafe EventSource.EventData* datas;

		// Token: 0x040000B9 RID: 185
		private unsafe GCHandle* pins;

		// Token: 0x040000BA RID: 186
		private byte[] buffer;

		// Token: 0x040000BB RID: 187
		private int bufferPos;

		// Token: 0x040000BC RID: 188
		private int bufferNesting;

		// Token: 0x040000BD RID: 189
		private bool writingScalars;
	}
}
