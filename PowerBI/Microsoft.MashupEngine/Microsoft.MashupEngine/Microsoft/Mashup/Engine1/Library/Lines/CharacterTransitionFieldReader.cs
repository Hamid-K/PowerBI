using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Lines
{
	// Token: 0x020009C6 RID: 2502
	internal class CharacterTransitionFieldReader : DelimitedFieldReader
	{
		// Token: 0x0600474E RID: 18254 RVA: 0x000EEE33 File Offset: 0x000ED033
		public CharacterTransitionFieldReader(ILineReader reader, FunctionValue before, FunctionValue after)
			: base(reader, false)
		{
			this.before = before;
			this.after = after;
		}

		// Token: 0x0600474F RID: 18255 RVA: 0x000EEE4C File Offset: 0x000ED04C
		public override int ReadQuotedField(char[] text, int fieldStart, int lineEnd, bool quoting, out bool fieldHasQuotes, out int nextFieldStart)
		{
			fieldHasQuotes = false;
			int i = fieldStart;
			char? c = null;
			while (i < lineEnd)
			{
				char c2 = text[i];
				if (c != null && this.before.Invoke(TextValue.New(c.Value)).AsLogical.AsBoolean && this.after.Invoke(TextValue.New(c2)).AsLogical.AsBoolean)
				{
					nextFieldStart = i;
					return i;
				}
				c = new char?(c2);
				i++;
			}
			nextFieldStart = -1;
			return i;
		}

		// Token: 0x040025EC RID: 9708
		private readonly FunctionValue before;

		// Token: 0x040025ED RID: 9709
		private readonly FunctionValue after;
	}
}
