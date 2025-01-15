using System;
using System.IO;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BD9 RID: 7129
	public abstract class ChunkedOutputStream : Stream
	{
		// Token: 0x0600B1FA RID: 45562 RVA: 0x00244DAE File Offset: 0x00242FAE
		protected ChunkedOutputStream(int startChunkSize, int maxChunkSize)
		{
			this.chunk = new byte[startChunkSize];
			this.maxChunkSize = maxChunkSize;
		}

		// Token: 0x17002CB4 RID: 11444
		// (get) Token: 0x0600B1FB RID: 45563 RVA: 0x00002105 File Offset: 0x00000305
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002CB5 RID: 11445
		// (get) Token: 0x0600B1FC RID: 45564 RVA: 0x00002139 File Offset: 0x00000339
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002CB6 RID: 11446
		// (get) Token: 0x0600B1FD RID: 45565 RVA: 0x00002105 File Offset: 0x00000305
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002CB7 RID: 11447
		// (get) Token: 0x0600B1FE RID: 45566 RVA: 0x000033E7 File Offset: 0x000015E7
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002CB8 RID: 11448
		// (get) Token: 0x0600B1FF RID: 45567 RVA: 0x000033E7 File Offset: 0x000015E7
		// (set) Token: 0x0600B200 RID: 45568 RVA: 0x000033E7 File Offset: 0x000015E7
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600B201 RID: 45569 RVA: 0x00244DCC File Offset: 0x00242FCC
		public override void Flush()
		{
			if (this.offset > 0)
			{
				byte[] array;
				if (this.offset == this.chunk.Length)
				{
					array = this.chunk;
				}
				else
				{
					array = new byte[this.offset];
					Buffer.BlockCopy(this.chunk, 0, array, 0, this.offset);
				}
				this.offset = 0;
				this.WriteNextChunk(array);
			}
		}

		// Token: 0x0600B202 RID: 45570 RVA: 0x000033E7 File Offset: 0x000015E7
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600B203 RID: 45571 RVA: 0x00244E2C File Offset: 0x0024302C
		public override void WriteByte(byte b)
		{
			byte[] array = this.chunk;
			int num = this.offset;
			this.offset = num + 1;
			array[num] = b;
			this.WriteChunkIfFull();
		}

		// Token: 0x0600B204 RID: 45572 RVA: 0x00244E58 File Offset: 0x00243058
		public override void Write(byte[] buffer, int offset, int count)
		{
			int num = this.chunk.Length - this.offset;
			if (count >= num)
			{
				this.WriteLarge(buffer, offset, count);
				return;
			}
			switch (count)
			{
			case 1:
			{
				byte[] array = this.chunk;
				int num2 = this.offset;
				this.offset = num2 + 1;
				array[num2] = buffer[offset];
				return;
			}
			case 2:
			{
				byte[] array2 = this.chunk;
				int num2 = this.offset;
				this.offset = num2 + 1;
				array2[num2] = buffer[offset++];
				byte[] array3 = this.chunk;
				num2 = this.offset;
				this.offset = num2 + 1;
				array3[num2] = buffer[offset];
				return;
			}
			case 3:
			{
				byte[] array4 = this.chunk;
				int num2 = this.offset;
				this.offset = num2 + 1;
				array4[num2] = buffer[offset++];
				byte[] array5 = this.chunk;
				num2 = this.offset;
				this.offset = num2 + 1;
				array5[num2] = buffer[offset++];
				byte[] array6 = this.chunk;
				num2 = this.offset;
				this.offset = num2 + 1;
				array6[num2] = buffer[offset];
				return;
			}
			case 4:
			{
				byte[] array7 = this.chunk;
				int num2 = this.offset;
				this.offset = num2 + 1;
				array7[num2] = buffer[offset++];
				byte[] array8 = this.chunk;
				num2 = this.offset;
				this.offset = num2 + 1;
				array8[num2] = buffer[offset++];
				byte[] array9 = this.chunk;
				num2 = this.offset;
				this.offset = num2 + 1;
				array9[num2] = buffer[offset++];
				byte[] array10 = this.chunk;
				num2 = this.offset;
				this.offset = num2 + 1;
				array10[num2] = buffer[offset];
				return;
			}
			default:
				Buffer.BlockCopy(buffer, offset, this.chunk, this.offset, count);
				this.offset += count;
				return;
			}
		}

		// Token: 0x0600B205 RID: 45573 RVA: 0x00244FF8 File Offset: 0x002431F8
		private void WriteLarge(byte[] buffer, int offset, int count)
		{
			while (count > 0)
			{
				int num = Math.Min(this.chunk.Length - this.offset, count);
				Buffer.BlockCopy(buffer, offset, this.chunk, this.offset, num);
				this.offset += num;
				count -= num;
				offset += num;
				this.WriteChunkIfFull();
			}
		}

		// Token: 0x0600B206 RID: 45574 RVA: 0x00245054 File Offset: 0x00243254
		private void WriteChunkIfFull()
		{
			if (this.offset == this.chunk.Length)
			{
				this.offset = 0;
				this.WriteNextChunk(this.chunk);
				if (this.chunk.Length < this.maxChunkSize)
				{
					this.chunk = new byte[this.chunk.Length * 2];
				}
			}
		}

		// Token: 0x0600B207 RID: 45575 RVA: 0x000033E7 File Offset: 0x000015E7
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600B208 RID: 45576 RVA: 0x000033E7 File Offset: 0x000015E7
		public override void SetLength(long length)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600B209 RID: 45577 RVA: 0x002450A9 File Offset: 0x002432A9
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Flush();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600B20A RID: 45578
		protected abstract void WriteNextChunk(byte[] buffer);

		// Token: 0x04005B24 RID: 23332
		private byte[] chunk;

		// Token: 0x04005B25 RID: 23333
		private int offset;

		// Token: 0x04005B26 RID: 23334
		private readonly int maxChunkSize;
	}
}
