using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000028 RID: 40
	internal class CsdlEnumMemberReferenceExpression : CsdlExpressionBase
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x000035FF File Offset: 0x000017FF
		public CsdlEnumMemberReferenceExpression(string enumMemberPath, CsdlLocation location)
			: base(location)
		{
			this.enumMemberPath = enumMemberPath;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000360F File Offset: 0x0000180F
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.EnumMemberReference;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003613 File Offset: 0x00001813
		public string EnumMemberPath
		{
			get
			{
				return this.enumMemberPath;
			}
		}

		// Token: 0x0400003B RID: 59
		private readonly string enumMemberPath;
	}
}
