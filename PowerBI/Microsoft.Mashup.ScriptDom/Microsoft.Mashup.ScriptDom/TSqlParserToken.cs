using System;
using antlr;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000E4 RID: 228
	internal sealed class TSqlParserToken : IToken
	{
		// Token: 0x06001465 RID: 5221 RVA: 0x0008F96A File Offset: 0x0008DB6A
		public TSqlParserToken()
			: this(TSqlTokenType.None, 0, null, 1, 1)
		{
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0008F977 File Offset: 0x0008DB77
		public TSqlParserToken(TSqlTokenType type, string text)
			: this(type, 0, text, 1, 1)
		{
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0008F984 File Offset: 0x0008DB84
		public TSqlParserToken(TSqlTokenType type, int offset, string text, int line, int column)
		{
			this._text = text;
			this._tokenType = type;
			this._offset = offset;
			this._line = line;
			this._column = column;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x0008F9B1 File Offset: 0x0008DBB1
		// (set) Token: 0x06001469 RID: 5225 RVA: 0x0008F9B9 File Offset: 0x0008DBB9
		public TSqlTokenType TokenType
		{
			get
			{
				return this._tokenType;
			}
			set
			{
				this._tokenType = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x0008F9C2 File Offset: 0x0008DBC2
		// (set) Token: 0x0600146B RID: 5227 RVA: 0x0008F9CA File Offset: 0x0008DBCA
		internal bool ConvertStringToIdentifier
		{
			get
			{
				return this._convertStringToIdentifier;
			}
			set
			{
				this._convertStringToIdentifier = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x0008F9D3 File Offset: 0x0008DBD3
		// (set) Token: 0x0600146D RID: 5229 RVA: 0x0008F9DB File Offset: 0x0008DBDB
		public int Offset
		{
			get
			{
				return this._offset;
			}
			set
			{
				this._offset = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x0008F9E4 File Offset: 0x0008DBE4
		// (set) Token: 0x0600146F RID: 5231 RVA: 0x0008F9EC File Offset: 0x0008DBEC
		public int Line
		{
			get
			{
				return this._line;
			}
			set
			{
				this._line = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x0008F9F5 File Offset: 0x0008DBF5
		// (set) Token: 0x06001471 RID: 5233 RVA: 0x0008F9FD File Offset: 0x0008DBFD
		public int Column
		{
			get
			{
				return this._column;
			}
			set
			{
				this._column = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x0008FA06 File Offset: 0x0008DC06
		// (set) Token: 0x06001473 RID: 5235 RVA: 0x0008FA0E File Offset: 0x0008DC0E
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value;
			}
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x0008FA17 File Offset: 0x0008DC17
		public bool IsKeyword()
		{
			return this.TokenType > TSqlTokenType.EndOfFile && this.TokenType < TSqlTokenType.Bang;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x0008FA31 File Offset: 0x0008DC31
		// (set) Token: 0x06001476 RID: 5238 RVA: 0x0008FA5A File Offset: 0x0008DC5A
		int IToken.Type
		{
			get
			{
				if (this._tokenType != TSqlTokenType.AsciiStringOrQuotedIdentifier)
				{
					return (int)this._tokenType;
				}
				if (this._convertStringToIdentifier)
				{
					return 233;
				}
				return 230;
			}
			set
			{
				if (this._tokenType != TSqlTokenType.AsciiStringOrQuotedIdentifier)
				{
					this._tokenType = (TSqlTokenType)value;
				}
			}
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x0008FA70 File Offset: 0x0008DC70
		int IToken.getColumn()
		{
			return this._column;
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x0008FA78 File Offset: 0x0008DC78
		void IToken.setColumn(int c)
		{
			this._column = c;
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x0008FA81 File Offset: 0x0008DC81
		int IToken.getLine()
		{
			return this._line;
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x0008FA89 File Offset: 0x0008DC89
		void IToken.setLine(int l)
		{
			this._line = l;
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x0008FA92 File Offset: 0x0008DC92
		string IToken.getFilename()
		{
			return null;
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x0008FA95 File Offset: 0x0008DC95
		void IToken.setFilename(string name)
		{
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x0008FA97 File Offset: 0x0008DC97
		string IToken.getText()
		{
			return this.Text;
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x0008FA9F File Offset: 0x0008DC9F
		void IToken.setText(string t)
		{
			this.Text = t;
		}

		// Token: 0x04000945 RID: 2373
		private string _text;

		// Token: 0x04000946 RID: 2374
		private int _offset;

		// Token: 0x04000947 RID: 2375
		private int _line;

		// Token: 0x04000948 RID: 2376
		private int _column;

		// Token: 0x04000949 RID: 2377
		private TSqlTokenType _tokenType;

		// Token: 0x0400094A RID: 2378
		private bool _convertStringToIdentifier;
	}
}
