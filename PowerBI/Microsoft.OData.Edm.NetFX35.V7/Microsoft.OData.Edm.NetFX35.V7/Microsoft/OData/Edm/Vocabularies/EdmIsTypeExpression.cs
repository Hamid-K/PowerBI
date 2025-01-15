using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000F4 RID: 244
	public class EdmIsTypeExpression : EdmElement, IEdmIsTypeExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x0600070C RID: 1804 RVA: 0x00013ABC File Offset: 0x00011CBC
		public EdmIsTypeExpression(IEdmExpression operand, IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmExpression>(operand, "operand");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			this.operand = operand;
			this.type = type;
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x00013AEA File Offset: 0x00011CEA
		public IEdmExpression Operand
		{
			get
			{
				return this.operand;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x00013AF2 File Offset: 0x00011CF2
		public IEdmTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x00013AFA File Offset: 0x00011CFA
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.IsType;
			}
		}

		// Token: 0x04000419 RID: 1049
		private readonly IEdmExpression operand;

		// Token: 0x0400041A RID: 1050
		private readonly IEdmTypeReference type;
	}
}
