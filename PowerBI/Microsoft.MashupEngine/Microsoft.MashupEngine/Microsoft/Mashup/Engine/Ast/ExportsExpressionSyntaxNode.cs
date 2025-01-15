using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B91 RID: 7057
	public sealed class ExportsExpressionSyntaxNode : RangeSyntaxNode, IExportsExpression, IExpression, ISyntaxNode
	{
		// Token: 0x0600B0BB RID: 45243 RVA: 0x002435E0 File Offset: 0x002417E0
		public ExportsExpressionSyntaxNode(Identifier name, TokenRange range)
			: base(range)
		{
			this.name = name;
		}

		// Token: 0x17002C22 RID: 11298
		// (get) Token: 0x0600B0BC RID: 45244 RVA: 0x0000240C File Offset: 0x0000060C
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.Exports;
			}
		}

		// Token: 0x17002C23 RID: 11299
		// (get) Token: 0x0600B0BD RID: 45245 RVA: 0x002435F0 File Offset: 0x002417F0
		public Identifier Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04005AD4 RID: 23252
		private Identifier name;
	}
}
