using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001F3 RID: 499
	internal class CsdlOperation : CsdlFunctionBase
	{
		// Token: 0x06000D9C RID: 3484 RVA: 0x00026096 File Offset: 0x00024296
		public CsdlOperation(string name, IEnumerable<CsdlOperationParameter> parameters, CsdlOperationReturn operationReturn, bool isBound, string entitySetPath, CsdlLocation location)
			: base(name, parameters, operationReturn, location)
		{
			this.IsBound = isBound;
			this.EntitySetPath = entitySetPath;
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x000260B3 File Offset: 0x000242B3
		// (set) Token: 0x06000D9E RID: 3486 RVA: 0x000260BB File Offset: 0x000242BB
		public bool IsBound { get; private set; }

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x000260C4 File Offset: 0x000242C4
		// (set) Token: 0x06000DA0 RID: 3488 RVA: 0x000260CC File Offset: 0x000242CC
		public string EntitySetPath { get; private set; }
	}
}
