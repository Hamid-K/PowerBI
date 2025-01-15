using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000F7 RID: 247
	public class EdmNavigationPropertyPathExpression : EdmPathExpression
	{
		// Token: 0x06000719 RID: 1817 RVA: 0x00013B8B File Offset: 0x00011D8B
		public EdmNavigationPropertyPathExpression(string path)
			: base(path)
		{
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00013B94 File Offset: 0x00011D94
		public EdmNavigationPropertyPathExpression(params string[] pathSegments)
			: base(pathSegments)
		{
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0000BF09 File Offset: 0x0000A109
		public EdmNavigationPropertyPathExpression(IEnumerable<string> pathSegments)
			: base(pathSegments)
		{
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x00013B9D File Offset: 0x00011D9D
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.NavigationPropertyPath;
			}
		}
	}
}
