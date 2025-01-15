using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001FD RID: 509
	internal class CsdlNavigationPropertyBinding : CsdlElement
	{
		// Token: 0x06000DD4 RID: 3540 RVA: 0x000263F0 File Offset: 0x000245F0
		public CsdlNavigationPropertyBinding(string path, string target, CsdlLocation location)
			: base(location)
		{
			this.path = path;
			this.target = target;
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x00026407 File Offset: 0x00024607
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x0002640F File Offset: 0x0002460F
		public string Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x0400079A RID: 1946
		private readonly string path;

		// Token: 0x0400079B RID: 1947
		private readonly string target;
	}
}
