using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001E3 RID: 483
	internal class CsdlSingleton : CsdlAbstractNavigationSource
	{
		// Token: 0x06000D6C RID: 3436 RVA: 0x00025D7F File Offset: 0x00023F7F
		public CsdlSingleton(string name, string type, IEnumerable<CsdlNavigationPropertyBinding> navigationPropertyBindings, CsdlLocation location)
			: base(name, navigationPropertyBindings, location)
		{
			this.type = type;
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x00025D92 File Offset: 0x00023F92
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04000764 RID: 1892
		private readonly string type;
	}
}
