using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B92 RID: 7058
	public sealed class ExclusiveIdentifierExpressionSyntaxNode : RangeSyntaxNode, IIdentifierExpression, IExpression, ISyntaxNode
	{
		// Token: 0x0600B0BE RID: 45246 RVA: 0x002435F8 File Offset: 0x002417F8
		public ExclusiveIdentifierExpressionSyntaxNode(Identifier name)
			: this(name, TokenRange.Null)
		{
		}

		// Token: 0x0600B0BF RID: 45247 RVA: 0x00243606 File Offset: 0x00241806
		public ExclusiveIdentifierExpressionSyntaxNode(Identifier name, TokenRange range)
			: base(range)
		{
			this.name = name;
		}

		// Token: 0x17002C24 RID: 11300
		// (get) Token: 0x0600B0C0 RID: 45248 RVA: 0x00002461 File Offset: 0x00000661
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.Identifier;
			}
		}

		// Token: 0x17002C25 RID: 11301
		// (get) Token: 0x0600B0C1 RID: 45249 RVA: 0x00243616 File Offset: 0x00241816
		public Identifier Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17002C26 RID: 11302
		// (get) Token: 0x0600B0C2 RID: 45250 RVA: 0x00002105 File Offset: 0x00000305
		public bool IsInclusive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04005AD5 RID: 23253
		private Identifier name;
	}
}
