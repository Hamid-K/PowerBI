using System;
using System.IO;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002F0 RID: 752
	internal class ChunkStream : Stream
	{
		// Token: 0x06001C20 RID: 7200 RVA: 0x00054B56 File Offset: 0x00052D56
		public ChunkStream()
			: this(65536)
		{
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x00054B64 File Offset: 0x00052D64
		public ChunkStream(int capacity)
		{
			if (capacity == 0)
			{
				capacity = 65536;
			}
			int num = capacity / 65536;
			if (capacity % 65536 > 0)
			{
				this._buffers = new byte[num + 1][];
				this._buffers[num] = new byte[capacity - num * 65536];
			}
			else
			{
				this._buffers = new byte[num][];
			}
			for (int i = 0; i < num; i++)
			{
				this._buffers[i] = new byte[65536];
			}
			this._currentBuffer = this._buffers[0];
			this._isOpen = true;
			this._isWritable = true;
			this._capacity = capacity;
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x00054C06 File Offset: 0x00052E06
		public ChunkStream(byte[][] buffers)
			: this(buffers, false)
		{
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x00054C10 File Offset: 0x00052E10
		public ChunkStream(ISerializationReader reader, int capacity)
		{
			if (capacity == 0)
			{
				capacity = 65536;
			}
			int num = capacity / 65536;
			if (capacity % 65536 > 0)
			{
				this._buffers = new byte[num + 1][];
				this._buffers[num] = new byte[capacity - num * 65536];
			}
			else
			{
				this._buffers = new byte[num][];
			}
			for (int i = 0; i < num; i++)
			{
				this._buffers[i] = new byte[65536];
				reader.Read(this._buffers[i], 0, 65536);
			}
			if (capacity % 65536 > 0)
			{
				reader.Read(this._buffers[num], 0, capacity - num * 65536);
			}
			this._currentBuffer = this._buffers[0];
			this._isOpen = true;
			this._isWritable = true;
			this._capacity = capacity;
			this._length = this._capacity;
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x00054CF8 File Offset: 0x00052EF8
		public ChunkStream(byte[][] buffers, bool writable)
		{
			int num = 0;
			for (int i = 0; i < buffers.Length; i++)
			{
				num += buffers[i].Length;
			}
			this._buffers = buffers;
			this._currentBuffer = this._buffers[0];
			this._isOpen = true;
			this._isWritable = writable;
			this._length = num;
			this._capacity = num;
			if (writable)
			{
				this._position = num;
				this._buffersPos = this._buffers.Length - 1;
				this._currentBuffer = this._buffers[this._buffersPos];
				this._posInCurrentBuffer = this._currentBuffer.Length;
			}
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x00054D90 File Offset: 0x00052F90
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

		// Token: 0x06001C26 RID: 7206 RVA: 0x00054DC8 File Offset: 0x00052FC8
		private bool EnsureCapacity(int value)
		{
			if (value <= 65536)
			{
				this.Capacity = 65536;
			}
			else
			{
				this.Capacity = (value + 65536) / 65536 * 65536;
			}
			return true;
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x000036A9 File Offset: 0x000018A9
		public override void Flush()
		{
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x00003CAB File Offset: 0x00001EAB
		public virtual byte[] GetBuffer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x00054DFC File Offset: 0x00052FFC
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
				int num2 = Math.Min(i, this._currentBuffer.Length - this._posInCurrentBuffer);
				if (num2 <= 8)
				{
					int num3 = num2;
					while (--num3 >= 0)
					{
						buffer[offset + num3] = this._currentBuffer[this._posInCurrentBuffer + num3];
					}
				}
				else
				{
					Buffer.BlockCopy(this._currentBuffer, this._posInCurrentBuffer, buffer, offset, num2);
				}
				i -= num2;
				offset += num2;
				this._position += num2;
				this._posInCurrentBuffer += num2;
				if (this._posInCurrentBuffer == this._currentBuffer.Length && this._buffersPos < this._buffers.Length && i != 0)
				{
					this._buffersPos++;
					this._currentBuffer = this._buffers[this._buffersPos];
					this._posInCurrentBuffer = 0;
				}
			}
			return num;
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x00054F04 File Offset: 0x00053104
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
			while (this._posInCurrentBuffer == this._currentBuffer.Length && this._buffersPos < this._buffers.Length - 1)
			{
				this._buffersPos++;
				this._currentBuffer = this._buffers[this._buffersPos];
				this._posInCurrentBuffer = 0;
			}
			this._position++;
			this._posInCurrentBuffer++;
			return (int)this._currentBuffer[this._posInCurrentBuffer - 1];
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x00054FA8 File Offset: 0x000531A8
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
			this.SetBufferVariables(this._position);
			return (long)this._position;
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x00003D71 File Offset: 0x00001F71
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x00003CAB File Offset: 0x00001EAB
		public virtual byte[] ToArray()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x00055038 File Offset: 0x00053238
		public byte[][] ToChunkedArray()
		{
			if (this._capacity == this._length)
			{
				return this._buffers;
			}
			byte[][] array = new byte[this._buffersPos + 1][];
			for (int i = 0; i < this._buffersPos; i++)
			{
				array[i] = this._buffers[i];
			}
			if (this._posInCurrentBuffer == this._currentBuffer.Length)
			{
				array[this._buffersPos] = this._currentBuffer;
			}
			else
			{
				array[this._buffersPos] = new byte[this._posInCurrentBuffer];
				Buffer.BlockCopy(this._currentBuffer, 0, array[this._buffersPos], 0, this._posInCurrentBuffer);
			}
			return array;
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x000550D4 File Offset: 0x000532D4
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
					this.SetBufferVariables(this._position);
				}
				this._length = num;
			}
			while (count > 0)
			{
				int num2 = Math.Min(count, this._currentBuffer.Length - this._posInCurrentBuffer);
				if (num2 <= 8)
				{
					int num3 = num2;
					while (--num3 >= 0)
					{
						this._currentBuffer[this._posInCurrentBuffer + num3] = buffer[offset + num3];
					}
				}
				else
				{
					Buffer.BlockCopy(buffer, offset, this._currentBuffer, this._posInCurrentBuffer, num2);
				}
				count -= num2;
				offset += num2;
				this._position += num2;
				this._posInCurrentBuffer += num2;
				if (this._posInCurrentBuffer == this._currentBuffer.Length && this._buffersPos < this._buffers.Length - 1)
				{
					this._buffersPos++;
					this._currentBuffer = this._buffers[this._buffersPos];
					this._posInCurrentBuffer = 0;
				}
			}
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x00055220 File Offset: 0x00053420
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
					this.SetBufferVariables(this._position);
				}
				this._length = num;
			}
			while (this._posInCurrentBuffer == this._currentBuffer.Length && this._buffersPos < this._buffers.Length - 1)
			{
				this._buffersPos++;
				this._currentBuffer = this._buffers[this._buffersPos];
				this._posInCurrentBuffer = 0;
			}
			this._position++;
			this._posInCurrentBuffer++;
			this._currentBuffer[this._posInCurrentBuffer - 1] = value;
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x00055315 File Offset: 0x00053515
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

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001C32 RID: 7218 RVA: 0x00055339 File Offset: 0x00053539
		public override bool CanRead
		{
			get
			{
				return this._isOpen;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001C33 RID: 7219 RVA: 0x00002B16 File Offset: 0x00000D16
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001C34 RID: 7220 RVA: 0x00055341 File Offset: 0x00053541
		public override bool CanWrite
		{
			get
			{
				return this._isWritable;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001C35 RID: 7221 RVA: 0x00055349 File Offset: 0x00053549
		// (set) Token: 0x06001C36 RID: 7222 RVA: 0x00055360 File Offset: 0x00053560
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
					int num2 = num / 65536;
					byte[][] array;
					if (num % 65536 > 0)
					{
						array = new byte[this._buffers.Length + num2 + 1][];
						array[this._buffers.Length + num2] = new byte[num % 65536];
					}
					else
					{
						array = new byte[this._buffers.Length + num2][];
					}
					for (int i = 0; i < this._buffers.Length; i++)
					{
						array[i] = this._buffers[i];
					}
					for (int j = 0; j < num2; j++)
					{
						array[this._buffers.Length + j] = new byte[65536];
					}
					this._buffers = array;
					this._capacity = value;
				}
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001C37 RID: 7223 RVA: 0x00055443 File Offset: 0x00053643
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

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001C38 RID: 7224 RVA: 0x0005545B File Offset: 0x0005365B
		// (set) Token: 0x06001C39 RID: 7225 RVA: 0x00055474 File Offset: 0x00053674
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
				this._position = (int)value;
				this.SetBufferVariables(this._position);
			}
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x000554C8 File Offset: 0x000536C8
		private void SetBufferVariables(int position)
		{
			int num = 0;
			for (int i = 0; i < this._buffers.Length; i++)
			{
				num += this._buffers[i].Length;
				if (num >= position)
				{
					this._currentBuffer = this._buffers[i];
					this._buffersPos = i;
					break;
				}
			}
			if (position > num)
			{
				this._buffersPos = this._buffers.Length - 1;
				this._currentBuffer = this._buffers[this._buffersPos];
				this._posInCurrentBuffer = this._currentBuffer.Length;
				return;
			}
			this._posInCurrentBuffer = this._currentBuffer.Length - (num - position);
		}

		// Token: 0x04000EFF RID: 3839
		public const int MaxAllocSize = 65536;

		// Token: 0x04000F00 RID: 3840
		private byte[][] _buffers;

		// Token: 0x04000F01 RID: 3841
		private int _buffersPos;

		// Token: 0x04000F02 RID: 3842
		private byte[] _currentBuffer;

		// Token: 0x04000F03 RID: 3843
		private int _posInCurrentBuffer;

		// Token: 0x04000F04 RID: 3844
		private bool _isOpen;

		// Token: 0x04000F05 RID: 3845
		private bool _isWritable;

		// Token: 0x04000F06 RID: 3846
		private int _length;

		// Token: 0x04000F07 RID: 3847
		private int _capacity;

		// Token: 0x04000F08 RID: 3848
		private int _position;
	}
}
