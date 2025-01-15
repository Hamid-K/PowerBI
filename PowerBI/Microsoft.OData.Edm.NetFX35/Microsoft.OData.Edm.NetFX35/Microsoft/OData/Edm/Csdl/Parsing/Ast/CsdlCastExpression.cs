using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000025 RID: 37
	internal class CsdlCastExpression : CsdlExpressionBase
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x00003580 File Offset: 0x00001780
		public CsdlCastExpression(CsdlTypeReference type, CsdlExpressionBase operand, CsdlLocation location)
			: base(location)
		{
			this.type = type;
			this.operand = operand;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00003597 File Offset: 0x00001797
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Cast;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000359B File Offset: 0x0000179B
		public CsdlTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000035A3 File Offset: 0x000017A3
		public CsdlExpressionBase Operand
		{
			get
			{
				return this.operand;
			}
		}

		// Token: 0x04000036 RID: 54
		private readonly CsdlTypeReference type;

		// Token: 0x04000037 RID: 55
		private readonly CsdlExpressionBase operand;
	}
}
