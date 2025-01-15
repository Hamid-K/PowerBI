using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000F0 RID: 240
	public class EdmCastExpression : EdmElement, IEdmCastExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x060006F9 RID: 1785 RVA: 0x00013992 File Offset: 0x00011B92
		public EdmCastExpression(IEdmExpression operand, IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmExpression>(operand, "operand");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			this.operand = operand;
			this.type = type;
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x000139C0 File Offset: 0x00011BC0
		public IEdmExpression Operand
		{
			get
			{
				return this.operand;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x000139C8 File Offset: 0x00011BC8
		public IEdmTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x000139D0 File Offset: 0x00011BD0
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Cast;
			}
		}

		// Token: 0x04000411 RID: 1041
		private readonly IEdmExpression operand;

		// Token: 0x04000412 RID: 1042
		private readonly IEdmTypeReference type;
	}
}
