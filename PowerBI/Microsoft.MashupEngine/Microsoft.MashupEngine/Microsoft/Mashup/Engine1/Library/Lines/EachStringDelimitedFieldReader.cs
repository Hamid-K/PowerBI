using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Lines
{
	// Token: 0x020009C9 RID: 2505
	internal class EachStringDelimitedFieldReader : DelimitedFieldReader
	{
		// Token: 0x06004753 RID: 18259 RVA: 0x000EEF8D File Offset: 0x000ED18D
		public EachStringDelimitedFieldReader(ILineReader reader, bool quoting, IEnumerable<string> delimiters)
			: base(reader, quoting)
		{
			this.delimiters = delimiters.GetEnumerator();
		}

		// Token: 0x06004754 RID: 18260 RVA: 0x000EEFA4 File Offset: 0x000ED1A4
		public override int ReadQuotedField(char[] text, int fieldStart, int lineEnd, bool quoting, out bool fieldHasQuotes, out int nextFieldStart)
		{
			if (this.lineEnd != lineEnd)
			{
				this.delimiters.Reset();
				this.lineEnd = lineEnd;
			}
			string text2;
			if (this.delimiters.MoveNext())
			{
				text2 = this.delimiters.Current;
			}
			else
			{
				text2 = null;
			}
			int i = fieldStart;
			fieldHasQuotes = false;
			while (i < lineEnd)
			{
				char c = text[i];
				if (text2 != null && EachStringDelimitedFieldReader.Match(text, i, lineEnd, text2))
				{
					nextFieldStart = i + text2.Length;
					return i;
				}
				if (c == '"' && quoting)
				{
					fieldHasQuotes = true;
					i = DelimitedFieldReader.ReadQuotedText(text, i, lineEnd);
				}
				else
				{
					i++;
				}
			}
			nextFieldStart = -1;
			return i;
		}

		// Token: 0x06004755 RID: 18261 RVA: 0x000EF038 File Offset: 0x000ED238
		private static bool Match(char[] text, int delimiterStart, int lineEnd, string delimiter)
		{
			if (delimiterStart + delimiter.Length > text.Length)
			{
				return false;
			}
			for (int i = 0; i < delimiter.Length; i++)
			{
				if (text[delimiterStart + i] != delimiter[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040025F0 RID: 9712
		private IEnumerator<string> delimiters;

		// Token: 0x040025F1 RID: 9713
		private int lineEnd;
	}
}
