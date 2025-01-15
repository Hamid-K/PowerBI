using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000184 RID: 388
	internal class CsdlKey : CsdlElement
	{
		// Token: 0x06000734 RID: 1844 RVA: 0x00011922 File Offset: 0x0000FB22
		public CsdlKey(IEnumerable<CsdlPropertyReference> properties, CsdlLocation location)
			: base(location)
		{
			this.properties = new List<CsdlPropertyReference>(properties);
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x00011937 File Offset: 0x0000FB37
		public IEnumerable<CsdlPropertyReference> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x040003C6 RID: 966
		private readonly List<CsdlPropertyReference> properties;
	}
}
