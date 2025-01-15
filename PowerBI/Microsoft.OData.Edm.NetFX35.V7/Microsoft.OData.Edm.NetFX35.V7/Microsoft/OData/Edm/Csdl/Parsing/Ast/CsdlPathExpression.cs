using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001CB RID: 459
	internal class CsdlPathExpression : CsdlExpressionBase
	{
		// Token: 0x06000C9B RID: 3227 RVA: 0x00023A1C File Offset: 0x00021C1C
		public CsdlPathExpression(string path, CsdlLocation location)
			: base(location)
		{
			this.path = path;
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x0000BFE1 File Offset: 0x0000A1E1
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Path;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x00023A2C File Offset: 0x00021C2C
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x040006DD RID: 1757
		private readonly string path;
	}
}
