using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BC9 RID: 7113
	public sealed class NullableTypeSyntaxNode : RangeSyntaxNode, INullableTypeExpression, IExpression, ISyntaxNode
	{
		// Token: 0x0600B189 RID: 45449 RVA: 0x00243C77 File Offset: 0x00241E77
		public NullableTypeSyntaxNode(IExpression itemType, TokenRange range)
			: base(range)
		{
			this.itemType = itemType;
		}

		// Token: 0x17002C92 RID: 11410
		// (get) Token: 0x0600B18A RID: 45450 RVA: 0x00243C87 File Offset: 0x00241E87
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.NullableType;
			}
		}

		// Token: 0x17002C93 RID: 11411
		// (get) Token: 0x0600B18B RID: 45451 RVA: 0x00243C8B File Offset: 0x00241E8B
		public IExpression ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x04005B01 RID: 23297
		private IExpression itemType;
	}
}
