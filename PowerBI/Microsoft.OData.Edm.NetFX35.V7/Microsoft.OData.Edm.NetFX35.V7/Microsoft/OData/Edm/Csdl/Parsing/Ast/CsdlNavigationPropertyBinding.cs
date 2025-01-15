using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001F0 RID: 496
	internal class CsdlNavigationPropertyBinding : CsdlElementWithDocumentation
	{
		// Token: 0x06000D25 RID: 3365 RVA: 0x000242B2 File Offset: 0x000224B2
		public CsdlNavigationPropertyBinding(string path, string target, CsdlDocumentation documentation, CsdlLocation location)
			: base(documentation, location)
		{
			this.path = path;
			this.target = target;
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x000242CB File Offset: 0x000224CB
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x000242D3 File Offset: 0x000224D3
		public string Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x04000724 RID: 1828
		private readonly string path;

		// Token: 0x04000725 RID: 1829
		private readonly string target;
	}
}
