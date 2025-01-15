using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000180 RID: 384
	internal class CsdlEntitySet : CsdlAbstractNavigationSource
	{
		// Token: 0x0600072A RID: 1834 RVA: 0x00011889 File Offset: 0x0000FA89
		public CsdlEntitySet(string name, string elementType, IEnumerable<CsdlNavigationPropertyBinding> navigationPropertyBindings, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, navigationPropertyBindings, documentation, location)
		{
			this.elementType = elementType;
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x0001189E File Offset: 0x0000FA9E
		public string ElementType
		{
			get
			{
				return this.elementType;
			}
		}

		// Token: 0x040003C0 RID: 960
		private readonly string elementType;
	}
}
