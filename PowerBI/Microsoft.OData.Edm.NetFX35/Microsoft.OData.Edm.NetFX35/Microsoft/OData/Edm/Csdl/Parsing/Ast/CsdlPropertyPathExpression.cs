using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000031 RID: 49
	internal class CsdlPropertyPathExpression : CsdlPathExpression
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x0000377F File Offset: 0x0000197F
		public CsdlPropertyPathExpression(string path, CsdlLocation location)
			: base(path, location)
		{
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00003789 File Offset: 0x00001989
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.PropertyPath;
			}
		}
	}
}
