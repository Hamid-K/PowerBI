using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000179 RID: 377
	internal abstract class CsdlStructuredType : CsdlElementWithDocumentation
	{
		// Token: 0x06000715 RID: 1813 RVA: 0x00011748 File Offset: 0x0000F948
		protected CsdlStructuredType(IEnumerable<CsdlProperty> properties, CsdlDocumentation documentation, CsdlLocation location)
			: base(documentation, location)
		{
			this.properties = new List<CsdlProperty>(properties);
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0001175E File Offset: 0x0000F95E
		public IEnumerable<CsdlProperty> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x040003B2 RID: 946
		protected List<CsdlProperty> properties;
	}
}
