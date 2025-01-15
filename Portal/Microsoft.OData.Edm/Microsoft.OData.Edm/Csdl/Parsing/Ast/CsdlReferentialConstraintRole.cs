using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000203 RID: 515
	internal class CsdlReferentialConstraintRole : CsdlElement
	{
		// Token: 0x06000DE3 RID: 3555 RVA: 0x000264B2 File Offset: 0x000246B2
		public CsdlReferentialConstraintRole(string role, IEnumerable<CsdlPropertyReference> properties, CsdlLocation location)
			: base(location)
		{
			this.role = role;
			this.properties = new List<CsdlPropertyReference>(properties);
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x000264CE File Offset: 0x000246CE
		public string Role
		{
			get
			{
				return this.role;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x000264D6 File Offset: 0x000246D6
		public IEnumerable<CsdlPropertyReference> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x000264DE File Offset: 0x000246DE
		public int IndexOf(CsdlPropertyReference reference)
		{
			return this.properties.IndexOf(reference);
		}

		// Token: 0x040007A3 RID: 1955
		private readonly string role;

		// Token: 0x040007A4 RID: 1956
		private readonly List<CsdlPropertyReference> properties;
	}
}
