using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001F7 RID: 503
	internal class CsdlKey : CsdlElement
	{
		// Token: 0x06000DAD RID: 3501 RVA: 0x0002617D File Offset: 0x0002437D
		public CsdlKey(IEnumerable<CsdlPropertyReference> properties, CsdlLocation location)
			: base(location)
		{
			this.properties = new List<CsdlPropertyReference>(properties);
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x00026192 File Offset: 0x00024392
		public IEnumerable<CsdlPropertyReference> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04000784 RID: 1924
		private readonly List<CsdlPropertyReference> properties;
	}
}
