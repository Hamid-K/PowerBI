using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B93 RID: 7059
	public sealed class InclusiveIdentifierExpressionSyntaxNode : RangeSyntaxNode, IIdentifierExpression, IExpression, ISyntaxNode
	{
		// Token: 0x0600B0C3 RID: 45251 RVA: 0x0024361E File Offset: 0x0024181E
		public InclusiveIdentifierExpressionSyntaxNode(Identifier name)
			: this(name, TokenRange.Null)
		{
		}

		// Token: 0x0600B0C4 RID: 45252 RVA: 0x0024362C File Offset: 0x0024182C
		public InclusiveIdentifierExpressionSyntaxNode(Identifier name, TokenRange range)
			: base(range)
		{
			this.name = name;
		}

		// Token: 0x17002C27 RID: 11303
		// (get) Token: 0x0600B0C5 RID: 45253 RVA: 0x00002461 File Offset: 0x00000661
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.Identifier;
			}
		}

		// Token: 0x17002C28 RID: 11304
		// (get) Token: 0x0600B0C6 RID: 45254 RVA: 0x0024363C File Offset: 0x0024183C
		public Identifier Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17002C29 RID: 11305
		// (get) Token: 0x0600B0C7 RID: 45255 RVA: 0x00002139 File Offset: 0x00000339
		public bool IsInclusive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04005AD6 RID: 23254
		private Identifier name;
	}
}
