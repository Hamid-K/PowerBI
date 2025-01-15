using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B8F RID: 7055
	public sealed class VerbatimExpressionSyntaxNode : RangeSyntaxNode, IVerbatimExpression, IExpression, ISyntaxNode
	{
		// Token: 0x0600B0B5 RID: 45237 RVA: 0x0024359E File Offset: 0x0024179E
		public VerbatimExpressionSyntaxNode(IConstantExpression2 text, TokenRange range)
			: base(range)
		{
			this.text = text;
		}

		// Token: 0x17002C1E RID: 11294
		// (get) Token: 0x0600B0B6 RID: 45238 RVA: 0x002435AE File Offset: 0x002417AE
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.Verbatim;
			}
		}

		// Token: 0x17002C1F RID: 11295
		// (get) Token: 0x0600B0B7 RID: 45239 RVA: 0x002435B2 File Offset: 0x002417B2
		public IConstantExpression2 Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x04005AD1 RID: 23249
		private IConstantExpression2 text;
	}
}
