using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Lines
{
	// Token: 0x020009C8 RID: 2504
	internal class EachCharacterDelimitedFieldReader : DelimitedFieldReader
	{
		// Token: 0x06004751 RID: 18257 RVA: 0x000EEEE5 File Offset: 0x000ED0E5
		public EachCharacterDelimitedFieldReader(ILineReader reader, bool quoting, IEnumerable<string> delimiters)
			: base(reader, quoting)
		{
			this.delimiters = delimiters.GetEnumerator();
			this.lineEnd = -1;
		}

		// Token: 0x06004752 RID: 18258 RVA: 0x000EEF04 File Offset: 0x000ED104
		public override int ReadQuotedField(char[] text, int fieldStart, int lineEnd, bool quoting, out bool fieldHasQuotes, out int nextFieldStart)
		{
			if (this.lineEnd != lineEnd)
			{
				this.delimiters.Reset();
				this.lineEnd = lineEnd;
			}
			int num;
			if (this.delimiters.MoveNext())
			{
				num = (int)this.delimiters.Current[0];
			}
			else
			{
				num = -1;
			}
			int i = fieldStart;
			fieldHasQuotes = false;
			while (i < lineEnd)
			{
				char c = text[i];
				if ((int)c == num)
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

		// Token: 0x040025EE RID: 9710
		private IEnumerator<string> delimiters;

		// Token: 0x040025EF RID: 9711
		private int lineEnd;
	}
}
