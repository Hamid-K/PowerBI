using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000021 RID: 33
	internal class CsdlAction : CsdlOperation
	{
		// Token: 0x0600009F RID: 159 RVA: 0x000034EF File Offset: 0x000016EF
		public CsdlAction(string name, IEnumerable<CsdlOperationParameter> parameters, CsdlTypeReference returnType, bool isBound, string entitySetPath, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, parameters, returnType, isBound, entitySetPath, documentation, location)
		{
		}
	}
}
