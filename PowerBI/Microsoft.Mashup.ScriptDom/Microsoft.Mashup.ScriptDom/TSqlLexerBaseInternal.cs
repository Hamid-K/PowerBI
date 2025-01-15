using System;
using System.Globalization;
using System.IO;
using antlr;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200006B RID: 107
	internal abstract class TSqlLexerBaseInternal : CharScanner
	{
		// Token: 0x0600023B RID: 571 RVA: 0x0000687F File Offset: 0x00004A7F
		protected TSqlLexerBaseInternal(LexerSharedInputState state)
			: base(state)
		{
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00006888 File Offset: 0x00004A88
		public void InitializeForNewInput(int startOffset, int startLine, int startColumn, TextReader input)
		{
			this.setTabSize(1);
			base.resetState(input);
			this.setColumn(startColumn);
			this.setLine(startLine);
			this._currentLineStartOffset = startOffset - (startColumn - 1);
			this._acceptableGoOffset = startOffset;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600023D RID: 573 RVA: 0x000068B9 File Offset: 0x00004AB9
		public int CurrentOffset
		{
			get
			{
				return this._currentLineStartOffset + this.getColumn() - 1;
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000068CA File Offset: 0x00004ACA
		public override void newline()
		{
			this._currentLineStartOffset += this.getColumn() - 1;
			base.newline();
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000068E7 File Offset: 0x00004AE7
		protected internal override IToken makeToken(int t)
		{
			return new TSqlParserToken((TSqlTokenType)t, this.CurrentOffset - this.text.Length, null, this.inputState.tokenStartLine, this.inputState.tokenStartColumn);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00006918 File Offset: 0x00004B18
		protected void checkEOF(TSqlLexerBaseInternal.TokenKind currentToken)
		{
			if (this.LA(1) == CharScanner.EOF_CHAR)
			{
				this.uponEOF();
				ParseError parseError = null;
				switch (currentToken)
				{
				case TSqlLexerBaseInternal.TokenKind.String:
					parseError = TSql80ParserBaseInternal.CreateParseError("SQL46030", this._complexTokenStartOffset, this.inputState.tokenStartLine, this.inputState.tokenStartColumn, TSqlParserResource.SQL46030Message, new string[] { this.getText().TrimEnd(new char[] { CharScanner.EOF_CHAR }) });
					break;
				case TSqlLexerBaseInternal.TokenKind.SqlCommandIdentifier:
					parseError = TSql80ParserBaseInternal.CreateParseError("SQL46033", this._complexTokenStartOffset, this.inputState.tokenStartLine, this.inputState.tokenStartColumn, TSqlParserResource.SQL46033Message, new string[] { this.getText().TrimEnd(new char[] { CharScanner.EOF_CHAR }) });
					break;
				case TSqlLexerBaseInternal.TokenKind.QuotedIdentifier:
					parseError = TSql80ParserBaseInternal.CreateParseError("SQL46031", this._complexTokenStartOffset, this.inputState.tokenStartLine, this.inputState.tokenStartColumn, TSqlParserResource.SQL46031Message, new string[] { this.getText().TrimEnd(new char[] { CharScanner.EOF_CHAR }) });
					break;
				case TSqlLexerBaseInternal.TokenKind.MultiLineComment:
					parseError = TSql80ParserBaseInternal.CreateParseError("SQL46032", this.CurrentOffset, this.getLine(), this.getColumn(), TSqlParserResource.SQL46032Message, new string[0]);
					break;
				}
				if (parseError != null)
				{
					throw new TSqlParseErrorException(parseError);
				}
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00006A9F File Offset: 0x00004C9F
		protected void beginComplexToken()
		{
			this._complexTokenStartOffset = this.CurrentOffset;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00006AB0 File Offset: 0x00004CB0
		internal static bool IsValueTooLargeForTokenInteger(string source)
		{
			int length = source.Length;
			if (length > 11)
			{
				return true;
			}
			if (length >= 10)
			{
				long num = long.Parse(source, CultureInfo.InvariantCulture.NumberFormat);
				return num > 2147483647L;
			}
			return false;
		}

		// Token: 0x0400018C RID: 396
		private int _complexTokenStartOffset;

		// Token: 0x0400018D RID: 397
		protected int _currentLineStartOffset;

		// Token: 0x0400018E RID: 398
		protected int _acceptableGoOffset;

		// Token: 0x0200006C RID: 108
		protected enum TokenKind
		{
			// Token: 0x04000190 RID: 400
			Common,
			// Token: 0x04000191 RID: 401
			String,
			// Token: 0x04000192 RID: 402
			SqlCommandIdentifier,
			// Token: 0x04000193 RID: 403
			QuotedIdentifier,
			// Token: 0x04000194 RID: 404
			MultiLineComment
		}
	}
}
