using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001EA RID: 490
	internal class CsdlKey : CsdlElement
	{
		// Token: 0x06000CFE RID: 3326 RVA: 0x0002403A File Offset: 0x0002223A
		public CsdlKey(IEnumerable<CsdlPropertyReference> properties, CsdlLocation location)
			: base(location)
		{
			this.properties = new List<CsdlPropertyReference>(properties);
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x0002404F File Offset: 0x0002224F
		public IEnumerable<CsdlPropertyReference> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x0400070E RID: 1806
		private readonly List<CsdlPropertyReference> properties;
	}
}
