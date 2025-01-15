using System;

namespace Microsoft.DataIntegration.FuzzyClustering
{
	// Token: 0x02000003 RID: 3
	internal static class Tuple
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002131 File Offset: 0x00000331
		internal static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
		{
			return new Tuple<T1, T2>(item1, item2);
		}
	}
}
