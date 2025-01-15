using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001C8 RID: 456
	internal class CsdlAnnotationPathExpression : CsdlPathExpression
	{
		// Token: 0x06000D1B RID: 3355 RVA: 0x0002599E File Offset: 0x00023B9E
		public CsdlAnnotationPathExpression(string path, CsdlLocation location)
			: base(path, location)
		{
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x0001B060 File Offset: 0x00019260
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.AnnotationPath;
			}
		}
	}
}
