using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001CC RID: 460
	internal class CsdlPropertyPathExpression : CsdlPathExpression
	{
		// Token: 0x06000C9E RID: 3230 RVA: 0x000237EE File Offset: 0x000219EE
		public CsdlPropertyPathExpression(string path, CsdlLocation location)
			: base(path, location)
		{
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x00013BF8 File Offset: 0x00011DF8
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.PropertyPath;
			}
		}
	}
}
