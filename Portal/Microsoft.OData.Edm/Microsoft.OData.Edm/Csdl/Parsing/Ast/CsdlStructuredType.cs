using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000206 RID: 518
	internal abstract class CsdlStructuredType : CsdlElement
	{
		// Token: 0x06000DF3 RID: 3571 RVA: 0x000265E6 File Offset: 0x000247E6
		protected CsdlStructuredType(IEnumerable<CsdlProperty> structuralProperties, IEnumerable<CsdlNavigationProperty> navigationProperties, CsdlLocation location)
			: base(location)
		{
			this.structuralProperties = new List<CsdlProperty>(structuralProperties);
			this.navigationProperties = new List<CsdlNavigationProperty>(navigationProperties);
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x00026607 File Offset: 0x00024807
		public IEnumerable<CsdlProperty> StructuralProperties
		{
			get
			{
				return this.structuralProperties;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x0002660F File Offset: 0x0002480F
		public IEnumerable<CsdlNavigationProperty> NavigationProperties
		{
			get
			{
				return this.navigationProperties;
			}
		}

		// Token: 0x040007AF RID: 1967
		protected List<CsdlProperty> structuralProperties;

		// Token: 0x040007B0 RID: 1968
		protected List<CsdlNavigationProperty> navigationProperties;
	}
}
