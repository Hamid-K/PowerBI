using System;
using Microsoft.Data.Edm.Expressions;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast
{
	// Token: 0x02000017 RID: 23
	internal class CsdlPathExpression : CsdlExpressionBase
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public CsdlPathExpression(string path, CsdlLocation location)
			: base(location)
		{
			this.path = path;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002C08 File Offset: 0x00000E08
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Path;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002C0C File Offset: 0x00000E0C
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x04000023 RID: 35
		private readonly string path;
	}
}
