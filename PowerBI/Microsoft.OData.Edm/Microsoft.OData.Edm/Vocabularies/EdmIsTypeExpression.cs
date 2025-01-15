using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000ED RID: 237
	public class EdmIsTypeExpression : EdmElement, IEdmIsTypeExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000739 RID: 1849 RVA: 0x00011FA0 File Offset: 0x000101A0
		public EdmIsTypeExpression(IEdmExpression operand, IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmExpression>(operand, "operand");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			this.operand = operand;
			this.type = type;
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x00011FCE File Offset: 0x000101CE
		public IEdmExpression Operand
		{
			get
			{
				return this.operand;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x00011FD6 File Offset: 0x000101D6
		public IEdmTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x00011FDE File Offset: 0x000101DE
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.IsType;
			}
		}

		// Token: 0x0400030D RID: 781
		private readonly IEdmExpression operand;

		// Token: 0x0400030E RID: 782
		private readonly IEdmTypeReference type;
	}
}
