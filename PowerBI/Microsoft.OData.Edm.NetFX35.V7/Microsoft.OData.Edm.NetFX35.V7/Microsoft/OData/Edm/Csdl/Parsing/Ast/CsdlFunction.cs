using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001C5 RID: 453
	internal class CsdlFunction : CsdlOperation
	{
		// Token: 0x06000C85 RID: 3205 RVA: 0x00023924 File Offset: 0x00021B24
		public CsdlFunction(string name, IEnumerable<CsdlOperationParameter> parameters, CsdlTypeReference returnType, bool isBound, string entitySetPath, bool isComposable, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, parameters, returnType, isBound, entitySetPath, documentation, location)
		{
			this.IsComposable = isComposable;
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x0002393F File Offset: 0x00021B3F
		// (set) Token: 0x06000C87 RID: 3207 RVA: 0x00023947 File Offset: 0x00021B47
		public bool IsComposable { get; private set; }
	}
}
