using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000373 RID: 883
	internal sealed class SegmentedMemoryStream : Stream
	{
		// Token: 0x06001CE4 RID: 7396 RVA: 0x00074D23 File Offset: 0x00072F23
		public static Stream CreateMemoryStream(int size)
		{
			if (size <= 81920)
			{
				return new MemoryStream(size);
			}
			return new SegmentedMemoryStream(size);
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x00074D3A File Offset: 0x00072F3A
		public SegmentedMemoryStream()
		{
			this.EnsureCapacity(8192);
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x00074D4D File Offset: 0x00072F4D
		public SegmentedMemoryStream(int capacity)
		{
			this.EnsureCapacity(capacity);
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06001CE7 RID: 7399 RVA: 0x000053DC File Offset: 0x000035DC
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06001CE8 RID: 7400 RVA: 0x000053DC File Offset: 0x000035DC
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06001CE9 RID: 7401 RVA: 0x000053DC File Offset: 0x000035DC
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public override void Flush()
		{
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06001CEB RID: 7403 RVA: 0x00074D5C File Offset: 0x00072F5C
		public override long Length
		{
			get
			{
				return (long)this._length;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06001CEC RID: 7404 RVA: 0x00074D65 File Offset: 0x00072F65
		// (set) Token: 0x06001CED RID: 7405 RVA: 0x00061185 File Offset: 0x0005F385
		public override long Position
		{
			get
			{
				return (long)this._position;
			}
			set
			{
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x00074D70 File Offset: 0x00072F70
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (count == 0)
			{
				return 0;
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count <= 0 || offset < 0 || offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset or count");
			}
			int num = Math.Min(this._position + count, this._length);
			int position = this._position;
			this.CopyBuffers(buffer, offset, num, true);
			return this._position - position;
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x00074DD8 File Offset: 0x00072FD8
		public override long Seek(long offset, SeekOrigin origin)
		{
			switch (origin)
			{
			case SeekOrigin.Begin:
				this._position = (int)offset;
				break;
			case SeekOrigin.Current:
				this._position += (int)offset;
				break;
			case SeekOrigin.End:
				this._position = this._length + (int)offset;
				break;
			}
			if (this._position < 0)
			{
				this._position = 0;
			}
			else if (this._position > this._length)
			{
				this._position = this._length;
			}
			return (long)this._position;
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x00074E56 File Offset: 0x00073056
		public override void SetLength(long value)
		{
			if (value < 0L)
			{
				this._length = 0;
			}
			else
			{
				this._length = (int)value;
			}
			this.EnsureCapacity(this._length);
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x00074E7C File Offset: 0x0007307C
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (count == 0)
			{
				return;
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count <= 0 || offset < 0 || offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException();
			}
			this._length += count;
			this.EnsureCapacity(this._length);
			this.CopyBuffers(buffer, offset, this._length, false);
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x00074EDC File Offset: 0x000730DC
		public byte[] ToArray()
		{
			byte[] array = new byte[this._length];
			long position = this.Position;
			this.Position = 0L;
			this.Read(array, 0, array.Length);
			this.Position = position;
			return array;
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x00074F18 File Offset: 0x00073118
		private void EnsureCapacity(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			if (capacity <= this._capacity)
			{
				return;
			}
			if (capacity < this._capacity + 8192)
			{
				capacity = this._capacity + 8192;
			}
			int num = capacity / 81920;
			int num2 = capacity % 81920;
			int num3 = (capacity + 81920 - 1) / 81920;
			if (this._buffers != null && num3 == this._buffers.Count)
			{
				if (num2 == 0)
				{
					if (num != num3)
					{
						throw new InvalidOperationException("fullBuffers != numBuffers");
					}
					num2 = 81920;
				}
				this.GrowLastBuffer(num2);
				this._capacity = capacity;
				return;
			}
			int num4 = 0;
			if (this._buffers != null)
			{
				if (this._buffers.Count > 0)
				{
					num4 = this._buffers.Count;
					this.GrowLastBuffer(81920);
					this._capacity = num4 * 81920;
				}
			}
			else
			{
				this._buffers = new List<byte[]>(num3);
			}
			for (int i = num4; i < num; i++)
			{
				this._buffers.Add(new byte[81920]);
				this._capacity += 81920;
			}
			if (num2 > 0)
			{
				this._buffers.Add(new byte[num2]);
				this._capacity += num2;
			}
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x0007505C File Offset: 0x0007325C
		private void GrowLastBuffer(int size)
		{
			if (size <= 0 || size > 81920)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			byte[] array = this._buffers[this._buffers.Count - 1];
			if (array.Length != 81920)
			{
				if (size < array.Length)
				{
					throw new InvalidOperationException("size < buffer.Length");
				}
				Array.Resize<byte>(ref array, size);
				this._buffers[this._buffers.Count - 1] = array;
			}
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x000750D8 File Offset: 0x000732D8
		private void CopyBuffers(byte[] buffer, int offset, int endPosition, bool isRead)
		{
			int num = this._position / 81920;
			int num2 = endPosition / 81920;
			int num3 = this._position % 81920;
			int num4 = endPosition % 81920;
			if (num < num2)
			{
				byte[] array = this._buffers[num];
				if (isRead)
				{
					Array.Copy(array, num3, buffer, offset, 81920 - num3);
				}
				else
				{
					Array.Copy(buffer, offset, array, num3, 81920 - num3);
				}
				offset += 81920 - num3;
				num++;
				num3 = 0;
				if (isRead)
				{
					for (int i = num; i < num2; i++)
					{
						array = this._buffers[i];
						Array.Copy(array, 0, buffer, offset, 81920);
						offset += 81920;
					}
				}
				else
				{
					for (int j = num; j < num2; j++)
					{
						array = this._buffers[j];
						Array.Copy(buffer, offset, array, 0, 81920);
						offset += 81920;
					}
				}
			}
			if (num4 - num3 > 0)
			{
				byte[] array2 = this._buffers[num2];
				if (isRead)
				{
					Array.Copy(array2, num3, buffer, offset, num4 - num3);
				}
				else
				{
					Array.Copy(buffer, offset, array2, num3, num4 - num3);
				}
			}
			this._position = endPosition;
		}

		// Token: 0x04000C27 RID: 3111
		internal const int MaxBufferSize = 81920;

		// Token: 0x04000C28 RID: 3112
		internal const int BufferGrowSize = 8192;

		// Token: 0x04000C29 RID: 3113
		internal const int CopyBufferSize = 8192;

		// Token: 0x04000C2A RID: 3114
		private List<byte[]> _buffers;

		// Token: 0x04000C2B RID: 3115
		private int _length;

		// Token: 0x04000C2C RID: 3116
		private int _position;

		// Token: 0x04000C2D RID: 3117
		private int _capacity;
	}
}
