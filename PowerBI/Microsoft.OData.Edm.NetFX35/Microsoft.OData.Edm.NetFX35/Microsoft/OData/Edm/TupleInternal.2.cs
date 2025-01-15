using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020001F7 RID: 503
	internal class TupleInternal<T1, T2>
	{
		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002151B File Offset: 0x0001F71B
		public TupleInternal(T1 item1, T2 item2)
		{
			this.Item1 = item1;
			this.Item2 = item2;
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x00021531 File Offset: 0x0001F731
		// (set) Token: 0x06000BA8 RID: 2984 RVA: 0x00021539 File Offset: 0x0001F739
		public T1 Item1 { get; private set; }

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x00021542 File Offset: 0x0001F742
		// (set) Token: 0x06000BAA RID: 2986 RVA: 0x0002154A File Offset: 0x0001F74A
		public T2 Item2 { get; private set; }
	}
}
