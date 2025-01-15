using System;
using System.IO;
using System.Text;

namespace Microsoft.DataShaping.Common.Json
{
	// Token: 0x0200001D RID: 29
	internal sealed class IndentedTextWriter : TextWriter
	{
		// Token: 0x060000EE RID: 238 RVA: 0x0000401E File Offset: 0x0000221E
		internal IndentedTextWriter(TextWriter writer, bool enableIndentation)
			: this(writer, enableIndentation, null)
		{
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004029 File Offset: 0x00002229
		internal IndentedTextWriter(TextWriter writer, bool enableIndentation, string indentationString)
			: base(writer.FormatProvider)
		{
			this._writer = writer;
			this._enableIndentation = enableIndentation;
			if (indentationString != null)
			{
				this._indentationString = indentationString;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x0000405A File Offset: 0x0000225A
		public override Encoding Encoding
		{
			get
			{
				return this._writer.Encoding;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00004067 File Offset: 0x00002267
		public override string NewLine
		{
			get
			{
				return this._writer.NewLine;
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004074 File Offset: 0x00002274
		public void IncreaseIndentation()
		{
			this._indentLevel++;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004084 File Offset: 0x00002284
		public void DecreaseIndentation()
		{
			if (this._indentLevel < 1)
			{
				this._indentLevel = 0;
				return;
			}
			this._indentLevel--;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000040A5 File Offset: 0x000022A5
		public override void Close()
		{
			IndentedTextWriter.InternalCloseOrDispose();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000040AC File Offset: 0x000022AC
		public override void Flush()
		{
			this._writer.Flush();
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000040B9 File Offset: 0x000022B9
		public override void Write(string s)
		{
			this.WriteIndentation();
			this._writer.Write(s);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000040CD File Offset: 0x000022CD
		public override void Write(char value)
		{
			this.WriteIndentation();
			this._writer.Write(value);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000040E1 File Offset: 0x000022E1
		public override void WriteLine()
		{
			if (this._enableIndentation)
			{
				base.WriteLine();
			}
			this._indentationPending = true;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000040F8 File Offset: 0x000022F8
		private static void InternalCloseOrDispose()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004100 File Offset: 0x00002300
		private void WriteIndentation()
		{
			if (!this._enableIndentation || !this._indentationPending)
			{
				return;
			}
			for (int i = 0; i < this._indentLevel; i++)
			{
				this._writer.Write(this._indentationString);
			}
			this._indentationPending = false;
		}

		// Token: 0x0400004A RID: 74
		private readonly string _indentationString = "  ";

		// Token: 0x0400004B RID: 75
		private readonly TextWriter _writer;

		// Token: 0x0400004C RID: 76
		private readonly bool _enableIndentation;

		// Token: 0x0400004D RID: 77
		private int _indentLevel;

		// Token: 0x0400004E RID: 78
		private bool _indentationPending;
	}
}
