using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001EB RID: 491
	internal class CsdlComplexType : CsdlNamedStructuredType
	{
		// Token: 0x06000D7E RID: 3454 RVA: 0x00025E84 File Offset: 0x00024084
		public CsdlComplexType(string name, string baseTypeName, bool isAbstract, bool isOpen, IEnumerable<CsdlProperty> structuralProperties, IEnumerable<CsdlNavigationProperty> navigationProperties, CsdlLocation location)
			: base(name, baseTypeName, isAbstract, isOpen, structuralProperties, navigationProperties, location)
		{
		}
	}
}
