using System;
using System.IO;

namespace Microsoft.Mashup.Engine1.Library.Lines
{
	// Token: 0x02000A25 RID: 2597
	internal class UnquotedTextReaderLineReader : TextReaderLineReader
	{
		// Token: 0x0600489C RID: 18588 RVA: 0x000F2A78 File Offset: 0x000F0C78
		public UnquotedTextReaderLineReader(TextReader reader, bool includeLineSeparators)
			: base(reader)
		{
			this.state = UnquotedTextReaderLineReader.State.None;
			this.includeLineSeparators = includeLineSeparators;
		}

		// Token: 0x0600489D RID: 18589 RVA: 0x000F2A90 File Offset: 0x000F0C90
		protected override bool TryReadLine(char[] chars, int offset, int endOffset, ref bool hasQuotes, out int newOffset)
		{
			IL_007F:
			while (offset < endOffset)
			{
				char c = chars[offset++];
				UnquotedTextReaderLineReader.State state = this.state;
				if (state == UnquotedTextReaderLineReader.State.None)
				{
					for (;;)
					{
						if (c > '"')
						{
							if (offset >= endOffset)
							{
								goto IL_007F;
							}
							c = chars[offset++];
						}
						else
						{
							if (c == '\n')
							{
								goto Block_5;
							}
							if (c == '\r')
							{
								break;
							}
							if (c == '"')
							{
								goto Block_7;
							}
							if (offset >= endOffset)
							{
								goto IL_007F;
							}
							c = chars[offset++];
						}
					}
					this.state = UnquotedTextReaderLineReader.State.CarriageReturn;
					continue;
					Block_7:
					hasQuotes = true;
					continue;
					Block_5:
					newOffset = offset;
					return true;
				}
				if (state != UnquotedTextReaderLineReader.State.CarriageReturn)
				{
					throw new InvalidOperationException();
				}
				if (c != '\n')
				{
					offset--;
				}
				this.state = UnquotedTextReaderLineReader.State.None;
				newOffset = offset;
				return true;
			}
			newOffset = offset;
			return false;
		}

		// Token: 0x0600489E RID: 18590 RVA: 0x000F2B28 File Offset: 0x000F0D28
		protected override int GetLineEnd(char[] chars, int lineStart, int nextLineStart)
		{
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

		// Token: 0x040026C5 RID: 9925
		private UnquotedTextReaderLineReader.State state;

		// Token: 0x040026C6 RID: 9926
		private bool includeLineSeparators;

		// Token: 0x02000A26 RID: 2598
		private enum State
		{
			// Token: 0x040026C8 RID: 9928
			None,
			// Token: 0x040026C9 RID: 9929
			CarriageReturn
		}
	}
}
