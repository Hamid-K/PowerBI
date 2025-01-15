using System;
using System.Collections.Generic;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast
{
	// Token: 0x02000136 RID: 310
	internal class CsdlComplexType : CsdlNamedStructuredType
	{
		// Token: 0x060005E9 RID: 1513 RVA: 0x0000F4B4 File Offset: 0x0000D6B4
		public CsdlComplexType(string name, string baseTypeName, bool isAbstract, IEnumerable<CsdlProperty> properties, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, baseTypeName, isAbstract, properties, documentation, location)
		{
		}
	}
}
