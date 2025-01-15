using System;
using System.IO;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BD8 RID: 7128
	public abstract class ChunkedInputStream : Stream
	{
		// Token: 0x0600B1EC RID: 45548 RVA: 0x00244B40 File Offset: 0x00242D40
		protected ChunkedInputStream()
		{
			this.buffer = new byte[0];
		}

		// Token: 0x17002CAF RID: 11439
		// (get) Token: 0x0600B1ED RID: 45549 RVA: 0x00002139 File Offset: 0x00000339
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002CB0 RID: 11440
		// (get) Token: 0x0600B1EE RID: 45550 RVA: 0x00002105 File Offset: 0x00000305
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002CB1 RID: 11441
		// (get) Token: 0x0600B1EF RID: 45551 RVA: 0x00002105 File Offset: 0x00000305
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600B1F0 RID: 45552 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override void Flush()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17002CB2 RID: 11442
		// (get) Token: 0x0600B1F1 RID: 45553 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override long Length
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600B1F2 RID: 45554 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600B1F3 RID: 45555 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override void SetLength(long value)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600B1F4 RID: 45556 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17002CB3 RID: 11443
		// (get) Token: 0x0600B1F5 RID: 45557 RVA: 0x0000EE09 File Offset: 0x0000D009
		// (set) Token: 0x0600B1F6 RID: 45558 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override long Position
		{
			get
			{
				throw new InvalidOperationException();
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600B1F7 RID: 45559 RVA: 0x00244B54 File Offset: 0x00242D54
		public override int ReadByte()
		{
			if (this.offset == this.buffer.Length)
			{
				this.buffer = this.ReadNextChunk();
				this.offset = 0;
				if (this.buffer.Length == 0)
				{
					return -1;
				}
			}
			byte[] array = this.buffer;
			int num = this.offset;
			this.offset = num + 1;
			return array[num];
		}

		// Token: 0x0600B1F8 RID: 45560 RVA: 0x00244BA8 File Offset: 0x00242DA8
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.buffer.Length - this.offset >= count)
			{
				switch (count)
				{
				case 0:
					return 0;
				case 1:
				{
					int num = offset++;
					byte[] array = this.buffer;
					int num2 = this.offset;
					this.offset = num2 + 1;
					buffer[num] = array[num2];
					return 1;
				}
				case 2:
				{
					int num3 = offset++;
					byte[] array2 = this.buffer;
					int num2 = this.offset;
					this.offset = num2 + 1;
					buffer[num3] = array2[num2];
					int num4 = offset++;
					byte[] array3 = this.buffer;
					num2 = this.offset;
					this.offset = num2 + 1;
					buffer[num4] = array3[num2];
					return 2;
				}
				case 3:
				{
					int num5 = offset++;
					byte[] array4 = this.buffer;
					int num2 = this.offset;
					this.offset = num2 + 1;
					buffer[num5] = array4[num2];
					int num6 = offset++;
					byte[] array5 = this.buffer;
					num2 = this.offset;
					this.offset = num2 + 1;
					buffer[num6] = array5[num2];
					int num7 = offset++;
					byte[] array6 = this.buffer;
					num2 = this.offset;
					this.offset = num2 + 1;
					buffer[num7] = array6[num2];
					return 3;
				}
				case 4:
				{
					int num8 = offset++;
					byte[] array7 = this.buffer;
					int num2 = this.offset;
					this.offset = num2 + 1;
					buffer[num8] = array7[num2];
					int num9 = offset++;
					byte[] array8 = this.buffer;
					num2 = this.offset;
					this.offset = num2 + 1;
					buffer[num9] = array8[num2];
					int num10 = offset++;
					byte[] array9 = this.buffer;
					num2 = this.offset;
					this.offset = num2 + 1;
					buffer[num10] = array9[num2];
					int num11 = offset++;
					byte[] array10 = this.buffer;
					num2 = this.offset;
					this.offset = num2 + 1;
					buffer[num11] = array10[num2];
					return 4;
				}
				}
			}
			int num12 = 0;
			while (count > 0)
			{
				if (this.offset == this.buffer.Length)
				{
					this.buffer = this.ReadNextChunk();
					this.offset = 0;
					if (this.buffer.Length == 0)
					{
						break;
					}
				}
				int num13 = Math.Min(this.buffer.Length - this.offset, count);
				Buffer.BlockCopy(this.buffer, this.offset, buffer, offset, num13);
				this.offset += num13;
				offset += num13;
				count -= num13;
				num12 += num13;
			}
			return num12;
		}

		// Token: 0x0600B1F9 RID: 45561
		protected abstract byte[] ReadNextChunk();

		// Token: 0x04005B22 RID: 23330
		private int offset;

		// Token: 0x04005B23 RID: 23331
		private byte[] buffer;
	}
}
