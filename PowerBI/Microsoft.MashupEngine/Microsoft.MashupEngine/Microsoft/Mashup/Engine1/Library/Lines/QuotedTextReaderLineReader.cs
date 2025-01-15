using System;
using System.IO;

namespace Microsoft.Mashup.Engine1.Library.Lines
{
	// Token: 0x02000A27 RID: 2599
	internal class QuotedTextReaderLineReader : TextReaderLineReader
	{
		// Token: 0x0600489F RID: 18591 RVA: 0x000F2B59 File Offset: 0x000F0D59
		public QuotedTextReaderLineReader(TextReader reader, bool includeLineSeparators)
			: base(reader)
		{
			this.state = QuotedTextReaderLineReader.State.None;
			this.includeLineSeparators = includeLineSeparators;
		}

		// Token: 0x060048A0 RID: 18592 RVA: 0x000F2B70 File Offset: 0x000F0D70
		protected override bool TryReadLine(char[] chars, int offset, int endOffset, ref bool hasQuotes, out int newOffset)
		{
			IL_00A2:
			while (offset < endOffset)
			{
				char c = chars[offset++];
				switch (this.state)
				{
				case QuotedTextReaderLineReader.State.None:
					for (;;)
					{
						if (c > '"')
						{
							if (offset >= endOffset)
							{
								goto IL_00A2;
							}
							c = chars[offset++];
						}
						else
						{
							if (c == '\n')
							{
								goto Block_4;
							}
							if (c == '\r')
							{
								break;
							}
							if (c == '"')
							{
								goto Block_6;
							}
							if (offset >= endOffset)
							{
								goto IL_00A2;
							}
							c = chars[offset++];
						}
					}
					this.state = QuotedTextReaderLineReader.State.CarriageReturn;
					break;
					Block_6:
					this.state = QuotedTextReaderLineReader.State.Quote1;
					hasQuotes = true;
					break;
					Block_4:
					newOffset = offset;
					return true;
				case QuotedTextReaderLineReader.State.CarriageReturn:
					if (c != '\n')
					{
						offset--;
					}
					newOffset = offset;
					this.state = QuotedTextReaderLineReader.State.None;
					return true;
				case QuotedTextReaderLineReader.State.Quote1:
					if (c == '"')
					{
						this.state = QuotedTextReaderLineReader.State.None;
					}
					break;
				default:
					throw new InvalidOperationException();
				}
			}
			newOffset = offset;
			return false;
		}

		// Token: 0x060048A1 RID: 18593 RVA: 0x000F2C2C File Offset: 0x000F0E2C
		protected override int GetLineEnd(char[] chars, int lineStart, int nextLineStart)
		{
			if (this.state == QuotedTextReaderLineReader.State.Quote1)
			{
				return nextLineStart;
			}
			if (!this.includeLineSeparators)
			{
				while (nextLineStart > lineStart)
				{
					char c = chars[nextLineStart - 1];
					if (c != '\r' && c != '\n')
					{
						break;
					}
					nextLineStart--;
				}
			}
			return nextLineStart;
		}

		// Token: 0x040026CA RID: 9930
		private QuotedTextReaderLineReader.State state;

		// Token: 0x040026CB RID: 9931
		private bool includeLineSeparators;

		// Token: 0x02000A28 RID: 2600
		private enum State
		{
			// Token: 0x040026CD RID: 9933
			None,
			// Token: 0x040026CE RID: 9934
			CarriageReturn,
			// Token: 0x040026CF RID: 9935
			Quote1
		}
	}
}
