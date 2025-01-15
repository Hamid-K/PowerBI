using System;
using System.IO;

namespace Microsoft.OData.Core.Json
{
	// Token: 0x02000111 RID: 273
	internal sealed class IndentedTextWriter : TextWriterWrapper
	{
		// Token: 0x06000A57 RID: 2647 RVA: 0x00025F54 File Offset: 0x00024154
		public IndentedTextWriter(TextWriter writer)
			: base(writer.FormatProvider)
		{
			this.writer = writer;
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00025F69 File Offset: 0x00024169
		public override void IncreaseIndentation()
		{
			this.indentLevel++;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00025F79 File Offset: 0x00024179
		public override void DecreaseIndentation()
		{
			if (this.indentLevel < 1)
			{
				this.indentLevel = 0;
				return;
			}
			this.indentLevel--;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00025F9A File Offset: 0x0002419A
		public override void Close()
		{
			TextWriterWrapper.InternalCloseOrDispose();
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00025FA1 File Offset: 0x000241A1
		public override void Write(string s)
		{
			this.WriteIndentation();
			this.writer.Write(s);
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00025FB5 File Offset: 0x000241B5
		public override void Write(char value)
		{
			this.WriteIndentation();
			this.writer.Write(value);
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00025FC9 File Offset: 0x000241C9
		public override void WriteLine()
		{
			base.WriteLine();
			this.indentationPending = true;
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00025FD8 File Offset: 0x000241D8
		private void WriteIndentation()
		{
			if (!this.indentationPending)
			{
				return;
			}
			for (int i = 0; i < this.indentLevel; i++)
			{
				this.writer.Write("  ");
			}
			this.indentationPending = false;
		}

		// Token: 0x0400040F RID: 1039
		private const string IndentationString = "  ";

		// Token: 0x04000410 RID: 1040
		private int indentLevel;

		// Token: 0x04000411 RID: 1041
		private bool indentationPending;
	}
}
