using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200001E RID: 30
	internal abstract class CsdlAbstractNavigationSource : CsdlNamedElement
	{
		// Token: 0x06000095 RID: 149 RVA: 0x0000345E File Offset: 0x0000165E
		public CsdlAbstractNavigationSource(string name, IEnumerable<CsdlNavigationPropertyBinding> navigationPropertyBindings, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, documentation, location)
		{
			this.navigationPropertyBindings = new List<CsdlNavigationPropertyBinding>(navigationPropertyBindings);
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003476 File Offset: 0x00001676
		public IEnumerable<CsdlNavigationPropertyBinding> NavigationPropertyBindings
		{
			get
			{
				return this.navigationPropertyBindings;
			}
		}

		// Token: 0x0400002D RID: 45
		private readonly List<CsdlNavigationPropertyBinding> navigationPropertyBindings;
	}
}
