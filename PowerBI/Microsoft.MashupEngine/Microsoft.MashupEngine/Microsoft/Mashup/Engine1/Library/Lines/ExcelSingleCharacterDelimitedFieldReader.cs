using System;
using System.Text;

namespace Microsoft.Mashup.Engine1.Library.Lines
{
	// Token: 0x020009C2 RID: 2498
	internal class ExcelSingleCharacterDelimitedFieldReader : SingleCharacterDelimitedFieldReader
	{
		// Token: 0x06004744 RID: 18244 RVA: 0x000EEBE8 File Offset: 0x000ECDE8
		public ExcelSingleCharacterDelimitedFieldReader(ILineReader reader, char delimiter)
			: base(reader, true, delimiter)
		{
			this.delimiter = delimiter;
		}

		// Token: 0x06004745 RID: 18245 RVA: 0x000EEBFC File Offset: 0x000ECDFC
		public override int ReadQuotedField(char[] text, int fieldStart, int lineEnd, bool quoting, out bool fieldHasQuotes, out int nextFieldStart)
		{
			int i = fieldStart;
			fieldHasQuotes = false;
			if (fieldStart < lineEnd && text[fieldStart] == '"')
			{
				fieldHasQuotes = true;
				for (i++; i < lineEnd; i++)
				{
					if (text[i] == '"')
					{
						i++;
						if (i >= lineEnd || text[i] != '"')
						{
							break;
						}
					}
				}
			}
			while (i < lineEnd)
			{
				if (text[i] == this.delimiter)
				{
					nextFieldStart = i + 1;
					return i;
				}
				i++;
			}
			nextFieldStart = -1;
			return i;
		}

		// Token: 0x06004746 RID: 18246 RVA: 0x000EEC64 File Offset: 0x000ECE64
		protected override string GetQuotedField(char[] text, int fieldStart, int fieldEnd)
		{
			int i = fieldStart;
			StringBuilder stringBuilder = new StringBuilder(fieldEnd - i);
			if (fieldStart < fieldEnd && text[fieldStart] == '"')
			{
				for (i++; i < fieldEnd; i++)
				{
					char c = text[i];
					if (c == '"')
					{
						i++;
						if (i >= fieldEnd || text[i] != '"')
						{
							break;
						}
					}
					stringBuilder.Append(c);
				}
			}
			while (i < fieldEnd)
			{
				stringBuilder.Append(text[i++]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040025E9 RID: 9705
		private char delimiter;
	}
}
