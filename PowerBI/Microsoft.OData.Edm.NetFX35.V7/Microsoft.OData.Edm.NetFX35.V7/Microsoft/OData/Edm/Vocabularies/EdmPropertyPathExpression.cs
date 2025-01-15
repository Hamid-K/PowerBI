using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000FA RID: 250
	public class EdmPropertyPathExpression : EdmPathExpression
	{
		// Token: 0x06000724 RID: 1828 RVA: 0x00013B8B File Offset: 0x00011D8B
		public EdmPropertyPathExpression(string path)
			: base(path)
		{
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00013B94 File Offset: 0x00011D94
		public EdmPropertyPathExpression(params string[] pathSegments)
			: base(pathSegments)
		{
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0000BF09 File Offset: 0x0000A109
		public EdmPropertyPathExpression(IEnumerable<string> pathSegments)
			: base(pathSegments)
		{
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x00013BF8 File Offset: 0x00011DF8
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.PropertyPath;
			}
		}
	}
}
