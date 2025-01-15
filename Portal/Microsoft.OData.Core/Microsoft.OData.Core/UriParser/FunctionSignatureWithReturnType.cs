using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000141 RID: 321
	public sealed class FunctionSignatureWithReturnType
	{
		// Token: 0x060010BC RID: 4284 RVA: 0x0002EDA7 File Offset: 0x0002CFA7
		public FunctionSignatureWithReturnType(IEdmTypeReference returnType, params IEdmTypeReference[] argumentTypes)
		{
			this.argumentTypes = argumentTypes;
			this.returnType = returnType;
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x0002EDBD File Offset: 0x0002CFBD
		public IEdmTypeReference ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x0002EDC5 File Offset: 0x0002CFC5
		public IEdmTypeReference[] ArgumentTypes
		{
			get
			{
				return this.argumentTypes;
			}
		}

		// Token: 0x040007CB RID: 1995
		private readonly IEdmTypeReference[] argumentTypes;

		// Token: 0x040007CC RID: 1996
		private readonly IEdmTypeReference returnType;
	}
}
