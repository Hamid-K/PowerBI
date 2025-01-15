using System;
using System.IO;
using System.Text;

namespace Microsoft.ReportingServices.OData.Json
{
	// Token: 0x02000010 RID: 16
	internal sealed class IndentedTextWriter : TextWriter
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00003223 File Offset: 0x00001423
		public IndentedTextWriter(TextWriter writer, bool enableIndentation)
			: this(writer, enableIndentation, null)
		{
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000322E File Offset: 0x0000142E
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
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000325F File Offset: 0x0000145F
		public override Encoding Encoding
		{
			get
			{
				return this.writer.Encoding;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006B RID: 107 RVA: 0x0000326C File Offset: 0x0000146C
		public override string NewLine
		{
			get
			{
				return this.writer.NewLine;
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003279 File Offset: 0x00001479
		public void IncreaseIndentation()
		{
			this.indentLevel++;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003289 File Offset: 0x00001489
		public void DecreaseIndentation()
		{
			if (this.indentLevel < 1)
			{
				this.indentLevel = 0;
				return;
			}
			this.indentLevel--;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000032AA File Offset: 0x000014AA
		public override void Close()
		{
			IndentedTextWriter.InternalCloseOrDispose();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000032B1 File Offset: 0x000014B1
		public override void Flush()
		{
			this.writer.Flush();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000032BE File Offset: 0x000014BE
		public override void Write(string s)
		{
			this.WriteIndentation();
			this.writer.Write(s);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000032D2 File Offset: 0x000014D2
		public override void Write(char value)
		{
			this.WriteIndentation();
			this.writer.Write(value);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000032E6 File Offset: 0x000014E6
		public override void WriteLine()
		{
			if (this.enableIndentation)
			{
				base.WriteLine();
			}
			this.indentationPending = true;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000032FD File Offset: 0x000014FD
		private static void InternalCloseOrDispose()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003304 File Offset: 0x00001504
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

		// Token: 0x04000040 RID: 64
		private readonly string indentationString = "  ";

		// Token: 0x04000041 RID: 65
		private readonly TextWriter writer;

		// Token: 0x04000042 RID: 66
		private readonly bool enableIndentation;

		// Token: 0x04000043 RID: 67
		private int indentLevel;

		// Token: 0x04000044 RID: 68
		private bool indentationPending;
	}
}
