using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004F9 RID: 1273
	public abstract class SimpleType : EdmType
	{
		// Token: 0x06003EF2 RID: 16114 RVA: 0x000D1495 File Offset: 0x000CF695
		internal SimpleType()
		{
		}

		// Token: 0x06003EF3 RID: 16115 RVA: 0x000D149D File Offset: 0x000CF69D
		internal SimpleType(string name, string namespaceName, DataSpace dataSpace)
			: base(name, namespaceName, dataSpace)
		{
		}
	}
}
