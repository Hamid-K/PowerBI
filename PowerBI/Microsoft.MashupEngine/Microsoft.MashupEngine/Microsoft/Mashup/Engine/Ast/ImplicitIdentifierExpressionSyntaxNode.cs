using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B98 RID: 7064
	public sealed class ImplicitIdentifierExpressionSyntaxNode : RangeSyntaxNode, IImplicitIdentifierExpression, IIdentifierExpression, IExpression, ISyntaxNode
	{
		// Token: 0x0600B0D7 RID: 45271 RVA: 0x002436BB File Offset: 0x002418BB
		public ImplicitIdentifierExpressionSyntaxNode()
			: this(TokenRange.Null)
		{
		}

		// Token: 0x0600B0D8 RID: 45272 RVA: 0x002436C8 File Offset: 0x002418C8
		public ImplicitIdentifierExpressionSyntaxNode(TokenRange range)
			: base(range)
		{
		}

		// Token: 0x17002C33 RID: 11315
		// (get) Token: 0x0600B0D9 RID: 45273 RVA: 0x002436D1 File Offset: 0x002418D1
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.ImplicitIdentifier;
			}
		}

		// Token: 0x17002C34 RID: 11316
		// (get) Token: 0x0600B0DA RID: 45274 RVA: 0x002436D5 File Offset: 0x002418D5
		public Identifier Name
		{
			get
			{
				return Identifier.Underscore;
			}
		}

		// Token: 0x17002C35 RID: 11317
		// (get) Token: 0x0600B0DB RID: 45275 RVA: 0x00002105 File Offset: 0x00000305
		public bool IsInclusive
		{
			get
			{
				return false;
			}
		}
	}
}
