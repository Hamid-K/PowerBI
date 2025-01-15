using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001D0 RID: 464
	internal class CsdlCastExpression : CsdlExpressionBase
	{
		// Token: 0x06000D2B RID: 3371 RVA: 0x00025A3A File Offset: 0x00023C3A
		public CsdlCastExpression(CsdlTypeReference type, CsdlExpressionBase operand, CsdlLocation location)
			: base(location)
		{
			this.type = type;
			this.operand = operand;
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x00011EB4 File Offset: 0x000100B4
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Cast;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x00025A51 File Offset: 0x00023C51
		public CsdlTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x00025A59 File Offset: 0x00023C59
		public CsdlExpressionBase Operand
		{
			get
			{
				return this.operand;
			}
		}

		// Token: 0x04000744 RID: 1860
		private readonly CsdlTypeReference type;

		// Token: 0x04000745 RID: 1861
		private readonly CsdlExpressionBase operand;
	}
}
