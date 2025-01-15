using System;
using System.Text;
using NLog.Internal;

namespace NLog.Conditions
{
	// Token: 0x020001B4 RID: 436
	internal sealed class ConditionTokenizer
	{
		// Token: 0x06001352 RID: 4946 RVA: 0x00034A16 File Offset: 0x00032C16
		public ConditionTokenizer(SimpleStringReader stringReader)
		{
			this._stringReader = stringReader;
			this.TokenType = ConditionTokenType.BeginningOfInput;
			this.GetNextToken();
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001353 RID: 4947 RVA: 0x00034A32 File Offset: 0x00032C32
		// (set) Token: 0x06001354 RID: 4948 RVA: 0x00034A3A File Offset: 0x00032C3A
		public ConditionTokenType TokenType { get; private set; }

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001355 RID: 4949 RVA: 0x00034A43 File Offset: 0x00032C43
		// (set) Token: 0x06001356 RID: 4950 RVA: 0x00034A4B File Offset: 0x00032C4B
		public string TokenValue { get; private set; }

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001357 RID: 4951 RVA: 0x00034A54 File Offset: 0x00032C54
		public string StringTokenValue
		{
			get
			{
				string tokenValue = this.TokenValue;
				return tokenValue.Substring(1, tokenValue.Length - 2).Replace("''", "'");
			}
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00034A86 File Offset: 0x00032C86
		public void Expect(ConditionTokenType tokenType)
		{
			if (this.TokenType != tokenType)
			{
				throw new ConditionParseException(string.Format("Expected token of type: {0}, got {1} ({2}).", tokenType, this.TokenType, this.TokenValue));
			}
			this.GetNextToken();
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00034ABE File Offset: 0x00032CBE
		public string EatKeyword()
		{
			if (this.TokenType != ConditionTokenType.Keyword)
			{
				throw new ConditionParseException("Identifier expected");
			}
			string tokenValue = this.TokenValue;
			this.GetNextToken();
			return tokenValue;
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00034AE0 File Offset: 0x00032CE0
		public bool IsKeyword(string keyword)
		{
			return this.TokenType == ConditionTokenType.Keyword && this.TokenValue.Equals(keyword, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x00034AFA File Offset: 0x00032CFA
		public bool IsEOF()
		{
			return this.TokenType == ConditionTokenType.EndOfInput;
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x00034B05 File Offset: 0x00032D05
		public bool IsNumber()
		{
			return this.TokenType == ConditionTokenType.Number;
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x00034B10 File Offset: 0x00032D10
		public bool IsToken(ConditionTokenType tokenType)
		{
			return this.TokenType == tokenType;
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00034B1C File Offset: 0x00032D1C
		public void GetNextToken()
		{
			if (this.TokenType == ConditionTokenType.EndOfInput)
			{
				throw new ConditionParseException("Cannot read past end of stream.");
			}
			this.SkipWhitespace();
			int num = this.PeekChar();
			if (num == -1)
			{
				this.TokenType = ConditionTokenType.EndOfInput;
				return;
			}
			char c = (char)num;
			if (char.IsDigit(c))
			{
				this.ParseNumber(c);
				return;
			}
			if (c == '\'')
			{
				this.ParseSingleQuotedString(c);
				return;
			}
			if (c == '_' || char.IsLetter(c))
			{
				this.ParseKeyword(c);
				return;
			}
			if (c == '}' || c == ':')
			{
				this.TokenType = ConditionTokenType.EndOfInput;
				return;
			}
			this.TokenValue = c.ToString();
			if (this.TryGetComparisonToken(c))
			{
				return;
			}
			if (this.TryGetLogicalToken(c))
			{
				return;
			}
			if (c < ' ' || c >= '\u0080')
			{
				throw new ConditionParseException(string.Format("Invalid token: {0}", c));
			}
			ConditionTokenType conditionTokenType = ConditionTokenizer.CharIndexToTokenType[(int)c];
			if (conditionTokenType != ConditionTokenType.Invalid)
			{
				this.TokenType = conditionTokenType;
				this.TokenValue = new string(c, 1);
				this.ReadChar();
				return;
			}
			throw new ConditionParseException(string.Format("Invalid punctuation: {0}", c));
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00034C20 File Offset: 0x00032E20
		private bool TryGetComparisonToken(char ch)
		{
			if (ch == '<')
			{
				this.ReadChar();
				int num = this.PeekChar();
				if (num == 62)
				{
					this.TokenType = ConditionTokenType.NotEqual;
					this.TokenValue = "<>";
					this.ReadChar();
					return true;
				}
				if (num == 61)
				{
					this.TokenType = ConditionTokenType.LessThanOrEqualTo;
					this.TokenValue = "<=";
					this.ReadChar();
					return true;
				}
				this.TokenType = ConditionTokenType.LessThan;
				this.TokenValue = "<";
				return true;
			}
			else
			{
				if (ch != '>')
				{
					return false;
				}
				this.ReadChar();
				if (this.PeekChar() == 61)
				{
					this.TokenType = ConditionTokenType.GreaterThanOrEqualTo;
					this.TokenValue = ">=";
					this.ReadChar();
					return true;
				}
				this.TokenType = ConditionTokenType.GreaterThan;
				this.TokenValue = ">";
				return true;
			}
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00034CE0 File Offset: 0x00032EE0
		private bool TryGetLogicalToken(char ch)
		{
			if (ch == '!')
			{
				this.ReadChar();
				if (this.PeekChar() == 61)
				{
					this.TokenType = ConditionTokenType.NotEqual;
					this.TokenValue = "!=";
					this.ReadChar();
					return true;
				}
				this.TokenType = ConditionTokenType.Not;
				this.TokenValue = "!";
				return true;
			}
			else if (ch == '&')
			{
				this.ReadChar();
				if (this.PeekChar() == 38)
				{
					this.TokenType = ConditionTokenType.And;
					this.TokenValue = "&&";
					this.ReadChar();
					return true;
				}
				throw new ConditionParseException("Expected '&&' but got '&'");
			}
			else if (ch == '|')
			{
				this.ReadChar();
				if (this.PeekChar() == 124)
				{
					this.TokenType = ConditionTokenType.Or;
					this.TokenValue = "||";
					this.ReadChar();
					return true;
				}
				throw new ConditionParseException("Expected '||' but got '|'");
			}
			else
			{
				if (ch != '=')
				{
					return false;
				}
				this.ReadChar();
				if (this.PeekChar() == 61)
				{
					this.TokenType = ConditionTokenType.EqualTo;
					this.TokenValue = "==";
					this.ReadChar();
					return true;
				}
				this.TokenType = ConditionTokenType.EqualTo;
				this.TokenValue = "=";
				return true;
			}
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00034DF8 File Offset: 0x00032FF8
		private static ConditionTokenType[] BuildCharIndexToTokenType()
		{
			ConditionTokenizer.CharToTokenType[] array = new ConditionTokenizer.CharToTokenType[]
			{
				new ConditionTokenizer.CharToTokenType('(', ConditionTokenType.LeftParen),
				new ConditionTokenizer.CharToTokenType(')', ConditionTokenType.RightParen),
				new ConditionTokenizer.CharToTokenType('.', ConditionTokenType.Dot),
				new ConditionTokenizer.CharToTokenType(',', ConditionTokenType.Comma),
				new ConditionTokenizer.CharToTokenType('!', ConditionTokenType.Not),
				new ConditionTokenizer.CharToTokenType('-', ConditionTokenType.Minus)
			};
			ConditionTokenType[] array2 = new ConditionTokenType[128];
			for (int i = 0; i < 128; i++)
			{
				array2[i] = ConditionTokenType.Invalid;
			}
			foreach (ConditionTokenizer.CharToTokenType charToTokenType in array)
			{
				array2[(int)charToTokenType.Character] = charToTokenType.TokenType;
			}
			return array2;
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00034EBC File Offset: 0x000330BC
		private void ParseSingleQuotedString(char ch)
		{
			this.TokenType = ConditionTokenType.String;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(ch);
			this.ReadChar();
			int num;
			while ((num = this.PeekChar()) != -1)
			{
				ch = (char)num;
				stringBuilder.Append((char)this.ReadChar());
				if (ch == '\'')
				{
					if (this.PeekChar() != 39)
					{
						break;
					}
					stringBuilder.Append('\'');
					this.ReadChar();
				}
			}
			if (num == -1)
			{
				throw new ConditionParseException("String literal is missing a closing quote character.");
			}
			this.TokenValue = stringBuilder.ToString();
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00034F40 File Offset: 0x00033140
		private void ParseKeyword(char ch)
		{
			this.TokenType = ConditionTokenType.Keyword;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(ch);
			this.ReadChar();
			int num;
			while ((num = this.PeekChar()) != -1 && ((ushort)num == 95 || (ushort)num == 45 || char.IsLetterOrDigit((char)num)))
			{
				stringBuilder.Append((char)this.ReadChar());
			}
			this.TokenValue = stringBuilder.ToString();
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00034FA8 File Offset: 0x000331A8
		private void ParseNumber(char ch)
		{
			this.TokenType = ConditionTokenType.Number;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(ch);
			this.ReadChar();
			int num;
			while ((num = this.PeekChar()) != -1)
			{
				ch = (char)num;
				if (!char.IsDigit(ch) && ch != '.')
				{
					break;
				}
				stringBuilder.Append((char)this.ReadChar());
			}
			this.TokenValue = stringBuilder.ToString();
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x0003500C File Offset: 0x0003320C
		private void SkipWhitespace()
		{
			int num;
			while ((num = this.PeekChar()) != -1 && char.IsWhiteSpace((char)num))
			{
				this.ReadChar();
			}
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x00035036 File Offset: 0x00033236
		private int PeekChar()
		{
			return this._stringReader.Peek();
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x00035043 File Offset: 0x00033243
		private int ReadChar()
		{
			return this._stringReader.Read();
		}

		// Token: 0x0400052C RID: 1324
		private static readonly ConditionTokenType[] CharIndexToTokenType = ConditionTokenizer.BuildCharIndexToTokenType();

		// Token: 0x0400052D RID: 1325
		private readonly SimpleStringReader _stringReader;

		// Token: 0x020002B8 RID: 696
		private struct CharToTokenType
		{
			// Token: 0x06001765 RID: 5989 RVA: 0x0003D30C File Offset: 0x0003B50C
			public CharToTokenType(char character, ConditionTokenType tokenType)
			{
				this.Character = character;
				this.TokenType = tokenType;
			}

			// Token: 0x04000794 RID: 1940
			public readonly char Character;

			// Token: 0x04000795 RID: 1941
			public readonly ConditionTokenType TokenType;
		}
	}
}
