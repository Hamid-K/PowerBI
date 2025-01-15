using System;
using System.IO;
using Microsoft.Internal;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010DA RID: 4314
	internal sealed class LengthBinaryValue : StreamedBinaryValue
	{
		// Token: 0x060070EA RID: 28906 RVA: 0x0018394A File Offset: 0x00181B4A
		public LengthBinaryValue(long length)
		{
			this.length = length;
		}

		// Token: 0x060070EB RID: 28907 RVA: 0x00183959 File Offset: 0x00181B59
		public override bool TryGetLength(out long length)
		{
			length = this.length;
			return true;
		}

		// Token: 0x17001FBD RID: 8125
		// (get) Token: 0x060070EC RID: 28908 RVA: 0x00183964 File Offset: 0x00181B64
		public override long Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x060070ED RID: 28909 RVA: 0x0018396C File Offset: 0x00181B6C
		public override Stream Open()
		{
			return new LengthBinaryValue.LengthStream(this.length);
		}

		// Token: 0x04003E2F RID: 15919
		private readonly long length;

		// Token: 0x020010DB RID: 4315
		private sealed class LengthStream : ForwardReadOnlyStream
		{
			// Token: 0x060070EE RID: 28910 RVA: 0x00183979 File Offset: 0x00181B79
			public LengthStream(long length)
			{
				this.length = length;
			}

			// Token: 0x060070EF RID: 28911 RVA: 0x00183988 File Offset: 0x00181B88
			public override int Read(byte[] buffer, int offset, int count)
			{
				long num = Math.Min(this.length, this.position + (long)count);
				count = (int)(num - this.position);
				for (int i = 0; i < count; i++)
				{
					buffer[offset + i] = 0;
				}
				this.position = num;
				return count;
			}

			// Token: 0x060070F0 RID: 28912 RVA: 0x001839CF File Offset: 0x00181BCF
			public override int ReadByte()
			{
				if (this.position < this.length)
				{
					this.position += 1L;
					return 0;
				}
				return -1;
			}

			// Token: 0x04003E30 RID: 15920
			private readonly long length;

			// Token: 0x04003E31 RID: 15921
			private long position;
		}
	}
}
