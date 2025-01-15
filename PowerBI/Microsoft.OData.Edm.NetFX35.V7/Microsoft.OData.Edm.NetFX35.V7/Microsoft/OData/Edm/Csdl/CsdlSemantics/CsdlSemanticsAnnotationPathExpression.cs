using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001A9 RID: 425
	internal class CsdlSemanticsAnnotationPathExpression : CsdlSemanticsPathExpression
	{
		// Token: 0x06000B75 RID: 2933 RVA: 0x000194AD File Offset: 0x000176AD
		public CsdlSemanticsAnnotationPathExpression(CsdlPathExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(expression, bindingContext, schema)
		{
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x0001FB98 File Offset: 0x0001DD98
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.AnnotationPath;
			}
		}
	}
}
