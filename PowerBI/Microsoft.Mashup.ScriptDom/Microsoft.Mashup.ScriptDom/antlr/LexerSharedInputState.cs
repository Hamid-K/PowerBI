using System;
using System.IO;

namespace antlr
{
	// Token: 0x02000017 RID: 23
	internal class LexerSharedInputState
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x00003DC4 File Offset: 0x00001FC4
		public LexerSharedInputState(InputBuffer inbuf)
		{
			this.initialize();
			this.input = inbuf;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003DD9 File Offset: 0x00001FD9
		public LexerSharedInputState(Stream inStream)
			: this(new ByteBuffer(inStream))
		{
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003DE7 File Offset: 0x00001FE7
		public LexerSharedInputState(TextReader inReader)
			: this(new CharBuffer(inReader))
		{
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003DF5 File Offset: 0x00001FF5
		private void initialize()
		{
			this.column = 1;
			this.line = 1;
			this.tokenStartColumn = 1;
			this.tokenStartLine = 1;
			this.guessing = 0;
			this.filename = null;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003E21 File Offset: 0x00002021
		public virtual void reset()
		{
			this.initialize();
			this.input.reset();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003E34 File Offset: 0x00002034
		public virtual void resetInput(InputBuffer ib)
		{
			this.reset();
			this.input = ib;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003E43 File Offset: 0x00002043
		public virtual void resetInput(Stream s)
		{
			this.reset();
			this.input = new ByteBuffer(s);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003E57 File Offset: 0x00002057
		public virtual void resetInput(TextReader tr)
		{
			this.reset();
			this.input = new CharBuffer(tr);
		}

		// Token: 0x04000048 RID: 72
		protected internal int column;

		// Token: 0x04000049 RID: 73
		protected internal int line;

		// Token: 0x0400004A RID: 74
		protected internal int tokenStartColumn;

		// Token: 0x0400004B RID: 75
		protected internal int tokenStartLine;

		// Token: 0x0400004C RID: 76
		protected internal InputBuffer input;

		// Token: 0x0400004D RID: 77
		protected internal string filename;

		// Token: 0x0400004E RID: 78
		public int guessing;
	}
}
