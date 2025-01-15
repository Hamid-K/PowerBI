using System;
using System.IO;
using System.Text;

namespace Microsoft.ReportingServices.OData.Json
{
	// Token: 0x02000013 RID: 19
	internal sealed class IndentedTextWriter : TextWriter
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00003163 File Offset: 0x00001363
		public IndentedTextWriter(TextWriter writer, bool enableIndentation)
			: this(writer, enableIndentation, null)
		{
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000316E File Offset: 0x0000136E
		public IndentedTextWriter(TextWriter writer, bool enableIndentation, string indentationString)
			: base(writer.FormatProvider)
		{
			this.writer = writer;
			this.enableIndentation = enableIndentation;
			if (indentationString != null)
			{
				this.indentationString = indentationString;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000319F File Offset: 0x0000139F
		public override Encoding Encoding
		{
			get
			{
				return this.writer.Encoding;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000031AC File Offset: 0x000013AC
		public override string NewLine
		{
			get
			{
				return this.writer.NewLine;
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000031B9 File Offset: 0x000013B9
		public void IncreaseIndentation()
		{
			this.indentLevel++;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000031C9 File Offset: 0x000013C9
		public void DecreaseIndentation()
		{
			if (this.indentLevel < 1)
			{
				this.indentLevel = 0;
				return;
			}
			this.indentLevel--;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000031EA File Offset: 0x000013EA
		public override void Close()
		{
			IndentedTextWriter.InternalCloseOrDispose();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000031F1 File Offset: 0x000013F1
		public override void Flush()
		{
			this.writer.Flush();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000031FE File Offset: 0x000013FE
		public override void Write(string s)
		{
			this.WriteIndentation();
			this.writer.Write(s);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003212 File Offset: 0x00001412
		public override void Write(char value)
		{
			this.WriteIndentation();
			this.writer.Write(value);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003226 File Offset: 0x00001426
		public override void WriteLine()
		{
			if (this.enableIndentation)
			{
				base.WriteLine();
			}
			this.indentationPending = true;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000323D File Offset: 0x0000143D
		private static void InternalCloseOrDispose()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003244 File Offset: 0x00001444
		private void WriteIndentation()
		{
			if (!this.enableIndentation || !this.indentationPending)
			{
				return;
			}
			for (int i = 0; i < this.indentLevel; i++)
			{
				this.writer.Write(this.indentationString);
			}
			this.indentationPending = false;
		}

		// Token: 0x04000064 RID: 100
		private readonly string indentationString = "  ";

		// Token: 0x04000065 RID: 101
		private readonly TextWriter writer;

		// Token: 0x04000066 RID: 102
		private readonly bool enableIndentation;

		// Token: 0x04000067 RID: 103
		private int indentLevel;

		// Token: 0x04000068 RID: 104
		private bool indentationPending;
	}
}
