using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000187 RID: 391
	internal class CsdlNavigationPropertyBinding : CsdlElementWithDocumentation
	{
		// Token: 0x06000744 RID: 1860 RVA: 0x00011A15 File Offset: 0x0000FC15
		public CsdlNavigationPropertyBinding(string path, string target, CsdlDocumentation documentation, CsdlLocation location)
			: base(documentation, location)
		{
			this.path = path;
			this.target = target;
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x00011A2E File Offset: 0x0000FC2E
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x00011A36 File Offset: 0x0000FC36
		public string Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x040003D0 RID: 976
		private readonly string path;

		// Token: 0x040003D1 RID: 977
		private readonly string target;
	}
}
