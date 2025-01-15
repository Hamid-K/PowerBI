using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200002A RID: 42
	internal class CsdlFunction : CsdlOperation
	{
		// Token: 0x060000BB RID: 187 RVA: 0x0000365B File Offset: 0x0000185B
		public CsdlFunction(string name, IEnumerable<CsdlOperationParameter> parameters, CsdlTypeReference returnType, bool isBound, string entitySetPath, bool isComposable, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, parameters, returnType, isBound, entitySetPath, documentation, location)
		{
			this.IsComposable = isComposable;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003676 File Offset: 0x00001876
		// (set) Token: 0x060000BD RID: 189 RVA: 0x0000367E File Offset: 0x0000187E
		public bool IsComposable { get; private set; }
	}
}
