using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001F4 RID: 500
	internal abstract class CsdlFunctionBase : CsdlNamedElement
	{
		// Token: 0x06000DA1 RID: 3489 RVA: 0x000260D5 File Offset: 0x000242D5
		protected CsdlFunctionBase(string name, IEnumerable<CsdlOperationParameter> parameters, CsdlOperationReturn operationReturn, CsdlLocation location)
			: base(name, location)
		{
			this.parameters = new List<CsdlOperationParameter>(parameters);
			this.operationReturn = operationReturn;
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x000260F3 File Offset: 0x000242F3
		public IEnumerable<CsdlOperationParameter> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x000260FB File Offset: 0x000242FB
		public CsdlOperationReturn Return
		{
			get
			{
				return this.operationReturn;
			}
		}

		// Token: 0x0400077D RID: 1917
		private readonly List<CsdlOperationParameter> parameters;

		// Token: 0x0400077E RID: 1918
		private readonly CsdlOperationReturn operationReturn;
	}
}
