using System;
using System.Runtime.Serialization;

namespace antlr
{
	// Token: 0x0200001A RID: 26
	[Serializable]
	internal class RecognitionException : ANTLRException
	{
		// Token: 0x0600010B RID: 267 RVA: 0x00004414 File Offset: 0x00002614
		public RecognitionException()
			: base("parsing error")
		{
			this.fileName = null;
			this.line = -1;
			this.column = -1;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004436 File Offset: 0x00002636
		public RecognitionException(string s)
			: base(s)
		{
			this.fileName = null;
			this.line = -1;
			this.column = -1;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004454 File Offset: 0x00002654
		public RecognitionException(string s, string fileName_, int line_, int column_)
			: base(s)
		{
			this.fileName = fileName_;
			this.line = line_;
			this.column = column_;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004473 File Offset: 0x00002673
		protected RecognitionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000447D File Offset: 0x0000267D
		public virtual string getFilename()
		{
			return this.fileName;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004485 File Offset: 0x00002685
		public virtual int getLine()
		{
			return this.line;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000448D File Offset: 0x0000268D
		public virtual int getColumn()
		{
			return this.column;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004495 File Offset: 0x00002695
		public override string ToString()
		{
			return FileLineFormatter.getFormatter().getFormatString(this.fileName, this.line, this.column) + this.Message;
		}

		// Token: 0x04000064 RID: 100
		public string fileName;

		// Token: 0x04000065 RID: 101
		public int line;

		// Token: 0x04000066 RID: 102
		public int column;
	}
}
