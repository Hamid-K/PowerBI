using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001C8 RID: 456
	internal class CsdlIsTypeExpression : CsdlExpressionBase
	{
		// Token: 0x06000C90 RID: 3216 RVA: 0x000239B6 File Offset: 0x00021BB6
		public CsdlIsTypeExpression(CsdlTypeReference type, CsdlExpressionBase operand, CsdlLocation location)
			: base(location)
		{
			this.type = type;
			this.operand = operand;
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x00013AFA File Offset: 0x00011CFA
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.IsType;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x000239CD File Offset: 0x00021BCD
		public CsdlTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x000239D5 File Offset: 0x00021BD5
		public CsdlExpressionBase Operand
		{
			get
			{
				return this.operand;
			}
		}

		// Token: 0x040006D8 RID: 1752
		private readonly CsdlTypeReference type;

		// Token: 0x040006D9 RID: 1753
		private readonly CsdlExpressionBase operand;
	}
}
