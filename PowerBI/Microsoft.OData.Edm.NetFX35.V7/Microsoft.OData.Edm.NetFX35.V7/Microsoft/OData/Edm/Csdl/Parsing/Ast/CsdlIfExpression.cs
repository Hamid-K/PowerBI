using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001C7 RID: 455
	internal class CsdlIfExpression : CsdlExpressionBase
	{
		// Token: 0x06000C8B RID: 3211 RVA: 0x0002397F File Offset: 0x00021B7F
		public CsdlIfExpression(CsdlExpressionBase test, CsdlExpressionBase ifTrue, CsdlExpressionBase ifFalse, CsdlLocation location)
			: base(location)
		{
			this.test = test;
			this.ifTrue = ifTrue;
			this.ifFalse = ifFalse;
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x00013AB8 File Offset: 0x00011CB8
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.If;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x0002399E File Offset: 0x00021B9E
		public CsdlExpressionBase Test
		{
			get
			{
				return this.test;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x000239A6 File Offset: 0x00021BA6
		public CsdlExpressionBase IfTrue
		{
			get
			{
				return this.ifTrue;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x000239AE File Offset: 0x00021BAE
		public CsdlExpressionBase IfFalse
		{
			get
			{
				return this.ifFalse;
			}
		}

		// Token: 0x040006D5 RID: 1749
		private readonly CsdlExpressionBase test;

		// Token: 0x040006D6 RID: 1750
		private readonly CsdlExpressionBase ifTrue;

		// Token: 0x040006D7 RID: 1751
		private readonly CsdlExpressionBase ifFalse;
	}
}
