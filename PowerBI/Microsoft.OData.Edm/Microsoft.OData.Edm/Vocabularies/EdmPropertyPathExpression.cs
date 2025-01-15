using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000F3 RID: 243
	public class EdmPropertyPathExpression : EdmPathExpression
	{
		// Token: 0x06000751 RID: 1873 RVA: 0x0001206F File Offset: 0x0001026F
		public EdmPropertyPathExpression(string path)
			: base(path)
		{
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00012078 File Offset: 0x00010278
		public EdmPropertyPathExpression(params string[] pathSegments)
			: base(pathSegments)
		{
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00004556 File Offset: 0x00002756
		public EdmPropertyPathExpression(IEnumerable<string> pathSegments)
			: base(pathSegments)
		{
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x000120DC File Offset: 0x000102DC
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.PropertyPath;
			}
		}
	}
}
