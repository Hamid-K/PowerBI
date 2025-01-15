using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200002C RID: 44
	internal class CsdlIfExpression : CsdlExpressionBase
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x000036B6 File Offset: 0x000018B6
		public CsdlIfExpression(CsdlExpressionBase test, CsdlExpressionBase ifTrue, CsdlExpressionBase ifFalse, CsdlLocation location)
			: base(location)
		{
			this.test = test;
			this.ifTrue = ifTrue;
			this.ifFalse = ifFalse;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000036D5 File Offset: 0x000018D5
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.If;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000036D9 File Offset: 0x000018D9
		public CsdlExpressionBase Test
		{
			get
			{
				return this.test;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000036E1 File Offset: 0x000018E1
		public CsdlExpressionBase IfTrue
		{
			get
			{
				return this.ifTrue;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000036E9 File Offset: 0x000018E9
		public CsdlExpressionBase IfFalse
		{
			get
			{
				return this.ifFalse;
			}
		}

		// Token: 0x04000041 RID: 65
		private readonly CsdlExpressionBase test;

		// Token: 0x04000042 RID: 66
		private readonly CsdlExpressionBase ifTrue;

		// Token: 0x04000043 RID: 67
		private readonly CsdlExpressionBase ifFalse;
	}
}
