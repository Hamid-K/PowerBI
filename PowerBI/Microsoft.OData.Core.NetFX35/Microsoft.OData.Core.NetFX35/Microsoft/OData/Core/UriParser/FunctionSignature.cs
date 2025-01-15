using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x020001E5 RID: 485
	internal sealed class FunctionSignature
	{
		// Token: 0x060011C3 RID: 4547 RVA: 0x00040103 File Offset: 0x0003E303
		internal FunctionSignature(params IEdmTypeReference[] argumentTypes)
		{
			this.argumentTypes = argumentTypes;
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x00040112 File Offset: 0x0003E312
		public IEdmTypeReference[] ArgumentTypes
		{
			get
			{
				return this.argumentTypes;
			}
		}

		// Token: 0x040007AA RID: 1962
		private readonly IEdmTypeReference[] argumentTypes;
	}
}
