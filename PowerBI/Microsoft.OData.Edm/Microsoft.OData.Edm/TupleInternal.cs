using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000AF RID: 175
	internal static class TupleInternal
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x0000A7BE File Offset: 0x000089BE
		public static TupleInternal<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
		{
			return new TupleInternal<T1, T2>(item1, item2);
		}
	}
}
