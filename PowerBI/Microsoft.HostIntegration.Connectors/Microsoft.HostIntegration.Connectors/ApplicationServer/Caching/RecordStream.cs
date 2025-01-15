using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000367 RID: 871
	internal class RecordStream : Stream
	{
		// Token: 0x06001E8B RID: 7819 RVA: 0x0005D640 File Offset: 0x0005B840
		public RecordStream(MemoryAllocator allocator)
		{
			this._allocator = allocator;
			long num = this._allocator.AllocBuffer();
			this._currentBuffer = this._allocator.AccessBuffer(num);
			this._currentBuffer.Next = -1L;
			this._bufferList = new List<Buffer>(1);
			this._bufferList.Add(this._currentBuffer);
			this._capacity = this._currentBuffer.Count;
			this._isOpen = true;
			this._isWritable = true;
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x0005D6C1 File Offset: 0x0005B8C1
		public RecordStream(MemoryAllocator allocator, long streamId)
			: this(allocator, streamId, false)
		{
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x0005D6CC File Offset: 0x0005B8CC
		public RecordStream(MemoryAllocator allocator, long streamId, bool writable)
		{
			this._bufferList = new List<Buffer>();
			this._allocator = allocator;
			int num = 0;
			while (streamId != -1L)
			{
				Buffer buffer = this._allocator.AccessBuffer(streamId);
				this._bufferList.Add(buffer);
				num += buffer.Count;
				streamId = buffer.Next;
			}
			this._currentBuffer = this._bufferList[0];
			this._isOpen = true;
			this._isWritable = writable;
			this._length = num;
			this._capacity = num;
			if (writable)
			{
				this._position = num;
				this._posInBufferList = this._bufferList.Count - 1;
				this._currentBuffer = this._bufferList[this._posInBufferList];
				this._posInCurrentBuffer = this._currentBuffer.Count;
			}
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x0005D798 File Offset: 0x0005B998
		public static void ReleaseStream(MemoryAllocator allocator, long streamId)
		{
			while (streamId != -1L)
			{
				Buffer buffer = allocator.AccessBuffer(streamId);
				streamId = buffer.Next;
				allocator.FreeBuffer(buffer.Id);
			}
		}

		// Token: 0x06001E8F RID: 7823 RVA: 0x0005D7C8 File Offset: 0x0005B9C8
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this._isOpen = false;
					this._isWritable = false;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x0005D800 File Offset: 0x0005BA00
		private bool EnsureCapacity(int value)
		{
			if (value <= this._allocator.BufferCapacity)
			{
				this.Capacity = this._allocator.BufferCapacity;
			}
			else
			{
				this.Capacity = (value + this._allocator.BufferCapacity) / this._allocator.BufferCapacity * this._allocator.BufferCapacity;
			}
			return true;
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x000036A9 File Offset: 0x000018A9
		public override void Flush()
		{
		}

		// Token: 0x06001E92 RID: 7826 RVA: 0x00003CAB File Offset: 0x00001EAB
		public virtual byte[] GetBuffer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001E93 RID: 7827 RVA: 0x0005D85C File Offset: 0x0005BA5C
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (!this._isOpen)
			{
				throw new ObjectDisposedException(null);
			}
			int num = this._length - this._position;
			if (num > count)
			{
				num = count;
			}
			if (num <= 0)
			{
				return 0;
			}
			int i = num;
			while (i > 0)
			{
				int num2 = Math.Min(i, this._currentBuffer.Count - this._posInCurrentBuffer);
				if (num2 <= 8)
				{
					int num3 = num2;
					while (--num3 >= 0)
					{
						buffer[offset + num3] = this._currentBuffer[(long)(this._posInCurrentBuffer + num3)];
					}
				}
				else
				{
					this._currentBuffer.Read(this._posInCurrentBuffer, buffer, offset, num2);
				}
				i -= num2;
				offset += num2;
				this._position += num2;
				this._posInCurrentBuffer += num2;
				if (this._posInCurrentBuffer == this._currentBuffer.Count && this._posInBufferList < this._bufferList.Count && i != 0)
				{
					this._posInBufferList++;
					this._currentBuffer = this._bufferList[this._posInBufferList];
					this._posInCurrentBuffer = 0;
				}
			}
			return num;
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x0005D974 File Offset: 0x0005BB74
		public override int ReadByte()
		{
			if (!this._isOpen)
			{
				throw new ObjectDisposedException(null);
			}
			if (this._position >= this._length)
			{
				return -1;
			}
			while (this._posInCurrentBuffer == this._currentBuffer.Count && this._posInBufferList < this._bufferList.Count - 1)
			{
				this._posInBufferList++;
				this._currentBuffer = this._bufferList[this._posInBufferList];
				this._posInCurrentBuffer = 0;
			}
			this._position++;
			this._posInCurrentBuffer++;
			return (int)this._currentBuffer[(long)(this._posInCurrentBuffer - 1)];
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x0005DA24 File Offset: 0x0005BC24
		public override long Seek(long offset, SeekOrigin loc)
		{
			if (!this._isOpen)
			{
				throw new ObjectDisposedException(null);
			}
			long num = 0L;
			switch (loc)
			{
			case SeekOrigin.Begin:
				num = offset;
				break;
			case SeekOrigin.Current:
				num = (long)this._position + offset;
				break;
			case SeekOrigin.End:
				num = (long)this._length + offset;
				break;
			}
			if (num < 0L)
			{
				throw new IOException("Seek: OutOfRange");
			}
			if (num > 2147483647L)
			{
				throw new IOException("Seek: OutOfRange");
			}
			this._position = (int)num;
			ReleaseAssert.IsTrue(this._position <= this._length, "Seek forward is not supported beyond the end of stream.");
			this.SetRecordVariables(this._position);
			return (long)this._position;
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x0005DACD File Offset: 0x0005BCCD
		public override void SetLength(long value)
		{
			if (!this._isOpen)
			{
				throw new ObjectDisposedException(null);
			}
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException(null);
			}
			if (value > (long)this._capacity)
			{
				throw new ArgumentOutOfRangeException(null);
			}
			this._length = (int)value;
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x00003CAB File Offset: 0x00001EAB
		public virtual byte[] ToArray()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x0005DB03 File Offset: 0x0005BD03
		public long ToStreamId()
		{
			return this._bufferList[0].Id;
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x0005DB18 File Offset: 0x0005BD18
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (!this._isOpen)
			{
				throw new ObjectDisposedException(null);
			}
			if (!this._isWritable)
			{
				throw new NotSupportedException();
			}
			int num = this._position + count;
			if (num < 0)
			{
				throw new IOException("IOStreamTooLong");
			}
			if (num > this._length)
			{
				if (num > this._capacity)
				{
					this.EnsureCapacity(num);
				}
				if (this._position > this._length)
				{
					this.SetRecordVariables(this._position);
				}
				this._length = num;
			}
			while (count > 0)
			{
				int num2 = Math.Min(count, this._currentBuffer.Count - this._posInCurrentBuffer);
				if (num2 <= 8)
				{
					int num3 = num2;
					while (--num3 >= 0)
					{
						this._currentBuffer[(long)(this._posInCurrentBuffer + num3)] = buffer[offset + num3];
					}
				}
				else
				{
					this._currentBuffer.Write(this._posInCurrentBuffer, buffer, offset, num2);
				}
				count -= num2;
				offset += num2;
				this._position += num2;
				this._posInCurrentBuffer += num2;
				if (this._posInCurrentBuffer == this._currentBuffer.Count && this._posInBufferList < this._bufferList.Count - 1)
				{
					this._posInBufferList++;
					this._currentBuffer = this._bufferList[this._posInBufferList];
					this._posInCurrentBuffer = 0;
				}
			}
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x0005DC78 File Offset: 0x0005BE78
		public override void WriteByte(byte value)
		{
			if (!this._isOpen)
			{
				throw new ObjectDisposedException(null);
			}
			if (!this._isWritable)
			{
				throw new NotSupportedException();
			}
			int num = this._position + 1;
			if (num < 0)
			{
				throw new IOException("IOStreamTooLong");
			}
			if (num > this._length)
			{
				if (num > this._capacity)
				{
					this.EnsureCapacity(num);
				}
				if (this._position > this._length)
				{
					this.SetRecordVariables(this._position);
				}
				this._length = num;
			}
			while (this._posInCurrentBuffer == this._currentBuffer.Count && this._posInBufferList < this._bufferList.Count - 1)
			{
				this._posInBufferList++;
				this._currentBuffer = this._bufferList[this._posInBufferList];
				this._posInCurrentBuffer = 0;
			}
			this._position++;
			this._posInCurrentBuffer++;
			this._currentBuffer[(long)(this._posInCurrentBuffer - 1)] = value;
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x0005DD7C File Offset: 0x0005BF7C
		public virtual void WriteTo(Stream stream)
		{
			if (!this._isOpen)
			{
				throw new ObjectDisposedException(null);
			}
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			throw new NotImplementedException();
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001E9C RID: 7836 RVA: 0x0005DDA0 File Offset: 0x0005BFA0
		public override bool CanRead
		{
			get
			{
				return this._isOpen;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001E9D RID: 7837 RVA: 0x00002B16 File Offset: 0x00000D16
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001E9E RID: 7838 RVA: 0x0005DDA8 File Offset: 0x0005BFA8
		public override bool CanWrite
		{
			get
			{
				return this._isWritable;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001E9F RID: 7839 RVA: 0x0005DDB0 File Offset: 0x0005BFB0
		// (set) Token: 0x06001EA0 RID: 7840 RVA: 0x0005DDC8 File Offset: 0x0005BFC8
		public virtual int Capacity
		{
			get
			{
				if (!this._isOpen)
				{
					throw new ObjectDisposedException(null);
				}
				return this._capacity;
			}
			set
			{
				if (!this._isOpen)
				{
					throw new ObjectDisposedException(null);
				}
				if (value < this._capacity)
				{
					throw new NotImplementedException();
				}
				if (value > this._capacity)
				{
					int num = value - this._capacity;
					int num2 = num / this._allocator.BufferCapacity;
					if (num % this._allocator.BufferCapacity > 0)
					{
						num2++;
					}
					List<Buffer> list = new List<Buffer>(this._bufferList.Count + num2);
					for (int i = 0; i < this._bufferList.Count; i++)
					{
						list.Add(this._bufferList[i]);
					}
					for (int j = 0; j < num2; j++)
					{
						long num3 = this._allocator.AllocBuffer();
						Buffer buffer = this._allocator.AccessBuffer(num3);
						buffer.Next = -1L;
						list.Add(buffer);
						Buffer buffer2 = list[list.Count - 2];
						buffer2.Next = num3;
					}
					this._bufferList = list;
					this._capacity = value;
				}
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001EA1 RID: 7841 RVA: 0x0005DEC9 File Offset: 0x0005C0C9
		public override long Length
		{
			get
			{
				if (!this._isOpen)
				{
					throw new ObjectDisposedException(null);
				}
				return (long)this._length;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001EA2 RID: 7842 RVA: 0x0005DEE1 File Offset: 0x0005C0E1
		// (set) Token: 0x06001EA3 RID: 7843 RVA: 0x0005DEFC File Offset: 0x0005C0FC
		public override long Position
		{
			get
			{
				if (!this._isOpen)
				{
					throw new ObjectDisposedException(null);
				}
				return (long)this._position;
			}
			set
			{
				if (!this._isOpen)
				{
					throw new ObjectDisposedException(null);
				}
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				ReleaseAssert.IsTrue(value <= (long)this._length, "Seek forward is not supported beyond the end of stream.");
				this._position = (int)value;
				this.SetRecordVariables(this._position);
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001EA4 RID: 7844 RVA: 0x0005DF67 File Offset: 0x0005C167
		public int TotalSize
		{
			get
			{
				return this._bufferList.Count * 8 + this._capacity;
			}
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x0005DF80 File Offset: 0x0005C180
		private void SetRecordVariables(int position)
		{
			int num = 0;
			for (int i = 0; i < this._bufferList.Count; i++)
			{
				num += this._bufferList[i].Count;
				if (num >= position)
				{
					this._currentBuffer = this._bufferList[i];
					this._posInBufferList = i;
					break;
				}
			}
			if (position > num)
			{
				this._posInBufferList = this._bufferList.Count - 1;
				this._currentBuffer = this._bufferList[this._posInBufferList];
				this._posInCurrentBuffer = this._currentBuffer.Count;
				return;
			}
			this._posInCurrentBuffer = this._currentBuffer.Count - (num - position);
		}

		// Token: 0x0400114E RID: 4430
		private List<Buffer> _bufferList;

		// Token: 0x0400114F RID: 4431
		private int _posInBufferList;

		// Token: 0x04001150 RID: 4432
		private Buffer _currentBuffer;

		// Token: 0x04001151 RID: 4433
		private int _posInCurrentBuffer;

		// Token: 0x04001152 RID: 4434
		private bool _isOpen;

		// Token: 0x04001153 RID: 4435
		private bool _isWritable;

		// Token: 0x04001154 RID: 4436
		private int _length;

		// Token: 0x04001155 RID: 4437
		private int _capacity;

		// Token: 0x04001156 RID: 4438
		private int _position;

		// Token: 0x04001157 RID: 4439
		private MemoryAllocator _allocator;
	}
}
