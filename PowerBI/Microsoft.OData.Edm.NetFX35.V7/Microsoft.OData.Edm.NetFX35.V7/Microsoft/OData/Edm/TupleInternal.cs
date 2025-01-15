using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000019 RID: 25
	internal static class TupleInternal
	{
		// Token: 0x06000179 RID: 377 RVA: 0x0000753E File Offset: 0x0000573E
		public static TupleInternal<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
		{
			return new TupleInternal<T1, T2>(item1, item2);
		}
	}
}
