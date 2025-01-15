using System;
using System.IO;

namespace antlr
{
	// Token: 0x02000007 RID: 7
	internal class CharBuffer : InputBuffer
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002C4A File Offset: 0x00000E4A
		public CharBuffer(TextReader input_)
		{
			this.input = input_;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002C68 File Offset: 0x00000E68
		public override void fill(int amount)
		{
			this.syncConsume();
			int i = amount + this.markerOffset - this.queue.Count;
			while (i > 0)
			{
				int num = this.input.Read(this.buf, 0, 16);
				this.queue.Add(this.buf, num);
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

		// Token: 0x0400000D RID: 13
		private const int BUF_SIZE = 16;

		// Token: 0x0400000E RID: 14
		[NonSerialized]
		internal TextReader input;

		// Token: 0x0400000F RID: 15
		private char[] buf = new char[16];
	}
}
