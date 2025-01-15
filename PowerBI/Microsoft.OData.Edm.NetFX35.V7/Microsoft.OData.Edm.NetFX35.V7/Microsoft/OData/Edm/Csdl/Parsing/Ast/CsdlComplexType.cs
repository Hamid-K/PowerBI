using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001DC RID: 476
	internal class CsdlComplexType : CsdlNamedStructuredType
	{
		// Token: 0x06000CC9 RID: 3273 RVA: 0x00023CC4 File Offset: 0x00021EC4
		public CsdlComplexType(string name, string baseTypeName, bool isAbstract, bool isOpen, IEnumerable<CsdlProperty> structuralProperties, IEnumerable<CsdlNavigationProperty> navigationProperties, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, baseTypeName, isAbstract, isOpen, structuralProperties, navigationProperties, documentation, location)
		{
		}
	}
}
