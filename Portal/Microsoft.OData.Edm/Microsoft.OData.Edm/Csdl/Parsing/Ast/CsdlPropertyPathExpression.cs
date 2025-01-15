using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001DB RID: 475
	internal class CsdlPropertyPathExpression : CsdlPathExpression
	{
		// Token: 0x06000D53 RID: 3411 RVA: 0x0002599E File Offset: 0x00023B9E
		public CsdlPropertyPathExpression(string path, CsdlLocation location)
			: base(path, location)
		{
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x000120DC File Offset: 0x000102DC
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.PropertyPath;
			}
		}
	}
}
