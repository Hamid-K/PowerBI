using System;
using System.IO;

namespace Microsoft.Mashup.Engine1.Library.Lines
{
	// Token: 0x02000A29 RID: 2601
	internal class ExcelTextReaderLineReader : TextReaderLineReader
	{
		// Token: 0x060048A2 RID: 18594 RVA: 0x000F2C68 File Offset: 0x000F0E68
		public ExcelTextReaderLineReader(TextReader reader, bool includeLineSeparators, char delimiter)
			: base(reader)
		{
			this.state = ExcelTextReaderLineReader.State.StartField;
			this.includeLineSeparators = includeLineSeparators;
			this.delimiter = delimiter;
		}

		// Token: 0x060048A3 RID: 18595 RVA: 0x000F2C88 File Offset: 0x000F0E88
		protected override bool TryReadLine(char[] chars, int offset, int endOffset, ref bool hasQuotes, out int newOffset)
		{
			IL_0177:
			while (offset < endOffset)
			{
				char c = chars[offset++];
				switch (this.state)
				{
				case ExcelTextReaderLineReader.State.StartField:
					if (c != this.delimiter)
					{
						if (c == '"')
						{
							this.state = ExcelTextReaderLineReader.State.QuotedField;
							hasQuotes = true;
						}
						else
						{
							if (c == '\n')
							{
								newOffset = offset;
								return true;
							}
							if (c == '\r')
							{
								this.state = ExcelTextReaderLineReader.State.CarriageReturn;
							}
							else
							{
								this.state = ExcelTextReaderLineReader.State.UnquotedField;
							}
						}
					}
					break;
				case ExcelTextReaderLineReader.State.UnquotedField:
					while (c != this.delimiter)
					{
						if (c > '"')
						{
							if (offset >= endOffset)
							{
								goto IL_0177;
							}
							c = chars[offset++];
						}
						else
						{
							if (c == '\n')
							{
								this.state = ExcelTextReaderLineReader.State.StartField;
								newOffset = offset;
								return true;
							}
							if (c == '\r')
							{
								this.state = ExcelTextReaderLineReader.State.CarriageReturn;
								goto IL_0177;
							}
							if (c == '"')
							{
								hasQuotes = true;
								goto IL_0177;
							}
							if (offset >= endOffset)
							{
								goto IL_0177;
							}
							c = chars[offset++];
						}
					}
					this.state = ExcelTextReaderLineReader.State.StartField;
					break;
				case ExcelTextReaderLineReader.State.CarriageReturn:
					if (c != '\n')
					{
						offset--;
					}
					newOffset = offset;
					this.state = ExcelTextReaderLineReader.State.StartField;
					return true;
				case ExcelTextReaderLineReader.State.QuotedField:
					while (c != '"')
					{
						if (offset >= endOffset)
						{
							goto IL_0177;
						}
						c = chars[offset++];
					}
					hasQuotes = true;
					this.state = ExcelTextReaderLineReader.State.Quote;
					break;
				case ExcelTextReaderLineReader.State.Quote:
					if (c == '"')
					{
						hasQuotes = true;
						this.state = ExcelTextReaderLineReader.State.QuotedField;
					}
					else if (c == this.delimiter)
					{
						this.state = ExcelTextReaderLineReader.State.StartField;
					}
					else
					{
						if (c == '\n')
						{
							this.state = ExcelTextReaderLineReader.State.StartField;
							newOffset = offset;
							return true;
						}
						if (c == '\r')
						{
							this.state = ExcelTextReaderLineReader.State.CarriageReturn;
						}
						else
						{
							this.state = ExcelTextReaderLineReader.State.UnquotedField;
						}
					}
					break;
				default:
					throw new InvalidOperationException();
				}
			}
			newOffset = offset;
			return false;
		}

		// Token: 0x060048A4 RID: 18596 RVA: 0x000F2E18 File Offset: 0x000F1018
		protected override int GetLineEnd(char[] chars, int lineStart, int nextLineStart)
		{
			if (this.state == ExcelTextReaderLineReader.State.QuotedField)
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

		// Token: 0x040026D0 RID: 9936
		private ExcelTextReaderLineReader.State state;

		// Token: 0x040026D1 RID: 9937
		private bool includeLineSeparators;

		// Token: 0x040026D2 RID: 9938
		private char delimiter;

		// Token: 0x02000A2A RID: 2602
		private enum State
		{
			// Token: 0x040026D4 RID: 9940
			StartField,
			// Token: 0x040026D5 RID: 9941
			UnquotedField,
			// Token: 0x040026D6 RID: 9942
			CarriageReturn,
			// Token: 0x040026D7 RID: 9943
			QuotedField,
			// Token: 0x040026D8 RID: 9944
			Quote
		}
	}
}
