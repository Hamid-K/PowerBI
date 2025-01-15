using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001E4 RID: 484
	internal class CsdlEntityType : CsdlNamedStructuredType
	{
		// Token: 0x06000CE8 RID: 3304 RVA: 0x00023EF0 File Offset: 0x000220F0
		public CsdlEntityType(string name, string baseTypeName, bool isAbstract, bool isOpen, bool hasStream, CsdlKey key, IEnumerable<CsdlProperty> structualProperties, IEnumerable<CsdlNavigationProperty> navigationProperties, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, baseTypeName, isAbstract, isOpen, structualProperties, navigationProperties, documentation, location)
		{
			this.key = key;
			this.hasStream = hasStream;
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x00023F20 File Offset: 0x00022120
		public CsdlKey Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x00023F28 File Offset: 0x00022128
		public bool HasStream
		{
			get
			{
				return this.hasStream;
			}
		}

		// Token: 0x04000702 RID: 1794
		private readonly CsdlKey key;

		// Token: 0x04000703 RID: 1795
		private readonly bool hasStream;
	}
}
