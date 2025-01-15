using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001D4 RID: 468
	internal class CsdlSingleton : CsdlAbstractNavigationSource
	{
		// Token: 0x06000CB7 RID: 3255 RVA: 0x00023BB7 File Offset: 0x00021DB7
		public CsdlSingleton(string name, string type, IEnumerable<CsdlNavigationPropertyBinding> navigationPropertyBindings, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, navigationPropertyBindings, documentation, location)
		{
			this.type = type;
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x00023BCC File Offset: 0x00021DCC
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x040006EB RID: 1771
		private readonly string type;
	}
}
