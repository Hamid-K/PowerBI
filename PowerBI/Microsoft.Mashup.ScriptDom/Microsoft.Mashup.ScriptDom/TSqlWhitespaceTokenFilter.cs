using System;
using System.Collections.Generic;
using antlr;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000E2 RID: 226
	internal class TSqlWhitespaceTokenFilter : TokenStream
	{
		// Token: 0x06001453 RID: 5203 RVA: 0x0008F748 File Offset: 0x0008D948
		public TSqlWhitespaceTokenFilter(bool quotedIdentifier, IList<TSqlParserToken> streamToFilter)
		{
			this._quotedIdentifier = quotedIdentifier;
			this._streamToFilter = streamToFilter;
			this._currentTokenIndex = 0;
			if (streamToFilter.Count > 0)
			{
				this._lastToken = streamToFilter[0];
			}
			else
			{
				this._lastToken = new TSqlParserToken(TSqlTokenType.EndOfFile, null);
			}
			this._streamLength = streamToFilter.Count;
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x0008F7A4 File Offset: 0x0008D9A4
		public IToken nextToken()
		{
			TSqlParserToken tsqlParserToken = null;
			int num = -1;
			while (this._currentTokenIndex < this._streamLength)
			{
				tsqlParserToken = this._streamToFilter[this._currentTokenIndex];
				num = this._currentTokenIndex;
				this._currentTokenIndex++;
				if (tsqlParserToken.TokenType != TSqlTokenType.SingleLineComment && tsqlParserToken.TokenType != TSqlTokenType.MultilineComment && tsqlParserToken.TokenType != TSqlTokenType.WhiteSpace)
				{
					break;
				}
			}
			if (tsqlParserToken == null)
			{
				if (this._streamLength != 0)
				{
					TSqlParserToken tsqlParserToken2 = this._streamToFilter[this._streamToFilter.Count - 1];
					int num2 = ((tsqlParserToken2.Text == null) ? 0 : tsqlParserToken2.Text.Length);
					tsqlParserToken = new TSqlParserToken(TSqlTokenType.EndOfFile, tsqlParserToken2.Offset + num2, null, tsqlParserToken2.Line, tsqlParserToken2.Column + num2);
				}
				else
				{
					tsqlParserToken = new TSqlParserToken(TSqlTokenType.EndOfFile, null);
				}
			}
			else if (tsqlParserToken.TokenType == TSqlTokenType.AsciiStringOrQuotedIdentifier)
			{
				tsqlParserToken.ConvertStringToIdentifier = this._quotedIdentifier;
			}
			this._lastToken = tsqlParserToken;
			return new TSqlWhitespaceTokenFilter.TSqlParserTokenProxyWithIndex(tsqlParserToken, num);
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x0008F89F File Offset: 0x0008DA9F
		public TSqlParserToken LastToken
		{
			get
			{
				return this._lastToken;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x0008F8A7 File Offset: 0x0008DAA7
		// (set) Token: 0x06001457 RID: 5207 RVA: 0x0008F8AF File Offset: 0x0008DAAF
		public bool QuotedIdentifier
		{
			get
			{
				return this._quotedIdentifier;
			}
			set
			{
				this._quotedIdentifier = value;
			}
		}

		// Token: 0x0400093E RID: 2366
		private bool _quotedIdentifier;

		// Token: 0x0400093F RID: 2367
		private IList<TSqlParserToken> _streamToFilter;

		// Token: 0x04000940 RID: 2368
		private int _currentTokenIndex;

		// Token: 0x04000941 RID: 2369
		private int _streamLength;

		// Token: 0x04000942 RID: 2370
		private TSqlParserToken _lastToken;

		// Token: 0x020000E3 RID: 227
		internal class TSqlParserTokenProxyWithIndex : IToken
		{
			// Token: 0x06001458 RID: 5208 RVA: 0x0008F8B8 File Offset: 0x0008DAB8
			public TSqlParserTokenProxyWithIndex(TSqlParserToken token, int index)
			{
				this._token = token;
				this._index = index;
			}

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x06001459 RID: 5209 RVA: 0x0008F8CE File Offset: 0x0008DACE
			public TSqlParserToken Token
			{
				get
				{
					return (TSqlParserToken)this._token;
				}
			}

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x0600145A RID: 5210 RVA: 0x0008F8DB File Offset: 0x0008DADB
			public int TokenIndex
			{
				get
				{
					return this._index;
				}
			}

			// Token: 0x0600145B RID: 5211 RVA: 0x0008F8E3 File Offset: 0x0008DAE3
			public int getColumn()
			{
				return this._token.getColumn();
			}

			// Token: 0x0600145C RID: 5212 RVA: 0x0008F8F0 File Offset: 0x0008DAF0
			public void setColumn(int c)
			{
				this._token.setColumn(c);
			}

			// Token: 0x0600145D RID: 5213 RVA: 0x0008F8FE File Offset: 0x0008DAFE
			public int getLine()
			{
				return this._token.getLine();
			}

			// Token: 0x0600145E RID: 5214 RVA: 0x0008F90B File Offset: 0x0008DB0B
			public void setLine(int l)
			{
				this._token.setLine(l);
			}

			// Token: 0x0600145F RID: 5215 RVA: 0x0008F919 File Offset: 0x0008DB19
			public string getFilename()
			{
				return this._token.getFilename();
			}

			// Token: 0x06001460 RID: 5216 RVA: 0x0008F926 File Offset: 0x0008DB26
			public void setFilename(string name)
			{
				this._token.setFilename(name);
			}

			// Token: 0x06001461 RID: 5217 RVA: 0x0008F934 File Offset: 0x0008DB34
			public string getText()
			{
				return this._token.getText();
			}

			// Token: 0x06001462 RID: 5218 RVA: 0x0008F941 File Offset: 0x0008DB41
			public void setText(string t)
			{
				this._token.setText(t);
			}

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x06001463 RID: 5219 RVA: 0x0008F94F File Offset: 0x0008DB4F
			// (set) Token: 0x06001464 RID: 5220 RVA: 0x0008F95C File Offset: 0x0008DB5C
			public int Type
			{
				get
				{
					return this._token.Type;
				}
				set
				{
					this._token.Type = value;
				}
			}

			// Token: 0x04000943 RID: 2371
			private IToken _token;

			// Token: 0x04000944 RID: 2372
			private int _index;
		}
	}
}
