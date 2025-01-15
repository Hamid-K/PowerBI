using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001D4 RID: 468
	internal class CsdlFunction : CsdlOperation
	{
		// Token: 0x06000D3A RID: 3386 RVA: 0x00025AED File Offset: 0x00023CED
		public CsdlFunction(string name, IEnumerable<CsdlOperationParameter> parameters, CsdlOperationReturn operationReturn, bool isBound, string entitySetPath, bool isComposable, CsdlLocation location)
			: base(name, parameters, operationReturn, isBound, entitySetPath, location)
		{
			this.IsComposable = isComposable;
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x00025B06 File Offset: 0x00023D06
		// (set) Token: 0x06000D3C RID: 3388 RVA: 0x00025B0E File Offset: 0x00023D0E
		public bool IsComposable { get; private set; }
	}
}
