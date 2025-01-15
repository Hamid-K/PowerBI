using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.InfoNav.Utils;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002D6 RID: 726
	internal sealed class QueryStringWriter
	{
		// Token: 0x06001835 RID: 6197 RVA: 0x0002B384 File Offset: 0x00029584
		internal QueryStringWriter(bool emitExpressionNames, bool traceString)
		{
			this._writer = new StringWriter(CultureInfo.InvariantCulture);
			this._emitExpressionNames = emitExpressionNames;
			this._traceString = traceString;
			this._indentLevel = 0;
			this._writeSeparator = false;
			this._separator = QueryStringWriter.Separator.None;
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001836 RID: 6198 RVA: 0x0002B3BF File Offset: 0x000295BF
		internal bool EmitExpressionNames
		{
			get
			{
				return this._emitExpressionNames;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x0002B3C7 File Offset: 0x000295C7
		internal bool TraceString
		{
			get
			{
				return this._traceString;
			}
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x0002B3CF File Offset: 0x000295CF
		public override string ToString()
		{
			return this._writer.ToString().TrimEnd(new char[0]);
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x0002B3E7 File Offset: 0x000295E7
		internal void Write(string s)
		{
			this._writer.Write(s);
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x0002B3F5 File Offset: 0x000295F5
		internal void WriteCustomerContent(string s)
		{
			this._writer.Write(this.MarkAsCustomerContentIfNeeded(s));
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x0002B409 File Offset: 0x00029609
		internal void Write(char c)
		{
			this._writer.Write(c);
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x0002B417 File Offset: 0x00029617
		internal void WriteCustomerContent(char c)
		{
			this._writer.Write(this.MarkAsCustomerContentIfNeeded<char>(c));
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x0002B42B File Offset: 0x0002962B
		internal void Write(decimal d)
		{
			this._writer.Write(d);
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x0002B439 File Offset: 0x00029639
		internal void WriteCustomerContent(decimal d)
		{
			this._writer.Write(this.MarkAsCustomerContentIfNeeded<decimal>(d));
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x0002B44D File Offset: 0x0002964D
		internal void Write(long l)
		{
			this._writer.Write(l);
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x0002B45B File Offset: 0x0002965B
		internal void WriteCustomerContent(long l)
		{
			this._writer.Write(this.MarkAsCustomerContentIfNeeded<long>(l));
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x0002B46F File Offset: 0x0002966F
		internal void WriteError()
		{
			this.Write("#error#");
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x0002B47C File Offset: 0x0002967C
		internal void WriteLine()
		{
			this._writer.WriteLine();
			this._writer.Write(new string(' ', 4 * this._indentLevel));
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x0002B4A3 File Offset: 0x000296A3
		internal void WriteFormat(string format, params object[] values)
		{
			this._writer.Write(format, values);
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x0002B4B4 File Offset: 0x000296B4
		internal void WriteFormatCustomerContent(string format, params object[] values)
		{
			string text = StringUtil.FormatInvariant(format, values);
			this._writer.Write(this.MarkAsCustomerContentIfNeeded(text));
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x0002B4DB File Offset: 0x000296DB
		internal void WriteIdentifierCustomerContent(string identifier)
		{
			this.WriteCustomerContent((identifier == null) ? identifier : StringUtil.EscapeIdentifier(identifier));
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x0002B4F0 File Offset: 0x000296F0
		internal void WriteSeparator()
		{
			if (this._writeSeparator)
			{
				switch (this._separator)
				{
				case QueryStringWriter.Separator.None:
					break;
				case QueryStringWriter.Separator.Comma:
					this.Write(", ");
					return;
				case QueryStringWriter.Separator.Newline:
					this.WriteLine();
					return;
				case QueryStringWriter.Separator.CommaAndNewline:
					this.Write(",");
					this.WriteLine();
					return;
				default:
					return;
				}
			}
			else
			{
				this._writeSeparator = true;
			}
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x0002B54F File Offset: 0x0002974F
		internal IDisposable NewSeparatorScope(QueryStringWriter.Separator separator)
		{
			return new QueryStringWriter.SeparatorScope(this, separator);
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x0002B558 File Offset: 0x00029758
		internal IDisposable NewIndentScope()
		{
			return new QueryStringWriter.IndentScope(this);
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x0002B560 File Offset: 0x00029760
		internal IDisposable NewClauseScope(string clauseName, QueryStringWriter.Separator separator)
		{
			this.Write(clauseName + " ");
			return new QueryStringWriter.ClauseScope(this, separator);
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x0002B57C File Offset: 0x0002977C
		internal void WriteExpressionAndName(QueryExpressionContainer expression)
		{
			bool emitExpressionNames = this._emitExpressionNames;
			this._emitExpressionNames = true;
			expression.WriteQueryString(this);
			this._emitExpressionNames = emitExpressionNames;
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x0002B5A8 File Offset: 0x000297A8
		internal void WriteExpressions(List<QueryExpressionContainer> expressions)
		{
			if (expressions.Count == 1)
			{
				this.WriteExpressionOrError(expressions[0]);
				return;
			}
			this.Write("(");
			using (this.NewSeparatorScope(QueryStringWriter.Separator.Comma))
			{
				for (int i = 0; i < expressions.Count; i++)
				{
					this.WriteSeparator();
					this.WriteExpressionOrError(expressions[i]);
				}
			}
			this.Write(")");
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x0002B62C File Offset: 0x0002982C
		private void WriteExpressionOrError(QueryExpressionContainer expression)
		{
			if (expression == null)
			{
				this.WriteError();
				return;
			}
			expression.WriteQueryString(this);
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x0002B645 File Offset: 0x00029845
		private string MarkAsCustomerContentIfNeeded<T>(T value)
		{
			return this.MarkAsCustomerContentIfNeeded(value.ToString());
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x0002B65A File Offset: 0x0002985A
		private string MarkAsCustomerContentIfNeeded(string value)
		{
			if (!this._traceString)
			{
				return value;
			}
			return value.MarkAsCustomerContent();
		}

		// Token: 0x04000896 RID: 2198
		private const string ErrorTag = "#error#";

		// Token: 0x04000897 RID: 2199
		private const int IndentSize = 4;

		// Token: 0x04000898 RID: 2200
		private readonly StringWriter _writer;

		// Token: 0x04000899 RID: 2201
		private readonly bool _traceString;

		// Token: 0x0400089A RID: 2202
		private bool _emitExpressionNames;

		// Token: 0x0400089B RID: 2203
		private int _indentLevel;

		// Token: 0x0400089C RID: 2204
		private bool _writeSeparator;

		// Token: 0x0400089D RID: 2205
		private QueryStringWriter.Separator _separator;

		// Token: 0x02000356 RID: 854
		private class SeparatorScope : IDisposable
		{
			// Token: 0x06001A57 RID: 6743 RVA: 0x0002F414 File Offset: 0x0002D614
			internal SeparatorScope(QueryStringWriter w, QueryStringWriter.Separator separator)
			{
				this._w = w;
				this._writeSeparatorSav = this._w._writeSeparator;
				this._separatorSav = this._w._separator;
				this._w._writeSeparator = false;
				this._w._separator = separator;
			}

			// Token: 0x06001A58 RID: 6744 RVA: 0x0002F468 File Offset: 0x0002D668
			public void Dispose()
			{
				this._w._writeSeparator = this._writeSeparatorSav;
				this._w._separator = this._separatorSav;
			}

			// Token: 0x040009E3 RID: 2531
			private readonly QueryStringWriter _w;

			// Token: 0x040009E4 RID: 2532
			private readonly bool _writeSeparatorSav;

			// Token: 0x040009E5 RID: 2533
			private readonly QueryStringWriter.Separator _separatorSav;
		}

		// Token: 0x02000357 RID: 855
		private class IndentScope : IDisposable
		{
			// Token: 0x06001A59 RID: 6745 RVA: 0x0002F48C File Offset: 0x0002D68C
			internal IndentScope(QueryStringWriter w)
			{
				this._w = w;
				this._w._indentLevel++;
			}

			// Token: 0x06001A5A RID: 6746 RVA: 0x0002F4AE File Offset: 0x0002D6AE
			public void Dispose()
			{
				this._w._indentLevel--;
			}

			// Token: 0x040009E6 RID: 2534
			private readonly QueryStringWriter _w;
		}

		// Token: 0x02000358 RID: 856
		private class ClauseScope : IDisposable
		{
			// Token: 0x06001A5B RID: 6747 RVA: 0x0002F4C3 File Offset: 0x0002D6C3
			internal ClauseScope(QueryStringWriter w, QueryStringWriter.Separator separator)
			{
				this._separatorScope = new QueryStringWriter.SeparatorScope(w, separator);
				this._indentScope = new QueryStringWriter.IndentScope(w);
			}

			// Token: 0x06001A5C RID: 6748 RVA: 0x0002F4E4 File Offset: 0x0002D6E4
			public void Dispose()
			{
				if (this._separatorScope != null)
				{
					this._separatorScope.Dispose();
					this._separatorScope = null;
				}
				if (this._indentScope != null)
				{
					this._indentScope.Dispose();
					this._indentScope = null;
				}
			}

			// Token: 0x040009E7 RID: 2535
			private QueryStringWriter.SeparatorScope _separatorScope;

			// Token: 0x040009E8 RID: 2536
			private QueryStringWriter.IndentScope _indentScope;
		}

		// Token: 0x02000359 RID: 857
		internal enum Separator
		{
			// Token: 0x040009EA RID: 2538
			None,
			// Token: 0x040009EB RID: 2539
			Comma,
			// Token: 0x040009EC RID: 2540
			Newline,
			// Token: 0x040009ED RID: 2541
			CommaAndNewline
		}
	}
}
