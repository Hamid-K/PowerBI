using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200001B RID: 27
	internal class CsdlNavigationPropertyPathExpression : CsdlPathExpression
	{
		// Token: 0x0600008E RID: 142 RVA: 0x0000340D File Offset: 0x0000160D
		public CsdlNavigationPropertyPathExpression(string path, CsdlLocation location)
			: base(path, location)
		{
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003417 File Offset: 0x00001617
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.NavigationPropertyPath;
			}
		}
	}
}
