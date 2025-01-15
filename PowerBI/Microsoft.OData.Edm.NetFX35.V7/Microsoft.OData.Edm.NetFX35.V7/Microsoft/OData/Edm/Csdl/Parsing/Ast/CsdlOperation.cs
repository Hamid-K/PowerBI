using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001E6 RID: 486
	internal class CsdlOperation : CsdlFunctionBase
	{
		// Token: 0x06000CED RID: 3309 RVA: 0x00023F49 File Offset: 0x00022149
		public CsdlOperation(string name, IEnumerable<CsdlOperationParameter> parameters, CsdlTypeReference returnType, bool isBound, string entitySetPath, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, parameters, returnType, documentation, location)
		{
			this.IsBound = isBound;
			this.EntitySetPath = entitySetPath;
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x00023F68 File Offset: 0x00022168
		// (set) Token: 0x06000CEF RID: 3311 RVA: 0x00023F70 File Offset: 0x00022170
		public bool IsBound { get; private set; }

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x00023F79 File Offset: 0x00022179
		// (set) Token: 0x06000CF1 RID: 3313 RVA: 0x00023F81 File Offset: 0x00022181
		public string EntitySetPath { get; private set; }
	}
}
