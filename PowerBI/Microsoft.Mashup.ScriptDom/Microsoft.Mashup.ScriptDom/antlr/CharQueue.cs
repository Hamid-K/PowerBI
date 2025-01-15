using System;

namespace antlr
{
	// Token: 0x02000008 RID: 8
	internal class CharQueue
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00002CE4 File Offset: 0x00000EE4
		public CharQueue(int minSize)
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

		// Token: 0x06000045 RID: 69 RVA: 0x00002D2C File Offset: 0x00000F2C
		public void append(char tok)
		{
			if (this.nbrEntries == this.buffer.Length)
			{
				this.expand();
			}
			this.buffer[(this.offset + this.nbrEntries) & this.sizeLessOne] = tok;
			this.nbrEntries++;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002D79 File Offset: 0x00000F79
		public char elementAt(int idx)
		{
			return this.buffer[(this.offset + idx) & this.sizeLessOne];
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002D94 File Offset: 0x00000F94
		private void expand()
		{
			char[] array = new char[this.buffer.Length * 2];
			for (int i = 0; i < this.buffer.Length; i++)
			{
				array[i] = this.elementAt(i);
			}
			this.buffer = array;
			this.sizeLessOne = this.buffer.Length - 1;
			this.offset = 0;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002DEC File Offset: 0x00000FEC
		public virtual void init(int size)
		{
			this.buffer = new char[size];
			this.sizeLessOne = size - 1;
			this.offset = 0;
			this.nbrEntries = 0;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002E11 File Offset: 0x00001011
		public void reset()
		{
			this.offset = 0;
			this.nbrEntries = 0;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002E21 File Offset: 0x00001021
		public void removeFirst()
		{
			this.offset = (this.offset + 1) & this.sizeLessOne;
			this.nbrEntries--;
		}

		// Token: 0x04000010 RID: 16
		protected internal char[] buffer;

		// Token: 0x04000011 RID: 17
		private int sizeLessOne;

		// Token: 0x04000012 RID: 18
		private int offset;

		// Token: 0x04000013 RID: 19
		protected internal int nbrEntries;
	}
}
