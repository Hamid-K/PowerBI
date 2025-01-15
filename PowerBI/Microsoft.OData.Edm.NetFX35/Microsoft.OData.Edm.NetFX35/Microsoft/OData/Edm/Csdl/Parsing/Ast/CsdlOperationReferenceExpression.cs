using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000040 RID: 64
	internal class CsdlOperationReferenceExpression : CsdlExpressionBase
	{
		// Token: 0x060000FC RID: 252 RVA: 0x000039D8 File Offset: 0x00001BD8
		public CsdlOperationReferenceExpression(string operation, CsdlLocation location)
			: base(location)
		{
			this.operation = operation;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000039E8 File Offset: 0x00001BE8
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.OperationReference;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000039EC File Offset: 0x00001BEC
		public string Operation
		{
			get
			{
				return this.operation;
			}
		}

		// Token: 0x0400005F RID: 95
		private readonly string operation;
	}
}
