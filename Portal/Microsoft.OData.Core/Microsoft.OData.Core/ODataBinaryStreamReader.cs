using System;
using System.IO;

namespace Microsoft.OData
{
	// Token: 0x0200000B RID: 11
	internal sealed class ODataBinaryStreamReader : Stream
	{
		// Token: 0x06000061 RID: 97 RVA: 0x00002C8B File Offset: 0x00000E8B
		internal ODataBinaryStreamReader(Func<char[], int, int, int> reader)
		{
			this.reader = reader;
			this.chars = new char[this.charLength];
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002393 File Offset: 0x00000593
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002390 File Offset: 0x00000590
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002390 File Offset: 0x00000590
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002396 File Offset: 0x00000596
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002396 File Offset: 0x00000596
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002396 File Offset: 0x00000596
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

		// Token: 0x06000068 RID: 104 RVA: 0x00002CC4 File Offset: 0x00000EC4
		public override int Read(byte[] buffer, int offset, int count)
		{
			int i = 0;
			int num = this.bytes.Length - this.bytesOffset;
			while (i < count)
			{
				if (num == 0)
				{
					int num2 = this.reader(this.chars, offset, this.charLength);
					if (num2 < 1)
					{
						break;
					}
					this.bytes = Convert.FromBase64CharArray(this.chars, 0, num2);
					num = this.bytes.Length;
					this.bytesOffset = 0;
					if (num < 1)
					{
						break;
					}
				}
				buffer[i] = this.bytes[this.bytesOffset];
				i++;
				this.bytesOffset++;
				num--;
			}
			return i;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002396 File Offset: 0x00000596
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002396 File Offset: 0x00000596
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002396 File Offset: 0x00000596
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002396 File Offset: 0x00000596
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000018 RID: 24
		private readonly Func<char[], int, int, int> reader;

		// Token: 0x04000019 RID: 25
		private readonly int charLength = 1024;

		// Token: 0x0400001A RID: 26
		private char[] chars;

		// Token: 0x0400001B RID: 27
		private int bytesOffset;

		// Token: 0x0400001C RID: 28
		private byte[] bytes = new byte[0];
	}
}
