using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000140 RID: 320
	internal sealed class FunctionSignature
	{
		// Token: 0x060010B9 RID: 4281 RVA: 0x0002ED4B File Offset: 0x0002CF4B
		internal FunctionSignature(IEdmTypeReference[] argumentTypes, FunctionSignature.CreateArgumentTypeWithFacets[] createArgumentTypesWithFacets)
		{
			this.argumentTypes = argumentTypes;
			this.createArgumentTypesWithFacets = createArgumentTypesWithFacets;
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060010BA RID: 4282 RVA: 0x0002ED61 File Offset: 0x0002CF61
		internal IEdmTypeReference[] ArgumentTypes
		{
			get
			{
				return this.argumentTypes;
			}
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0002ED6C File Offset: 0x0002CF6C
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

		// Token: 0x040007C9 RID: 1993
		private readonly IEdmTypeReference[] argumentTypes;

		// Token: 0x040007CA RID: 1994
		private FunctionSignature.CreateArgumentTypeWithFacets[] createArgumentTypesWithFacets;

		// Token: 0x0200038F RID: 911
		// (Invoke) Token: 0x06001F7C RID: 8060
		internal delegate IEdmTypeReference CreateArgumentTypeWithFacets(int? precision, int? scale);
	}
}
