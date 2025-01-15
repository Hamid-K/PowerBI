using System;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x02000592 RID: 1426
	internal abstract class TileQueryProcessor<T_Query> where T_Query : ITileQuery
	{
		// Token: 0x060044E8 RID: 17640
		internal abstract T_Query Intersect(T_Query arg1, T_Query arg2);

		// Token: 0x060044E9 RID: 17641
		internal abstract T_Query Difference(T_Query arg1, T_Query arg2);

		// Token: 0x060044EA RID: 17642
		internal abstract T_Query Union(T_Query arg1, T_Query arg2);

		// Token: 0x060044EB RID: 17643
		internal abstract bool IsSatisfiable(T_Query query);

		// Token: 0x060044EC RID: 17644
		internal abstract T_Query CreateDerivedViewBySelectingConstantAttributes(T_Query query);
	}
}
