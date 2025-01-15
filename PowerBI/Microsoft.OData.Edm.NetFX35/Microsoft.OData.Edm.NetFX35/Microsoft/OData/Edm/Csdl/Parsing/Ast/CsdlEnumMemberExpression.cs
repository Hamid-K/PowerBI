using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000004 RID: 4
	internal class CsdlEnumMemberExpression : CsdlExpressionBase
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000021C9 File Offset: 0x000003C9
		public CsdlEnumMemberExpression(string enumMemberPath, CsdlLocation location)
			: base(location)
		{
			this.enumMemberPath = enumMemberPath;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021D9 File Offset: 0x000003D9
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.EnumMember;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021DD File Offset: 0x000003DD
		public string EnumMemberPath
		{
			get
			{
				return this.enumMemberPath;
			}
		}

		// Token: 0x04000003 RID: 3
		private readonly string enumMemberPath;
	}
}
