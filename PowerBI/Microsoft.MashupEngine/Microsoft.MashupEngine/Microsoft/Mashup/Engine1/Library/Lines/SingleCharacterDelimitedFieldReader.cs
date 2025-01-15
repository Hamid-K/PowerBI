using System;

namespace Microsoft.Mashup.Engine1.Library.Lines
{
	// Token: 0x020009C1 RID: 2497
	internal class SingleCharacterDelimitedFieldReader : DelimitedFieldReader
	{
		// Token: 0x06004741 RID: 18241 RVA: 0x000EEB52 File Offset: 0x000ECD52
		public SingleCharacterDelimitedFieldReader(ILineReader reader, bool quoting, char delimiter)
			: base(reader, quoting)
		{
			this.delimiter = delimiter;
		}

		// Token: 0x06004742 RID: 18242 RVA: 0x000EEB64 File Offset: 0x000ECD64
		public override int ReadField(char[] text, int fieldStart, int lineEnd, out int nextFieldStart)
		{
			int i;
			for (i = fieldStart; i < lineEnd; i++)
			{
				if (text[i] == this.delimiter)
				{
					nextFieldStart = i + 1;
					return i;
				}
			}
			nextFieldStart = -1;
			return i;
		}

		// Token: 0x06004743 RID: 18243 RVA: 0x000EEB98 File Offset: 0x000ECD98
		public override int ReadQuotedField(char[] text, int fieldStart, int lineEnd, bool quoting, out bool fieldHasQuotes, out int nextFieldStart)
		{
			int i = fieldStart;
			fieldHasQuotes = false;
			while (i < lineEnd)
			{
				char c = text[i];
				if (c == this.delimiter)
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

		// Token: 0x040025E8 RID: 9704
		private char delimiter;
	}
}
