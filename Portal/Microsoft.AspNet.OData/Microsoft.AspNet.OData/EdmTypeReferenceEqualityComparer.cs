using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData
{
	// Token: 0x0200004A RID: 74
	internal class EdmTypeReferenceEqualityComparer : IEqualityComparer<IEdmTypeReference>
	{
		// Token: 0x060001B9 RID: 441 RVA: 0x00007CFF File Offset: 0x00005EFF
		public bool Equals(IEdmTypeReference x, IEdmTypeReference y)
		{
			return x.IsEquivalentTo(y);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00007D08 File Offset: 0x00005F08
		public int GetHashCode(IEdmTypeReference obj)
		{
			string text = obj.FullName();
			if (text == null)
			{
				return 0;
			}
			return text.GetHashCode();
		}
	}
}
