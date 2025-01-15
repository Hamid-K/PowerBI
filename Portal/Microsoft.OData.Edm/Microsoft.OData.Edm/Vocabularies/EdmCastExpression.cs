using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000E9 RID: 233
	public class EdmCastExpression : EdmElement, IEdmCastExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000726 RID: 1830 RVA: 0x00011E76 File Offset: 0x00010076
		public EdmCastExpression(IEdmExpression operand, IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmExpression>(operand, "operand");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			this.operand = operand;
			this.type = type;
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x00011EA4 File Offset: 0x000100A4
		public IEdmExpression Operand
		{
			get
			{
				return this.operand;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x00011EAC File Offset: 0x000100AC
		public IEdmTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x00011EB4 File Offset: 0x000100B4
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Cast;
			}
		}

		// Token: 0x04000305 RID: 773
		private readonly IEdmExpression operand;

		// Token: 0x04000306 RID: 774
		private readonly IEdmTypeReference type;
	}
}
