using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200003F RID: 63
	internal class CsdlEntitySetReferenceExpression : CsdlExpressionBase
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x000039BC File Offset: 0x00001BBC
		public CsdlEntitySetReferenceExpression(string entitySetPath, CsdlLocation location)
			: base(location)
		{
			this.entitySetPath = entitySetPath;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000039CC File Offset: 0x00001BCC
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.EntitySetReference;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000FB RID: 251 RVA: 0x000039D0 File Offset: 0x00001BD0
		public string EntitySetPath
		{
			get
			{
				return this.entitySetPath;
			}
		}

		// Token: 0x0400005E RID: 94
		private readonly string entitySetPath;
	}
}
