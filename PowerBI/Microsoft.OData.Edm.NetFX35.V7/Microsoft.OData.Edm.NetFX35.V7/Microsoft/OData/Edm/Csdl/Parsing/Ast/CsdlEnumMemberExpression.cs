using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001BB RID: 443
	internal class CsdlEnumMemberExpression : CsdlExpressionBase
	{
		// Token: 0x06000C69 RID: 3177 RVA: 0x000237D6 File Offset: 0x000219D6
		public CsdlEnumMemberExpression(string enumMemberPath, CsdlLocation location)
			: base(location)
		{
			this.enumMemberPath = enumMemberPath;
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x00013A4F File Offset: 0x00011C4F
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.EnumMember;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x000237E6 File Offset: 0x000219E6
		public string EnumMemberPath
		{
			get
			{
				return this.enumMemberPath;
			}
		}

		// Token: 0x040006C7 RID: 1735
		private readonly string enumMemberPath;
	}
}
