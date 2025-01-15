using System;
using System.IO;
using System.Text;

namespace Microsoft.Spatial
{
	// Token: 0x02000065 RID: 101
	internal abstract class TextLexerBase
	{
		// Token: 0x06000262 RID: 610 RVA: 0x00005F64 File Offset: 0x00004164
		protected TextLexerBase(TextReader text)
		{
			this.reader = text;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00005F73 File Offset: 0x00004173
		public LexerToken CurrentToken
		{
			get
			{
				return this.currentToken;
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00005F7C File Offset: 0x0000417C
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

		// Token: 0x06000265 RID: 613 RVA: 0x00005FDC File Offset: 0x000041DC
		public bool Next()
		{
			if (this.peekToken != null)
			{
				this.currentToken = this.peekToken;
				this.peekToken = null;
				return true;
			}
			LexerToken lexerToken = this.CurrentToken;
			int? num = default(int?);
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

		// Token: 0x06000266 RID: 614
		protected abstract bool MatchTokenType(char nextChar, int? currentType, out int type);

		// Token: 0x040000A6 RID: 166
		private TextReader reader;

		// Token: 0x040000A7 RID: 167
		private LexerToken currentToken;

		// Token: 0x040000A8 RID: 168
		private LexerToken peekToken;
	}
}
