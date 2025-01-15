using System;
using System.IO;
using System.Text;

namespace Microsoft.Data.OData.Json
{
	// Token: 0x0200029F RID: 671
	internal sealed class IndentedTextWriter : TextWriter
	{
		// Token: 0x06001559 RID: 5465 RVA: 0x0004E16D File Offset: 0x0004C36D
		public IndentedTextWriter(TextWriter writer, bool enableIndentation)
			: base(writer.FormatProvider)
		{
			this.writer = writer;
			this.enableIndentation = enableIndentation;
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x0004E189 File Offset: 0x0004C389
		public override Encoding Encoding
		{
			get
			{
				return this.writer.Encoding;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x0004E196 File Offset: 0x0004C396
		public override string NewLine
		{
			get
			{
				return this.writer.NewLine;
			}
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x0004E1A3 File Offset: 0x0004C3A3
		public void IncreaseIndentation()
		{
			this.indentLevel++;
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x0004E1B3 File Offset: 0x0004C3B3
		public void DecreaseIndentation()
		{
			if (this.indentLevel < 1)
			{
				this.indentLevel = 0;
				return;
			}
			this.indentLevel--;
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x0004E1D4 File Offset: 0x0004C3D4
		public override void Close()
		{
			IndentedTextWriter.InternalCloseOrDispose();
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x0004E1DB File Offset: 0x0004C3DB
		public override void Flush()
		{
			this.writer.Flush();
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x0004E1E8 File Offset: 0x0004C3E8
		public override void Write(string s)
		{
			this.WriteIndentation();
			this.writer.Write(s);
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x0004E1FC File Offset: 0x0004C3FC
		public override void Write(char value)
		{
			this.WriteIndentation();
			this.writer.Write(value);
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x0004E210 File Offset: 0x0004C410
		public override void WriteLine()
		{
			if (this.enableIndentation)
			{
				base.WriteLine();
			}
			this.indentationPending = true;
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x0004E227 File Offset: 0x0004C427
		private static void InternalCloseOrDispose()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x0004E230 File Offset: 0x0004C430
		private void WriteIndentation()
		{
			if (!this.enableIndentation || !this.indentationPending)
			{
				return;
			}
			for (int i = 0; i < this.indentLevel; i++)
			{
				this.writer.Write("  ");
			}
			this.indentationPending = false;
		}

		// Token: 0x04000927 RID: 2343
		private const string IndentationString = "  ";

		// Token: 0x04000928 RID: 2344
		private readonly TextWriter writer;

		// Token: 0x04000929 RID: 2345
		private readonly bool enableIndentation;

		// Token: 0x0400092A RID: 2346
		private int indentLevel;

		// Token: 0x0400092B RID: 2347
		private bool indentationPending;
	}
}
