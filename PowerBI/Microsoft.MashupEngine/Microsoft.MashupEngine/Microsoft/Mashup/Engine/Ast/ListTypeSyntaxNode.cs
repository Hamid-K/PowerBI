using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BC7 RID: 7111
	public sealed class ListTypeSyntaxNode : RangeSyntaxNode, IListTypeExpression, IExpression, ISyntaxNode
	{
		// Token: 0x0600B183 RID: 45443 RVA: 0x00243C47 File Offset: 0x00241E47
		public ListTypeSyntaxNode(IExpression itemType, TokenRange range)
			: base(range)
		{
			this.itemType = itemType;
		}

		// Token: 0x17002C8E RID: 11406
		// (get) Token: 0x0600B184 RID: 45444 RVA: 0x000E78AA File Offset: 0x000E5AAA
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.ListType;
			}
		}

		// Token: 0x17002C8F RID: 11407
		// (get) Token: 0x0600B185 RID: 45445 RVA: 0x00243C57 File Offset: 0x00241E57
		public IExpression ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x04005AFF RID: 23295
		private IExpression itemType;
	}
}
