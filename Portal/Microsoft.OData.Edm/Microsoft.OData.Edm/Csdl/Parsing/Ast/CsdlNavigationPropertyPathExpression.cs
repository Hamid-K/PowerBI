using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001CB RID: 459
	internal class CsdlNavigationPropertyPathExpression : CsdlPathExpression
	{
		// Token: 0x06000D21 RID: 3361 RVA: 0x0002599E File Offset: 0x00023B9E
		public CsdlNavigationPropertyPathExpression(string path, CsdlLocation location)
			: base(path, location)
		{
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x00012081 File Offset: 0x00010281
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.NavigationPropertyPath;
			}
		}
	}
}
