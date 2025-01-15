using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001BD RID: 445
	internal abstract class CsdlAbstractNavigationSource : CsdlNamedElement
	{
		// Token: 0x06000C6E RID: 3182 RVA: 0x000237F8 File Offset: 0x000219F8
		public CsdlAbstractNavigationSource(string name, IEnumerable<CsdlNavigationPropertyBinding> navigationPropertyBindings, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, documentation, location)
		{
			this.navigationPropertyBindings = new List<CsdlNavigationPropertyBinding>(navigationPropertyBindings);
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x00023810 File Offset: 0x00021A10
		public IEnumerable<CsdlNavigationPropertyBinding> NavigationPropertyBindings
		{
			get
			{
				return this.navigationPropertyBindings;
			}
		}

		// Token: 0x040006C8 RID: 1736
		private readonly List<CsdlNavigationPropertyBinding> navigationPropertyBindings;
	}
}
