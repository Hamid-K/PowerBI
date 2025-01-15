using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x020002B6 RID: 694
	public sealed class FunctionSignatureWithReturnType
	{
		// Token: 0x060017EC RID: 6124 RVA: 0x00052054 File Offset: 0x00050254
		public FunctionSignatureWithReturnType(IEdmTypeReference returnType, params IEdmTypeReference[] argumentTypes)
		{
			this.argumentTypes = argumentTypes;
			this.returnType = returnType;
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x0005206A File Offset: 0x0005026A
		public IEdmTypeReference ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x00052072 File Offset: 0x00050272
		public IEdmTypeReference[] ArgumentTypes
		{
			get
			{
				return this.argumentTypes;
			}
		}

		// Token: 0x04000A3E RID: 2622
		private readonly IEdmTypeReference[] argumentTypes;

		// Token: 0x04000A3F RID: 2623
		private readonly IEdmTypeReference returnType;
	}
}
