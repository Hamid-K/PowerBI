using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B94 RID: 7060
	public sealed class SectionIdentifierExpressionSyntaxNode : RangeSyntaxNode, ISectionIdentifierExpression, IExpression, ISyntaxNode
	{
		// Token: 0x0600B0C8 RID: 45256 RVA: 0x00243644 File Offset: 0x00241844
		public SectionIdentifierExpressionSyntaxNode(Identifier section, Identifier name, TokenRange range)
			: base(range)
		{
			this.section = section;
			this.name = name;
		}

		// Token: 0x17002C2A RID: 11306
		// (get) Token: 0x0600B0C9 RID: 45257 RVA: 0x00227072 File Offset: 0x00225272
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.SectionIdentifier;
			}
		}

		// Token: 0x17002C2B RID: 11307
		// (get) Token: 0x0600B0CA RID: 45258 RVA: 0x0024365B File Offset: 0x0024185B
		public Identifier Section
		{
			get
			{
				return this.section;
			}
		}

		// Token: 0x17002C2C RID: 11308
		// (get) Token: 0x0600B0CB RID: 45259 RVA: 0x00243663 File Offset: 0x00241863
		public Identifier Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04005AD7 RID: 23255
		private Identifier section;

		// Token: 0x04005AD8 RID: 23256
		private Identifier name;
	}
}
