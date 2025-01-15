using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library.Expressions
{
	// Token: 0x02000109 RID: 265
	public class EdmNavigationPropertyPathExpression : EdmPathExpression
	{
		// Token: 0x06000533 RID: 1331 RVA: 0x0000D713 File Offset: 0x0000B913
		public EdmNavigationPropertyPathExpression(string path)
			: base(path)
		{
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0000D71C File Offset: 0x0000B91C
		public EdmNavigationPropertyPathExpression(params string[] path)
			: base(path)
		{
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0000D725 File Offset: 0x0000B925
		public EdmNavigationPropertyPathExpression(IEnumerable<string> path)
			: base(path)
		{
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0000D72E File Offset: 0x0000B92E
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.NavigationPropertyPath;
			}
		}
	}
}
