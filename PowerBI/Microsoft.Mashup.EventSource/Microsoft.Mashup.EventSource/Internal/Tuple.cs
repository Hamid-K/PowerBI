using System;

namespace Microsoft.Internal
{
	// Token: 0x02000008 RID: 8
	internal static class Tuple
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000021EF File Offset: 0x000003EF
		public static Tuple<T1> Create<T1>(T1 item1)
		{
			return new Tuple<T1>(item1);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021F7 File Offset: 0x000003F7
		public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
		{
			return new Tuple<T1, T2>(item1, item2);
		}
	}
}
