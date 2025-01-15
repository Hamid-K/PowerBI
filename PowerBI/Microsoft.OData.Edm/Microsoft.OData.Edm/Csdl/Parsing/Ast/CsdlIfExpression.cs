using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001D6 RID: 470
	internal class CsdlIfExpression : CsdlExpressionBase
	{
		// Token: 0x06000D40 RID: 3392 RVA: 0x00025B44 File Offset: 0x00023D44
		public CsdlIfExpression(CsdlExpressionBase test, CsdlExpressionBase ifTrue, CsdlExpressionBase ifFalse, CsdlLocation location)
			: base(location)
		{
			this.test = test;
			this.ifTrue = ifTrue;
			this.ifFalse = ifFalse;
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x00011F9C File Offset: 0x0001019C
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.If;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x00025B63 File Offset: 0x00023D63
		public CsdlExpressionBase Test
		{
			get
			{
				return this.test;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000D43 RID: 3395 RVA: 0x00025B6B File Offset: 0x00023D6B
		public CsdlExpressionBase IfTrue
		{
			get
			{
				return this.ifTrue;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x00025B73 File Offset: 0x00023D73
		public CsdlExpressionBase IfFalse
		{
			get
			{
				return this.ifFalse;
			}
		}

		// Token: 0x0400074E RID: 1870
		private readonly CsdlExpressionBase test;

		// Token: 0x0400074F RID: 1871
		private readonly CsdlExpressionBase ifTrue;

		// Token: 0x04000750 RID: 1872
		private readonly CsdlExpressionBase ifFalse;
	}
}
