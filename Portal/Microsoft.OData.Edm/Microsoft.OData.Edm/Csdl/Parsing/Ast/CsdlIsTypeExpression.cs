using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001D7 RID: 471
	internal class CsdlIsTypeExpression : CsdlExpressionBase
	{
		// Token: 0x06000D45 RID: 3397 RVA: 0x00025B7B File Offset: 0x00023D7B
		public CsdlIsTypeExpression(CsdlTypeReference type, CsdlExpressionBase operand, CsdlLocation location)
			: base(location)
		{
			this.type = type;
			this.operand = operand;
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x00011FDE File Offset: 0x000101DE
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.IsType;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x00025B92 File Offset: 0x00023D92
		public CsdlTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x00025B9A File Offset: 0x00023D9A
		public CsdlExpressionBase Operand
		{
			get
			{
				return this.operand;
			}
		}

		// Token: 0x04000751 RID: 1873
		private readonly CsdlTypeReference type;

		// Token: 0x04000752 RID: 1874
		private readonly CsdlExpressionBase operand;
	}
}
