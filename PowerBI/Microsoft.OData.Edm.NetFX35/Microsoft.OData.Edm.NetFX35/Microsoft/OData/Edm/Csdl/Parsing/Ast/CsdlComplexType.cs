using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200017B RID: 379
	internal class CsdlComplexType : CsdlNamedStructuredType
	{
		// Token: 0x0600071C RID: 1820 RVA: 0x000117B1 File Offset: 0x0000F9B1
		public CsdlComplexType(string name, string baseTypeName, bool isAbstract, bool isOpen, IEnumerable<CsdlProperty> properties, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, baseTypeName, isAbstract, isOpen, properties, documentation, location)
		{
		}
	}
}
