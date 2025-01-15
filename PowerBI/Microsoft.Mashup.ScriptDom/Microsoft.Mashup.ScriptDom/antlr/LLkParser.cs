using System;

namespace antlr
{
	// Token: 0x02000019 RID: 25
	internal class LLkParser : Parser
	{
		// Token: 0x06000101 RID: 257 RVA: 0x00004261 File Offset: 0x00002461
		public LLkParser(int k_)
		{
			this.k = k_;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004270 File Offset: 0x00002470
		public LLkParser(ParserSharedInputState state, int k_)
		{
			this.k = k_;
			this.inputState = state;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004286 File Offset: 0x00002486
		public LLkParser(TokenBuffer tokenBuf, int k_)
		{
			this.k = k_;
			this.setTokenBuffer(tokenBuf);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000429C File Offset: 0x0000249C
		public LLkParser(TokenStream lexer, int k_)
		{
			this.k = k_;
			TokenBuffer tokenBuffer = new TokenBuffer(lexer);
			this.setTokenBuffer(tokenBuffer);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000042C4 File Offset: 0x000024C4
		public override void consume()
		{
			this.inputState.input.consume();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000042D6 File Offset: 0x000024D6
		public override int LA(int i)
		{
			return this.inputState.input.LA(i);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000042E9 File Offset: 0x000024E9
		public override IToken LT(int i)
		{
			return this.inputState.input.LT(i);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000042FC File Offset: 0x000024FC
		private void trace(string ee, string rname)
		{
			this.traceIndent();
			Console.Out.Write(ee + rname + ((this.inputState.guessing > 0) ? "; [guessing]" : "; "));
			for (int i = 1; i <= this.k; i++)
			{
				if (i != 1)
				{
					Console.Out.Write(", ");
				}
				if (this.LT(i) != null)
				{
					Console.Out.Write(string.Concat(new object[]
					{
						"LA(",
						i,
						")==",
						this.LT(i).getText()
					}));
				}
				else
				{
					Console.Out.Write("LA(" + i + ")==ull");
				}
			}
			Console.Out.WriteLine("");
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000043DC File Offset: 0x000025DC
		public override void traceIn(string rname)
		{
			this.traceDepth++;
			this.trace("> ", rname);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000043F8 File Offset: 0x000025F8
		public override void traceOut(string rname)
		{
			this.trace("< ", rname);
			this.traceDepth--;
		}

		// Token: 0x04000063 RID: 99
		internal int k;
	}
}
