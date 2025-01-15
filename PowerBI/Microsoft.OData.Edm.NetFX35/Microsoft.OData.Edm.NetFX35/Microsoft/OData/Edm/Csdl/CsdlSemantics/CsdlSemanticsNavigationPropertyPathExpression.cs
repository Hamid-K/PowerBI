using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200005A RID: 90
	internal class CsdlSemanticsNavigationPropertyPathExpression : CsdlSemanticsPathExpression
	{
		// Token: 0x0600014A RID: 330 RVA: 0x00003CB3 File Offset: 0x00001EB3
		public CsdlSemanticsNavigationPropertyPathExpression(CsdlPathExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(expression, bindingContext, schema)
		{
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00003CBE File Offset: 0x00001EBE
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.NavigationPropertyPath;
			}
		}
	}
}
