using System;
using System.Diagnostics;

namespace AngleSharp
{
	// Token: 0x02000016 RID: 22
	[DebuggerStepThrough]
	public struct TextRange : IEquatable<TextRange>, IComparable<TextRange>
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00003C27 File Offset: 0x00001E27
		public TextRange(TextPosition start, TextPosition end)
		{
			this._start = start;
			this._end = end;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003C37 File Offset: 0x00001E37
		public TextPosition Start
		{
			get
			{
				return this._start;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003C3F File Offset: 0x00001E3F
		public TextPosition End
		{
			get
			{
				return this._end;
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003C47 File Offset: 0x00001E47
		public override string ToString()
		{
			return string.Format("({0}) -- ({1})", this._start, this._end);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003C6C File Offset: 0x00001E6C
		public override int GetHashCode()
		{
			return this._end.GetHashCode() ^ this._start.GetHashCode();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003CA4 File Offset: 0x00001EA4
		public override bool Equals(object obj)
		{
			TextRange? textRange = obj as TextRange?;
			return textRange != null && this.Equals(textRange.Value);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003CD8 File Offset: 0x00001ED8
		public bool Equals(TextRange other)
		{
			return this._start.Equals(other._start) && this._end.Equals(other._end);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003D11 File Offset: 0x00001F11
		public static bool operator >(TextRange a, TextRange b)
		{
			return a._start > b._end;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003D24 File Offset: 0x00001F24
		public static bool operator <(TextRange a, TextRange b)
		{
			return a._end < b._start;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003D37 File Offset: 0x00001F37
		public int CompareTo(TextRange other)
		{
			if (this > other)
			{
				return 1;
			}
			if (other > this)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x04000030 RID: 48
		private readonly TextPosition _start;

		// Token: 0x04000031 RID: 49
		private readonly TextPosition _end;
	}
}
