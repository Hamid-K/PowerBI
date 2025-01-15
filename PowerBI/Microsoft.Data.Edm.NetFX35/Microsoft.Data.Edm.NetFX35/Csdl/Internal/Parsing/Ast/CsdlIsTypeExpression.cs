using System;
using Microsoft.Data.Edm.Expressions;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast
{
	// Token: 0x02000013 RID: 19
	internal class CsdlIsTypeExpression : CsdlExpressionBase
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00002B6A File Offset: 0x00000D6A
		public CsdlIsTypeExpression(CsdlTypeReference type, CsdlExpressionBase operand, CsdlLocation location)
			: base(location)
		{
			this.type = type;
			this.operand = operand;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002B81 File Offset: 0x00000D81
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.IsType;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002B85 File Offset: 0x00000D85
		public CsdlTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002B8D File Offset: 0x00000D8D
		public CsdlExpressionBase Operand
		{
			get
			{
				return this.operand;
			}
		}

		// Token: 0x0400001D RID: 29
		private readonly CsdlTypeReference type;

		// Token: 0x0400001E RID: 30
		private readonly CsdlExpressionBase operand;
	}
}
