using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020001F6 RID: 502
	internal static class TupleInternal
	{
		// Token: 0x06000BA5 RID: 2981 RVA: 0x00021512 File Offset: 0x0001F712
		public static TupleInternal<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
		{
			return new TupleInternal<T1, T2>(item1, item2);
		}
	}
}
