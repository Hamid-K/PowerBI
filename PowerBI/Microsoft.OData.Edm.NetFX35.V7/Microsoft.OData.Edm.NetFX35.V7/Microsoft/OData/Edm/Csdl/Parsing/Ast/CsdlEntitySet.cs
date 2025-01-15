using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001E3 RID: 483
	internal class CsdlEntitySet : CsdlAbstractNavigationSource
	{
		// Token: 0x06000CE3 RID: 3299 RVA: 0x00023EA8 File Offset: 0x000220A8
		public CsdlEntitySet(string name, string elementType, IEnumerable<CsdlNavigationPropertyBinding> navigationPropertyBindings, CsdlDocumentation documentation, CsdlLocation location)
			: this(name, elementType, navigationPropertyBindings, documentation, location, true)
		{
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x00023EB8 File Offset: 0x000220B8
		public CsdlEntitySet(string name, string elementType, IEnumerable<CsdlNavigationPropertyBinding> navigationPropertyBindings, CsdlDocumentation documentation, CsdlLocation location, bool includeInServiceDocument)
			: base(name, navigationPropertyBindings, documentation, location)
		{
			this.elementType = elementType;
			this.IncludeInServiceDocument = includeInServiceDocument;
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x00023ED5 File Offset: 0x000220D5
		public string ElementType
		{
			get
			{
				return this.elementType;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x00023EDD File Offset: 0x000220DD
		// (set) Token: 0x06000CE7 RID: 3303 RVA: 0x00023EE5 File Offset: 0x000220E5
		public bool IncludeInServiceDocument { get; private set; }

		// Token: 0x04000700 RID: 1792
		private readonly string elementType;
	}
}
