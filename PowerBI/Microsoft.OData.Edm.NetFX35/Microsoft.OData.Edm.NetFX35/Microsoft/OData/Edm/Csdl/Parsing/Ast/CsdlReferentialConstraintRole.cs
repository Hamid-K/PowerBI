using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200018C RID: 396
	internal class CsdlReferentialConstraintRole : CsdlElementWithDocumentation
	{
		// Token: 0x06000752 RID: 1874 RVA: 0x00011AD3 File Offset: 0x0000FCD3
		public CsdlReferentialConstraintRole(string role, IEnumerable<CsdlPropertyReference> properties, CsdlDocumentation documentation, CsdlLocation location)
			: base(documentation, location)
		{
			this.role = role;
			this.properties = new List<CsdlPropertyReference>(properties);
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x00011AF1 File Offset: 0x0000FCF1
		public string Role
		{
			get
			{
				return this.role;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x00011AF9 File Offset: 0x0000FCF9
		public IEnumerable<CsdlPropertyReference> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00011B01 File Offset: 0x0000FD01
		public int IndexOf(CsdlPropertyReference reference)
		{
			return this.properties.IndexOf(reference);
		}

		// Token: 0x040003D9 RID: 985
		private readonly string role;

		// Token: 0x040003DA RID: 986
		private readonly List<CsdlPropertyReference> properties;
	}
}
