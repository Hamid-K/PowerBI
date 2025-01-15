using System;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x0200012D RID: 301
	internal sealed class DaxExternalContentTokenizer
	{
		// Token: 0x06001080 RID: 4224 RVA: 0x0002D12B File Offset: 0x0002B32B
		internal DaxExternalContentTokenizer(string text)
		{
			this._text = text;
			this._nextCharIndex = 0;
			this._lineNumber = 1;
			this._positionInLine = 0;
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x0002D14F File Offset: 0x0002B34F
		internal int LineNumber
		{
			get
			{
				return this._lineNumber;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x0002D157 File Offset: 0x0002B357
		internal int PositionInLine
		{
			get
			{
				return this._positionInLine;
			}
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x0002D160 File Offset: 0x0002B360
		internal DaxExternalContentTokenType NextToken()
		{
			if (this._nextCharIndex >= this._text.Length)
			{
				return DaxExternalContentTokenType.EndOfInput;
			}
			char c;
			while (this.NextChar(out c))
			{
				if (c != '"')
				{
					switch (c)
					{
					case '\'':
						if (this.AdvanceThroughDelimitedItemEnd('\''))
						{
							return DaxExternalContentTokenType.QuotedIdentifier;
						}
						return DaxExternalContentTokenType.QuotedIdentifierStart;
					case '(':
						return DaxExternalContentTokenType.OpenParen;
					case ')':
						return DaxExternalContentTokenType.CloseParen;
					case '*':
					case '+':
					case ',':
					case '.':
						break;
					case '-':
						if (this.LookAhead(1, out c) && c == '-')
						{
							this.NextChar(out c);
							this.AdvanceThroughLineEnd();
							return DaxExternalContentTokenType.SQLComment;
						}
						break;
					case '/':
						if (this.LookAhead(1, out c))
						{
							if (c == '/')
							{
								this.NextChar(out c);
								this.AdvanceThroughLineEnd();
								return DaxExternalContentTokenType.SingleLineComment;
							}
							if (c == '*')
							{
								this.NextChar(out c);
								if (this.AdvanceThroughMultiLineComment())
								{
									return DaxExternalContentTokenType.MultiLineComment;
								}
								return DaxExternalContentTokenType.MultiLineCommentStart;
							}
						}
						break;
					default:
						if (c == '[')
						{
							if (this.AdvanceThroughDelimitedItemEnd(']'))
							{
								return DaxExternalContentTokenType.BracketedIdentifier;
							}
							return DaxExternalContentTokenType.BracketedIdentifierStart;
						}
						break;
					}
				}
				else
				{
					if (this.AdvanceThroughDelimitedItemEnd('"'))
					{
						return DaxExternalContentTokenType.String;
					}
					return DaxExternalContentTokenType.StringStart;
				}
			}
			return DaxExternalContentTokenType.EndOfInput;
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0002D264 File Offset: 0x0002B464
		private bool NextChar(out char c)
		{
			if (this._nextCharIndex < this._text.Length)
			{
				string text = this._text;
				int nextCharIndex = this._nextCharIndex;
				this._nextCharIndex = nextCharIndex + 1;
				c = text[nextCharIndex];
				if (c == '\r')
				{
					char c2;
					if (this.LookAhead(1, out c2) && c2 == '\n')
					{
						this._nextCharIndex++;
					}
					c = '\n';
				}
				if (c == '\n')
				{
					this._lineNumber++;
					this._positionInLine = 0;
				}
				else
				{
					this._positionInLine++;
				}
				return true;
			}
			c = '\0';
			return false;
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0002D2FC File Offset: 0x0002B4FC
		private bool LookAhead(int i, out char c)
		{
			int num = this._nextCharIndex + (i - 1);
			if (num < this._text.Length)
			{
				c = this._text[num];
				return true;
			}
			c = '\0';
			return false;
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x0002D338 File Offset: 0x0002B538
		private bool AdvanceThroughDelimitedItemEnd(char endChar)
		{
			char c;
			while (this.NextChar(out c))
			{
				if (c == endChar)
				{
					if (!this.LookAhead(1, out c) || c != endChar)
					{
						return true;
					}
					this.NextChar(out c);
				}
			}
			return false;
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x0002D374 File Offset: 0x0002B574
		private bool AdvanceThroughLineEnd()
		{
			char c;
			while (this.NextChar(out c))
			{
				if (c == '\n')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x0002D398 File Offset: 0x0002B598
		private bool AdvanceThroughMultiLineComment()
		{
			int num = 1;
			char c;
			while (this.NextChar(out c))
			{
				if (c != '*')
				{
					if (c == '/')
					{
						if (this.NextChar(out c) && c == '*')
						{
							num++;
						}
					}
				}
				else if (this.NextChar(out c) && c == '/')
				{
					num--;
					if (num == 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04000A8D RID: 2701
		private readonly string _text;

		// Token: 0x04000A8E RID: 2702
		private int _nextCharIndex;

		// Token: 0x04000A8F RID: 2703
		private int _lineNumber;

		// Token: 0x04000A90 RID: 2704
		private int _positionInLine;
	}
}
