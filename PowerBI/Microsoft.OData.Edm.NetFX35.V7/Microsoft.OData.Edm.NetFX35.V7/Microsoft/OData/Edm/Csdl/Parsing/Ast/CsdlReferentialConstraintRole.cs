using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001F6 RID: 502
	internal class CsdlReferentialConstraintRole : CsdlElementWithDocumentation
	{
		// Token: 0x06000D34 RID: 3380 RVA: 0x0002437B File Offset: 0x0002257B
		public CsdlReferentialConstraintRole(string role, IEnumerable<CsdlPropertyReference> properties, CsdlDocumentation documentation, CsdlLocation location)
			: base(documentation, location)
		{
			this.role = role;
			this.properties = new List<CsdlPropertyReference>(properties);
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x00024399 File Offset: 0x00022599
		public string Role
		{
			get
			{
				return this.role;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x000243A1 File Offset: 0x000225A1
		public IEnumerable<CsdlPropertyReference> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x000243A9 File Offset: 0x000225A9
		public int IndexOf(CsdlPropertyReference reference)
		{
			return this.properties.IndexOf(reference);
		}

		// Token: 0x0400072D RID: 1837
		private readonly string role;

		// Token: 0x0400072E RID: 1838
		private readonly List<CsdlPropertyReference> properties;
	}
}
