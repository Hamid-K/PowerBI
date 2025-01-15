using System;
using System.IO;

namespace antlr
{
	// Token: 0x02000006 RID: 6
	internal class ByteBuffer : InputBuffer
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002BA5 File Offset: 0x00000DA5
		public ByteBuffer(Stream input_)
		{
			this.input = input_;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002BC4 File Offset: 0x00000DC4
		public override void fill(int amount)
		{
			this.syncConsume();
			int i = amount + this.markerOffset - this.queue.Count;
			while (i > 0)
			{
				int num = this.input.Read(this.buf, 0, 16);
				for (int j = 0; j < num; j++)
				{
					this.queue.Add((char)this.buf[j]);
				}
				i -= num;
				if (num < 16)
				{
					while (i-- > 0)
					{
						this.queue.Add(CharScanner.EOF_CHAR);
					}
					return;
				}
			}
		}

		// Token: 0x0400000A RID: 10
		private const int BUF_SIZE = 16;

		// Token: 0x0400000B RID: 11
		[NonSerialized]
		internal Stream input;

		// Token: 0x0400000C RID: 12
		private byte[] buf = new byte[16];
	}
}
