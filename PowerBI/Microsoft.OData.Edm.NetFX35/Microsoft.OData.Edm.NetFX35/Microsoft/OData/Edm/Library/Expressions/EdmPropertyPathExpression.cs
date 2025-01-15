using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library.Expressions
{
	// Token: 0x020001CB RID: 459
	public class EdmPropertyPathExpression : EdmPathExpression
	{
		// Token: 0x060009A0 RID: 2464 RVA: 0x000197EB File Offset: 0x000179EB
		public EdmPropertyPathExpression(string path)
			: base(path)
		{
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x000197F4 File Offset: 0x000179F4
		public EdmPropertyPathExpression(params string[] path)
			: base(path)
		{
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x000197FD File Offset: 0x000179FD
		public EdmPropertyPathExpression(IEnumerable<string> path)
			: base(path)
		{
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x00019806 File Offset: 0x00017A06
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.PropertyPath;
			}
		}
	}
}
