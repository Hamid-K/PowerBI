using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001DA RID: 474
	internal class CsdlPathExpression : CsdlExpressionBase
	{
		// Token: 0x06000D50 RID: 3408 RVA: 0x00025BE1 File Offset: 0x00023DE1
		public CsdlPathExpression(string path, CsdlLocation location)
			: base(location)
		{
			this.path = path;
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x0000462D File Offset: 0x0000282D
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Path;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x00025BF1 File Offset: 0x00023DF1
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x04000756 RID: 1878
		private readonly string path;
	}
}
