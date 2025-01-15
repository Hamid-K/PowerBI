using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200001A RID: 26
	internal class CsdlPathExpression : CsdlExpressionBase
	{
		// Token: 0x0600008B RID: 139 RVA: 0x000033F1 File Offset: 0x000015F1
		public CsdlPathExpression(string path, CsdlLocation location)
			: base(location)
		{
			this.path = path;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003401 File Offset: 0x00001601
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Path;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003405 File Offset: 0x00001605
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x0400002A RID: 42
		private readonly string path;
	}
}
