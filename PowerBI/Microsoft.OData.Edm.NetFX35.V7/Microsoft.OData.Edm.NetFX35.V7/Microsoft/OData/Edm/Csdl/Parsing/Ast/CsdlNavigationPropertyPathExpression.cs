using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001BC RID: 444
	internal class CsdlNavigationPropertyPathExpression : CsdlPathExpression
	{
		// Token: 0x06000C6C RID: 3180 RVA: 0x000237EE File Offset: 0x000219EE
		public CsdlNavigationPropertyPathExpression(string path, CsdlLocation location)
			: base(path, location)
		{
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x00013B9D File Offset: 0x00011D9D
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.NavigationPropertyPath;
			}
		}
	}
}
