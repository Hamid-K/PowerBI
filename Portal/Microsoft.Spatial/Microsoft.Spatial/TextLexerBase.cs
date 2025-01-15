using System;
using System.IO;
using System.Text;

namespace Microsoft.Spatial
{
	// Token: 0x0200006A RID: 106
	internal abstract class TextLexerBase
	{
		// Token: 0x060002D8 RID: 728 RVA: 0x00006C2C File Offset: 0x00004E2C
		protected TextLexerBase(TextReader text)
		{
			this.reader = text;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x00006C3B File Offset: 0x00004E3B
		public LexerToken CurrentToken
		{
			get
			{
				return this.currentToken;
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00006C44 File Offset: 0x00004E44
		public bool Peek(out LexerToken token)
		{
			if (this.peekToken != null)
			{
				token = this.peekToken;
				return true;
			}
			LexerToken lexerToken = this.currentToken;
			if (this.Next())
			{
				this.peekToken = this.currentToken;
				token = this.currentToken;
				this.currentToken = lexerToken;
				return true;
			}
			this.peekToken = null;
			token = null;
			this.currentToken = lexerToken;
			return false;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00006CA4 File Offset: 0x00004EA4
		public bool Next()
		{
			if (this.peekToken != null)
			{
				this.currentToken = this.peekToken;
				this.peekToken = null;
				return true;
			}
			LexerToken lexerToken = this.CurrentToken;
			int? num = null;
			StringBuilder stringBuilder = null;
			bool flag = false;
			int num2;
			while (!flag && (num2 = this.reader.Peek()) >= 0)
			{
				char c = (char)num2;
				int num3;
				flag = this.MatchTokenType(c, num, out num3);
				if (num == null)
				{
					num = new int?(num3);
					stringBuilder = new StringBuilder();
					stringBuilder.Append(c);
					this.reader.Read();
				}
				else if (num == num3)
				{
					stringBuilder.Append(c);
					this.reader.Read();
				}
				else
				{
					flag = true;
				}
			}
			if (num != null)
			{
				this.currentToken = new LexerToken
				{
					Text = stringBuilder.ToString(),
					Type = num.Value
				};
			}
			return lexerToken != this.currentToken;
		}

		// Token: 0x060002DC RID: 732
		protected abstract bool MatchTokenType(char nextChar, int? currentType, out int type);

		// Token: 0x040000B3 RID: 179
		private TextReader reader;

		// Token: 0x040000B4 RID: 180
		private LexerToken currentToken;

		// Token: 0x040000B5 RID: 181
		private LexerToken peekToken;
	}
}
