using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001CD RID: 461
	internal class CsdlAction : CsdlOperation
	{
		// Token: 0x06000D25 RID: 3365 RVA: 0x000259E9 File Offset: 0x00023BE9
		public CsdlAction(string name, IEnumerable<CsdlOperationParameter> parameters, CsdlOperationReturn operationReturn, bool isBound, string entitySetPath, CsdlLocation location)
			: base(name, parameters, operationReturn, isBound, entitySetPath, location)
		{
		}
	}
}
