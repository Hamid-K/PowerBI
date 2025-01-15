using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003F5 RID: 1013
	internal abstract class TypedColumnMap : StructuredColumnMap
	{
		// Token: 0x06002F55 RID: 12117 RVA: 0x00095BAA File Offset: 0x00093DAA
		internal TypedColumnMap(TypeUsage type, string name, ColumnMap[] properties)
			: base(type, name, properties)
		{
		}
	}
}
