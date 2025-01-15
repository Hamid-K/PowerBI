using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000F0 RID: 240
	public class EdmNavigationPropertyPathExpression : EdmPathExpression
	{
		// Token: 0x06000746 RID: 1862 RVA: 0x0001206F File Offset: 0x0001026F
		public EdmNavigationPropertyPathExpression(string path)
			: base(path)
		{
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00012078 File Offset: 0x00010278
		public EdmNavigationPropertyPathExpression(params string[] pathSegments)
			: base(pathSegments)
		{
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00004556 File Offset: 0x00002756
		public EdmNavigationPropertyPathExpression(IEnumerable<string> pathSegments)
			: base(pathSegments)
		{
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x00012081 File Offset: 0x00010281
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.NavigationPropertyPath;
			}
		}
	}
}
