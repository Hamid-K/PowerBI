using System;
using System.Text;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Lines
{
	// Token: 0x020009C0 RID: 2496
	internal abstract class DelimitedFieldReader : IFieldReader<IValueReference>, IDisposable
	{
		// Token: 0x06004734 RID: 18228 RVA: 0x000EE80D File Offset: 0x000ECA0D
		public DelimitedFieldReader(ILineReader reader, bool quoting)
		{
			this.reader = reader;
			this.quoting = quoting;
		}

		// Token: 0x170016C6 RID: 5830
		// (get) Token: 0x06004735 RID: 18229 RVA: 0x000EE824 File Offset: 0x000ECA24
		public IValueReference Current
		{
			get
			{
				if (this.current == null)
				{
					if (this.quoting && this.fieldHasQuotes)
					{
						string text;
						if (DelimitedFieldReader.TryDecode(this.text, this.fieldStart, this.fieldEnd, out text))
						{
							this.current = TextValue.New(text);
						}
						else
						{
							this.current = TextValue.New(this.GetQuotedField(this.text, this.fieldStart, this.fieldEnd));
						}
					}
					else
					{
						this.current = TextValue.New(DelimitedFieldReader.GetField(this.text, this.fieldStart, this.fieldEnd));
					}
				}
				return this.current;
			}
		}

		// Token: 0x06004736 RID: 18230 RVA: 0x000EE8C4 File Offset: 0x000ECAC4
		public bool MoveNextRow()
		{
			if (!this.reader.MoveNext())
			{
				return false;
			}
			this.nextFieldStart = this.reader.LineStart;
			this.text = this.reader.Text;
			this.lineEnd = this.reader.LineEnd;
			this.lineHasQuotes = this.reader.HasQuotes;
			return true;
		}

		// Token: 0x06004737 RID: 18231 RVA: 0x000EE928 File Offset: 0x000ECB28
		public bool MoveNextField()
		{
			this.current = null;
			if (this.nextFieldStart == -1)
			{
				return false;
			}
			this.fieldStart = this.nextFieldStart;
			if (this.lineHasQuotes && this.quoting)
			{
				this.fieldEnd = this.ReadQuotedField(this.text, this.fieldStart, this.lineEnd, true, out this.fieldHasQuotes, out this.nextFieldStart);
			}
			else
			{
				this.fieldEnd = this.ReadField(this.text, this.fieldStart, this.lineEnd, out this.nextFieldStart);
			}
			return true;
		}

		// Token: 0x06004738 RID: 18232 RVA: 0x000EE9B5 File Offset: 0x000ECBB5
		public void Dispose()
		{
			this.reader.Dispose();
		}

		// Token: 0x06004739 RID: 18233 RVA: 0x000EE9C4 File Offset: 0x000ECBC4
		public virtual int ReadField(char[] text, int fieldStart, int lineEnd, out int nextFieldStart)
		{
			bool flag;
			return this.ReadQuotedField(text, fieldStart, lineEnd, false, out flag, out nextFieldStart);
		}

		// Token: 0x0600473A RID: 18234
		public abstract int ReadQuotedField(char[] text, int fieldStart, int lineEnd, bool quoting, out bool fieldHasQuotes, out int nextFieldStart);

		// Token: 0x0600473B RID: 18235 RVA: 0x000EE9DF File Offset: 0x000ECBDF
		protected static int ReadQuotedText(char[] text, int fieldEnd, int lineEnd)
		{
			for (fieldEnd++; fieldEnd < lineEnd; fieldEnd++)
			{
				if (text[fieldEnd] == '"')
				{
					fieldEnd++;
					if (fieldEnd >= lineEnd || text[fieldEnd] != '"')
					{
						break;
					}
				}
			}
			return fieldEnd;
		}

		// Token: 0x0600473C RID: 18236 RVA: 0x000EEA0C File Offset: 0x000ECC0C
		private static string GetField(char[] text, int fieldStart, int fieldEnd)
		{
			int num = fieldEnd - fieldStart;
			if (num == 0)
			{
				return string.Empty;
			}
			return new string(text, fieldStart, num);
		}

		// Token: 0x0600473D RID: 18237 RVA: 0x000EEA30 File Offset: 0x000ECC30
		protected virtual string GetQuotedField(char[] text, int fieldStart, int fieldEnd)
		{
			int i = fieldStart;
			StringBuilder stringBuilder = new StringBuilder(fieldEnd - i);
			while (i < fieldEnd)
			{
				char c = text[i];
				if (c == '"')
				{
					i = DelimitedFieldReader.Decode(text, i, fieldEnd, stringBuilder);
				}
				else
				{
					stringBuilder.Append(c);
					i++;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600473E RID: 18238 RVA: 0x000EEA78 File Offset: 0x000ECC78
		private static bool TryDecode(char[] text, int offset, int endOffset, out string value)
		{
			if (offset + 1 < endOffset && text[offset] == '"' && text[endOffset - 1] == '"')
			{
				bool flag = false;
				for (int i = offset + 1; i < endOffset - 1; i++)
				{
					if (text[i] == '"')
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					value = new string(text, offset + 1, endOffset - offset - 2);
					return true;
				}
			}
			value = null;
			return false;
		}

		// Token: 0x0600473F RID: 18239 RVA: 0x000EEAD4 File Offset: 0x000ECCD4
		private static int Decode(char[] text, int offset, int endOffset, StringBuilder builder)
		{
			offset++;
			while (offset < endOffset)
			{
				char c = text[offset++];
				if (c == '"')
				{
					if (offset == endOffset || text[offset] != '"')
					{
						break;
					}
					offset++;
				}
				builder.Append(c);
			}
			return offset;
		}

		// Token: 0x06004740 RID: 18240 RVA: 0x000EEB14 File Offset: 0x000ECD14
		protected static bool StartsWith(char[] text, int textStart, int textEnd, string value)
		{
			if (textStart + value.Length > text.Length)
			{
				return false;
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (text[textStart + i] != value[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040025DE RID: 9694
		private ILineReader reader;

		// Token: 0x040025DF RID: 9695
		private bool quoting;

		// Token: 0x040025E0 RID: 9696
		private char[] text;

		// Token: 0x040025E1 RID: 9697
		private int lineEnd;

		// Token: 0x040025E2 RID: 9698
		private int nextFieldStart;

		// Token: 0x040025E3 RID: 9699
		private int fieldStart;

		// Token: 0x040025E4 RID: 9700
		private int fieldEnd;

		// Token: 0x040025E5 RID: 9701
		private bool fieldHasQuotes;

		// Token: 0x040025E6 RID: 9702
		private bool lineHasQuotes;

		// Token: 0x040025E7 RID: 9703
		private TextValue current;
	}
}
