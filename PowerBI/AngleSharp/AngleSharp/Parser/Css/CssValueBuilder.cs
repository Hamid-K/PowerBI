using System;
using System.Collections.Generic;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Parser.Css
{
	// Token: 0x02000080 RID: 128
	internal sealed class CssValueBuilder
	{
		// Token: 0x06000416 RID: 1046 RVA: 0x0001B77D File Offset: 0x0001997D
		public CssValueBuilder()
		{
			this._values = new List<CssToken>();
			this.Reset();
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0001B797 File Offset: 0x00019997
		public bool IsReady
		{
			get
			{
				return this._open == 0 && this._values.Count > 0;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0001B7B1 File Offset: 0x000199B1
		public bool IsValid
		{
			get
			{
				return this._valid && this.IsReady;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0001B7C3 File Offset: 0x000199C3
		public bool IsImportant
		{
			get
			{
				return this._important;
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001B7CB File Offset: 0x000199CB
		public CssValue GetResult()
		{
			return new CssValue(this._values);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0001B7D8 File Offset: 0x000199D8
		public void Apply(CssToken token)
		{
			switch (token.Type)
			{
			case CssTokenType.String:
			case CssTokenType.Url:
			case CssTokenType.Color:
			case CssTokenType.Number:
			case CssTokenType.Percentage:
			case CssTokenType.Dimension:
			case CssTokenType.Delim:
			case CssTokenType.Comma:
				this.Add(token);
				return;
			case CssTokenType.Comment:
				return;
			case CssTokenType.Ident:
				this._important = this.CheckImportant(token);
				return;
			case CssTokenType.Function:
				this.Add(token);
				return;
			case CssTokenType.RoundBracketOpen:
				this._open++;
				this.Add(token);
				return;
			case CssTokenType.RoundBracketClose:
				this._open--;
				this.Add(token);
				return;
			case CssTokenType.Whitespace:
				if (this._values.Count > 0 && !CssValueBuilder.IsSlash(this._values[this._values.Count - 1]))
				{
					this._buffer = token;
					return;
				}
				return;
			}
			this._valid = false;
			this.Add(token);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001B8ED File Offset: 0x00019AED
		public CssValueBuilder Reset()
		{
			this._open = 0;
			this._valid = true;
			this._buffer = null;
			this._important = false;
			this._values.Clear();
			return this;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0001B918 File Offset: 0x00019B18
		private bool CheckImportant(CssToken token)
		{
			if (this._values.Count != 0 && token.Data == Keywords.Important && CssValueBuilder.IsExclamationMark(this._values[this._values.Count - 1]))
			{
				do
				{
					this._values.RemoveAt(this._values.Count - 1);
				}
				while (this._values.Count > 0 && this._values[this._values.Count - 1].Type == CssTokenType.Whitespace);
				return true;
			}
			this.Add(token);
			return this._important;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001B9BC File Offset: 0x00019BBC
		private void Add(CssToken token)
		{
			if (this._buffer != null && !CssValueBuilder.IsCommaOrSlash(token))
			{
				this._values.Add(this._buffer);
			}
			else if (this._values.Count != 0 && !CssValueBuilder.IsComma(token) && CssValueBuilder.IsComma(this._values[this._values.Count - 1]))
			{
				this._values.Add(CssToken.Whitespace);
			}
			this._buffer = null;
			if (this._important)
			{
				this._valid = false;
			}
			this._values.Add(token);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001BA52 File Offset: 0x00019C52
		private static bool IsCommaOrSlash(CssToken token)
		{
			return CssValueBuilder.IsComma(token) || CssValueBuilder.IsSlash(token);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0001BA64 File Offset: 0x00019C64
		private static bool IsComma(CssToken token)
		{
			return token.Type == CssTokenType.Comma;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0001BA70 File Offset: 0x00019C70
		private static bool IsExclamationMark(CssToken token)
		{
			return token.Type == CssTokenType.Delim && token.Data.Has('!', 0);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0001BA8C File Offset: 0x00019C8C
		private static bool IsSlash(CssToken token)
		{
			return token.Type == CssTokenType.Delim && token.Data.Has('/', 0);
		}

		// Token: 0x0400032A RID: 810
		private readonly List<CssToken> _values;

		// Token: 0x0400032B RID: 811
		private CssToken _buffer;

		// Token: 0x0400032C RID: 812
		private bool _valid;

		// Token: 0x0400032D RID: 813
		private bool _important;

		// Token: 0x0400032E RID: 814
		private int _open;
	}
}
