using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001CC RID: 460
	internal abstract class CsdlAbstractNavigationSource : CsdlNamedElement
	{
		// Token: 0x06000D23 RID: 3363 RVA: 0x000259CB File Offset: 0x00023BCB
		public CsdlAbstractNavigationSource(string name, IEnumerable<CsdlNavigationPropertyBinding> navigationPropertyBindings, CsdlLocation location)
			: base(name, location)
		{
			this.navigationPropertyBindings = new List<CsdlNavigationPropertyBinding>(navigationPropertyBindings);
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x000259E1 File Offset: 0x00023BE1
		public IEnumerable<CsdlNavigationPropertyBinding> NavigationPropertyBindings
		{
			get
			{
				return this.navigationPropertyBindings;
			}
		}

		// Token: 0x04000741 RID: 1857
		private readonly List<CsdlNavigationPropertyBinding> navigationPropertyBindings;
	}
}
