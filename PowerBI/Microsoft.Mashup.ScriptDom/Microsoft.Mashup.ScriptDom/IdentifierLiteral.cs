using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000067 RID: 103
	[Serializable]
	internal class IdentifierLiteral : Literal
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000231 RID: 561 RVA: 0x000067CB File Offset: 0x000049CB
		public override LiteralType LiteralType
		{
			get
			{
				return LiteralType.Identifier;
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000067CF File Offset: 0x000049CF
		internal void SetUnquotedIdentifier(string text)
		{
			base.Value = text;
			this._quoteType = QuoteType.NotQuoted;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000067DF File Offset: 0x000049DF
		internal void SetIdentifier(string text)
		{
			base.Value = Identifier.DecodeIdentifier(text, out this._quoteType);
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000234 RID: 564 RVA: 0x000067F3 File Offset: 0x000049F3
		// (set) Token: 0x06000235 RID: 565 RVA: 0x000067FB File Offset: 0x000049FB
		public QuoteType QuoteType
		{
			get
			{
				return this._quoteType;
			}
			set
			{
				this._quoteType = value;
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00006804 File Offset: 0x00004A04
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00006810 File Offset: 0x00004A10
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04000181 RID: 385
		private QuoteType _quoteType;
	}
}
