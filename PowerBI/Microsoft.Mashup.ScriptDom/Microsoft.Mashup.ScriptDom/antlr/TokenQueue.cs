using System;

namespace antlr
{
	// Token: 0x02000025 RID: 37
	internal class TokenQueue
	{
		// Token: 0x0600013D RID: 317 RVA: 0x00004F34 File Offset: 0x00003134
		public TokenQueue(int minSize)
		{
			if (minSize < 0)
			{
				this.init(16);
				return;
			}
			if (minSize >= 1073741823)
			{
				this.init(int.MaxValue);
				return;
			}
			int i;
			for (i = 2; i < minSize; i *= 2)
			{
			}
			this.init(i);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004F7C File Offset: 0x0000317C
		public void append(IToken tok)
		{
			if (this.nbrEntries == this.buffer.Length)
			{
				this.expand();
			}
			this.buffer[(this.offset + this.nbrEntries) & this.sizeLessOne] = tok;
			this.nbrEntries++;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004FC9 File Offset: 0x000031C9
		public IToken elementAt(int idx)
		{
			return this.buffer[(this.offset + idx) & this.sizeLessOne];
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004FE4 File Offset: 0x000031E4
		private void expand()
		{
			IToken[] array = new IToken[this.buffer.Length * 2];
			for (int i = 0; i < this.buffer.Length; i++)
			{
				array[i] = this.elementAt(i);
			}
			this.buffer = array;
			this.sizeLessOne = this.buffer.Length - 1;
			this.offset = 0;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000503C File Offset: 0x0000323C
		private void init(int size)
		{
			this.buffer = new IToken[size];
			this.sizeLessOne = size - 1;
			this.offset = 0;
			this.nbrEntries = 0;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00005061 File Offset: 0x00003261
		public void reset()
		{
			this.offset = 0;
			this.nbrEntries = 0;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00005071 File Offset: 0x00003271
		public void removeFirst()
		{
			this.offset = (this.offset + 1) & this.sizeLessOne;
			this.nbrEntries--;
		}

		// Token: 0x0400008C RID: 140
		private IToken[] buffer;

		// Token: 0x0400008D RID: 141
		private int sizeLessOne;

		// Token: 0x0400008E RID: 142
		private int offset;

		// Token: 0x0400008F RID: 143
		protected internal int nbrEntries;
	}
}
