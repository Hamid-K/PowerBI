using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001FD RID: 509
	internal class CsdlAnnotationPathExpression : CsdlPathExpression
	{
		// Token: 0x06000D4B RID: 3403 RVA: 0x000237EE File Offset: 0x000219EE
		public CsdlAnnotationPathExpression(string path, CsdlLocation location)
			: base(path, location)
		{
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x0001FB98 File Offset: 0x0001DD98
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.AnnotationPath;
			}
		}
	}
}
