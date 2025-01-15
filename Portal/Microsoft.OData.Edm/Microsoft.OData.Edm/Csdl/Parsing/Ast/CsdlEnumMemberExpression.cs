using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001CA RID: 458
	internal class CsdlEnumMemberExpression : CsdlExpressionBase
	{
		// Token: 0x06000D1E RID: 3358 RVA: 0x000259B3 File Offset: 0x00023BB3
		public CsdlEnumMemberExpression(string enumMemberPath, CsdlLocation location)
			: base(location)
		{
			this.enumMemberPath = enumMemberPath;
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x00011F33 File Offset: 0x00010133
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.EnumMember;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x000259C3 File Offset: 0x00023BC3
		public string EnumMemberPath
		{
			get
			{
				return this.enumMemberPath;
			}
		}

		// Token: 0x04000740 RID: 1856
		private readonly string enumMemberPath;
	}
}
