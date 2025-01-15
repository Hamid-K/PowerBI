using System;
using System.IO;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql
{
	// Token: 0x02000019 RID: 25
	internal sealed class FormattedStringWriter
	{
		// Token: 0x06000141 RID: 321 RVA: 0x00006B67 File Offset: 0x00004D67
		internal FormattedStringWriter()
			: this(null)
		{
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006B70 File Offset: 0x00004D70
		internal FormattedStringWriter(IFormatProvider formatProvider)
		{
			this.m_sw = new StringWriter(formatProvider);
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00006B8F File Offset: 0x00004D8F
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00006B97 File Offset: 0x00004D97
		internal int IndentationLevel
		{
			get
			{
				return this.m_indentationLevel;
			}
			set
			{
				this.m_indentationLevel = value;
				this.m_indentation = new string(' ', this.m_indentationLevel * 4);
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006BB5 File Offset: 0x00004DB5
		internal void Write(string value)
		{
			this.m_sw.Write(value);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006BC3 File Offset: 0x00004DC3
		internal void Write(string format, params object[] args)
		{
			this.m_sw.Write(format, args);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006BD2 File Offset: 0x00004DD2
		internal void IndentWriteLine(string value)
		{
			this.m_sw.WriteLine(this.m_indentation + value);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00006BEB File Offset: 0x00004DEB
		internal void IndentWriteLine(string format, params object[] args)
		{
			this.m_sw.WriteLine(this.m_indentation + format, args);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00006C05 File Offset: 0x00004E05
		internal void WriteLineIndent(string value)
		{
			this.m_sw.Write(value + this.m_sw.NewLine + this.m_indentation);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00006C29 File Offset: 0x00004E29
		internal void WriteLineIndent(string format, params object[] args)
		{
			this.m_sw.Write(format + this.m_sw.NewLine + this.m_indentation, args);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00006C4E File Offset: 0x00004E4E
		internal void WriteLineIndent()
		{
			this.m_sw.Write(this.m_sw.NewLine + this.m_indentation);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00006C71 File Offset: 0x00004E71
		public override string ToString()
		{
			this.m_sw.Flush();
			return this.m_sw.ToString();
		}

		// Token: 0x04000067 RID: 103
		private readonly StringWriter m_sw;

		// Token: 0x04000068 RID: 104
		private int m_indentationLevel;

		// Token: 0x04000069 RID: 105
		private string m_indentation = "";
	}
}
