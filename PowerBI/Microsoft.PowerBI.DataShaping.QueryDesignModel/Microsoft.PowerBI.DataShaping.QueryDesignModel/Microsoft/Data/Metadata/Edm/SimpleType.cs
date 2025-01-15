using System;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x020000B1 RID: 177
	public abstract class SimpleType : EdmType
	{
		// Token: 0x06000B81 RID: 2945 RVA: 0x0001D721 File Offset: 0x0001B921
		internal SimpleType()
		{
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0001D729 File Offset: 0x0001B929
		internal SimpleType(string name, string namespaceName, DataSpace dataSpace)
			: base(name, namespaceName, dataSpace)
		{
		}
	}
}
