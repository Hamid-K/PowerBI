using System;
using Microsoft.Data.Edm.Expressions;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast
{
	// Token: 0x02000012 RID: 18
	internal class CsdlIfExpression : CsdlExpressionBase
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002B2F File Offset: 0x00000D2F
		public CsdlIfExpression(CsdlExpressionBase test, CsdlExpressionBase ifTrue, CsdlExpressionBase ifFalse, CsdlLocation location)
			: base(location)
		{
			this.test = test;
			this.ifTrue = ifTrue;
			this.ifFalse = ifFalse;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002B4E File Offset: 0x00000D4E
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.If;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002B52 File Offset: 0x00000D52
		public CsdlExpressionBase Test
		{
			get
			{
				return this.test;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002B5A File Offset: 0x00000D5A
		public CsdlExpressionBase IfTrue
		{
			get
			{
				return this.ifTrue;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002B62 File Offset: 0x00000D62
		public CsdlExpressionBase IfFalse
		{
			get
			{
				return this.ifFalse;
			}
		}

		// Token: 0x0400001A RID: 26
		private readonly CsdlExpressionBase test;

		// Token: 0x0400001B RID: 27
		private readonly CsdlExpressionBase ifTrue;

		// Token: 0x0400001C RID: 28
		private readonly CsdlExpressionBase ifFalse;
	}
}
