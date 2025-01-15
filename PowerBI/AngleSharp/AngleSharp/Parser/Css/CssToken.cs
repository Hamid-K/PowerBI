using System;

namespace AngleSharp.Parser.Css
{
	// Token: 0x02000088 RID: 136
	internal class CssToken
	{
		// Token: 0x06000441 RID: 1089 RVA: 0x0001BE00 File Offset: 0x0001A000
		public CssToken(CssTokenType type, string data, TextPosition position)
		{
			this._type = type;
			this._data = data;
			this._position = position;
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0001BE1D File Offset: 0x0001A01D
		public TextPosition Position
		{
			get
			{
				return this._position;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0001BE25 File Offset: 0x0001A025
		public CssTokenType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0001BE2D File Offset: 0x0001A02D
		public string Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0001BE2D File Offset: 0x0001A02D
		public virtual string ToValue()
		{
			return this._data;
		}

		// Token: 0x04000337 RID: 823
		private readonly CssTokenType _type;

		// Token: 0x04000338 RID: 824
		private readonly string _data;

		// Token: 0x04000339 RID: 825
		private readonly TextPosition _position;

		// Token: 0x0400033A RID: 826
		public static readonly CssToken Whitespace = new CssToken(CssTokenType.Whitespace, " ", TextPosition.Empty);

		// Token: 0x0400033B RID: 827
		public static readonly CssToken Comma = new CssToken(CssTokenType.Comma, ",", TextPosition.Empty);
	}
}
