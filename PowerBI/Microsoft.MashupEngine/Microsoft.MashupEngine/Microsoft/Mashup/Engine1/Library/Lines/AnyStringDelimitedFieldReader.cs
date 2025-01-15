using System;

namespace Microsoft.Mashup.Engine1.Library.Lines
{
	// Token: 0x020009C5 RID: 2501
	internal class AnyStringDelimitedFieldReader : DelimitedFieldReader
	{
		// Token: 0x0600474B RID: 18251 RVA: 0x000EED8C File Offset: 0x000ECF8C
		public AnyStringDelimitedFieldReader(ILineReader reader, bool quoting, string[] delimiters)
			: base(reader, quoting)
		{
			this.delimiters = delimiters;
		}

		// Token: 0x0600474C RID: 18252 RVA: 0x000EEDA0 File Offset: 0x000ECFA0
		public override int ReadQuotedField(char[] text, int fieldStart, int lineEnd, bool quoting, out bool fieldHasQuotes, out int nextFieldStart)
		{
			int i = fieldStart;
			fieldHasQuotes = false;
			while (i < lineEnd)
			{
				char c = text[i];
				int num = AnyStringDelimitedFieldReader.IndexOf(text, i, lineEnd, this.delimiters);
				if (num != -1)
				{
					nextFieldStart = i + this.delimiters[num].Length;
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

		// Token: 0x0600474D RID: 18253 RVA: 0x000EEE08 File Offset: 0x000ED008
		private static int IndexOf(char[] text, int textStart, int textEnd, string[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				if (DelimitedFieldReader.StartsWith(text, textStart, textEnd, values[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x040025EB RID: 9707
		private string[] delimiters;
	}
}
