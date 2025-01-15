using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200019E RID: 414
	public sealed class FunctionSignatureWithReturnType
	{
		// Token: 0x060010D7 RID: 4311 RVA: 0x0002ED2D File Offset: 0x0002CF2D
		public FunctionSignatureWithReturnType(IEdmTypeReference returnType, params IEdmTypeReference[] argumentTypes)
		{
			this.argumentTypes = argumentTypes;
			this.returnType = returnType;
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x060010D8 RID: 4312 RVA: 0x0002ED43 File Offset: 0x0002CF43
		public IEdmTypeReference ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x0002ED4B File Offset: 0x0002CF4B
		public IEdmTypeReference[] ArgumentTypes
		{
			get
			{
				return this.argumentTypes;
			}
		}

		// Token: 0x040008B2 RID: 2226
		private readonly IEdmTypeReference[] argumentTypes;

		// Token: 0x040008B3 RID: 2227
		private readonly IEdmTypeReference returnType;
	}
}
