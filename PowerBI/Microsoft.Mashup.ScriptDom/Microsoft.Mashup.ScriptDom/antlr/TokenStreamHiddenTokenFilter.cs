using System;
using antlr.collections.impl;

namespace antlr
{
	// Token: 0x02000028 RID: 40
	internal class TokenStreamHiddenTokenFilter : TokenStreamBasicFilter, TokenStream
	{
		// Token: 0x0600014B RID: 331 RVA: 0x00005121 File Offset: 0x00003321
		public TokenStreamHiddenTokenFilter(TokenStream input)
			: base(input)
		{
			this.hideMask = new BitSet();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005135 File Offset: 0x00003335
		protected internal virtual void consume()
		{
			this.nextMonitoredToken = (IHiddenStreamToken)this.input.nextToken();
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005150 File Offset: 0x00003350
		private void consumeFirst()
		{
			this.consume();
			IHiddenStreamToken hiddenStreamToken = null;
			while (this.hideMask.member(this.LA(1).Type) || this.discardMask.member(this.LA(1).Type))
			{
				if (this.hideMask.member(this.LA(1).Type))
				{
					if (hiddenStreamToken == null)
					{
						hiddenStreamToken = this.LA(1);
					}
					else
					{
						hiddenStreamToken.setHiddenAfter(this.LA(1));
						this.LA(1).setHiddenBefore(hiddenStreamToken);
						hiddenStreamToken = this.LA(1);
					}
					this.lastHiddenToken = hiddenStreamToken;
					if (this.firstHidden == null)
					{
						this.firstHidden = hiddenStreamToken;
					}
				}
				this.consume();
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005200 File Offset: 0x00003400
		public virtual BitSet getDiscardMask()
		{
			return this.discardMask;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005208 File Offset: 0x00003408
		public virtual IHiddenStreamToken getHiddenAfter(IHiddenStreamToken t)
		{
			return t.getHiddenAfter();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005210 File Offset: 0x00003410
		public virtual IHiddenStreamToken getHiddenBefore(IHiddenStreamToken t)
		{
			return t.getHiddenBefore();
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005218 File Offset: 0x00003418
		public virtual BitSet getHideMask()
		{
			return this.hideMask;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005220 File Offset: 0x00003420
		public virtual IHiddenStreamToken getInitialHiddenToken()
		{
			return this.firstHidden;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005228 File Offset: 0x00003428
		public virtual void hide(int m)
		{
			this.hideMask.add(m);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005236 File Offset: 0x00003436
		public virtual void hide(BitSet mask)
		{
			this.hideMask = mask;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000523F File Offset: 0x0000343F
		protected internal virtual IHiddenStreamToken LA(int i)
		{
			return this.nextMonitoredToken;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005248 File Offset: 0x00003448
		public override IToken nextToken()
		{
			if (this.LA(1) == null)
			{
				this.consumeFirst();
			}
			IHiddenStreamToken hiddenStreamToken = this.LA(1);
			hiddenStreamToken.setHiddenBefore(this.lastHiddenToken);
			this.lastHiddenToken = null;
			this.consume();
			IHiddenStreamToken hiddenStreamToken2 = hiddenStreamToken;
			while (this.hideMask.member(this.LA(1).Type) || this.discardMask.member(this.LA(1).Type))
			{
				if (this.hideMask.member(this.LA(1).Type))
				{
					hiddenStreamToken2.setHiddenAfter(this.LA(1));
					if (hiddenStreamToken2 != hiddenStreamToken)
					{
						this.LA(1).setHiddenBefore(hiddenStreamToken2);
					}
					hiddenStreamToken2 = (this.lastHiddenToken = this.LA(1));
				}
				this.consume();
			}
			return hiddenStreamToken;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000530A File Offset: 0x0000350A
		public virtual void resetState()
		{
			this.firstHidden = null;
			this.lastHiddenToken = null;
			this.nextMonitoredToken = null;
		}

		// Token: 0x04000092 RID: 146
		protected internal BitSet hideMask;

		// Token: 0x04000093 RID: 147
		private IHiddenStreamToken nextMonitoredToken;

		// Token: 0x04000094 RID: 148
		protected internal IHiddenStreamToken lastHiddenToken;

		// Token: 0x04000095 RID: 149
		protected internal IHiddenStreamToken firstHidden;
	}
}
