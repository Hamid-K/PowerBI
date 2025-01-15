using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B9C RID: 7068
	public abstract class ElementAccessExpressionSyntaxNode : RangeSyntaxNode, IElementAccessExpression, IExpression, ISyntaxNode
	{
		// Token: 0x0600B0E7 RID: 45287 RVA: 0x0024372C File Offset: 0x0024192C
		protected ElementAccessExpressionSyntaxNode(IExpression collection, IExpression key, TokenRange range)
			: base(range)
		{
			this.collection = collection;
			this.key = key;
		}

		// Token: 0x17002C3C RID: 11324
		// (get) Token: 0x0600B0E8 RID: 45288 RVA: 0x000023C4 File Offset: 0x000005C4
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.ElementAccess;
			}
		}

		// Token: 0x17002C3D RID: 11325
		// (get) Token: 0x0600B0E9 RID: 45289 RVA: 0x00243743 File Offset: 0x00241943
		public IExpression Collection
		{
			get
			{
				return this.collection;
			}
		}

		// Token: 0x17002C3E RID: 11326
		// (get) Token: 0x0600B0EA RID: 45290 RVA: 0x0024374B File Offset: 0x0024194B
		public IExpression Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x17002C3F RID: 11327
		// (get) Token: 0x0600B0EB RID: 45291
		public abstract bool IsOptional { get; }

		// Token: 0x04005ADD RID: 23261
		private IExpression collection;

		// Token: 0x04005ADE RID: 23262
		private IExpression key;
	}
}
