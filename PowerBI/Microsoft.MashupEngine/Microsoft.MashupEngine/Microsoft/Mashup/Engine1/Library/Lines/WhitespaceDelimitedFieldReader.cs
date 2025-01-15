using System;

namespace Microsoft.Mashup.Engine1.Library.Lines
{
	// Token: 0x020009C4 RID: 2500
	internal class WhitespaceDelimitedFieldReader : AnyCharacterDelimitedFieldReader
	{
		// Token: 0x06004749 RID: 18249 RVA: 0x000EED35 File Offset: 0x000ECF35
		public WhitespaceDelimitedFieldReader(ILineReader reader, bool quoting)
			: base(reader, quoting, new Func<char, bool>(char.IsWhiteSpace))
		{
		}

		// Token: 0x0600474A RID: 18250 RVA: 0x000EED4C File Offset: 0x000ECF4C
		public override int ReadQuotedField(char[] text, int fieldStart, int lineEnd, bool quoting, out bool fieldHasQuotes, out int nextFieldStart)
		{
			int num = base.ReadQuotedField(text, fieldStart, lineEnd, quoting, out fieldHasQuotes, out nextFieldStart);
			if (nextFieldStart != -1)
			{
				while (nextFieldStart < lineEnd && char.IsWhiteSpace(text[nextFieldStart]))
				{
					nextFieldStart++;
				}
			}
			return num;
		}
	}
}
