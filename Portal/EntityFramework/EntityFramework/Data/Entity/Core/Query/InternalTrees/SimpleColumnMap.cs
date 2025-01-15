using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003E5 RID: 997
	internal abstract class SimpleColumnMap : ColumnMap
	{
		// Token: 0x06002F06 RID: 12038 RVA: 0x000953F7 File Offset: 0x000935F7
		internal SimpleColumnMap(TypeUsage type, string name)
			: base(type, name)
		{
		}
	}
}
