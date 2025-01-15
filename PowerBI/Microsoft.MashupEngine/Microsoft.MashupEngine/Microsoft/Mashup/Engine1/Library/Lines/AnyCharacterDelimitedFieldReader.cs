using System;

namespace Microsoft.Mashup.Engine1.Library.Lines
{
	// Token: 0x020009C3 RID: 2499
	internal class AnyCharacterDelimitedFieldReader : DelimitedFieldReader
	{
		// Token: 0x06004747 RID: 18247 RVA: 0x000EECCF File Offset: 0x000ECECF
		public AnyCharacterDelimitedFieldReader(ILineReader reader, bool quoting, Func<char, bool> delimiters)
			: base(reader, quoting)
		{
			this.delimiters = delimiters;
		}

		// Token: 0x06004748 RID: 18248 RVA: 0x000EECE0 File Offset: 0x000ECEE0
		public override int ReadQuotedField(char[] text, int fieldStart, int lineEnd, bool quoting, out bool fieldHasQuotes, out int nextFieldStart)
		{
			int i = fieldStart;
			fieldHasQuotes = false;
			while (i < lineEnd)
			{
				char c = text[i];
				if (this.delimiters(c))
				{
					nextFieldStart = i + 1;
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

		// Token: 0x040025EA RID: 9706
		private Func<char, bool> delimiters;
	}
}
