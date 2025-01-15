using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001F9 RID: 505
	internal abstract class CsdlStructuredType : CsdlElementWithDocumentation
	{
		// Token: 0x06000D44 RID: 3396 RVA: 0x000244B4 File Offset: 0x000226B4
		protected CsdlStructuredType(IEnumerable<CsdlProperty> structuralProperties, IEnumerable<CsdlNavigationProperty> navigationProperties, CsdlDocumentation documentation, CsdlLocation location)
			: base(documentation, location)
		{
			this.structuralProperties = new List<CsdlProperty>(structuralProperties);
			this.navigationProperties = new List<CsdlNavigationProperty>(navigationProperties);
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x000244D7 File Offset: 0x000226D7
		public IEnumerable<CsdlProperty> StructuralProperties
		{
			get
			{
				return this.structuralProperties;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x000244DF File Offset: 0x000226DF
		public IEnumerable<CsdlNavigationProperty> NavigationProperties
		{
			get
			{
				return this.navigationProperties;
			}
		}

		// Token: 0x04000739 RID: 1849
		protected List<CsdlProperty> structuralProperties;

		// Token: 0x0400073A RID: 1850
		protected List<CsdlNavigationProperty> navigationProperties;
	}
}
