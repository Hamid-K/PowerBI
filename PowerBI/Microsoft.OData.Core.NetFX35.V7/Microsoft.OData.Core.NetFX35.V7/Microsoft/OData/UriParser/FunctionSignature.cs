using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000101 RID: 257
	internal sealed class FunctionSignature
	{
		// Token: 0x06000C2F RID: 3119 RVA: 0x0002170F File Offset: 0x0001F90F
		internal FunctionSignature(IEdmTypeReference[] argumentTypes, FunctionSignature.CreateArgumentTypeWithFacets[] createArgumentTypesWithFacets)
		{
			this.argumentTypes = argumentTypes;
			this.createArgumentTypesWithFacets = createArgumentTypesWithFacets;
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x00021725 File Offset: 0x0001F925
		internal IEdmTypeReference[] ArgumentTypes
		{
			get
			{
				return this.argumentTypes;
			}
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00021730 File Offset: 0x0001F930
		internal IEdmTypeReference GetArgumentTypeWithFacets(int index, int? precision, int? scale)
		{
			if (this.createArgumentTypesWithFacets == null)
			{
				return this.argumentTypes[index];
			}
			FunctionSignature.CreateArgumentTypeWithFacets createArgumentTypeWithFacets = this.createArgumentTypesWithFacets[index];
			if (createArgumentTypeWithFacets == null)
			{
				return this.argumentTypes[index];
			}
			return createArgumentTypeWithFacets(precision, scale);
		}

		// Token: 0x040006B1 RID: 1713
		private readonly IEdmTypeReference[] argumentTypes;

		// Token: 0x040006B2 RID: 1714
		private FunctionSignature.CreateArgumentTypeWithFacets[] createArgumentTypesWithFacets;

		// Token: 0x020002B6 RID: 694
		// (Invoke) Token: 0x06001892 RID: 6290
		internal delegate IEdmTypeReference CreateArgumentTypeWithFacets(int? precision, int? scale);
	}
}
