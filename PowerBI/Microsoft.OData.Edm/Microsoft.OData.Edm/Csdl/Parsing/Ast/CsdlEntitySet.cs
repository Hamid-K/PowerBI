using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001F0 RID: 496
	internal class CsdlEntitySet : CsdlAbstractNavigationSource
	{
		// Token: 0x06000D92 RID: 3474 RVA: 0x00026008 File Offset: 0x00024208
		public CsdlEntitySet(string name, string elementType, IEnumerable<CsdlNavigationPropertyBinding> navigationPropertyBindings, CsdlLocation location)
			: this(name, elementType, navigationPropertyBindings, location, true)
		{
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00026016 File Offset: 0x00024216
		public CsdlEntitySet(string name, string elementType, IEnumerable<CsdlNavigationPropertyBinding> navigationPropertyBindings, CsdlLocation location, bool includeInServiceDocument)
			: base(name, navigationPropertyBindings, location)
		{
			this.elementType = elementType;
			this.IncludeInServiceDocument = includeInServiceDocument;
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x00026031 File Offset: 0x00024231
		public string ElementType
		{
			get
			{
				return this.elementType;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x00026039 File Offset: 0x00024239
		// (set) Token: 0x06000D96 RID: 3478 RVA: 0x00026041 File Offset: 0x00024241
		public bool IncludeInServiceDocument { get; private set; }

		// Token: 0x04000776 RID: 1910
		private readonly string elementType;
	}
}
