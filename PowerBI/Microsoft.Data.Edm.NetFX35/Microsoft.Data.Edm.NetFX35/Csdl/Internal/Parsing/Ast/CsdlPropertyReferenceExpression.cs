using System;
using Microsoft.Data.Edm.Expressions;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast
{
	// Token: 0x02000018 RID: 24
	internal class CsdlPropertyReferenceExpression : CsdlExpressionBase
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00002C14 File Offset: 0x00000E14
		public CsdlPropertyReferenceExpression(string property, CsdlExpressionBase baseExpression, CsdlLocation location)
			: base(location)
		{
			this.property = property;
			this.baseExpression = baseExpression;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002C2B File Offset: 0x00000E2B
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.PropertyReference;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002C2F File Offset: 0x00000E2F
		public string Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002C37 File Offset: 0x00000E37
		public CsdlExpressionBase BaseExpression
		{
			get
			{
				return this.baseExpression;
			}
		}

		// Token: 0x04000024 RID: 36
		private readonly string property;

		// Token: 0x04000025 RID: 37
		private readonly CsdlExpressionBase baseExpression;
	}
}
