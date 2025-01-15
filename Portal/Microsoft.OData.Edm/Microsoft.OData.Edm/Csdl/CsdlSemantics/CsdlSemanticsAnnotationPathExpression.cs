using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200015F RID: 351
	internal class CsdlSemanticsAnnotationPathExpression : CsdlSemanticsPathExpression
	{
		// Token: 0x06000989 RID: 2441 RVA: 0x0001B055 File Offset: 0x00019255
		public CsdlSemanticsAnnotationPathExpression(CsdlPathExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(expression, bindingContext, schema)
		{
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x0001B060 File Offset: 0x00019260
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.AnnotationPath;
			}
		}
	}
}
