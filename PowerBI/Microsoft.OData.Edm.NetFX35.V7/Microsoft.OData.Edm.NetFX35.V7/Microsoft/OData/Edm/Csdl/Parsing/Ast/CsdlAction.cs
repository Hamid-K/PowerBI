using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001BE RID: 446
	internal class CsdlAction : CsdlOperation
	{
		// Token: 0x06000C70 RID: 3184 RVA: 0x00023818 File Offset: 0x00021A18
		public CsdlAction(string name, IEnumerable<CsdlOperationParameter> parameters, CsdlTypeReference returnType, bool isBound, string entitySetPath, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, parameters, returnType, isBound, entitySetPath, documentation, location)
		{
		}
	}
}
