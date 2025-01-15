using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000159 RID: 345
	internal class CsdlSemanticsNavigationPropertyPathExpression : CsdlSemanticsPathExpression
	{
		// Token: 0x060008FD RID: 2301 RVA: 0x000194AD File Offset: 0x000176AD
		public CsdlSemanticsNavigationPropertyPathExpression(CsdlPathExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(expression, bindingContext, schema)
		{
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00013B9D File Offset: 0x00011D9D
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.NavigationPropertyPath;
			}
		}
	}
}
