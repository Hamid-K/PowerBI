using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200002D RID: 45
	internal class CsdlIsTypeExpression : CsdlExpressionBase
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x000036F1 File Offset: 0x000018F1
		public CsdlIsTypeExpression(CsdlTypeReference type, CsdlExpressionBase operand, CsdlLocation location)
			: base(location)
		{
			this.type = type;
			this.operand = operand;
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003708 File Offset: 0x00001908
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.IsType;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000370C File Offset: 0x0000190C
		public CsdlTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003714 File Offset: 0x00001914
		public CsdlExpressionBase Operand
		{
			get
			{
				return this.operand;
			}
		}

		// Token: 0x04000044 RID: 68
		private readonly CsdlTypeReference type;

		// Token: 0x04000045 RID: 69
		private readonly CsdlExpressionBase operand;
	}
}
