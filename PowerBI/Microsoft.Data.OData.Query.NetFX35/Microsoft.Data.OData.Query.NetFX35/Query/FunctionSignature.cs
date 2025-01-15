using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x0200000E RID: 14
	internal class FunctionSignature
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00003353 File Offset: 0x00001553
		internal FunctionSignature(params IEdmTypeReference[] argumentTypes)
		{
			this.argumentTypes = argumentTypes;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00003362 File Offset: 0x00001562
		internal IEdmTypeReference[] ArgumentTypes
		{
			get
			{
				return this.argumentTypes;
			}
		}

		// Token: 0x0400003D RID: 61
		private readonly IEdmTypeReference[] argumentTypes;
	}
}
