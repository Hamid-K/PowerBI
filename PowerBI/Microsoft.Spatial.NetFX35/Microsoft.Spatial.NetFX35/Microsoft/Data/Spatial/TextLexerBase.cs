using System;
using System.IO;
using System.Text;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000049 RID: 73
	internal abstract class TextLexerBase
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x0000575C File Offset: 0x0000395C
		protected TextLexerBase(TextReader text)
		{
			this.reader = text;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000576B File Offset: 0x0000396B
		public LexerToken CurrentToken
		{
			get
			{
				return this.currentToken;
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00005774 File Offset: 0x00003974
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

		// Token: 0x060001EB RID: 491 RVA: 0x000057D4 File Offset: 0x000039D4
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

		// Token: 0x060001EC RID: 492
		protected abstract bool MatchTokenType(char nextChar, int? currentType, out int type);

		// Token: 0x0400004A RID: 74
		private TextReader reader;

		// Token: 0x0400004B RID: 75
		private LexerToken currentToken;

		// Token: 0x0400004C RID: 76
		private LexerToken peekToken;
	}
}
