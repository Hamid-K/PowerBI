using System;
using antlr.collections.impl;

namespace antlr
{
	// Token: 0x02000026 RID: 38
	internal class TokenStreamBasicFilter : TokenStream
	{
		// Token: 0x06000144 RID: 324 RVA: 0x00005096 File Offset: 0x00003296
		public TokenStreamBasicFilter(TokenStream input)
		{
			this.input = input;
			this.discardMask = new BitSet();
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000050B0 File Offset: 0x000032B0
		public virtual void discard(int ttype)
		{
			this.discardMask.add(ttype);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000050BE File Offset: 0x000032BE
		public virtual void discard(BitSet mask)
		{
			this.discardMask = mask;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000050C8 File Offset: 0x000032C8
		public virtual IToken nextToken()
		{
			IToken token = this.input.nextToken();
			while (token != null && this.discardMask.member(token.Type))
			{
				token = this.input.nextToken();
			}
			return token;
		}

		// Token: 0x04000090 RID: 144
		protected internal BitSet discardMask;

		// Token: 0x04000091 RID: 145
		protected internal TokenStream input;
	}
}
