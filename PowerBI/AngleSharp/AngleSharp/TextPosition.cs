using System;

namespace AngleSharp
{
	// Token: 0x02000015 RID: 21
	public struct TextPosition : IEquatable<TextPosition>, IComparable<TextPosition>
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00003A59 File Offset: 0x00001C59
		public TextPosition(ushort line, ushort column, int position)
		{
			this._line = line;
			this._column = column;
			this._position = position;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003A70 File Offset: 0x00001C70
		public int Line
		{
			get
			{
				return (int)this._line;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003A78 File Offset: 0x00001C78
		public int Column
		{
			get
			{
				return (int)this._column;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003A80 File Offset: 0x00001C80
		public int Position
		{
			get
			{
				return this._position;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003A88 File Offset: 0x00001C88
		public TextPosition Shift(int columns)
		{
			return new TextPosition(this._line, (ushort)((int)this._column + columns), this._position + columns);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003AA8 File Offset: 0x00001CA8
		public TextPosition After(char chr)
		{
			ushort num = this._line;
			ushort num2 = this._column;
			if (chr == '\n')
			{
				num += 1;
				num2 = 0;
			}
			return new TextPosition(num, num2 + 1, this._position + 1);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003AE4 File Offset: 0x00001CE4
		public TextPosition After(string str)
		{
			ushort num = this._line;
			ushort num2 = this._column;
			for (int i = 0; i < str.Length; i++)
			{
				if (str[i] == '\n')
				{
					num += 1;
					num2 = 0;
				}
				num2 += 1;
			}
			return new TextPosition(num, num2, this._position + str.Length);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003B3D File Offset: 0x00001D3D
		public override string ToString()
		{
			return string.Format("Ln {0}, Col {1}, Pos {2}", this._line, this._column, this._position);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003B6A File Offset: 0x00001D6A
		public override int GetHashCode()
		{
			return this._position ^ (int)((this._line | this._column) + this._line);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003B88 File Offset: 0x00001D88
		public override bool Equals(object obj)
		{
			TextPosition? textPosition = obj as TextPosition?;
			return textPosition != null && this.Equals(textPosition.Value);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003BB9 File Offset: 0x00001DB9
		public bool Equals(TextPosition other)
		{
			return this._position == other._position && this._column == other._column && this._line == other._line;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003BE7 File Offset: 0x00001DE7
		public static bool operator >(TextPosition a, TextPosition b)
		{
			return a._position > b._position;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003BF7 File Offset: 0x00001DF7
		public static bool operator <(TextPosition a, TextPosition b)
		{
			return a._position < b._position;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003C07 File Offset: 0x00001E07
		public int CompareTo(TextPosition other)
		{
			if (this.Equals(other))
			{
				return 0;
			}
			if (!(this > other))
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x0400002C RID: 44
		public static readonly TextPosition Empty;

		// Token: 0x0400002D RID: 45
		private readonly ushort _line;

		// Token: 0x0400002E RID: 46
		private readonly ushort _column;

		// Token: 0x0400002F RID: 47
		private readonly int _position;
	}
}
