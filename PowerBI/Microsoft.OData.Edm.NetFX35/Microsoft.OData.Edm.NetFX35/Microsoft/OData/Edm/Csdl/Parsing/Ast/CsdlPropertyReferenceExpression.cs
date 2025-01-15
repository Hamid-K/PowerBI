using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000032 RID: 50
	internal class CsdlPropertyReferenceExpression : CsdlExpressionBase
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x0000378D File Offset: 0x0000198D
		public CsdlPropertyReferenceExpression(string property, CsdlExpressionBase baseExpression, CsdlLocation location)
			: base(location)
		{
			this.property = property;
			this.baseExpression = baseExpression;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x000037A4 File Offset: 0x000019A4
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.PropertyReference;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000037A8 File Offset: 0x000019A8
		public string Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x000037B0 File Offset: 0x000019B0
		public CsdlExpressionBase BaseExpression
		{
			get
			{
				return this.baseExpression;
			}
		}

		// Token: 0x0400004A RID: 74
		private readonly string property;

		// Token: 0x0400004B RID: 75
		private readonly CsdlExpressionBase baseExpression;
	}
}
