using System;
using System.IO;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Lines
{
	// Token: 0x02000A24 RID: 2596
	internal abstract class TextReaderLineReader : ILineReader, IDisposable
	{
		// Token: 0x06004892 RID: 18578 RVA: 0x000F282C File Offset: 0x000F0A2C
		public TextReaderLineReader(TextReader reader)
		{
			this.reader = reader;
			this.chars = new char[1024];
			this.lineStart = 0;
			this.lineOffset = 0;
			this.lineEnd = 0;
			this.nextLineStart = 0;
			this.hasQuotes = false;
		}

		// Token: 0x06004893 RID: 18579 RVA: 0x000F2879 File Offset: 0x000F0A79
		public void Dispose()
		{
			this.reader.Dispose();
		}

		// Token: 0x17001701 RID: 5889
		// (get) Token: 0x06004894 RID: 18580 RVA: 0x000F2886 File Offset: 0x000F0A86
		public char[] Text
		{
			get
			{
				return this.chars;
			}
		}

		// Token: 0x17001702 RID: 5890
		// (get) Token: 0x06004895 RID: 18581 RVA: 0x000F288E File Offset: 0x000F0A8E
		public int LineStart
		{
			get
			{
				return this.lineStart;
			}
		}

		// Token: 0x17001703 RID: 5891
		// (get) Token: 0x06004896 RID: 18582 RVA: 0x000F2896 File Offset: 0x000F0A96
		public int LineEnd
		{
			get
			{
				return this.lineEnd;
			}
		}

		// Token: 0x17001704 RID: 5892
		// (get) Token: 0x06004897 RID: 18583 RVA: 0x000F289E File Offset: 0x000F0A9E
		public bool HasQuotes
		{
			get
			{
				return this.hasQuotes;
			}
		}

		// Token: 0x06004898 RID: 18584 RVA: 0x000F28A6 File Offset: 0x000F0AA6
		public string GetLine()
		{
			return new string(this.chars, this.lineStart, this.lineEnd - this.lineStart);
		}

		// Token: 0x06004899 RID: 18585 RVA: 0x000F28C8 File Offset: 0x000F0AC8
		public bool MoveNext()
		{
			this.lineStart = this.nextLineStart;
			this.hasQuotes = false;
			while (!this.TryReadLine(this.chars, this.nextLineStart, this.lineOffset, ref this.hasQuotes, out this.nextLineStart))
			{
				if (this.chars.Length == this.lineOffset)
				{
					if (this.lineStart == 0)
					{
						if (this.chars.Length >= 1000000000)
						{
							throw ValueException.NewDataFormatError<Message1>(Strings.Lines_LineTooLong(PiiFree.New(1000000000)), Value.Null, null);
						}
						char[] array;
						try
						{
							array = new char[this.chars.Length * 2];
						}
						catch (OutOfMemoryException)
						{
							throw ValueException.NewDataFormatError<Message1>(Strings.Lines_LineTooLongOom(PiiFree.New(this.chars.Length)), Value.Null, null);
						}
						Array.Copy(this.chars, array, this.chars.Length);
						this.chars = array;
					}
					else
					{
						Array.Copy(this.chars, this.lineStart, this.chars, 0, this.chars.Length - this.lineStart);
						this.nextLineStart -= this.lineStart;
						this.lineOffset -= this.lineStart;
						this.lineStart = 0;
					}
				}
				int num = this.reader.ReadBlock(this.chars, this.lineOffset, this.chars.Length - this.lineOffset);
				if (num == 0)
				{
					if (this.nextLineStart == this.lineStart)
					{
						return false;
					}
					break;
				}
				else
				{
					this.lineOffset += num;
				}
			}
			this.lineEnd = this.GetLineEnd(this.chars, this.lineStart, this.nextLineStart);
			return true;
		}

		// Token: 0x0600489A RID: 18586
		protected abstract bool TryReadLine(char[] chars, int offset, int endOffset, ref bool hasQuotes, out int newOffset);

		// Token: 0x0600489B RID: 18587
		protected abstract int GetLineEnd(char[] chars, int lineStart, int nextLineStart);

		// Token: 0x040026BD RID: 9917
		private const int MaximumLineLength = 1000000000;

		// Token: 0x040026BE RID: 9918
		private TextReader reader;

		// Token: 0x040026BF RID: 9919
		private char[] chars;

		// Token: 0x040026C0 RID: 9920
		private int lineStart;

		// Token: 0x040026C1 RID: 9921
		private int lineOffset;

		// Token: 0x040026C2 RID: 9922
		private int lineEnd;

		// Token: 0x040026C3 RID: 9923
		private int nextLineStart;

		// Token: 0x040026C4 RID: 9924
		private bool hasQuotes;
	}
}
