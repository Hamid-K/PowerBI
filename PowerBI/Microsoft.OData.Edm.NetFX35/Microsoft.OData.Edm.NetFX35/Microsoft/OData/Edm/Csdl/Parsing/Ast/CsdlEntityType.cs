using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000181 RID: 385
	internal class CsdlEntityType : CsdlNamedStructuredType
	{
		// Token: 0x0600072C RID: 1836 RVA: 0x000118A6 File Offset: 0x0000FAA6
		public CsdlEntityType(string name, string baseTypeName, bool isAbstract, bool isOpen, bool hasStream, CsdlKey key, IEnumerable<CsdlProperty> properties, IEnumerable<CsdlNavigationProperty> navigationProperties, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, baseTypeName, isAbstract, isOpen, properties, documentation, location)
		{
			this.key = key;
			this.hasStream = hasStream;
			this.navigationProperties = new List<CsdlNavigationProperty>(navigationProperties);
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x000118D6 File Offset: 0x0000FAD6
		public IEnumerable<CsdlNavigationProperty> NavigationProperties
		{
			get
			{
				return this.navigationProperties;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x000118DE File Offset: 0x0000FADE
		public CsdlKey Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x000118E6 File Offset: 0x0000FAE6
		public bool HasStream
		{
			get
			{
				return this.hasStream;
			}
		}

		// Token: 0x040003C1 RID: 961
		private readonly CsdlKey key;

		// Token: 0x040003C2 RID: 962
		private readonly bool hasStream;

		// Token: 0x040003C3 RID: 963
		private readonly List<CsdlNavigationProperty> navigationProperties;
	}
}
