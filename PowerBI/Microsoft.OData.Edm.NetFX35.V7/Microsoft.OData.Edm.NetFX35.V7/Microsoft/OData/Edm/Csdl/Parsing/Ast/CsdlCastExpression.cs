using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001C1 RID: 449
	internal class CsdlCastExpression : CsdlExpressionBase
	{
		// Token: 0x06000C76 RID: 3190 RVA: 0x0002386D File Offset: 0x00021A6D
		public CsdlCastExpression(CsdlTypeReference type, CsdlExpressionBase operand, CsdlLocation location)
			: base(location)
		{
			this.type = type;
			this.operand = operand;
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x000139D0 File Offset: 0x00011BD0
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Cast;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x00023884 File Offset: 0x00021A84
		public CsdlTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x0002388C File Offset: 0x00021A8C
		public CsdlExpressionBase Operand
		{
			get
			{
				return this.operand;
			}
		}

		// Token: 0x040006CB RID: 1739
		private readonly CsdlTypeReference type;

		// Token: 0x040006CC RID: 1740
		private readonly CsdlExpressionBase operand;
	}
}
