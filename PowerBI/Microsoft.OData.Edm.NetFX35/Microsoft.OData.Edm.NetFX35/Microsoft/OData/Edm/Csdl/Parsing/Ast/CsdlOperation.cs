using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000020 RID: 32
	internal class CsdlOperation : CsdlFunctionBase
	{
		// Token: 0x0600009A RID: 154 RVA: 0x000034AE File Offset: 0x000016AE
		public CsdlOperation(string name, IEnumerable<CsdlOperationParameter> parameters, CsdlTypeReference returnType, bool isBound, string entitySetPath, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, parameters, returnType, documentation, location)
		{
			this.IsBound = isBound;
			this.EntitySetPath = entitySetPath;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000034CD File Offset: 0x000016CD
		// (set) Token: 0x0600009C RID: 156 RVA: 0x000034D5 File Offset: 0x000016D5
		public bool IsBound { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000034DE File Offset: 0x000016DE
		// (set) Token: 0x0600009E RID: 158 RVA: 0x000034E6 File Offset: 0x000016E6
		public string EntitySetPath { get; private set; }
	}
}
