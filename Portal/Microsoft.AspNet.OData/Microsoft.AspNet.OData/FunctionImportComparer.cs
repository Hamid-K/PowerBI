using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000035 RID: 53
	internal class FunctionImportComparer : IEqualityComparer<IEdmFunctionImport>
	{
		// Token: 0x06000143 RID: 323 RVA: 0x00006494 File Offset: 0x00004694
		public bool Equals(IEdmFunctionImport left, IEdmFunctionImport right)
		{
			return left == right || (left != null && right != null && left.Name == right.Name);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000064B5 File Offset: 0x000046B5
		public int GetHashCode(IEdmFunctionImport functionImport)
		{
			if (functionImport == null)
			{
				return 0;
			}
			if (functionImport.Function.Name != null)
			{
				return functionImport.Function.Name.GetHashCode();
			}
			return 0;
		}
	}
}
